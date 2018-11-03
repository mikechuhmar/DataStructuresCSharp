using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class Matrix
    {
        // Элементы матрицы
        public double[,] elements;
        //Размерность матрицы
        //public int length;
        public int ColumnsCount
        {
            get
            {
                return elements.GetLength(0);
            }
        }
        public int RowsCount
        {
            get
            {
                return elements.GetLength(1);
            }
        }
        
        //Геттер и сеттер для элементов матрицы
        public double this[int i, int j]
        {
            get
            {
                return elements[i, j];
            }
            set
            {
                elements[i, j] = value;
            }
        }
        //Конструктор, принимающий двумерный массив элементов
        public Matrix(double[,] elements)
        {
            
            this.elements = new double[elements.GetLength(0), elements.GetLength(1)];
            for (int i = 0; i < elements.GetLength(0); i++)
                for (int j = 0; j < elements.GetLength(1); j++)
                    this.elements[i, j] = elements[i, j];
        }
        //Конструктор, не принимающий ничего
        public Matrix()
        {

        }
        public bool IsSameDimension(Matrix matrix)
        {
            return (RowsCount == matrix.RowsCount && ColumnsCount == ColumnsCount);
        }
        public static Matrix operator +(Matrix a, Matrix b)
        {
            
            Matrix answ;
            if (a.IsSameDimension(b))
            {
                double[,] elements = new double[a.RowsCount, a.ColumnsCount];
                int i, j;
                for (i = 0; i < a.RowsCount; i++)
                {

                    for (j = 0; j < a.ColumnsCount; j++)
                    {
                        elements[i, j] = a[i, j] + b[i, j];
                    }
                }
                answ = new Matrix(elements);
            }
            else
                answ = null;
            return answ;

        }

        public static Matrix operator -(Matrix a, Matrix b)
        {
          
            Matrix answ;
            if (a.IsSameDimension(b))
            {
                double[,] elements = new double[a.RowsCount, a.ColumnsCount];
                int i, j;
                for (i = 0; i < a.RowsCount; i++)
                {

                    for (j = 0; j < a.ColumnsCount; j++)
                    {
                        elements[i, j] = a[i, j] - b[i, j];
                    }
                }
                answ = new Matrix(elements);
            }
            else
                answ = null;
            return answ;
        }
        public static Matrix operator -(Matrix m)
        {
            double[,] elements = new double[m.RowsCount, m.ColumnsCount];
            int i, j;
            for (i = 0; i < m.RowsCount; i++)
            {

                for (j = 0; j < m.ColumnsCount; j++)
                {
                    elements[i, j] = -m[i, j];
                }
            }
            Matrix answ = new Matrix(elements);
            return answ;
        }
        public static Matrix operator *(double alfa, Matrix m)
        {
            double[,] elements = new double[m.RowsCount, m.ColumnsCount];
            int i, j;
            for (i = 0; i < m.RowsCount; i++)
            {
                for (j = 0; j < m.ColumnsCount; j++)
                {
                    elements[i, j] = alfa * m[i, j];
                }
            }
            Matrix answ = new Matrix(elements);
            return answ;
        }
        public static Matrix operator *(Matrix m, double alfa)
        {
            double[,] elements = new double[m.RowsCount, m.ColumnsCount];
            int i, j;
            for (i = 0; i < m.RowsCount; i++)
            {
                for (j = 0; j < m.ColumnsCount; j++)
                {
                    elements[i, j] = alfa * m[i, j];
                }
            }
            Matrix answ = new Matrix(elements);
            return answ;
        }
        public static Matrix operator /(Matrix m, double alfa)
        {
            double[,] elements = new double[m.RowsCount, m.ColumnsCount];
            int i, j;
            for (i = 0; i < m.RowsCount; i++)
            {
                for (j = 0; j < m.ColumnsCount; j++)
                {
                    elements[i, j] = m[i, j] / alfa;
                }
            }
            Matrix answ = new Matrix(elements);
            return answ;
        }
        public static Matrix operator *(Matrix a, Matrix b)
        {
            Matrix answ;
            if (a.IsSameDimension(b))
            {
                double[,] elements = new double[a.RowsCount, a.ColumnsCount];
                int i, j;
                for (i = 0; i < a.RowsCount; i++)
                {

                    for (j = 0; j < a.ColumnsCount; j++)
                    {
                        elements[i, j] = a[i, j] * b[i, j];
                    }
                }
                answ = new Matrix(elements);
            }
            else
                answ = null;
            return answ;
        }
        //Транспонированная матрица
        public static Matrix transp(Matrix m)
        {
            double[,] elements = new double[m.RowsCount, m.ColumnsCount];
            int i, j;
            for (i = 0; i < m.RowsCount; i++)
            {
                for (j = 0; j < m.ColumnsCount; j++)
                {
                    elements[i, j] = m[j, i];
                }
            }
            Matrix answ = new Matrix(elements);
            return answ;
        }
        public virtual Matrix Transp
        {
            get
            {
                return transp(this);
            }
        }
        
        
        
        /*public static Matrix inverseMatrixGJ(Matrix matr)
        {
            if (matr.Det == 0)
                Console.WriteLine("нет");
            Matrix answ = Matrix.E(matr.length);
            

        }*/
        //Вывод в консоль
        public void output()
        {
            for (int i = 0; i < RowsCount; i++)
            {
                for (int j = 0; j < ColumnsCount; j++)
                {
                    Console.Write("{0}  ", elements[i, j]);
                }
                Console.Write("\n");
            }
        }
        //Единичная матрица
        public static Matrix E(int length)
        {
            double[,] el = new double[length, length];
            for (int i = 0; i < length; i++)
                for (int j = 0; j < length; j++)
                {
                    if (i == j)
                        el[i, j] = 1;
                    else
                        el[i, j] = 0;
                }
            return new Matrix(el);
        }
        
    }
}
