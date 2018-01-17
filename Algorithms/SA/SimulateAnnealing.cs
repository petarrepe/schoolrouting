using SchoolRouting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.SA
{
    public class SimulateAnnealing
    {

        public Solution StartAnnealing(double temperature, double epsilon, double alpha, List<Cluster> resultCluster, Instance instance, Solution initialSolution)
        {

            double delta;
            Random random = new Random();
            double xProbability;
            Solution bestSolution = initialSolution;
            Solution finalSolution = initialSolution;
            Solution nextSolution = initialSolution;
            int distance = 0;//cost function
            delta = CostFunction(bestSolution, instance);

            while (temperature > epsilon)
            {
                nextSolution = Neighbourhood(bestSolution, resultCluster, instance);//->izracunaj susjeda() S od trenutnog S0

                if (Solution.IsInfeasible(nextSolution, instance.Capacity)==true) continue;

                delta = CostFunction(nextSolution, instance) - CostFunction(bestSolution, instance);//-> delta = f(S) - f(S0) 
                

                if (delta < 0)
                {
                    bestSolution = nextSolution;//S0 = S;
                    finalSolution = nextSolution;
                }
                else
                {
                    xProbability = random.NextDouble();// generate ,random x e(0,1)
                    if (xProbability < Math.Exp(-delta / temperature))
                    {
                        bestSolution = nextSolution;//S0 = S;
                    }
                    else
                    {
                        //reject S   
                    }

                }
                temperature = alpha * temperature;//proces hladenja
            }
            return finalSolution;
        }

        public static Solution Neighbourhood(Solution solution, List<Cluster> cluster, Instance instanca)
        {
            Random random = new Random();
            int randomNumber = random.Next(4) + 1;

            switch (randomNumber)
            {
                case 1: return OnePointMove(solution, cluster, instanca);
                case 2: return TwoPointMove(solution, cluster);
                case 3: return TwoOptMove(solution, cluster);
                case 4: return CrossExchange(solution, cluster);
                default: return solution;
            }
        }

        private static Solution CrossExchange(Solution solution, List<Cluster> cluster)
        {
            Random random = new Random();
            int randomRouteIndex1 = random.Next(solution.BusTours.Count()); //odaberi prvu rutu
            int randomRouteIndex2 = random.Next(solution.BusTours.Count()); //odaberi drugu random rutu
            int elementsRoute1 = solution.BusTours.ElementAt(randomRouteIndex1).Count();//broj stanica prve rute
            int elementsRoute2 = solution.BusTours.ElementAt(randomRouteIndex2).Count(); //broj stanica druge rute


            if (elementsRoute1 >= 2 && elementsRoute2 >= 2 && (randomRouteIndex1 != randomRouteIndex2))//minimalno 2 stanice po ruti inace se ne moze traziti rjesenje i rute trebaju biti razlicite
            {
                List<int> route1 = new List<int>();
                List<int> route2 = new List<int>();
                int x = random.Next(elementsRoute1 - 1);//index elementa za mijenjanje
                int y = random.Next(elementsRoute2 - 1);//index elementa za mijenjanje
                int tempStation1 = solution.BusTours.ElementAt(randomRouteIndex1).ElementAt(x);
                int tempStation2 = solution.BusTours.ElementAt(randomRouteIndex1).ElementAt(x + 1);

                for (int j = 0; j < elementsRoute1; j++)
                {
                    if (j == x)
                    {
                        route1.Add(solution.BusTours.ElementAt(randomRouteIndex2).ElementAt(y));
                    }
                    else if (j == (x + 1))
                    {
                        route1.Add(solution.BusTours.ElementAt(randomRouteIndex2).ElementAt(y + 1));
                    }
                    else
                    {
                        route1.Add(solution.BusTours.ElementAt(randomRouteIndex1).ElementAt(j));
                    }
                }

                for (int j = 0; j < elementsRoute2; j++)
                {
                    if (j == y)
                    {
                        route2.Add(solution.BusTours.ElementAt(randomRouteIndex1).ElementAt(x));
                    }
                    else if (j == (y + 1))
                    {
                        route2.Add(solution.BusTours.ElementAt(randomRouteIndex2).ElementAt(x + 1));
                    }
                    else
                    {
                        route2.Add(solution.BusTours.ElementAt(randomRouteIndex1).ElementAt(j));
                    }
                }

                solution.BusTours.RemoveAt(randomRouteIndex1);
                solution.BusTours.Insert(randomRouteIndex1, route1);
                solution.BusTours.RemoveAt(randomRouteIndex2);
                solution.BusTours.Insert(randomRouteIndex2, route2);

            }

            return solution;
        }

        private static Solution OnePointMove(Solution solution, List<Cluster> cluster, Instance instanca)
        {
            Random random = new Random();
            int randomRouteIndex = random.Next(solution.BusTours.Count()); //izaberi random rutu
            int randomBusStopIndex = random.Next(solution.BusTours.ElementAt(randomRouteIndex).Count());//izaberi random index jedne od stanica na random ruti
            while (solution.BusTours.ElementAt(randomRouteIndex).Count == 1)
            {
                if (solution.BusTours.Count(t => t.Count > 1) < 2)//ako samo jedna ima više od 1 postaje onda se može dogoditi tura sa 0 stanica...ovo nije opet najsretnije jer se algo može malo zaglaviti dok ne nađe baš taj random broj koji ima više tura
                {
                    return solution;
                }
                randomRouteIndex = random.Next(solution.BusTours.Count); //izaberi random rutu, ali ne onu koja ima jednu postaju
            }

            randomBusStopIndex = random.Next(solution.BusTours.ElementAt(randomRouteIndex).Count);
            int randomStop = solution.BusTours.ElementAt(randomRouteIndex).ElementAt(randomBusStopIndex);//odabran random ruta i u njoj random stanica
                                                                                                         //int studentsInCluster = solution.ClusterList.ElementAt(randomStop).Count(); //broj studenata na toj stanici koju mijenjamo

            int studentsInCluster = cluster.Find(t => t.StopIndex == randomStop).Count();

            int busCapacity = (int)instanca.Capacity;
            int studentsOnRoute = 0;

            foreach (List<int> route in solution.BusTours)
            {
                if (!route.Equals(solution.BusTours.ElementAt(randomRouteIndex)))
                {
                    foreach (int busStation in route)//zbrajanje ukupno studenata na svakoj ruti
                    {
                        studentsOnRoute += cluster.Find(t => t.StopIndex == busStation).Count();
                    }
                    if (studentsOnRoute + studentsInCluster < busCapacity)
                    {
                        route.Add(randomStop);//dodanj stanicu na kraj rute
                        solution.BusTours.ElementAt(randomRouteIndex).RemoveAt(randomBusStopIndex);//makni stanicu iz rute 
                        break;
                    }
                    studentsOnRoute = 0;
                }
            }
            return solution;
        }

        private static Solution TwoPointMove(Solution solution, List<Cluster> cluster)
        {
            Random random = new Random();
            int randomRouteIndex = random.Next(solution.BusTours.Count()); //izaberi random rutu
            int randomBusStopIndex1 = random.Next(solution.BusTours.ElementAt(randomRouteIndex).Count()); //izaberi prvu stanicu na random ruti
            int randomBusStopIndex2 = random.Next(solution.BusTours.ElementAt(randomRouteIndex).Count()); //izaberi drugu stanicu na random ruti

            int randomStation1 = solution.BusTours.ElementAt(randomRouteIndex).ElementAt(randomBusStopIndex1);
            int randomStation2 = solution.BusTours.ElementAt(randomRouteIndex).ElementAt(randomBusStopIndex2);

            solution.BusTours.ElementAt(randomRouteIndex).RemoveAt(randomBusStopIndex1);
            solution.BusTours.ElementAt(randomRouteIndex).Insert(randomBusStopIndex1, randomStation2);
            solution.BusTours.ElementAt(randomRouteIndex).RemoveAt(randomBusStopIndex2);
            solution.BusTours.ElementAt(randomRouteIndex).Insert(randomBusStopIndex2, randomStation1);

            return solution;
        }

        private static Solution TwoOptMove(Solution solution, List<Cluster> cluster)
        {
            Random random = new Random();
            int randomRouteIndex = random.Next(solution.BusTours.Count()); //izaberi random rutu
            int stationsInRoute = solution.BusTours.ElementAt(randomRouteIndex).Count();
            int randomBusStopIndex1 = random.Next(stationsInRoute); //izaberi prvu random stanicu na random ruti
            int randomBusStopIndex2 = random.Next(stationsInRoute); //izaberi drugu random stanicu na random ruti

            if (randomBusStopIndex1 != randomBusStopIndex2)//provjera da se ne radi o istoj stanici
            {
                int temp = randomBusStopIndex1;
                randomBusStopIndex1 = min(temp, randomBusStopIndex2);
                randomBusStopIndex2 = max(temp, randomBusStopIndex2);
                int counter = randomBusStopIndex2;

                //provjeriti za manje slucajeve, kad program puca...

                List<int> route = new List<int>();

                for (int i = 0; i < stationsInRoute; i++)
                {
                    if (i >= randomBusStopIndex1 && i < randomBusStopIndex2)
                    {
                        route.Add(solution.BusTours.ElementAt(randomRouteIndex).ElementAt(counter));
                        counter--;
                    }
                    else
                    {
                        route.Add(solution.BusTours.ElementAt(randomRouteIndex).ElementAt(i));
                    }
                }

                solution.BusTours.RemoveAt(randomRouteIndex);
                solution.BusTours.Insert(randomRouteIndex, route);
            }

            return solution;
        }

        public static double CostFunction(Solution temporarySolution, Instance instanca)
        {
            int alpha = 10000;
            int beta = 1;
            double totalSolutionDistance = 0;
            Point schoolCoordinates = instanca.SchoolCoordinates;
            Point previousPoint = new Point(0, 0);
            previousPoint = schoolCoordinates;

            foreach (List<int> route in temporarySolution.BusTours)
            {
                foreach (int busStation in route)
                {
                    totalSolutionDistance = totalSolutionDistance + previousPoint.EuclidianDistance(instanca.StopsCoordinates[busStation]);
                    previousPoint = instanca.StopsCoordinates[busStation];
                }
                totalSolutionDistance = totalSolutionDistance + previousPoint.EuclidianDistance(schoolCoordinates);
                previousPoint = schoolCoordinates;
            }

            return beta * totalSolutionDistance; //alpha*temporarySolution.BusTours.Count()   //racuna se broj ruta po solutionu i ukupna duljina. Naglasak je na duljini ruta
        }

        private static int max(int a, int b)
        {
            if (a > b)
                return a;
            else
                return b;
        }
        private static int min(int a, int b)
        {
            if (a < b)
                return a;
            else
                return b;
        }
        /*
        public static Solution InitialSolution(List<Cluster> resultCluster)
        {
            Solution initialSolution = new Solution();
            List<int> singleBusRoute = new List<int>();
            int leftSpaceInBus;
            //initialSolution.
            //resultCluster. 
            while (resultCluster.Count > 0)
            {
                leftSpaceInBus = 25;
                //singleBusRoute.Add(resultCluster.Where());
                //initialSolution.busTours.Add();
                
            }
            
            return initialSolution;
        }
        */
    }
}
