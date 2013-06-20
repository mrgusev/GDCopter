#include "Arduino.h"

#include "Enums.h"
#include "SensorsService.h"
#include "RotorService.h"
#include "Stabilizator.h"
#include "Controller.h"
#include "ClientService.h"
#include "CommandManager.h"


long previousMillis;
int dt;
Stabilizator stabilizator;
ClientService clientService;
Controller controller;
RotorService rotorService;
SensorsService sensorsService;
CommandManager commandManager;

void setup()
{
	delay(10000);
	previousMillis = 0;
	sensorsService.Innitialize();
	rotorService.Initialize();
	clientService.Initialize();
	stabilizator.Initialize(&sensorsService);
	controller.Initialize(&rotorService);
	commandManager.Initialize(&clientService, &stabilizator, &sensorsService, &controller);
	pinMode(13,OUTPUT);
}

void loop()
{	
	unsigned long currentMillis = micros();
	dt = currentMillis - previousMillis;
	controller.SetDt(dt);
	stabilizator.SetDt(dt);
	commandManager.SetDt(dt);
	stabilizator.CalculatePosition();
	controller.Update();
	commandManager.Update();
	previousMillis=currentMillis;
}