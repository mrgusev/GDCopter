#include "Arduino.h"

// I2Cdev and MPU6050 must be installed as libraries, or else the .cpp/.h files
// for both classes must be in the include path of your project

#include "SensorsService.h"
#include "RotorService.h"
#include "Stabilizator.h"
#include "CommandParser.h"
#include "Controller.h"
#include "ClientService.h"

Stabilizator stabilizator;
ClientService clientService;
Controller controller;
RotorService rotorService;
SensorsService sensorsService;

long previousMillis;
void setup()
{
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
	sensorsService.UpdateValues();
	stabilizator.CalculateAngles();
	clientService.Update();
	controller.Update();
}