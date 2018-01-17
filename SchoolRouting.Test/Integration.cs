using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SchoolRouting.Test
{
    [TestClass]
    public class Integration
    {
        /// <summary>
        /// Poboljšati pretragu parametara.
        /// </summary>
        [TestMethod]
        public void FindOptimumSimmulatedAnnealingParameters()
        {
            double bestCost = double.MaxValue;
            List<Tuple<double, double, double>> bestParameters = new List<Tuple<double, double, double>>();
            for (double temperature = 400; temperature < 1000; temperature++)
            {
                for (double epsilon = 0.01; epsilon > 0.0001; epsilon -= 0.0001)
                {
                    for (double alpha = 0.99; alpha < 0.999; alpha += 0.001)
                    {
                        double totalCostForParameters = 0;
                        for (int instance = 1; instance < 11; instance++)
                        {
                            totalCostForParameters += Program.MainMoq(temperature, epsilon, alpha, instance);
                        }
                        if (totalCostForParameters < bestCost)
                        {
                            bestCost = totalCostForParameters;
                            bestParameters.Add(new Tuple<double, double, double>(temperature,epsilon,alpha));
                        }
                    }
                }
            }
        }
    }
}
