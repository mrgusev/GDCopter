#include "Arduino.h"

// I2Cdev and MPU6050 must be installed as libraries, or else the .cpp/.h files
// for both classes must be in the include path of your project

#include "SensorsService.h"
#include "Stabilizator.h"
#include "ClientService.h"

Stabilizator stabilizator;
ClientService clientService;
SensorsService sensorsService;

long previousMillis;
void setup()
{
	stabilizator.Initialize(&sensorsService);
	clientService.Initialize(&stabilizator,&sensorsService);
	pinMode(13,OUTPUT);
}

bool blinkState = false;
void loop()
{
	sensorsService.UpdateValues();
	stabilizator.CalculateAngles();
	clientService.Update();
}