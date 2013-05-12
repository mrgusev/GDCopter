/*
 * OutgoingMessage.h
 *
 * Created: 09.05.2013 12:33:59
 *  Author: Kirill
 */ 

union float2chars { float f; char b[sizeof(float)]; };
class OutputMessage
{
	private:
	
	Vector3f orienation;
	Vector3f accel;
	Vector3f gyro;
	Vector3f compass;
	char rotors[4];
	
	char* GetFloatchars(float f)
	{		
		float2chars f2b;	
		f2b.f = f;
		return f2b.b;	
	}
	
	char* GetVector3fchars(Vector3f vec)
	{
		char result[12];
		
		char* floatchars = GetFloatchars(vec.x);
		for (uint16_t j = 0; j < 4; j++)
		{
			result[j] = floatchars[j];
		}
		
		floatchars = GetFloatchars(vec.y);		
		for (uint16_t j = 0; j < 4; j++)
		{
			result[j+1] = floatchars[j];
		}
		
		floatchars = GetFloatchars(vec.z);
		for (uint16_t j = 0; j < 4; j++)
		{
			result[j+2] = floatchars[j];
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
	
	void SetRotors(char r1,char r2,char r3,char r4)
	{
		rotors[0] = r1;
		rotors[1] = r2;
		rotors[2] = r3;
		rotors[3] = r4;
	}
	
	char* Getchars()
	{
		char chars[52];
		uint16_t charsCounter = 0;
		uint16_t i;
		char* buf = GetVector3fchars(orienation);
		for(i = 0; i < 12; i++, charsCounter++)
		{
			chars[charsCounter] = buf[i];
		}
		
		buf = GetVector3fchars(gyro);
		for(i = 0; i < 12; i++, charsCounter++)
		{
			chars[charsCounter] = buf[i];
		}
		
		buf = GetVector3fchars(accel);
		for(i = 0; i < 12; i++, charsCounter++)
		{
			chars[charsCounter] = buf[i];
		}
		
		buf = GetVector3fchars(compass);
		for(i = 0; i < 12; i++, charsCounter++)
		{
			chars[charsCounter] = buf[i];
		}
		
		for (i = 0; i < 4; i++, charsCounter++)
		{
			chars[charsCounter] = rotors[i];
		}
	}
};