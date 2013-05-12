enum ControllerState
{
	StopRotors = 0,
	Stabilization = 1,
	DirectValues = 2,
	ControlStabilization = 3
};

enum SendingDataType
{
	Stop = 0,
	Sensors = 1,
	Orientation = 2,
	Compass = 3,
	Rotors = 4
};
///*
//* Commands.h
//*
//* Created: 14.02.2013 15:55:39
//*  Author: Kirill
//*/
//enum CommandType
//{
	//ContrllerCommand = 0,
	//RotorSpeed = 1,
	//ClientServiceCommand = 2
//};
//
//enum ClientServiceState
//{
	//Stop = 0,
	//Sensors = 1,
	//Orientation = 2,
	//Compass = 3,
	//Delay = 4,
	//Rotors =5
//};
//
//enum ControllerState
//{
	//StopRotors = 0,
	//Stabilization = 1,
	//DirectValues = 2
//};
//
//
//class CommandParser
//{
	//public:
	//
	//static ClientServiceState ParseClientServiceCommand(String message)
	//{
		//if(message=="$stop"){
			//return Stop;
		//}
		//if(message=="$sensors"){
			//return Sensors;
		//}
		//if(message=="$orientation"){
			//return Orientation;
		//}
		//if(message=="$compass"){
			//return Compass;
		//}
		//if(message=="$delay"){
			//return Delay;
		//}
		//if(message=="$rotors"){
		//return Rotors;}
		//return (ClientServiceState)-1;
	//};
	//
	//static ControllerState ParceControllerCommand(String message)
	//{
		//if(message == "*stabilize"){
			//return Stabilization;
		//}
		//if(message == "*directvalues"){
			//return DirectValues;
		//}
		//if(message == "*stop"){
			//return StopRotors;
		//}
		//return (ControllerState)-1;
	//};
	//
	//static void ParseRotorSpeed(String message, int* value1, int* value2, int* value3, int* value4)
	//{
		//if(GetCommandType(message) == RotorSpeed)
		//{
			//int valuecounter = 0;
		//int indeces[4];
		//indeces[0] = 0;
		//for (int i=0;i<message.length();++i)
		//{
			//if(message[i] == '#')
			//{
				//valuecounter++;
				//indeces[valuecounter] = i;
				//char buffer[6];
				//message.substring(indeces[valuecounter-1]+1,indeces[valuecounter]).toCharArray(buffer,6);
//
				//switch(valuecounter){
					//case 1:
						//*value1 = atoi(buffer);
						//break;
					//case 2:
						//*value2 = atoi(buffer);
						//break;
					//case 3:
						//*value3 = atoi(buffer);
						//break;
					//case 4:
						//*value4 = atoi(buffer);
						//break;
				//}
			//}
		//}
	//}
//};
//
//static CommandType GetCommandType(String message)
//{
	//if(message[0] == '$')
	//return ClientServiceCommand;
	//if(message[0] == '*')
	//return ContrllerCommand;
	//if(message[0] == '@')
	//return RotorSpeed;
	//return (CommandType)-1;
//};
//};