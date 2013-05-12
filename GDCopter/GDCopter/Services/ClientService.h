/*
* ClientService.cpp
*
* Created: 09.12.2012 12:51:40
*  Author: Kirill
*/
class ClientService
{
	public:
	void Initialize()
	{
		Serial.begin(57600);
		Serial.setTimeout(10);
	}
	void SendText(char* s)
	{
		Serial.print(s);
		Serial.println(s);
	}
	void SendMessage(char* message)
	{
		Serial.write(*message);
	}
	char* GetMessage()
	{
		char * result;
		if(Serial.available())
		{
			 Serial.readBytes(result, 7);
		}
		return result;
	}
	
};