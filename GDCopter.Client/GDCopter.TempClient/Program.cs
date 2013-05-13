using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDCopter.TempClient
{
    class Program
    {

        static void Main(string[] args)
        {
            var clientService = new ClientService();
            clientService.OpenConnection();
            Console.ReadLine();
        }
    }
}
