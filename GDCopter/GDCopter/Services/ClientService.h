/*
* ClientService.cpp
*
* Created: 09.12.2012 12:51:40
*  Author: Kirill
*/
class ClientService
{
	public:
	
	void Initialize(Stabilizator* stabilizator, SensorsService * sensorsService)
	{
		Serial.begin(115200);
		_stabilizator = stabilizator;
		_sensorsService = sensorsService;
		SendData=&ClientService::StopSend;
	}
	
	void Update()
	{
		UpdateClientCommand();
		(this->*SendData)();
	}
	

	


	private:
	
	void (ClientService::*SendData)();
	
	Stabilizator* _stabilizator;
	
	SensorsService* _sensorsService;
	

	void UpdateClientCommand()
	{
		if(Serial.available())
		{
			String s = Serial.readString();
			if(s[0]=='$')
			{
				switch (ParseCommand(s))
				{
					case 0:
						SendData= &ClientService::StopSend;
						Serial.println("Transmitting stopped");
						break;
					case 1:
						SendData= &ClientService::SendSensorsValues;
						Serial.println("Begin transmitting sensors values");
						break;
					case 2:
						SendData= &ClientService::SendOrientation;
						Serial.println("Begin transmitting orientation values");
						break;
					case 3:
						SendData= &ClientService::StopSend;
						break;
					case 4:
						SendData = &ClientService::SendDelay;
						break;
					default:
						SendData= &ClientService::StopSend;
						Serial.print(s);
						Serial.println(" - Unknown command");
						break;
				}
				delay(500);
			}
		}
	}
	
	int ParseCommand(String s){
		if(s=="$stop"){
			return 0;
		}
		if(s=="$sensors"){
			return 1;
		}
		if(s=="$orientation"){
			return 2;
		}
		if(s=="$compass"){
			return 3;
		}
		if(s=="$delay"){
			return 4;
		}
		return -1;
	}
	
	void SendOrientation()
	{
		Serial.print(_stabilizator->GetYaw());
		Serial.print("#");
		Serial.print(_stabilizator->GetRoll());
		Serial.print("#");
		Serial.println(_stabilizator->GetPitch());
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
		Serial.println(_stabilizator->dt);
	}
	void StopSend(){}

};