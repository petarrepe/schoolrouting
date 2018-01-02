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
            List<Instance> instancesList = new List<Instance>();
            for (int i = 1; i < 11; i++)//provjera mogu li se ispravno učitati svi fajlovi
            {
                instancesList.Add(InputService.Parse(i));
            }

            var test = new Algorithms.GurobiExample();
            Console.Write(test.Example());
        }
    }
}
