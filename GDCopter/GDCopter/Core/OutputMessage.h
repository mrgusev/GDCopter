/*
 * OutgoingMessage.h
 *
 * Created: 09.05.2013 12:33:59
 *  Author: Kirill
 */ 

class OutputMessage
{
	private:
	
	Vector3f orienation;
	Vector3f accel;
	Vector3f gyro;
	Vector3f compass;
	byte rotors[4];
	
	byte* GetFloatbytes(float f)
	{		
		return (byte*)&f;
	}
	
	byte* GetVector3fbytes(Vector3f vec)
	{
		byte result[12];
		
		byte* floatbytes = GetFloatbytes(vec.x);
		for (uint16_t j = 0; j < 4; j++)
		{
			result[j] = floatbytes[j];
		}
		
		floatbytes = GetFloatbytes(vec.y);		
		for (uint16_t j = 0; j < 4; j++)
		{
			result[j+1] = floatbytes[j];
		}
		
		floatbytes = GetFloatbytes(vec.z);
		for (uint16_t j = 0; j < 4; j++)
		{
			result[j+2] = floatbytes[j];
		}
	}
	
	public:
	
	void SetOrientation(Vector3f vec)
	{
		orienation = vec;
	}
	
	void SetAccel(Vector3f vec)
	{
		accel = vec;
	}
	
	void SetGyro(Vector3f vec)
	{
		gyro = vec;
	}
	
	void SetCompass(Vector3f vec)
	{
		compass = vec;
	}
	
	void SetRotors(byte r1,byte r2,byte r3,byte r4)
	{
		rotors[0] = r1;
		rotors[1] = r2;
		rotors[2] = r3;
		rotors[3] = r4;
	}
	
	byte* Getbytes()
	{
		byte bytes[52];
		uint16_t bytesCounter = 0;
		uint16_t i;
		byte* buf = GetVector3fbytes(orienation);
		for(i = 0; i < 12; i++, bytesCounter++)
		{
			bytes[bytesCounter] = buf[i];
		}
		
		buf = GetVector3fbytes(gyro);
		for(i = 0; i < 12; i++, bytesCounter++)
		{
			bytes[bytesCounter] = buf[i];
		}
		
		buf = GetVector3fbytes(accel);
		for(i = 0; i < 12; i++, bytesCounter++)
		{
			bytes[bytesCounter] = buf[i];
		}
		
		buf = GetVector3fbytes(compass);
		for(i = 0; i < 12; i++, bytesCounter++)
		{
			bytes[bytesCounter] = buf[i];
		}
		
		for (i = 0; i < 4; i++, bytesCounter++)
		{
			bytes[bytesCounter] = rotors[i];
		}
	}
};