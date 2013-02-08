/*
* ClientService.cpp
*
* Created: 09.12.2012 12:51:40
*  Author: Kirill
*/
class ClientService
{
	public:
	void Initialize(Stabilizator* stabilizator )
	{
		Serial.begin(115200);
		_stabilizator = stabilizator;		
	}
	
	void SendOrientation()
	{
		//Vector3<float> orientation ;//= _stabilizator->GetOrientation();
		Serial.print(_stabilizator->GetYaw());
		Serial.print("#");
		Serial.print(_stabilizator->GetRoll());
		Serial.print("#");
		Serial.println(_stabilizator->GetPitch());
	}
	
	void SendSensorsValues()
	{
		Vector3<float> gyroVec = _stabilizator->GetGyroValues();
		Vector3<float> accelVec = _stabilizator->GetAccelValues();
		Vector3<float> compassVec = _stabilizator->GetCompassValues();
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
	}
	private:
	Stabilizator* _stabilizator;
};