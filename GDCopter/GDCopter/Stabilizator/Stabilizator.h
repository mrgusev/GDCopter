/*
* Stabilizator.h
*
* Created: 13.12.2012 2:42:19
*  Author: Kirill
*/

#include "SensorsService.h"
#include "matrix3.h"
class Stabilizator
{
	public:
	void Initialize()
	{
		sensorsService.Innitialize();
		previousMillis = 0;
		rotationMatrix.a = Vector3i(1,0,0);
		rotationMatrix.b = Vector3i(0,1,0);
		rotationMatrix.c = Vector3i(0,0,1);
		earthAngles = Vector3i(1,1,1);
		planeAngles = Vector3i(1,1,1);
		
		x=0;
		y=0;
		z=0;
	}
	
	void UpdateSensorsData()
	{
		sensorsService.UpdateValues();
	}
	
	void CalculateAngles()
	{
		unsigned long currentMillis = millis();
		dt = currentMillis - previousMillis;
		fx = sensorsService.GetGyroValues().x * dt;
		fy = sensorsService.GetGyroValues().y * dt;
		fz = sensorsService.GetGyroValues().z * dt;
		Matrix3i tempMatrix;
		tempMatrix.a = Vector3i(1,-fz,fy);
		tempMatrix.b = Vector3i(fz,1,-fx);
		tempMatrix.c = Vector3i(-fy,fx,1);
		rotationMatrix = rotationMatrix.operator *(tempMatrix);		
		x += fx;
		y += fy;
		z += fz;;
		
		//
		//Serial.println(currentMillis);
		//Serial.println(previousMillis);
		//Serial.println("@");
		//Serial.println(fx);
		//Serial.println(fy);
		//Serial.println(fz);
		//Serial.println(";");
		previousMillis=currentMillis;
	}
	
	Vector3<int> GetGyroValues()
	{
		return sensorsService.GetGyroValues();
	}
	Vector3<int> GetAccelValues()
	{
		return sensorsService.GetAccelValues();
	}
	Vector3<int> GetCompassValues()
	{
		return sensorsService.GetCompassValues();
	}
	
	Vector3i GetOrientation()
	{
		return earthAngles;		
	}
	
	int GetPitch()
	{
		return fx;
	}
	int GetYaw()
	{
		return fy;
	}
	int GetRoll()
	{
		return fz;
	}
	private:
	long previousMillis;
	SernsorsService sensorsService;
	Matrix3i rotationMatrix;
	Vector3i planeAngles;
	Vector3i earthAngles;
	
	int dt;
	int fx;
	int fy;
	int fz;
	
	int x;
	int y;
	int z;
	
	
	
	
};