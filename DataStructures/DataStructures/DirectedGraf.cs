using System;
using System.Collections.Generic;
using System.Linq;

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
        int NeighborIndex(int index, List<int> addedIndexes)
        {
            double min = double.MaxValue;
            int indexOfMin = int.MaxValue;
            for (int i = 0; i < NodesCount; i++)
            {
                if (i != index && !addedIndexes.Contains(i) /*&& WeightMatrix[index, i] < min*/ && WeightMatrix[index, i] > 0 && indexOfMin == int.MaxValue)
                {
                    min = WeightMatrix[index, i];
                    indexOfMin = i;
                }
            }
            return indexOfMin;
        }
        public List<int> FindWay(int index)
        {


            List<int> indexes = new List<int>();
            indexes.Add(index);
            int currentIndex = index;
            while (indexes.Count < NodesCount)
            {
                currentIndex = NeighborIndex(currentIndex, indexes);
                if (currentIndex == int.MaxValue)
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

            } while (LengthCycle(resultCycle) == 0);
            
            return resultCycle;
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
                //result = FindWay(i);
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
            return result;

        }

        double AntWayProbability(int index1, int index2, List<int> tabuListIndexes, List<List<double>> intensityList, double alfa, double beta)
        {
            if (WeightMatrix[index1, index2] <= 0 || tabuListIndexes.Contains(index2))
                return -1;
            else
            {
                double sum = 0;
                for (int j = 0; j < NodesCount; j++)
                {
                    if (!tabuListIndexes.Contains(j) && WeightMatrix[index1, j] > 0)
                        sum += Math.Pow(WeightMatrix[index1, j], alfa) / Math.Pow(intensityList[index1][j], beta);
                }
                return Math.Pow(WeightMatrix[index1, index2], alfa) / Math.Pow(intensityList[index1][index2], beta) / sum;
            }
        }
        int AntNeighbor(int index, List<int> tabuListIndexes, List<List<double>> intensityList, double alfa, double beta)
        {
            Random random = new Random();
            List<double> P_list = new List<double>();
            List<Probability> probabilities = new List<Probability>();
            List<Interval> intervals = new List<Interval>();
            int result = int.MaxValue;
            for(int i = 0; i < NodesCount; i++)
            {
                double P = AntWayProbability(index, i, tabuListIndexes, intensityList, alfa, beta);
                if (P != -1)
                    probabilities.Add(new Probability(i, P));
            }
            if (probabilities.Count == 0)
                return result;
            intervals.Add(new Interval(probabilities[0].index, 0, probabilities[0].P));
            if (probabilities.Count>1)
            {
                for (int i = 1; i < probabilities.Count; i++)
                {
                    Interval interval = new Interval(probabilities[i].index, intervals[i - 1].b, intervals[i - 1].b + probabilities[i].P);
                    intervals.Add(interval);
                }
            }
            double randP = random.NextDouble();
            for (int i = 0; i < intervals.Count && result == int.MaxValue; i++)
            {
                if (intervals[i].Contains(randP))
                {
                    result = intervals[i].index;
                }
            }
            return result;
        }
        List<int> AntWay(int index, List<List<double>> intensityList, double alfa, double beta)
        {
            int currentIndex = index;
            List<int> tabuListIndexes = new List<int>();
            tabuListIndexes.Add(currentIndex);
            while (tabuListIndexes.Count < NodesCount)
            {
                currentIndex = AntNeighbor(currentIndex, tabuListIndexes, intensityList, alfa, beta);
                if (currentIndex == int.MaxValue)
                    return null;
                tabuListIndexes.Add(currentIndex);
            }
            if (WeightMatrix[currentIndex, index] > 0)
                tabuListIndexes.Add(index);
            else
                return null;
            return tabuListIndexes;
            

        }
        public List<int> AntAlgorithm(int N, double alfa, double beta)
        {
            Random random = new Random();
            List<int> currentWay = null, result = null;
            List<int> tabuListIndexes = new List<int>();
            List<List<double>> intensityList = new List<List<double>>();
            double /*alfa = 0.5, beta = 0.05, */e = 0.05, k = 0.5;
            for (int i = 0; i < NodesCount; i++)
            {
                intensityList.Add(new List<double>());
                for (int j = 0; j < NodesCount; j++)
                {
                    if (WeightMatrix[i, j] > 0)
                        intensityList[i].Add(1);
                    else
                        intensityList[i].Add(-1);
                }
            }
            for (int i = 0; i < NodesCount && result == null; i++)
            {
                result = FindWay(i);
            }
            if (result == null)
                return result;
            Console.WriteLine(Cycle(result));

            for (int i = 0; i < result.Count - 1; i++)
            {
                int index1 = result[i];
                int index2 = result[i + 1];
                intensityList[index1][index2] = (1 - e) * intensityList[index1][index2] + k / WeightMatrix[index1, index2];
            }
            for (int i = 1; i < N; i++)
            {
                int index = random.Next(0, NodesCount);
                Console.WriteLine(index.ToString());
                currentWay = AntWay(index, intensityList, alfa, beta);
                if (currentWay != null)
                {
                    if (LengthCycle(currentWay) < LengthCycle(result))
                        result = currentWay;
                }
                Console.WriteLine(Cycle(result));
            }

            

            return result;

        }
    }
}
