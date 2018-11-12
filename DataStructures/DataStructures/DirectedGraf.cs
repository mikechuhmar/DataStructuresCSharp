using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class DirectedGraf : Graf
    {
        public DirectedGraf(double[,] elements) : base(elements)
        {
        }
        public DirectedGraf(SquareMatrix squareMatrix): base(squareMatrix)
        {

        }

        static Random random = new Random(DateTime.Now.Millisecond);

        public static string Cycle(List<int> indexes)
        {
            string s = "";
            if (indexes == null)
                s = "Нет гамильтонова цикла";


            else
            {
                foreach (var index in indexes)
                {
                    s += OutVertex(index);
                }
            }
            return s;
        }
        bool IsCycle(List<int> cycle)
        {
            for (int i = 0; i < NodesCount; i++)
                if (WeightMatrix[cycle[i], cycle[i + 1]] <= 0)
                    return false;
            return true;
        }
        public double LengthCycle(List<int> indexes)
        {
            double length = 0;
            if (indexes == null || indexes.Count < WeightMatrix.ColumnsCount + 1)
                length = 0;
            else
            {
                for (int i = 0; i < indexes.Count - 1; i++)
                {
                    if (WeightMatrix[indexes[i], indexes[i + 1]] < 0)

                    {
                        length = 0;
                        break;
                    }
                    else
                        length += WeightMatrix[indexes[i], indexes[i + 1]];
                }
            }
            return length;
        }

        public List<int> FindCycle(int startVertex)
        {


            List<int> result = new List<int>();
            result.Add(startVertex);
            int currentVertex = startVertex, nextVertex = -1;
            for (int i = 1; i < NodesCount; i++)
            {
                for (int j = 0; j < NodesCount; j++)
                    if (WeightMatrix[currentVertex, j] > 0 && !result.Contains(j) && nextVertex < 0)
                    {
                        nextVertex = j;
                    }
                if (nextVertex >= 0)
                {
                    currentVertex = nextVertex;
                    result.Add(currentVertex);
                    nextVertex = -1;
                }
                else
                {
                    return null;
                }
            }

            if (WeightMatrix[currentVertex, startVertex] > 0)   //Если есть путь из конца к началу (цикл)
            {
                result.Add(startVertex);         //Добавляем стартовую точку
                return result;
            }
            else
            {
                return null;    //Иначе - не цикл
            }
        }
        //Индекс ближайшего соседа
        int NearestNeighborIndex(int index, List<int> addedIndexes)
        {
            double min = double.MaxValue;
            int indexOfMin = int.MaxValue;
            for(int i = 0; i < NodesCount; i++)
            {
                if(i != index && !addedIndexes.Contains(i) && WeightMatrix[index, i] < min && WeightMatrix[index, i] > 0)
                {
                    min = WeightMatrix[index, i];
                    indexOfMin = i;
                }
            }
            return indexOfMin;
        }
        //Алгоритм ближайшего соседа для стартовой точки
        public List<int> NearestNeighborAlgorithm(int index)
        {

            
            List<int> indexes = new List<int>();
            indexes.Add(index);
            int currentIndex = index;
            while (indexes.Count < NodesCount)
            {
                currentIndex = NearestNeighborIndex(currentIndex, indexes);
                if(currentIndex == int.MaxValue)
                    return null;
                indexes.Add(currentIndex);


            }
            if (indexes.Count == NodesCount && WeightMatrix[currentIndex, index] > 0)
            {
                indexes.Add(index);
                
            }
            else
            {
                return null;
            }
            
            return indexes;
        }
        //Алгоритм ближайшего соседа
        public List<int> NearestNeighborAlgorithm()
        {
            
            List<List<int>> allIndexes = new List<List<int>>();
            List<double> listLength = new List<double>();
            for(int i = 0; i < NodesCount; i++)
            {
                List<int> indexes = NearestNeighborAlgorithm(i);
                if (indexes != null)
                {
                    allIndexes.Add(indexes);
                    listLength.Add(LengthCycle(indexes));
                }
            }
            List<int> result;
            if (allIndexes.Count == 0)
                result = null;
            else
            {
                double minLength = listLength.Min();
                result = allIndexes.First(x => LengthCycle(x) == minLength);
            }
            return result;

        }
        public List<int> NewWayAnnealing(List<int> prevCycle)
        {
            List<int> resultCycle = new List<int>(prevCycle.Count);

            int rnd1, rnd2;

            for (int i = 0; i < prevCycle.Count; i++)
            {
                resultCycle.Add(prevCycle[i]);
            }
            do
            {
                //Console.WriteLine("Find");
                do
                {
                    rnd1 = random.Next(0, prevCycle.Count - 1);
                    rnd2 = random.Next(0, prevCycle.Count - 1);
                } while (prevCycle[rnd1] == prevCycle[rnd2]);

                for (int i = 0; i < prevCycle.Count; i++)
                {
                    resultCycle[i] = prevCycle[i];
                }

                resultCycle[rnd1] = prevCycle[rnd2];
                resultCycle[rnd2] = prevCycle[rnd1];


                resultCycle[prevCycle.Count - 1] = resultCycle[0];
                string s = "";
                foreach (var v in resultCycle)
                    s += v.ToString();
                //Console.WriteLine(s);

            } while (LengthCycle(resultCycle) == 0);
            
            return resultCycle;
            //if (EnergyHaving(result))
            //    return result;
            //else
            //    return null;
        }
        bool canTransit(double d, double T)
        {
            double P = 100 * Math.Pow(Math.E, -Math.Abs(d) / T);
            double rand = random.Next(0, 100);
            return rand < P;
        }
        public List<int> AnneallingSimullationAlgorithm()
        {
            double Temperature = 100;
            double minTemperature = 1;
            double alfa = 0.95;
            List<List<int>> allIndexes = new List<List<int>>();
            List<int> currentWay = null, result = null;
            List<double> listLength = new List<double>();
            double energy;
            for (int i = 0; i < NodesCount && result == null; i++)
            {
                result = NearestNeighborAlgorithm(i);
            }
            if(result != null)
            {
                energy = LengthCycle(result);
            }
            else
            {
                return result;
            }

            do
            {
                currentWay = NewWayAnnealing(result);
                double currentEnergy = LengthCycle(currentWay);
                if (currentEnergy < energy || canTransit(energy - currentEnergy, Temperature))
                {
                    result = currentWay;
                    energy = currentEnergy;
                }
                Temperature *= alfa;
                Console.WriteLine(Temperature.ToString());
            } while (Temperature > minTemperature);
            //result = NewWayAnnealing(result);

            return result;

        }
        
    }
}
