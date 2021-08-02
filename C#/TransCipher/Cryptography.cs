using System;
using System.Collections.Generic;
using Utility;


namespace Cryptography
{
    class Encrypt
    {
        public static string Trans(string plaintext, string key, bool removeWhitespace=true)
        {
            if (removeWhitespace)
                plaintext = plaintext.Replace(" ", "");
            
            int cols = key.Length;
            int rows = plaintext.Length / cols;
            rows = (plaintext.Length % cols == 0) ? rows : rows + 1;

            // Distributing the characters of the plaintext into the table
            char [,] table = CryptUtil.ToTable(plaintext, rows, cols);
            
            // Transposing the table
            char [,] transposedTable = CryptUtil.TransposeArray(table);

            // Reading Index from the key
            int[] indices = CryptUtil.Map(key);

            // Converting from Multidim-Array to Jagged Array
            char[][] jtable = CryptUtil.JaggedArray(transposedTable);

            string ciphertext = "";
            foreach (int index in indices)
            {
                string text = new string(jtable[index]);

                if (text[text.Length - 1] == '*')
                    text = text[0..(text.Length - 1)];
                ciphertext += text;
            }

            return ciphertext;
        }

        public static string Trans(string plaintext, int key)
        {
            string ciphertext = "";
            string distribute = "";
            string [] row_distribute;

            for (int i = 0; i < plaintext.Length; i++)
            {
                if (((i % key) == 0) && (i != 0))
                {
                    distribute += "\n";
                }
                distribute += plaintext[i];
            }

            row_distribute = distribute.Split("\n");
            
            for (int i = 0; i < row_distribute[0].Length; i++)
            {
                distribute = "";

                for (int j = 0; j <= row_distribute.Length; j++)
                {
                    try
                    {
                        distribute += row_distribute[j][i];
                    }
                    catch
                    {
                        break;
                    }
                }

                ciphertext += distribute;
            }

            return ciphertext;
        }

        public static string ShiftChipher( string plaintext, int key)
        {
            string ciphertext = "";
            int ord, newOrd;

            for (int i = 0; i < plaintext.Length; i++)
            {
                ord = (int)plaintext[i];

                if (ord >= 48 && ord <=57)
                    newOrd = 48 + ((ord - 48 + key) % 10);

                else if (ord >= 65 && ord <= 90)
                    newOrd = 65 + ((ord - 65 + key) % 26);

                else if (ord >= 97 && ord <= 122)
                    newOrd = 97 + ((ord - 97 + key) % 26);

                else
                    newOrd = ord;

                ciphertext += (char)newOrd;
            }

            return ciphertext;
        }

        public static string Ceaser(string plaintext)
        {
            return ShiftChipher(plaintext, 3);
        }
    }

    class Decrypt
    {
        public static string Trans(string ciphertext, int key)
        {
            string plaintext = "";
            int evenlyPerColumn = ciphertext.Length / key;
            int rest = ciphertext.Length % key;

            int [] columns = new int[key];

            for (int i = 0; i < key; i++)
            {
                columns[i] = evenlyPerColumn;
            }

            for (int i = 0; i < rest; i++)
            {
                columns[i] = ++columns[i];
            }


            string[] columnDistribute = new string[key];

            int start = 0;
            int stop = 0;
            int k = 0;
            foreach (int perColumn in columns)
            {
                stop += perColumn;
                columnDistribute[k] = ciphertext[start..stop];
                start += perColumn;
                k++;
            }

            
            for (int i = 0; i < columnDistribute[0].Length; i++)
            {
                string distribute = "";

                for (int j = 0; j < columnDistribute.Length; j++)
                {
                    try
                    {
                        distribute += columnDistribute[j][i];
                    }
                    catch
                    {
                        break;
                    }
                }

                plaintext += distribute;
            }

            return plaintext;
        }

