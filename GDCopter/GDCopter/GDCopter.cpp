#include "Arduino.h"

// I2Cdev and MPU6050 must be installed as libraries, or else the .cpp/.h files
// for both classes must be in the include path of your project

int dt;
#include "SensorsService.h"
#include "RotorService.h"
#include "Stabilizator.h"
#include "CommandParser.h"
#include "Controller.h"
#include "ClientService.h"

long previousMillis;
Stabilizator stabilizator;
ClientService clientService;
Controller controller;
RotorService rotorService;
SensorsService sensorsService;

void setup()
{
	previousMillis = 0;
	sensorsService.Innitialize();
	rotorService.Initialize();
	stabilizator.Initialize(&sensorsService);
	controller.Initialize(&rotorService);
	clientService.Initialize(&stabilizator,&sensorsService,&controller);
	pinMode(13,OUTPUT);
}

bool blinkState = false;
void loop()
{
	unsigned long currentMillis = micros();
	dt = currentMillis - previousMillis;
	sensorsService.UpdateValues();
	stabilizator.CalculateAngles();
	clientService.Update();
	controller.Update();
	previousMillis=currentMillis;
}