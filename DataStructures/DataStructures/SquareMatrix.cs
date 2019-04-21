using System;

namespace DataStructures
{
    public class SquareMatrix: Matrix
    {
        //Конструктор, принимающий двумерный массив элементов
        public SquareMatrix(double[,] elements): base(elements)
        {
            
        }
        //Конструктор, не принимающий ничего
        public SquareMatrix(): base()
        {

        }
        public static SquareMatrix operator /(SquareMatrix m, double alfa)
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
            SquareMatrix answ = new SquareMatrix(elements);
            return answ;
        }
        //Минор при заданных столбце и строчке
        public static SquareMatrix minor(Matrix matr, int a, int b)
        {
            int m = matr.RowsCount;
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
            return new SquareMatrix(answ);
        }
        public SquareMatrix Minor(int a, int b)
        {
            return minor(this, a, b);
        }
        //Определитель матрицы
        public static double det(SquareMatrix matr)
        {
            int m = matr.RowsCount;
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
                    SquareMatrix minor = matr.Minor(i, 0);
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
        public static SquareMatrix alliedMatrix(SquareMatrix matr)
        {

            int m = matr.RowsCount;
            double[,] answ = new double[m, m];
            for (int i = 0; i < m; i++)
                for (int j = 0; j < m; j++)
                    answ[i, j] = Math.Pow(-1, i + j) * matr.Minor(i, j).Det;
            return new SquareMatrix(answ);
        }
        public SquareMatrix AlliedMatrix
        {
            get
            {
                return alliedMatrix(this);
            }
        }
        public static SquareMatrix transp(SquareMatrix m)
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
            SquareMatrix answ = new SquareMatrix(elements);
            return answ;
        }
        public new SquareMatrix Transp
        {
            get
            {
                return transp(this);
            }
        }

        //Обратная матрица
        public static SquareMatrix inverseMatrix(SquareMatrix matr)
        {
            if (matr.Det == 0)
            {
                Console.WriteLine("нет");
            }

            SquareMatrix answ = matr.AlliedMatrix.Transp / matr.Det;
            return answ;
        }
        public Matrix InverseMatrix
        {
            get { return inverseMatrix(this); }
        }
    }
}
