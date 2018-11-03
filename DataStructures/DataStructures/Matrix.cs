using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    class Matrix
    {
        // Элементы матрицы
        public double[,] elements;
        //Размерность матрицы
        public int length;
       
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
        public Matrix(double[,] _elements)
        {
            length = (int)Math.Sqrt(_elements.Length);
            elements = new double[length, length];
            for (int i = 0; i < length; i++)
                for (int j = 0; j < length; j++)
                    elements[i, j] = _elements[i, j];
        }
        //Конструктор, не принимающий ничего
        public Matrix()
        {

        }
        
        public static Matrix operator +(Matrix a, Matrix b)
        {
            double[,] elements = new double[a.length, a.length];
            int i, j;
            for (i = 0; i < a.length; i++)
            {

                for (j = 0; j < a.length; j++)
                {
                    elements[i, j] = a[i, j] + b[i, j];
                }
            }
            Matrix answ = new Matrix(elements);
            return answ;
        }
        public static Matrix operator -(Matrix a, Matrix b)
        {
            double[,] elements = new double[a.length, a.length];
            int i, j;
            for (i = 0; i < a.length; i++)
            {

                for (j = 0; j < a.length; j++)
                {
                    elements[i, j] = a[i, j] - b[i, j];
                }
            }
            Matrix answ = new Matrix(elements);
            return answ;
        }
        public static Matrix operator -(Matrix m)
        {
            double[,] elements = new double[m.length, m.length];
            int i, j;
            for (i = 0; i < m.length; i++)
            {

                for (j = 0; j < m.length; j++)
                {
                    elements[i, j] = -m[i, j];
                }
            }
            Matrix answ = new Matrix(elements);
            return answ;
        }
        public static Matrix operator *(double alfa, Matrix m)
        {
            double[,] elements = new double[m.length, m.length];
            int i, j;
            for (i = 0; i < m.length; i++)
            {
                for (j = 0; j < m.length; j++)
                {
                    elements[i, j] = alfa * m[i, j];
                }
            }
            Matrix answ = new Matrix(elements);
            return answ;
        }
        public static Matrix operator *(Matrix m, double alfa)
        {
            double[,] elements = new double[m.length, m.length];
            int i, j;
            for (i = 0; i < m.length; i++)
            {
                for (j = 0; j < m.length; j++)
                {
                    elements[i, j] = alfa * m[i, j];
                }
            }
            Matrix answ = new Matrix(elements);
            return answ;
        }
        public static Matrix operator /(Matrix m, double alfa)
        {
            double[,] elements = new double[m.length, m.length];
            int i, j;
            for (i = 0; i < m.length; i++)
            {
                for (j = 0; j < m.length; j++)
                {
                    elements[i, j] = m[i, j] / alfa;
                }
            }
            Matrix answ = new Matrix(elements);
            return answ;
        }
        public static Matrix operator *(Matrix a, Matrix b)
        {
            double[,] elements = new double[a.length, a.length];
            int i, j;
            for (i = 0; i < a.length; i++)
            {

                for (j = 0; j < a.length; j++)
                {
                    elements[i, j] = 0;
                    for (int k = 0; k < a.length; k++)
                    {
                        elements[i, j] += a[i, k] * b[k, j];
                    }
                }
            }
            Matrix answ = new Matrix(elements);
            return answ;
        }
        //Транспонированная матрица
        public static Matrix transp(Matrix m)
        {
            double[,] elements = new double[m.length, m.length];
            int i, j;
            for (i = 0; i < m.length; i++)
            {
                for (j = 0; j < m.length; j++)
                {
                    elements[i, j] = m[j, i];
                }
            }
            Matrix answ = new Matrix(elements);
            return answ;
        }
        public Matrix Transp
        {
            get
            {
                return transp(this);
            }
        }
        //Минор при заданных столбце и строчке
        public static Matrix minor(Matrix matr, int a, int b)
        {
            int m = matr.length;
            double[,] answ = new double[m - 1, m - 1];
            int di = 0, dj;
            for (int i = 0; i < m - 1; i++)
            {
                if (i == a)
                    di = 1;
                dj = 0;
                for (int j = 0; j < m - 1; j++)
                {
                    if (j == b)
                        dj = 1;
                    answ[i, j] = matr[i + di, j + dj];
                }
            }
            return new Matrix(answ);
        }
        public Matrix Minor(int a, int b)
        {
            return minor(this, a, b);
        }
        //Определитель матрицы
        public static double det(Matrix matr)
        {
            int m = matr.length;
            double[,] p = new double[m, m];
            double answ = 0;
            int k = 1;
            int n = m - 1;
            if (m == 1)
            {
                answ = matr[0, 0];
                return answ;
            }
            if (m == 2)
            {
                answ = matr[0, 0] * matr[1, 1] - matr[1, 0] * matr[0, 1];
            }
            if (m > 2)
            {
                for (int i = 0; i < m; i++)
                {
                    Console.WriteLine(i);
                    Matrix minor = matr.Minor(i, 0);
                    //Console.WriteLine(matr[i,0]);
                    //minor.output();
                    answ += k * matr[i, 0] * det(minor);

                    k = -k;
                }
            }
            return answ;
        }
        public double Det
        {
            get
            {
                return det(this);
            }
        }
        //Союзная матрица
        public static Matrix alliedMatrix(Matrix matr)
        {

            int m = matr.length;
            double[,] answ = new double[m, m];
            for (int i = 0; i < m; i++)
                for (int j = 0; j < m; j++)
                    answ[i, j] = Math.Pow(-1, i + j) * matr.Minor(i, j).Det;
            return new Matrix(answ);
        }
        public Matrix AlliedMatrix
        {
            get
            {
                return alliedMatrix(this);
            }
        }
        //Обратная матрица
        public static Matrix inverseMatrix(Matrix matr)
        {
            if (matr.Det == 0)
                Console.WriteLine("нет");
            Matrix answ = matr.AlliedMatrix.Transp / matr.Det;
            return answ;
        }
        public Matrix InverseMatrix
        {
            get { return inverseMatrix(this); }
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
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
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
