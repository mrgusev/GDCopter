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
		mag.initialize();	
		CalibrateGyro();
	}
	
	void UpdateValues()
	{
		accelgyro.getMotion6(&ax, &ay, &az, &gx, &gy, &gz);		
		mag.getHeading(&mx, &my, &mz);
	}
	
	Vector3<int> GetGyroValues()
	{
		return Vector3<int>(gx-xGyroOffset,gy-yGyroOffset,gz-zGyroOffset);
	} 
	Vector3<int> GetAccelValues()
	{
		return Vector3<int>(ax,ay,az);
	}
	Vector3<int> GetCompassValues()
	{
		return Vector3<int>(mx,my,mz);
	}
	
	private:
	
	HMC5883L mag;
	MPU6050 accelgyro;
	
	int16_t mx, my, mz;
	int16_t ax, ay, az;
	int16_t gx, gy, gz;
	int xGyroOffset;
	int yGyroOffset;
	int zGyroOffset;
	void CalibrateGyro()
	{
		accelgyro.setFullScaleGyroRange(4);
		long sumX = 0;
		long sumY = 0;
		long sumZ = 0;
		for (int i=0; i<1000; i++)
		{
			UpdateValues();
			sumX += gx;
			sumY += gy;
			sumZ += gz;
		}
		xGyroOffset=sumX/1000;
		yGyroOffset=sumY/1000;
		zGyroOffset=sumZ/1000;
			
	}
	
};