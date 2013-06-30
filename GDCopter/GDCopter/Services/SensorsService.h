/*
* SensorsService.cpp
*
* Created: 09.12.2012 12:51:54
*  Author: Kirill
*/
#define BAROMETER_BUF_SIZE 32

#include "I2Cdev.h"
#include "MPU6050.h"
#include "HMC5883L.h"
#include "MS561101BA.h"
#include <Wire.h>
#include "vector3.h"
class SensorsService
{
	public:

	void Innitialize()
	{
		Wire.begin();
		accelgyro.initialize();
		//accelgyro.setI2CBypassEnabled(true);
		accelgyro.setFullScaleGyroRange(3);
		mag.initialize();
		
		_barometer.init(MS561101BA_ADDR_CSB_LOW);
		delay(100);
		float temp;
		float initialPressureBuffer[100];
		
		float tempTemp = 1.0f, tempPres = 1.0f;
		_pressure = 1.0f;
		for (int i=99; i>=0; i--)
		{
			tempPres =_barometer.getPressure(MS561101BA_OSR_4096, &tempTemp);
			if(tempTemp != 0)
			{
				_temperature = tempTemp;//чтобы после инициализации его считал Stabilizator для нахождения начальной высоты
			}
			if(tempPres !=0)
			{			
				_pressure = tempPres;
			}	
							
			initialPressureBuffer[i] = _pressure;		
			if (i<BAROMETER_BUF_SIZE)
			{
				PushToBarometerBuffer(_pressure);
			}
			if(tempPres == 0 || tempTemp == 0)
			{
				i++;
			}
			delay(10);
		}
		_pressure = GetAverage(initialPressureBuffer, 100);				//тут _pressure единственный раз хранит среднее давление,
		
		
		xGyroOffset = -37.61f;
		yGyroOffset = 6.68f;
		zGyroOffset = -14.30f;
		xAccelOffset = 500.0f;
		yAccelOffset = -100.0f;
		zAccelOffset = -650.0f;
		
		xCompassOffset = 0.0f;
		yCompassOffset = 300.0f;
		zCompassOffset = 75.0f;
		
		xCompassScale = 550.0f;
		yCompassScale = 1000.0f;
		zCompassScale = 850.0f;
		
		//xCompassOffset = 0;
		//yCompassOffset = 0;
		//zCompassOffset = 0;
		//
		//xCompassScale = 1;
		//yCompassScale = 1;
		//zCompassScale = 1;
		//To add the definition for the magntometer and accel offsets
	}
	
	void UpdateValues()
	{
		accelgyro.getMotion6(&ax, &ay, &az, &gx, &gy, &gz);
		mag.getHeading(&mx, &my, &mz);
		
		gyroValues = Vector3f((float)gx-xGyroOffset,(float)gy-yGyroOffset,(float)gz-zGyroOffset);
		accellValues = Vector3f((float)ax-xAccelOffset,(float)ay-yAccelOffset,(float)az-zAccelOffset);
		compassValues = Vector3f((mx+zCompassOffset)/xCompassScale,(my+yCompassOffset)/yCompassScale,
		(mz+zCompassOffset)/zCompassScale);
		
		gyroValues = (gyroValues/14.375f)*DEG_TO_RAD;
		accellValues/= 16384.f;
		float temperature;
		float tempPressure = _barometer.getPressure(MS561101BA_OSR_4096, &temperature);
		if(tempPressure != 0)
		{
			_pressure =tempPressure;
		}			
		if(temperature != 0) //{ вроде нормально работает и без этого, но мало ли
		_temperature = temperature;
	//}
		if (_pressure != NULL)
		{
			PushToBarometerBuffer(_pressure);
		}
	}

Vector3<float> GetGyroValues()
{
	return gyroValues;
}
Vector3<float> GetAccelValues()
{
	return accellValues;
}
Vector3<float> GetCompassValues()
{
	return compassValues;
}

float GetRawPressure()
{
	return _pressure;
}

float GetBarometerValue()
{
	return GetAverage(_barometerAverageBuffer, BAROMETER_BUF_SIZE);
}

void PushToBarometerBuffer(float pushedValue)
{
	_barometerAverageBuffer[_baroBufferCurrentElement] = pushedValue;
	_baroBufferCurrentElement = (_baroBufferCurrentElement + 1) % BAROMETER_BUF_SIZE;
}

float GetAverage(float* buffer, int size)
{
	float sum = 0.0;
	for(int i=0; i<size; i++)
	{
		sum += buffer[i];
	}
	return sum / size;
}

float GetTermometerValue()
{
	return _temperature;
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
MS561101BA _barometer;

int16_t mx, my, mz;
int16_t ax, ay, az;
int16_t gx, gy, gz;
float xGyroOffset;
float yGyroOffset;
float zGyroOffset;
float xAccelOffset;
float yAccelOffset;
float zAccelOffset;
float xCompassOffset;
float yCompassOffset;
float zCompassOffset;
float xCompassScale;
float yCompassScale;
float zCompassScale;

Vector3f gyroValues;
Vector3f accellValues;
Vector3f compassValues;

float _temperature;
float _pressure;
float _barometerAverageBuffer[BAROMETER_BUF_SIZE];
int _baroBufferCurrentElement;

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