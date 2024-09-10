using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASDLab01
{
    internal class Metric
    {
        public static void Main(string[] args)
        {
            double[] vector = FileReader.ReadOneDimensionalArray("../../Data/vector.txt", double.Parse);
            double[,] tensor = FileReader.ReadTwoDimensionalArray("../../Data/tensor.txt", ' ', double.Parse);
            uint size = FileReader.ReadSingleValueFromFile("../../Data/size.txt", UInt32.Parse);

            Console.WriteLine("Vector length: " + Math.Sqrt(DotProduct(tensor, vector, size)));
            Console.ReadKey();
        }

        static bool IsSymmetric<T>(T[,] matrix)
        {
            int N = matrix.GetLength(0);
            for (int i = 0; i < N; i++)
            {
                for (int j = i + 1; j < N; j++)
                {
                    if (!matrix[i, j].Equals(matrix[j, i]))
                        return false;
                }
            }
            return true;

        }

        static double DotProduct(double[,] tensor, double[] vector, uint size)
        {
            if (size != vector.Length || size * size != tensor.Length) throw new InvalidOperationException("The dimensions do not match");

            if (!IsSymmetric(tensor)) throw new InvalidOperationException("The tensor matrix is not symmetrical.");

            int N = tensor.GetLength(0);
            double result = 0;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    result += vector[i] * tensor[i, j] * vector[j];
                }
            }
            return result;
        }

    }
}
