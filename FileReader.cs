using System;
using System.IO;
using System.Linq;

public static class FileReader
{
    public static T ReadSingleValueFromFile<T>(string filePath, Func<string, T> parser)
    {
        string line = File.ReadLines(filePath).FirstOrDefault();
        return parser(line);
    }

    public static T[] ReadOneDimensionalArray<T>(string filePath, Func<string, T> parser)
    {
        string[] lines = File.ReadAllLines(filePath);
        return lines.Select(parser).ToArray();
    }

    public static T[,] ReadTwoDimensionalArray<T>(string filePath, char separator, Func<string, T> parser)
    {
        string[] lines = File.ReadAllLines(filePath);

        int rows = lines.Length;
        int cols = lines[0].Split(separator).Length;

        T[,] array = new T[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            string[] values = lines[i].Split(separator);
            for (int j = 0; j < cols; j++)
            {
                array[i, j] = parser(values[j]);
            }
        }

        return array;
    }
}