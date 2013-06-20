/*
* ClientService.cpp
*
* Created: 09.12.2012 12:51:40
*  Author: Kirill
*/
#include "InputMessage.h"
#include "OutputMessage.h"
class CommandManager
{
	public:
	
	void Initialize(ClientService* clientService, Stabilizator* stabilizator, SensorsService* sensorsService, Controller* controller)
	{
		_stabilizator = stabilizator;
		_sensorsService = sensorsService;
		_controller = controller;
		_clientService = clientService;
		_timerCount = 0;
	}

	void SetDt (int dt)
	{
		_dt = dt;	
	}

	void Update()
	{
		_timerCount += _dt;
		if(_timerCount >= 50)
		{
			_timerCount -= 50;
			//_inputMessage.Parse(_clientService->GetMessage());
			//SetInputCommands();
			SetOutputData();
			_clientService->SendMessage(_outputMessage.GetBytes());
		}
	}
	
	private:
	
	boolean sendingDataTypes[5];	
	int _timerCount;
	int _dt;
	ClientService* _clientService;
	Stabilizator* _stabilizator;
	SensorsService* _sensorsService;
	
	Controller* _controller;

	InputMessage _inputMessage;
	
	OutputMessage _outputMessage;
	
	void SetInputCommands()
	{
		if(_inputMessage.setState)
		{
			_controller->SetState(_inputMessage.controllerState);
		}
		if(_inputMessage.setSendingData)
		{
			//sendingDataTypes = _inputMessage.sendingDataTypes;
		}
		if(_inputMessage.setRotorSpeeds)
		{
			_controller->SetRotorSpeeds(_inputMessage.rotorSpeeds[0],_inputMessage.rotorSpeeds[1],
				_inputMessage.rotorSpeeds[2],_inputMessage.rotorSpeeds[3]);
		}
		if(_inputMessage.resetAll)
		{
			
		}
	}
	
	void SetOutputData()
	{
		_outputMessage.SetOrientation(_stabilizator->GetOrientation());
		_outputMessage.SetGyro(_sensorsService->GetGyroValues());
		_outputMessage.SetAccel(_sensorsService->GetAccelValues());
		_outputMessage.SetCompass(_sensorsService->GetCompassValues());
		_outputMessage.SetAltitude(_stabilizator->GetAltitude());
		
		float r1, r2, r3, r4;
		r1 = _controller->GetRotorSpeed1();
		r2 = _controller->GetRotorSpeed2();
		r3 = _controller->GetRotorSpeed3();
		r4 = _controller->GetRotorSpeed4();
		_outputMessage.SetRotors(r1, r2, r3, r4);
	}
};