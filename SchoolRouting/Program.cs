using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRouting
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new Algorithms.GurobiExample();
            Console.Write(test.Example());
        }
    }
}
