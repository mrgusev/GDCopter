/*
* Stabilizator.h
*
* Created: 13.12.2012 2:42:19
*  Author: Kirill
*/

#define SEA_PRESSURE 1013.25f
#include "matrix3.h"

class Stabilizator
{
	private:
	long dt;
	long previousMillis;
	SensorsService* _sensorsService;
	Matrix3f rotationMatrix;
	Vector3l earthAngles;
	float _initialAltitude;
	float _relativeAltitude;
	
	float currentPitch;
	float currentYaw;
	float currentRoll;
	float currentAltitude;	
	
	public:
	
	float FilterGyroCoef;
	float FilterAccelCoef;
	float FilterCompasCoef;
	
	void Initialize(SensorsService* sensorsService)
	{
		_sensorsService = sensorsService;
		_sensorsService->Innitialize();
		_initialAltitude = CalculateAltitude(_sensorsService->GetTermometerValue(),_sensorsService->GetRawPressure());
		previousMillis = 0;
		rotationMatrix.a = Vector3f(1,0,0);
		rotationMatrix.b = Vector3f(0,1,0);
		rotationMatrix.c = Vector3f(0,0,1);
		
		FilterGyroCoef=0.7;
		FilterAccelCoef=0.15;
		FilterCompasCoef=0.15;
		
		currentPitch=0;
		currentYaw=0;
		currentRoll=0;
		currentAltitude = 0;
	}
	
	float CalculateAltitude(float temperature, float pressure)
	{
		return ((pow((SEA_PRESSURE / pressure), 1/5.257) - 1.0) * (temperature + 273.15)) / 0.0065;
	}
	
	void CalculateRelativeAltitude()
	{
		_relativeAltitude = CalculateAltitude(_sensorsService->GetTermometerValue(),_sensorsService->GetBarometerValue()) - _initialAltitude;
	}
	
	void CalculateAngles()
	{		
		_sensorsService->UpdateValues();
		
		Vector3<float> currentGyroValues = _sensorsService->GetGyroValues();
		Vector3<float> currentAccelValues = _sensorsService->GetAccelValues();
		Vector3<float> currentCompassValues = _sensorsService->GetCompassValues();
		
		//¬ычисл€ем длины векторов, дл€ нахождени€ косинусов углов между ними и ос€ми
		float gravityVectorLength = currentAccelValues.length();
		float magneticVectorLength = currentCompassValues.length();
		
		//¬ычисл€ем косинусы углов между ос€ми инерционной и объектной систем
		//¬ычисленные косинусы составл€ют первую и третью строки матрицы дл€ данной итерации
		Vector3<float> currentRotationMatrixFirstRow = Vector3<float>(currentCompassValues.x/magneticVectorLength,currentCompassValues.y/magneticVectorLength,currentCompassValues.z/magneticVectorLength); //принципиально использование функции не из библиотеки,
		Vector3<float> currentRotationMatrixThirdRow = Vector3<float>(-currentAccelValues.x/gravityVectorLength,-currentAccelValues.y/gravityVectorLength,-currentAccelValues.z/gravityVectorLength);       //так как там длина вычисл€етс€ несколько раз
		//
		//Ќаходим угловое приращение трем€ разными способами
		Vector3f gyroAngularDisplacement = currentGyroValues * (float)dt/1000000.0f;
		Vector3f accelAngularDisplacement = rotationMatrix.c % (currentRotationMatrixThirdRow - rotationMatrix.c);
		Vector3f compasAngularDisplacement = rotationMatrix.a % (currentRotationMatrixFirstRow - rotationMatrix.a);
		//
		//Ќаходим среднее между трем€ способами
		Vector3f averageAngularDisplacement = gyroAngularDisplacement*  FilterGyroCoef + accelAngularDisplacement * FilterAccelCoef + compasAngularDisplacement * FilterCompasCoef;
		
		//Ќаходим новую матрицу поворота
		rotationMatrix.a += (rotationMatrix.a % averageAngularDisplacement);
		rotationMatrix.b += (rotationMatrix.b % averageAngularDisplacement);
		rotationMatrix.c += (rotationMatrix.c % averageAngularDisplacement);
		
		float error = (rotationMatrix.a * rotationMatrix.c) / 2;
		Vector3f rotationMatrixOldA = rotationMatrix.a;
		rotationMatrix.a -= rotationMatrix.c * error;
		rotationMatrix.a.normalize();
		rotationMatrix.c -= rotationMatrixOldA * error;
		rotationMatrix.c.normalize();
		rotationMatrix.b = rotationMatrix.a % rotationMatrix.c;
		
		currentPitch = -asin(rotationMatrix.c.y);
		currentYaw= atan2(-rotationMatrix.c.x, rotationMatrix.c.z);
		currentRoll = atan2(-rotationMatrix.a.y, rotationMatrix.b.y);
		
		earthAngles.x = currentPitch;
		earthAngles.y = currentYaw;
		earthAngles.z = currentRoll;
	}

	Vector3f GetOrientation()
	{
		return Vector3f(currentPitch, currentYaw, currentRoll);
	}
	
	Matrix3f GetRotationMatrix()
	{
		return rotationMatrix;
	}
	
	float GetAltitude()
	{
		return _relativeAltitude;
	}
		
	float GetPitch()
	{
		return currentPitch;
	}
	
	float GetYaw()
	{
		return currentYaw;
	}
	
	float GetRoll()
	{
		return currentRoll;
	}
};