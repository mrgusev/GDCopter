/*
* Controller.h
*
* Created: 22.02.2013 19:40:45
*  Author: Kirill
*/

class Controller
{
	public:
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
	
	void SetCustomValues()
	{
		CommandParser::ParseRotorSpeed(Message,&_speed1,&_speed2,&_speed3,&_speed4);
		_rotorService->SetRotorsSpeed(_speed1, _speed2, _speed3, _speed4);
	}
};