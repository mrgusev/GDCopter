
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
	int rotorSpeeds[4];
	
	InputMessage()
	{
	}
	
	void Parse(char chars[])
	{
		if(sizeof(chars)/sizeof(char) > 0)
		{
			uint16_t charsCounter = 0;
		
			//—читываем информационный байт
			setState = chars[charsCounter] & 0<<1;
			setSendingData = chars[charsCounter] & 1<<1;
			setRotorSpeeds = chars[charsCounter] & 2<<1;
			setGain = chars[charsCounter] & 3<<1;
			setCompassOffset = chars[charsCounter] & 4<<1;
			resetAll = chars[charsCounter] & 7<<1;
			
			charsCounter++;
			
			if(setState)
			{
				//—читываем значение байта состо€ни€ 
				controllerState = (ControllerState)chars[charsCounter];	
				charsCounter++;
			}
			if(setSendingData)
			{
				//”станавливаем массив типов данных дл€ отправки на клиент
				sendingDataTypes[Stop] = chars[charsCounter] & 0<<1;
				sendingDataTypes[Sensors] = chars[charsCounter] & 1<<1;
				sendingDataTypes[Orientation] = chars[charsCounter] & 2<<1;
				sendingDataTypes[Rotors] = chars[charsCounter] & 3<<1;
				charsCounter++;
			}
			if(setRotorSpeeds)
			{
				//устанавливаем скорости роторов
				rotorSpeeds[0] = chars[charsCounter++];
				rotorSpeeds[1] = chars[charsCounter++];
				rotorSpeeds[2] = chars[charsCounter++];
				rotorSpeeds[3] = chars[charsCounter++];
			}
		}
	}	
};