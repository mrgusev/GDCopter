#include "Arduino.h"

// I2Cdev and MPU6050 must be installed as libraries, or else the .cpp/.h files
// for both classes must be in the include path of your project

#include "Stabilizator.h"
#include "ClientService.h"

Stabilizator stabilizator;
ClientService clientService;


long previousMillis;
void setup()
{
	stabilizator.Initialize();
	clientService.Initialize(&stabilizator);
	pinMode(13,OUTPUT);
}

bool blinkState = false;
void loop()
{
	
	stabilizator.UpdateSensorsData();
	stabilizator.CalculateAngles();
	
	clientService.SendOrientation();
//	blinkState = !blinkState;
	
//
	//clientService.SendOrientation();	
	
	//unsigned long currentMillis = millis();
	//int dt = currentMillis - previousMillis;
	//Serial.println(dt);
	//previousMillis=currentMillis;
	
}