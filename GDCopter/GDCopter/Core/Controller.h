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
	
	void Initialize(RotorService* rotorService, Stabilizator* stabilizator)
	{
		_rotorService  = rotorService;
		_stabilizator = stabilizator;
		controllerState = Stabilization;
		previousPitch = 0.0f;
		previousYaw = 0.0f;
		previousRoll = 0.0f;
		previousAltitude = 0.0f;
		
		QuadInertiaX = 0.020126;
		QuadInertiaY = 0.019842;
		QuadInertiaZ = 0.038769;
		
		RotorLiftConstant = 0.000004;
		RotorDragConstant = 0.000002;
		
		QuadRadius = 0.277;
		QuadMass = 1.27;
		
		ThrustDerivativeCoef = 2.5;
		PitchDerivativeCoef = 1.75;
		RollDerivativeCoef = 1.75;
		YawDerivativeCoef = 1.75;
		ThrustProportionalCoef = 1.5;
		PitchProportionalCoef = 6;
		RollProportionalCoef = 6;
		YawProportionalCoef = 6;
	}
	float GetRotorSpeed1()
	{
		return firstMotorSpeedSq;
	}
	float GetRotorSpeed2()
	{
		return secondMotorSpeedSq;
	}
	float GetRotorSpeed3()
	{
		return thirdRotorSpeedSq;
	}
	float GetRotorSpeed4()
	{
		return fourthRotorSpeedSq;
	}
	
	void SetRotorSpeeds(float speed1, float speed2, float speed3, float speed4)
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
	
	void SetDt(float dt)
	{
		_dt = dt;
	}
	
	void Update()
	{
		CalculateMotorsSpeeds();
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
	
	float _dt;
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
		float totalThrust = (9.8f + ThrustDerivativeCoef * (previousAltitude - currentAltitude)/_dt - ThrustProportionalCoef * currentAltitude) * QuadMass / rotationMatrix.c.z;
		float rollTorque = (ThrustDerivativeCoef * (previousRoll - currentRoll)/_dt- ThrustProportionalCoef * currentRoll) * QuadInertiaX;
		float pitchTorque = (ThrustDerivativeCoef * (previousPitch - currentPitch)/_dt  - ThrustProportionalCoef * currentPitch) * QuadInertiaY;
		float yawTorque = (ThrustDerivativeCoef * (previousYaw - currentYaw)/_dt - ThrustProportionalCoef * currentYaw) * QuadInertiaZ;
		
		float thrustAdditive = totalThrust / 4 / RotorLiftConstant;
		float pitchAdditive = pitchTorque / (2 * RotorLiftConstant * QuadRadius);
		float rollAdditive = rollTorque / (2 * RotorLiftConstant * QuadRadius);
		float yawAdditive = yawTorque / 4 / RotorDragConstant;
		
		//it is necessary to add the limitations for the values of the
		//squared rotors' speeds (e. g. not less than zero) but it is
		//supposed to be done in the future, while linearizing the
		////dependency between the PWM value and a rotor's squared speed just speed)
		//firstMotorSpeedSq = thrustAdditive - pitchAdditive - yawAdditive;
		//secondMotorSpeedSq = thrustAdditive - rollAdditive + yawAdditive;
		//thirdRotorSpeedSq = thrustAdditive + pitchAdditive - yawAdditive;
		//fourthRotorSpeedSq = thrustAdditive + rollAdditive + yawAdditive;
		
		firstMotorSpeedSq = _dt;
		secondMotorSpeedSq = _dt;
		thirdRotorSpeedSq = _dt;
		fourthRotorSpeedSq = _dt;
		
		previousAltitude = currentAltitude;
		previousPitch = currentPitch;
		previousRoll = currentRoll;
		previousYaw = currentYaw;
	}
};