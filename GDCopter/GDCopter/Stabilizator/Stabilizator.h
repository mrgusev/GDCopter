/*
* Stabilizator.h
*
* Created: 13.12.2012 2:42:19
*  Author: Kirill
*/

#include "SensorsService.h"
#include "matrix3.h"
const int INTEGR_DELAY = 20;
const int SERIAL_DELAY = 100;

// Pololu LPR550AL
const float vref = 3.3f;
const float vzero = 1.23f;
const float sens = 0.0005f;
const float adc = 1023.f;
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
		//earthAngles = Vector3l(0,0,0);
		planeAngles = Vector3i(1,1,1);
		
		x=0;
		y=0;
		z=0;
	}
	void UpdateSensorsData()
	{
		//sensorsService.SendGyroOffsets();
		sensorsService.UpdateValues();
	}
	
	void CalculateAngles()
	{
		unsigned long currentMillis = micros();
		dt = currentMillis - previousMillis;
		//
		//fx = sensorsService.GetGyroValues().x * dt;
		//fy = sensorsService.GetGyroValues().y * dt;
		//fz = sensorsService.GetGyroValues().z * dt;
		//Matrix3i tempMatrix;
		//tempMatrix.a = Vector3i(1,-fz,fy);
		//tempMatrix.b = Vector3i(fz,1,-fx);
		//tempMatrix.c = Vector3i(-fy,fx,1);
		//rotationMatrix = rotationMatrix.operator *(tempMatrix);
		//x +=  (((sensorsService.GetGyroValues().x)/adc))*dt;
		//y +=  (((sensorsService.GetGyroValues().y)/adc))*dt;
		//z +=  (((sensorsService.GetGyroValues().z)/adc))*dt;
		
		x += (float)sensorsService.GetGyroValues().x*((float)dt/1000000)/14.375; // Without any filter
		
		y += (float)sensorsService.GetGyroValues().y*((float)dt/1000000)/14.375; // Without any filter
		
		z += (float)sensorsService.GetGyroValues().z*((float)dt/1000000)/14.375; // Without any filter
		
		//double accXangle = getXangle();
		//
		//double accYangle = getYangle();
		//
		//compAngleX = (0.93*(compAngleX+(gyroXrate*(double)(micros()-timer)/1000000)))+(0.07*accXangle);
		//compAngleY = (0.93*(compAngleY+(gyroYrate*(double)(micros()-timer)/1000000)))+(0.07*accYangle);
		//
		//
		earthAngles.x = x;
		earthAngles.y = y;
		earthAngles.z = z;
		previousMillis=currentMillis;
	}
	//void p(char *fmt, ... ){
		//char tmp[32]; // resulting string limited to 128 chars
		//va_list args;
		//va_start (args, fmt );
		//vsnprintf(tmp, 32, fmt, args);
		//va_end (args);
		//Serial.print(tmp);
	//}
	
	Vector3<float> GetGyroValues()
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
	
	Vector3l GetOrientation()
	{
		return earthAngles;
	}
	
	float GetPitch()
	{
		return x;
	}
	float GetYaw()
	{
		return y;
	}
	float GetRoll()
	{
		return z;
	}
	
	private:
	long previousMillis;
	SernsorsService sensorsService;
	Matrix3i rotationMatrix;
	Vector3i planeAngles;
	Vector3l earthAngles;
	
	
	int dt;
	int fx;
	int fy;
	int fz;
	
	float x;
	float y;
	float z;
	
	
	
	
};