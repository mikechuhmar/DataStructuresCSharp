using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class Graf
    {
        public SquareMatrix WeightMatrix { get; set; }
        public int NodesCount
        {
            get
            {
                return WeightMatrix.RowsCount;
            }
        }
        public Graf(double [,] elements)
        {
            double[,] new_elements = new double[elements.GetLength(0), elements.GetLength(1)];
            for (int i = 0; i < elements.GetLength(0); i++)
            {
                for (int j = 0; j < elements.GetLength(1); j++)
                {
                    if(i == j)
                        new_elements[i, j] = 0;
                    else
                    {
                        if (elements[i, j] < 0)
                            new_elements[i, j] = -1;
                        else
                            new_elements[i, j] = elements[i, j];
                    }
                    
                }
            }
            WeightMatrix = new SquareMatrix(new_elements);
        }
        public Graf(SquareMatrix squareMatrix)
        {
            //Graf(squareMatrix.elements);
            //WeightMatrix = new SquareMatrix(squareMatrix.elements);
            double[,] elements = squareMatrix.elements;
            double[,] new_elements = new double[elements.GetLength(0), elements.GetLength(1)];
            for (int i = 0; i < elements.GetLength(0); i++)
            {
                for (int j = 0; j < elements.GetLength(1); j++)
                {
                    if (i == j)
                        new_elements[i, j] = 0;
                    else
                    {
                        if (elements[i, j] < 0)
                            new_elements[i, j] = -1;
                        else
                            new_elements[i, j] = elements[i, j];
                    }

                }
            }
            WeightMatrix = new SquareMatrix(new_elements);
        }
        protected string OutEdge(int index1, int index2)
        {
            string leters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return leters[index1].ToString() + leters[index2].ToString();

            
        }
        public static string OutVertex(int index)
        {
            string leters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return leters[index].ToString();


        }
        
    }
}
