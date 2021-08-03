package Cryptography;


public class CryptUtil {
    static char[][] toTable(String text, int rows, int cols, char fill) {
        char[][] table = new char[rows][cols];
        int k = 0;
        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++) {
                if (k < text.length()) {
                    table[i][j] = text.charAt(k);
                    k++;
                }
                else {
                    table[i][j] = fill;
                }
                
            }
        }
        return table;
    }

    static void printArray(char[][] array, int space) {

        // Printing the First Heading Line
        System.out.print("+");
        for (int j = 0; j < array[0].length; j++) {
            System.out.print("-".repeat(space + 1) + "-".repeat(space) + "+");
        }
        System.out.println();

        // End of Printing

        // Printing the rest of the table

        for (int i = 0; i < array.length; i++) {
            System.out.print("|");
            for (int j = 0; j < array[0].length; j++) {
                System.out.print(" ".repeat(space) + array[i][j] + " ".repeat(space) + "|");
            }
            System.out.print("\n" + "+");
            for (int j = 0; j < array[0].length; j++) {
                System.out.print("-".repeat(space + 1) + "-".repeat(space) + "+");
            }

            System.out.println();
        }
        // End of Printing
        
    }

    static void printArray(char[][] array) {
        printArray(array, 1);
    }

    static void printArray(int[] array) {
        String str = "[";

        for (int i = 0; i < array.length; i++) {
            str += array[i];

            if (i < array.length - 1) {
                str += ", ";
            }
        }
        str += "]";

        System.out.println(str);
    }

    static char[][] transposeTable(char[][] array) {
        int rows = array.length;
        int cols = array[0].length;

        char[][] transposedArray = new char [cols][rows];

        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++) 
            {
                transposedArray[j][i] = array[i][j];
            }
        }

        return transposedArray;
    }

    static int[] map(String key) {
        int[] indices = new int[key.length()];

        String sortedKey  = bumbleSort(key);

        for (int i = 0; i < key.length(); i++) {
            indices[i] = key.indexOf(sortedKey.charAt(i));
        }

        return indices;

    }

    static void bumbleSort(int[] array) {
        boolean sorted = true;
        for (int i = 0; i < array.length - 1; i++) {
            if (array[i] > array[i + 1]) {
                int temp = array[i];
                array[i] = array[i + 1];
                array[i + 1] = temp;
                sorted = false;
            }
        }
        if (!sorted)
            bumbleSort(array);
    }

    static void bumbleSort(char[] array) {
        int[] intArray = new int[array.length];

        for (int i = 0; i < array.length; i++) {
            intArray[i] = (int)array[i];
        }

        bumbleSort(intArray);

        for (int i = 0; i < array.length; i++) {
            array[i] = (char)intArray[i];
        }

    }

    static String bumbleSort(String str) {
        char[] arrStr = str.toCharArray();
        bumbleSort(arrStr);

        String newStr = new String(arrStr);

        return newStr;
    }
}
