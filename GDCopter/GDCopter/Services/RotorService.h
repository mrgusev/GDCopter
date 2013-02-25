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

	void StopAllRotors()
	{
		SetRotorsSpeed(5,5,5,5);
	}
	
	void SetRotorsSpeed(int r1, int r2, int r3, int r4)
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