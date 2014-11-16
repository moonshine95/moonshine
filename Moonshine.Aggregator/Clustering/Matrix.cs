using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moonshine.Aggregator.Clustering
{
    public class Matrix
    {
        private int _row;
        private int _column;
        private double[,] _matrix;

        public Matrix(double[,] matrix)
        {
            _matrix = matrix;
            _row = matrix.GetLength(0);
            _column = matrix.GetLength(1);
        }
    }
}
