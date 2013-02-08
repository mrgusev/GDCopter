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
	private:
	long previousMillis;
	SernsorsService sensorsService;
	Matrix3f RotationMatrix;
	Vector3i planeAngles;
	Vector3l earthAngles;	
	
	int dt;
	int fx;
	int fy;
	int fz;
	
	float x;
	float y;
	float z;
	
	public:
	
	float filterGyroCoef;
	float filterAccelCoef;
	float filterCompasCoef;
	
	void Initialize()
	{
		sensorsService.Innitialize();
		previousMillis = 0;
		RotationMatrix.a = Vector3f(1,0,0);
		RotationMatrix.b = Vector3f(0,1,0);
		RotationMatrix.c = Vector3f(0,0,1);
		//earthAngles = Vector3l(0,0,0);
		planeAngles = Vector3i(1,1,1);
		
		filterGyroCoef=0.5;
		filterAccelCoef=0.25;
		filterCompasCoef=0.25;
		
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
		
		Vector3<float> currentGyroValues = GetGyroValues();
		Vector3<float> currentAccelValues = GetAccelValues();
		Vector3<float> currentCompassValues = GetCompassValues();
		
		//¬ычисл€ем длины векторов, дл€ нахождени€ косинусов углов между ними и ос€ми
		float gravityVectorLength = currentAccelValues.length();
		float magneticVectorLength = currentCompassValues.length();
		
		//¬ычисл€ем косинусы углов между ос€ми инерционной и объектной систем
		//¬ычисленные косинусы составл€ют первую и третью строки матрицы дл€ данной итерации
		Vector3<float> currentRotationMatrixFirstRow = Vector3<float>(currentCompassValues.x/magneticVectorLength,currentCompassValues.y/magneticVectorLength,currentCompassValues.z/magneticVectorLength); //принципиально использование функции не из библиотеки,
		Vector3<float> currentRotationMatrixThirdRow = Vector3<float>(-currentAccelValues.x/gravityVectorLength,-currentAccelValues.y/gravityVectorLength,-currentAccelValues.z/gravityVectorLength);       //так как там длина вычисл€етс€ несколько раз
		
		//Ќаходим угловое приращение трем€ разными способами
		Vector3f gyroAngularDisplacement = currentGyroValues * dt;		
		Vector3f accelAngularDisplacement = RotationMatrix.c % (currentRotationMatrixThirdRow - RotationMatrix.c);
		Vector3f compasAngularDisplacement = RotationMatrix.a % (currentRotationMatrixFirstRow - RotationMatrix.a);
		
		//Ќаходим среднее между трем€ способами
		Vector3f averageAngularDisplacement = gyroAngularDisplacement * filterGyroCoef + accelAngularDisplacement * filterAccelCoef + compasAngularDisplacement * filterCompasCoef;
		
		//Ќаходим новую матрицу поворота
		RotationMatrix.a += (RotationMatrix.a % averageAngularDisplacement);
		RotationMatrix.c += (RotationMatrix.c % averageAngularDisplacement);
		float error = (RotationMatrix.a * RotationMatrix.c) / 2;		
		Vector3f rotationMatrixOldA = RotationMatrix.a;		
		RotationMatrix.a -= RotationMatrix.c * error;
		RotationMatrix.a.normalize();
		RotationMatrix.c -= rotationMatrixOldA * error;
		RotationMatrix.c.normalize();		
		RotationMatrix.b = RotationMatrix.a % RotationMatrix.c;
		
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
		
		x += averageAngularDisplacement.x;//(float)sensorsService.GetGyroValues().x*((float)dt/1000000)/14.375; // Without any filter
		
		y += averageAngularDisplacement.y;//(float)sensorsService.GetGyroValues().y*((float)dt/1000000)/14.375; // Without any filter
		
		z += averageAngularDisplacement.z;//(float)sensorsService.GetGyroValues().z*((float)dt/1000000)/14.375; // Without any filter
		
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
	Vector3<float> GetAccelValues()
	{
		return sensorsService.GetAccelValues();
	}
	Vector3<float> GetCompassValues()
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
};