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
	
	float data[16];

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
	
	void SetRotors(float r1,float r2,float r3,float r4)
	{
		rotors[0] = r1;
		rotors[1] = r2;
		rotors[2] = r3;
		rotors[3] = r4;
	}
	
	byte* GetBytes()
	{
		data[0] = 111.111f;//orienation.x;
		data[1] = 222.222f;//orienation.y;
		data[2] = 333.333f;//orienation.z;
		data[3] = 444.444f;//gyro.x;
		data[4] = 555.555f;//gyro.y;
		data[5] = 666.666f;//gyro.z;
		data[6] = 777.777f;//accel.x;
		data[7] = 888.888f;//accel.y;
		data[8] = 999.999f;//accel.z;
		data[9] = 101.101f;//compass.x;
		data[10] = 111.111f;//compass.y;
		data[11] = 121.121f;//compass.z;
		data[12] = 131.131f;//rotors[0];
		data[13] = 141.141f;//rotors[1];
		data[14] = 151.151f;//rotors[2];
		data[15] = 161.161f;//rotors[3];
		return (byte*)data;
	}
};