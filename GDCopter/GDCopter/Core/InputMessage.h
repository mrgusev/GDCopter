union u_tag 
{
	byte bytesArray[4];
	float floatValue;
}u;

class InputMessage
{
	public:	
	boolean setState;
	boolean setSendingData;
	boolean setRotorSpeeds;	
	boolean setGain;
	boolean setCompassOffset;	
	boolean resetAll;
		
	ControllerState controllerState;
	boolean sendingDataTypes[5];
	float rotorSpeeds[4];
	
	InputMessage()
	{
	}
	
	void Parse(char chars[])
	{
		char convertedArray[4];
		for (int i=0; i<4; i++)
		{
			convertedArray[0]=chars[i*4];
			convertedArray[1]=chars[i*4+1];
			convertedArray[2]=chars[i*4+2];
			convertedArray[3]=chars[i*4+3];
			rotorSpeeds[i]=ConvertBytesToFloat(convertedArray);
		}
		//if(sizeof(chars) / sizeof(char) > 0)
		//{
			//uint16_t charsCounter = 0;
		//
			////—читываем информационный байт
			//setState = chars[charsCounter] & 0<<1;
			//setSendingData = chars[charsCounter] & 1<<1;
			//setRotorSpeeds = chars[charsCounter] & 2<<1;
			//setGain = chars[charsCounter] & 3<<1;
			//setCompassOffset = chars[charsCounter] & 4<<1;
			//resetAll = chars[charsCounter] & 7<<1;
			//
			//charsCounter++;
			//
			//if(setState)
			//{
				////—читываем значение байта состо€ни€ 
				//controllerState = (ControllerState)chars[charsCounter];	
				//charsCounter++;
			//}
			//if(setSendingData)
			//{
				////”станавливаем массив типов данных дл€ отправки на клиент
				//sendingDataTypes[Stop] = chars[charsCounter] & 0<<1;
				//sendingDataTypes[Sensors] = chars[charsCounter] & 1<<1;
				//sendingDataTypes[Orientation] = chars[charsCounter] & 2<<1;
				//sendingDataTypes[Rotors] = chars[charsCounter] & 3<<1;
				//charsCounter++;
			//}
			//if(setRotorSpeeds)
			//{
				////устанавливаем скорости роторов
				//rotorSpeeds[0] = chars[charsCounter++];
				//rotorSpeeds[1] = chars[charsCounter++];
				//rotorSpeeds[2] = chars[charsCounter++];
				//rotorSpeeds[3] = chars[charsCounter++];
			//}
		//}
	}
	
	private:
	float ConvertBytesToFloat(char array[4])
	{
		u.bytesArray[0] = array[0];
		u.bytesArray[1] = array[1];
		u.bytesArray[2] = array[2];
		u.bytesArray[3] = array[3];
		return u.floatValue;
	}	
};