        public static string Trans(string ciphertext, string key, bool removeWhitespace=true)
        {
            if (removeWhitespace)
                ciphertext = ciphertext.Replace(" ", "");
            int cols = key.Length;
            int rows = ciphertext.Length / cols;
            rows = (ciphertext.Length % cols == 0) ? rows : rows + 1;

            // Calculating each character per column

            int [] charPerColumn = new int [cols];

            int perColumn = ciphertext.Length / cols;

            for (int i = 0; i < cols; i++)
            {
                charPerColumn[i] = perColumn;
            }

            if (rows > perColumn)
            {
                for (int i = 0; i < (ciphertext.Length - (perColumn * cols)); i++)
                {
                    charPerColumn[i] = ++charPerColumn[i];
                }
            }

            int[] indices = CryptUtil.Map(key);

            // Reading the characters per columns from the ciphertext into table

            int start = 0;
            char [,] table = new char[rows, cols];
            for (int i = 0; i < cols; i++)
            {
                int col = indices[i];
                int perCol = charPerColumn[col];

                string text = ciphertext.Substring(start, perCol);
                for (int j = 0; j < perCol; j++)
                {
                    table[j, col] = text[j];
                }

                start += perCol;
            }

            // Reading the plaintext from the table

            string plaintext = "";
            char[][] jtable = CryptUtil.JaggedArray(table);
            for(int i = 0; i < jtable.Length; i++)
            {
                string text = new string(jtable[i]);
                plaintext += text;
            }

            // Removing extra whitespaces after the plain text
            if (rows > perColumn)
            {
                int rem = (rows * cols) - ciphertext.Length;
                plaintext = plaintext[0.. (plaintext.Length - rem)];
            }

            return plaintext;
        }

        public static string ShiftChipher( string ciphertext, int key)
        {
            string plaintext = "";
            int index, newIndex, ord, newOrd;

            for (int i = 0; i < ciphertext.Length; i++)
            {
                ord = (int)ciphertext[i];

                if (ord >= 48 && ord <=57)
                {
                    index = ord - 48;
                    newIndex = (index - key) % 10; 
                    newIndex = (newIndex < 0) ? newIndex + 10 : newIndex;
                    newOrd = 48 + newIndex;
                }
                else if (ord >= 65 && ord <= 90)
                {
                    index = ord - 65;
                    newIndex = (index - key) % 26;
                    newIndex = (newIndex < 0) ? newIndex + 26 : newIndex;
                    newOrd = 65 + newIndex;
                }
                else if (ord >= 97 && ord <= 122){
                    index = ord - 97;
                    newIndex = (index - key) % 26;
                    newIndex = (newIndex < 0) ? newIndex + 26 : newIndex;
                    newOrd = 97 + newIndex;
                }
                else
                {
                    newOrd = ord;
                }

                plaintext += (char)newOrd;
            }
            
            return plaintext;
        }

        public static string Ceaser(string ciphertext)
        {
            return ShiftChipher(ciphertext, 3);
        }

    }

    class CryptUtil
    {
        internal static char [,] ToTable(string str, int rows, int cols, char fill = '*')
        {
            char [,] table = new char [rows,cols];

            int k = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (k < str.Length)
                    {
                        table[i,j] = str[k];
                    }
                    else
                    {
                        table[i,j] = fill;
                    }
                    k++;                  
                }
            }
            return table;
        }

        internal static char[][] TransposedTable(char[][] array, int[] key)
        {
            char [][] transposedTable = new char [key.Length][];

            for (int i = 0; i < array.GetLength(0); i++)
            {
                transposedTable[i] = array[key[i]];
            }

            return transposedTable;
        }

        internal static char[,] TransposeArray(char[,] array)
        {
            int rows = array.GetLength(0);
            int cols = array.GetLength(1);

            char[,] newArray = new char[ cols, rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    newArray[j,i] = array[i,j];
                }
            }

            return newArray;
        }

        internal static int[] Map(string key)
        {
            // Objective: To properly return index of repeated characters
            int[] indices = new int[key.Length];

            char[] arrKey = key.ToCharArray();
            Array.Sort(arrKey);

            string sortedKey = new string(arrKey);
            
            Dictionary<char, int> charCount = new Dictionary<char, int>();
            int index;
            for (int i = 0; i < key.Length; i++)
            {
                if (charCount.ContainsKey(sortedKey[i]))
                {
                    int count = charCount[sortedKey[i]];
                    count = key.Substring(count + 1).IndexOf(sortedKey[i]) + count;
                    index = ++count;
                    charCount[sortedKey[i]] = count;
                }
                else
                {
                    index = key.IndexOf(sortedKey[i]);
                    charCount.Add(sortedKey[i], index);
                }
                indices[i] = index;
            }

            return indices;
        }

        internal static char[][] JaggedArray(char[,] array)
        {
            char[][] jagged = new char[array.GetLength(0)][];

            for (int i = 0; i < array.GetLength(0); i++)
            {
                char[] arr = new char[array.GetLength(1)];

                for (int j = 0; j < array.GetLength(1); j++)
                {
                    arr[j] = array[i, j];
                }

                jagged[i] = arr;
            }

            return jagged;
        }
    }
}
