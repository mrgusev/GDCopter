/*
* ClientService.cpp
*
* Created: 09.12.2012 12:51:40
*  Author: Kirill
*/
class ClientService
{
	public:
	
	String lastMessage;
	
	void Initialize(Stabilizator* stabilizator, SensorsService* sensorsService, Controller* controller)
	{
		Serial.begin(57600);
		Serial.setTimeout(10);
		_stabilizator = stabilizator;
		_sensorsService = sensorsService;
		_controller = controller;
		SendData=&ClientService::StopSend;
		_timerCount = 0;
		lastMessage = "";
	}
	
	void Update()
	{
		_timerCount += dt;
		UpdateClientCommand();
		if(_timerCount >= 50)
		{
			(this->*SendData)();
			_timerCount = 0;
		}
	}
	

	


	private:
	
	int _timerCount;
	
	void (ClientService::*SendData)();
	
	Stabilizator* _stabilizator;
	
	SensorsService* _sensorsService;
	
	Controller* _controller;

	void UpdateClientCommand()
	{
		if(Serial.available())
		{
			lastMessage = Serial.readString();
			CommandType commandType = CommandParser::GetCommandType(lastMessage);
			switch(commandType)
			{
				case ClientServiceCommand:
				SetState(CommandParser::ParseClientServiceCommand(lastMessage));
				break;
				case ContrllerCommand:
				_controller->SetState(CommandParser::ParceControllerCommand(lastMessage));
				break;
				default:
				_controller->Message = lastMessage;
				break;
			}
			
		}
	}
	
	void SetState(ClientServiceState state)
	{
		switch (state)
		{
			case Stop:
			SendData= &ClientService::StopSend;
			Serial.println("Transmitting stopped");
			break;
			case Sensors:
			SendData= &ClientService::SendSensorsValues;
			Serial.println("Begin transmitting sensors values");
			break;
			case Orientation:
			SendData= &ClientService::SendOrientation;
			Serial.println("Begin transmitting orientation values");
			break;
			case Compass:
			SendData= &ClientService::StopSend;
			break;
			case Delay:
			SendData = &ClientService::SendDelay;
			break;
			case Rotors:
			SendData = &ClientService::SendRotorSpeeds;
			Serial.println("Begin transmitting rotor speeds");
			break;
			default:
			SendData= &ClientService::StopSend;
			Serial.println("Unknown command");
			break;
		}
		
		delay(500);
	}
	
	void SendOrientation()
	{
		Serial.print(_stabilizator->GetYaw());
		Serial.print("#");
		Serial.print(_stabilizator->GetRoll());
		Serial.print("#");
		Serial.println(_stabilizator->GetPitch());
	}
	
	void SendRotorSpeeds()
	{
		int s1,s2,s3,s4;
		_controller->GetRotorSpeeds(&s1,&s2,&s3,&s4);
		Serial.print(s1);
		Serial.print("#");
		Serial.print(s2);
		Serial.print("#");
		Serial.print(s3);
		Serial.print("#");
		Serial.println(s4);
	}
	
	void SendSensorsValues()
	{
		Vector3<float> gyroVec = _sensorsService->GetGyroValues();
		Vector3<float> accelVec = _sensorsService->GetAccelValues();
		Vector3<float> compassVec = _sensorsService->GetCompassValues();
		Serial.print(gyroVec.x);
		Serial.print("#");
		Serial.print(gyroVec.y);
		Serial.print("#");
		Serial.print(gyroVec.z);
		Serial.print(";");
		Serial.print(accelVec.x);
		Serial.print("#");
		Serial.print(accelVec.y);
		Serial.print("#");
		Serial.print(accelVec.z);
		Serial.print(";");
		Serial.print(compassVec.x);
		Serial.print("#");
		Serial.print(compassVec.y);
		Serial.print("#");
		Serial.println(compassVec.z);
	}

	void SendDelay(){
		Serial.println(dt);
	}
void StopSend(){}

};