/*
 * RotorService.cpp
 *
 * Created: 09.12.2012 12:52:17
 *  Author: Kirill
 */ 
#include "Servo.h" 
class RotorService
{
public:
	void Initialize()
	{
		rotor1.attach(9);
		rotor2.attach(10);
		rotor3.attach(11);
		rotor4.attach(12);
	}
	
	void SetRotorsSpeed(float r1, float r2, float r3, float r4)
	{
		rotor1.write( map(r1, 0, 1023, 0, 179));
		rotor2.write( map(r2, 0, 1023, 0, 179));
		rotor3.write( map(r3, 0, 1023, 0, 179));
		rotor4.write( map(r4, 0, 1023, 0, 179));
	}
private:
Servo rotor1;
Servo rotor2;
Servo rotor3;
Servo rotor4;

};