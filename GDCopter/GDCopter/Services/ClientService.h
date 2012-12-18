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
		Serial.begin(9600);
		_stabilizator = stabilizator;		
	}
	
	void SendOrientation()
	{
		Vector3i orientation = _stabilizator->GetOrientation();
		Serial.print(orientation.x);
		Serial.print("#");
		Serial.print(orientation.y);
		Serial.print("#");
		Serial.println(orientation.z);
	}
	
	void SendSensorsValues()
	{
		Vector3<int> gyroVec = _stabilizator->GetGyroValues();
		Vector3<int> accelVec = _stabilizator->GetAccelValues();
		Vector3<int> compassVec = _stabilizator->GetCompassValues();
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
	private:
	Stabilizator* _stabilizator;
};