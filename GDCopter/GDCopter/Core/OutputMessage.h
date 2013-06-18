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
	float _altitude;
	
	float data[17];

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
	
	void SetAltitude(float altitude)
	{
		altitude=_altitude;
	}
	
	void SetRotors(float r1,float r2,float r3,float r4)
	{
		rotors[0] = r1;
		rotors[1] = r2;
		rotors[2] = r3;
		rotors[3] = r4;
	}
	
	byte* GetBytes()
	{
		data[0] = orienation.x;
		data[1] = orienation.y;
		data[2] = orienation.z;
		data[3] = gyro.x;
		data[4] = gyro.y;
		data[5] = gyro.z;
		data[6] = accel.x;
		data[7] = accel.y;
		data[8] = accel.z;
		data[9] = compass.x;
		data[10] = compass.y;
		data[11] = compass.z;
		data[12] = rotors[0];
		data[13] = rotors[1];
		data[14] = rotors[2];
		data[15] = rotors[3];
		data[16] = _altitude;
		return (byte*)data;
	}
};