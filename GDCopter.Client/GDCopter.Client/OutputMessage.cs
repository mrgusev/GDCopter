using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDCopter.Client
{
    public class OutputMessage
    {
        public float Rotor1 { get; set; }
        public float Rotor2 { get; set; }
        public float Rotor3 { get; set; }
        public float Rotor4 { get; set; }

        public byte[] GetBytes()
        {
            var floatArray = new [] {Rotor1, Rotor2, Rotor3, Rotor4};
            var byteArray = new byte[floatArray.Length * 4];
            Buffer.BlockCopy(floatArray, 0, byteArray, 0, byteArray.Length);
            return byteArray;
        }
    }
}
