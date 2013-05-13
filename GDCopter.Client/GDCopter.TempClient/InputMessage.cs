using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDCopter.TempClient
{
    class InputMessage
    {

        public string Parse(byte[] bytes)
        {
            int byteCounter = 0;
            float x = System.BitConverter.ToSingle(bytes, byteCounter);
            byteCounter += 4;
            float y = System.BitConverter.ToSingle(bytes, byteCounter);
            byteCounter += 4;
            float z = System.BitConverter.ToSingle(bytes, byteCounter);
            byteCounter += 4;
            return String.Format("x={0}, y={1}, z={2}", x, y, z);
        }
	    
        
    }
}
