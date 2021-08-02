using System;
using System.Collections.Generic;

namespace Utility
{
    class uArray
    {
        public static void PrintArray(char [,] array, int space = 1)
        {
            // Printing the First Heading Line
            Console.Write("+");
            for (int j = 0; j < array.GetLength(1); j++)
            {
                Console.Write( RepeatString("-", space + 1) + RepeatString("-", space) + "+");
            }
            Console.WriteLine();
            // End of Printing

            // Printing the rest of the table
            for (int i = 0; i < array.GetLength(0); i++)
            {
                Console.Write("|");
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write( RepeatString(" ", space) + array[i, j] + RepeatString(" ", space) + "|");
                }
                Console.Write("\n" + "+");
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write( RepeatString("-", space + 1) + RepeatString("-", space) + "+");
                }                
                
                
                Console.WriteLine();
            }
            // End of Printing
        }

        public static void PrintArray( char[][] array, string sep=" ")
        {
            Console.Write("[ ");
            for (int i = 0; i < array.Length; i++)
            {
                if (i < array.Length - 1)
                    Console.Write(ToString(array[i]) + "," + sep);
                else
                    Console.Write(ToString(array[i]));
            }
            Console.WriteLine(" ]");
        }

        public static string ToString(string[] array)
        {
            string str = "[";

            for (int i = 0; i < array.Length; i++)
            {
                str += "\"" + array[i] + "\"";

                if (i < array.Length - 1)
                    str += ", ";
            }

            str += "]";

            return str;
        }

        public static void PrintArray(string[] array)
        {
            Console.WriteLine(ToString(array));
        }

        public static string ToString(char[] array)
        {
            string str = "[";

            for (int i = 0; i < array.Length; i++)
            {
                str += "'" + array[i] + "'";

                if (i < array.Length - 1)
                    str += ", ";
            }

            str += "]";

            return str;
        }

        public static void PrintArray(char[] array)
        {
            Console.WriteLine(ToString(array));
        }

        public static void PrintArray(int[] array)
        {
            string str = "[";

            for (int i = 0; i < array.Length; i++)
            {
                str += Convert.ToString(array[i]);

                if (i < array.Length - 1)
                    str += ", ";
            }

            str += "]";

            Console.WriteLine(str);
        }

        public static string RepeatString(string str, int num)
        {
            string repStr = "";

            for (int i = 0; i < num; i++)
            {
                repStr += str;
            }

            return repStr;
        }

        public static char [,] TransposeArray (char [,] array)
        {
            int rows = array.GetLength(0);
            int cols = array.GetLength(1);

            char [,] transposedArray = new char [cols, rows];
            
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    transposedArray[j, i] = array[i, j];
                }
            }
            return transposedArray;
        }

        public static char[][] MultiDimToJagged(char [,] array)
        {
            int rows = array.GetLength(0);
            int cols = array.GetLength(1);
            char[][] jagged = new char[rows][];

            for (int i = 0; i < rows; i++)
            {
                char [] row = new char[cols];
                for (int j = 0; j < cols; j++)
                {
                    row[j] = array[i, j];
                }
                jagged[i] = row;
            }
            return jagged;
        }

        public static char[,] JaggedToMultiDim(char [][] array)
        {
            int rows = array.Length;
            int cols = array[0].Length;
            char[,] multiDim = new char[rows,cols];
            
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    multiDim[i, j] = array[i][j];
                }
            }
            return multiDim;
        }

    }
}