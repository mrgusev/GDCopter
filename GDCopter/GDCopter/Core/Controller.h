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
		_speed1 = 0;
		_speed2 = 0;
		_speed3 = 0;
		_speed4 = 0;
		UpdateRotors= &Controller::EmptyAction;
	}
	
	void GetRotorSpeeds(int* speed1,int* speed2,int* speed3,int* speed4)
	{
		*speed1 = _speed1;
		*speed2 = _speed2;
		*speed3 = _speed3;
		*speed4 = _speed4;
	}
	
	void SetState(ControllerState state)
	{
		switch (state)
		{
			case StopRotors:
			StopAllRotors();
			
			UpdateRotors=  &Controller::EmptyAction;
			break;
			case Stabilization:
			UpdateRotors= &Controller::Stabilize;
			break;
			case DirectValues:
			UpdateRotors= &Controller::SetCustomValues;
			break;
			default:
			UpdateRotors= &Controller::StopAllRotors;
			break;
		}
	}
	
	void Update()
	{
		(this->*UpdateRotors)();
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
	
	Stabilizator* _stabilizator;
	RotorService* _rotorService;
	
	int _speed1, _speed2, _speed3, _speed4;
	
	void (Controller::*UpdateRotors)();
	
	void StopAllRotors()
	{
		_speed1= _speed2= _speed3= _speed4=100;
		_rotorService->SetRotorsSpeed(_speed1, _speed2, _speed3, _speed4);
	}
	
	void EmptyAction()
{}
	
	void Stabilize()
	{
		
	}
	
	void CalculateMotorsSpeeds()
	{
		float currentAltitude = _stabilizator->GetAltitude();
		float currentPitch = _stabilizator->GetPitch();
		float currentRoll = _stabilizator->GetRoll();
		float currentYaw = _stabilizator->GetYaw();
		Matrix3f rotationMatrix = _stabilizator->GetRotationMatrix();
		float totalThrust = (9.8f + ThrustDerivativeCoef * (previousAltitude - currentAltitude) / dt - ThrustProportionalCoef * currentAltitude) * QuadMass / rotationMatrix.c.z;
		float rollTorque = (ThrustDerivativeCoef * (previousRoll - currentRoll) / dt - ThrustProportionalCoef * currentRoll) * QuadInertiaX;
		float pitchTorque = (ThrustDerivativeCoef * (previousPitch - currentPitch) / dt - ThrustProportionalCoef * currentPitch) * QuadInertiaY;
		float yawTorque = (ThrustDerivativeCoef * (previousYaw - currentYaw) / dt - ThrustProportionalCoef * currentYaw) * QuadInertiaZ;
		
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
	
	void SetCustomValues()
	{
		CommandParser::ParseRotorSpeed(Message,&_speed1,&_speed2,&_speed3,&_speed4);
		_rotorService->SetRotorsSpeed(_speed1, _speed2, _speed3, _speed4);
	}
};