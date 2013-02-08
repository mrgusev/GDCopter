/*
* SensorsService.cpp
*
* Created: 09.12.2012 12:51:54
*  Author: Kirill
*/
#include "I2Cdev.h"
#include "MPU6050.h"
#include "HMC5883L.h"
#include <Wire.h>
#include "vector3.h"

class SernsorsService
{
	public:

	void Innitialize()
	{
		Wire.begin();
		accelgyro.initialize();
		accelgyro.setI2CBypassEnabled(true);
		
		accelgyro.setFullScaleGyroRange(3);
		mag.initialize();	
		xGyroOffset = -37.61f;
		yGyroOffset = 6.68f;
		zGyroOffset = -14.30f;	
		
	}
	
	void UpdateValues()
	{
		accelgyro.getMotion6(&ax, &ay, &az, &gx, &gy, &gz);		
		mag.getHeading(&mx, &my, &mz);
	}
	
	Vector3<float> GetGyroValues()
	{
		return Vector3<float>((float)gx-xGyroOffset,(float)gy-yGyroOffset,(float)gz-zGyroOffset);
	} 
	Vector3<int> GetAccelValues()
	{
		return Vector3<int>(ax,ay,az);
	}
	Vector3<int> GetCompassValues()
	{
		return Vector3<int>(mx,my,mz);
	}
	
	void SendGyroOffsets()
	{
		Serial.println(xGyroOffset);
		Serial.println(yGyroOffset);
		Serial.println(zGyroOffset);
		delay(2000);
	}
	private:
	
	HMC5883L mag;
	MPU6050 accelgyro;
	
	int16_t mx, my, mz;
	int16_t ax, ay, az;
	int16_t gx, gy, gz;
	float xGyroOffset;
	float yGyroOffset;
	float zGyroOffset;
	void CalibrateGyro()
	{
		
		digitalWrite(13, HIGH);
		float sumX = 0;
		float sumY = 0;
		float sumZ = 0;
		for (int i=0; i<10000; i++)
		{
			UpdateValues();
			sumX += gx;
			sumY += gy;
			sumZ += gz;
		}
		xGyroOffset=sumX/10000;
		yGyroOffset=sumY/10000;
		zGyroOffset=sumZ/10000;
		digitalWrite(13, LOW);	
	}
	
};