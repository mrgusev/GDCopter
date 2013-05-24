/*
* Controller.h
*
* Created: 22.02.2013 19:40:45
*  Author: Kirill
*/

class Controller
{
	public:
	
	
	float ThrustDerivativeCoef;
	float PitchDerivativeCoef;
	float RollDerivativeCoef;
	float YawDerivativeCoef;
	float ThrustProportionalCoef;
	float PitchProportionalCoef;
	float RollProportionalCoef;
	float YawProportionalCoef;
	
	float QuadInertiaX;
	float QuadInertiaY;
	float QuadInertiaZ;
	
	float RotorLiftConstant;
	float RotorDragConstant;
	float QuadRadius;
	
	float QuadMass;
	
	void Initialize(RotorService* rotorService)
	{
		_rotorService  = rotorService;
		controllerState = Stabilization;
	}
	
	void GetRotorSpeeds(float* speed1, float* speed2, float* speed3, float* speed4)
	{
		*speed1 = firstMotorSpeedSq;
		*speed2 = secondMotorSpeedSq;
		*speed3 = thirdRotorSpeedSq;
		*speed4 = fourthRotorSpeedSq;
	}
	
	void SetRotorSpeeds(int speed1, int speed2, int speed3, int speed4)
	{
		_speed1 = speed1;
		_speed2 = speed2;
		_speed3 = speed3;
		_speed4 = speed4;
	}
	void SetState(ControllerState state)
	{
		controllerState = state;	
	}
	
	void SetDt(int dt)
	{
		_dt = dt;	
	}
	
	void Update()
	{
		switch (controllerState)
		{
			case StopRotors:
			_speed1 = _speed2 = _speed3 = _speed4 = 0;
			break;
			case Stabilization:
			CalculateMotorsSpeeds();
			break;
			case DirectValues:
			break;
			default:
			_speed1 = _speed2 = _speed3 = _speed4 = 0;
			break;
		}
		_rotorService->SetRotorsSpeed(_speed1, _speed2, _speed3, _speed4);
	}
	
	String Message;
	
	private:
	
	float previousPitch;
	float previousYaw;
	float previousRoll;
	float previousAltitude;
	
	float firstMotorSpeedSq;
	float secondMotorSpeedSq;
	float thirdRotorSpeedSq;
	float fourthRotorSpeedSq;
	
	int _dt;
	int controllerState;
	
	int _speed1, _speed2, _speed3, _speed4;
	
	Stabilizator* _stabilizator;
	RotorService* _rotorService;
	
	void CalculateMotorsSpeeds()
	{
		float currentAltitude = _stabilizator->GetAltitude();
		float currentPitch = _stabilizator->GetPitch();
		float currentRoll = _stabilizator->GetRoll();
		float currentYaw = _stabilizator->GetYaw();
		Matrix3f rotationMatrix = _stabilizator->GetRotationMatrix();
		float totalThrust = (9.8f + ThrustDerivativeCoef * (previousAltitude - currentAltitude) / _dt - ThrustProportionalCoef * currentAltitude) * QuadMass / rotationMatrix.c.z;
		float rollTorque = (ThrustDerivativeCoef * (previousRoll - currentRoll) / _dt - ThrustProportionalCoef * currentRoll) * QuadInertiaX;
		float pitchTorque = (ThrustDerivativeCoef * (previousPitch - currentPitch) / _dt - ThrustProportionalCoef * currentPitch) * QuadInertiaY;
		float yawTorque = (ThrustDerivativeCoef * (previousYaw - currentYaw) / _dt - ThrustProportionalCoef * currentYaw) * QuadInertiaZ;
		
		float thrustAdditive = totalThrust / 4 / RotorLiftConstant;
		float pitchAdditive = pitchTorque / (2 * RotorLiftConstant * QuadRadius);
		float rollAdditive = rollTorque / (2 * RotorLiftConstant * QuadRadius);
		float yawAdditive = yawTorque / 4 / RotorDragConstant;
		
		//it is necessary to add the limitations for the values of the
		//squared rotors' speeds (e. g. not less than zero) but it is
		//supposed to be done in the future, while linearizing the
		//dependency between the PWM value and a rotor's squared speed (just speed)
		firstMotorSpeedSq = thrustAdditive - pitchAdditive - yawAdditive;
		secondMotorSpeedSq = thrustAdditive - rollAdditive + yawAdditive;
		thirdRotorSpeedSq = thrustAdditive + pitchAdditive - yawAdditive;
		fourthRotorSpeedSq = thrustAdditive + rollAdditive + yawAdditive;
	}

};