using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePolygon
{
    class Program
    {
        static void Main(string[] args)
        {
            new One("hello");
            new Two("hello Two","another");
        }
    
    }
    class One
    {
        public One(string msg)
        {
            Console.WriteLine("msg");
        }
    }
    class Two : One
    {
        public Two(string msg, string msg2) : base(msg)
        {
            Console.WriteLine(msg2);
        }
    }
}
