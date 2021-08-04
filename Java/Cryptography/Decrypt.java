package Cryptography;

public class Decrypt{
    public static String trans(String ciphertext, int key) {
        int evenlyPerColum = ciphertext.length() / key;
        int rest = ciphertext.length() % key;

        int [] columns = new int[key];
        for (int i = 0; i < key; i++){
            columns[i] = evenlyPerColum;
        }

        for (int i = 0; i < rest; i++){
            columns[i] = ++columns[i];
            System.out.println(columns[i]);
        }

        String [] columnDistribute = new String[key];

        int start = 0;
        int stop = 0;
        int k = 0;

        for (int perColumn : columns){
            stop += perColumn;
            columnDistribute[k] = ciphertext.substring(start, stop);
            start += perColumn;
            k++;
        }

        String plaintext = "";

        for (int i = 0; i < columnDistribute[0].length(); i++){
            String distribute = "";
            for (int j = 0; j < columnDistribute.length; j++){
                try {
                    distribute += columnDistribute[j].substring(i, i + 1);
                }
                catch (Exception e) {
                    break;
                }
            }

            plaintext += distribute;
        }

        return plaintext;
    }

    public static String shiftCipher(String ciphertext, int key) {
        String plaintext = "";
        int index, newIndex, ord, newOrd;

        for (int i = 0; i < ciphertext.length(); i++){
            ord = (int)ciphertext.charAt(i);

            if (ord >= 48 && ord <=57){
                index = ord - 48;
                newIndex = (index - key) % 10; 
                newIndex = (newIndex < 0) ? newIndex + 10 : newIndex;
                newOrd = 48 + newIndex;
            }
            else if (ord >= 65 && ord <= 90){
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
            else{
                newOrd = ord;
            }
            
            plaintext += (char)newOrd;
        }

        return plaintext;
    }

    public static String ceaser(String ciphertext) {
        return shiftCipher(ciphertext, 3);
    }

    public static String trans(String ciphertext, String key) {
        int cols = key.length();
        int rows = ciphertext.length() / cols;
        rows = (ciphertext.length() % cols == 0) ? rows : rows + 1;


        // Calculating each character per column
        int[] charPerColumn = new int[cols];
        int perColumn = ciphertext.length() / cols;

        for (int i = 0; i < cols; i++) {
            charPerColumn[i] = perColumn;
        }

        if (rows > perColumn) {
            for (int i = 0; i < (ciphertext.length() - (perColumn * cols)); i++) {
                charPerColumn[i] = ++charPerColumn[i];
            }
        }

        int[] indices = CryptUtil.map(key);

        // Reading the characters per columns from the ciphertext into table
        int start = 0;
        char[][] table = new char[rows][cols];

        for(int i = 0; i < cols; i++) {
            int col = indices[i];
            int perCol = charPerColumn[col];

            int stop = start + perCol;
            String text = ciphertext.substring(start, stop);
            for (int j = 0; j < perCol; j++) {
                table[j][col] = text.charAt(j);
            }

            start = stop;
        }

        // Reading the plaintext from the table
        String plaintext = "";
        for (int i = 0; i < table.length; i++)
        {
            String text = new String(table[i]);
            plaintext += text;
        }

        // Removing extra whitespaces after the plain text
        if (rows > perColumn) {
            int rem = (rows * cols) - ciphertext.length();
            plaintext = plaintext.substring(0, (plaintext.length() - rem));
        }

        return plaintext;
    }
}