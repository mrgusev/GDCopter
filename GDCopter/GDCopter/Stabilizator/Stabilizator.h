/*
* Stabilizator.h
*
* Created: 13.12.2012 2:42:19
*  Author: Kirill
*/

#include "matrix3.h"

class Stabilizator
{
	private:
	long previousMillis;
	SensorsService* _sensorsService;
	Matrix3f rotationMatrix;
	Vector3l earthAngles;
	
	float currentPitch;
	float currentYaw;
	float currentRoll;
	float currentAltitude;
	
	float previousPitch;
	float previousYaw;
	float previousRoll;
	float previousAltitude;
	
	float firstMotorSpeedSq;
	float secondMotorSpeedSq;
	float thirdRotorSpeedSq;
	float fourthRotorSpeedSq;
	
	public:
	
	float FilterGyroCoef;
	float FilterAccelCoef;
	float FilterCompasCoef;
	
	float ThrustDerivativeCoef;
	float PitchDerivativeCoef;
	float RollDerivativeCoef;
	float YawDerivativeCoef;
	float ThrustProportionalCoef;
	float PitchProportionalCoef;
	float RollProportionalCoef;
	float YawProportionalCoef;
	
	float QuadInertiaX;
	float QuadInertiaY;
	float QuadInertiaZ;
	
	float RotorLiftConstant;
	float RotorDragConstant;
	float QuadRadius;
	
	float QuadMass;
	
	int dt;
	
	void Initialize(SensorsService* sensorsService)
	{
		_sensorsService = sensorsService;
		_sensorsService->Innitialize();
		previousMillis = 0;
		rotationMatrix.a = Vector3f(1,0,0);
		rotationMatrix.b = Vector3f(0,1,0);
		rotationMatrix.c = Vector3f(0,0,1);
		//earthAngles = Vector3l(0,0,0);
		
		FilterGyroCoef=0.9;
		FilterAccelCoef=0.1;
		FilterCompasCoef=0.15;
		
		currentPitch=0;
		currentYaw=0;
		currentRoll=0;
	}
	
	void CalculateAngles()
	{
		unsigned long currentMillis = micros();
		dt = currentMillis - previousMillis;
		
		Vector3<float> currentGyroValues = _sensorsService->GetGyroValues();
		Vector3<float> currentAccelValues = _sensorsService->GetAccelValues();
		Vector3<float> currentCompassValues = _sensorsService->GetCompassValues();
		
		//¬ычисл€ем длины векторов, дл€ нахождени€ косинусов углов между ними и ос€ми
		float gravityVectorLength = currentAccelValues.length();
		//float magneticVectorLength = currentCompassValues.length();
		
		//¬ычисл€ем косинусы углов между ос€ми инерционной и объектной систем
		//¬ычисленные косинусы составл€ют первую и третью строки матрицы дл€ данной итерации
		//Vector3<float> currentRotationMatrixFirstRow = Vector3<float>(currentCompassValues.x/magneticVectorLength,currentCompassValues.y/magneticVectorLength,currentCompassValues.z/magneticVectorLength); //принципиально использование функции не из библиотеки,
		Vector3<float> currentRotationMatrixThirdRow = Vector3<float>(-currentAccelValues.x/gravityVectorLength,-currentAccelValues.y/gravityVectorLength,-currentAccelValues.z/gravityVectorLength);       //так как там длина вычисл€етс€ несколько раз
		//
		//Ќаходим угловое приращение трем€ разными способами
		Vector3f gyroAngularDisplacement = currentGyroValues * (float)dt/1000000.0f;
		Vector3f accelAngularDisplacement = rotationMatrix.c % (currentRotationMatrixThirdRow - rotationMatrix.c);
	//	Vector3f compasAngularDisplacement = RotationMatrix.a % (currentRotationMatrixFirstRow - RotationMatrix.a);
		//
		//Ќаходим среднее между трем€ способами
		Vector3f averageAngularDisplacement = gyroAngularDisplacement*  FilterGyroCoef + accelAngularDisplacement * FilterAccelCoef;//; + compasAngularDisplacement * filterCompasCoef;
		
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
		
		//x += averageAngularDisplacement.x;//(float)sensorsService.GetGyroValues().x*((float)dt/1000000)/14.375; // Without any filter
		//
		//y += averageAngularDisplacement.y;//(float)sensorsService.GetGyroValues().y*((float)dt/1000000)/14.375; // Without any filter
		//
		//z += averageAngularDisplacement.z;//(float)sensorsService.GetGyroValues().z*((float)dt/1000000)/14.375; // Without any filter
		//
		previousPitch = currentPitch;
		currentPitch = -asin(rotationMatrix.c.y);
		previousYaw = currentYaw;
		currentYaw= atan2(-rotationMatrix.c.x, rotationMatrix.c.z);
		previousRoll = currentRoll;
		currentRoll = atan2(-rotationMatrix.a.y, rotationMatrix.b.y);
		
		//double accXangle = getXangle();
		//
		//double accYangle = getYangle();
		//
		//compAngleX = (0.93*(compAngleX+(gyroXrate*(double)(micros()-timer)/1000000)))+(0.07*accXangle);
		//compAngleY = (0.93*(compAngleY+(gyroYrate*(double)(micros()-timer)/1000000)))+(0.07*accYangle);
		//
		//
		earthAngles.x = currentPitch;
		earthAngles.y = currentYaw;
		earthAngles.z = currentRoll;
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
	
	void CalculateMotorsSpeeds()
	{
		float totalThrust = (9.8f + ThrustDerivativeCoef * (previousAltitude - currentAltitude) / dt - ThrustProportionalCoef * currentAltitude) * QuadMass / rotationMatrix.c.z;
		float rollTorque = (ThrustDerivativeCoef * (previousRoll - currentRoll) / dt - ThrustProportionalCoef * currentRoll) * QuadInertiaX;
		float pitchTorque = (ThrustDerivativeCoef * (previousPitch - currentPitch) / dt - ThrustProportionalCoef * currentPitch) * QuadInertiaY;
		float yawTorque = (ThrustDerivativeCoef * (previousYaw - currentYaw) / dt - ThrustProportionalCoef * currentYaw) * QuadInertiaZ;
		
		float thrustAdditive = totalThrust / 4 / RotorLiftConstant;
		float pitchAdditive = pitchTorque / (2 * RotorLiftConstant * QuadRadius);
		float rollAdditive = rollTorque / (2 * RotorLiftConstant * QuadRadius);
		float yawAdditive = yawTorque / 4 / RotorDragConstant;
		
		//it is necessary to add the limitations for the values of the
		//squared rotors' speeds (e. g. not less than zero) but it is
		//supposed to be done in the future, while linearizing the
		//dependency between the PWM value and a rotor's squared speed (just speed)
		firstMotorSpeedSq = thrustAdditive - pitchAdditive - yawAdditive;
		secondMotorSpeedSq = thrustAdditive - rollAdditive + yawAdditive;
		thirdRotorSpeedSq = thrustAdditive + pitchAdditive - yawAdditive;
		fourthRotorSpeedSq = thrustAdditive + rollAdditive + yawAdditive; 
	}
	
	Vector3l GetOrientation()
	{
		return earthAngles;
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