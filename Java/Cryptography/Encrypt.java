package Cryptography;


public class Encrypt{
    public static String trans(String plaintext, int key){

        String distribute = "";
        String[] rowDistribute;

        for (int i = 0; i < plaintext.length(); i++){
            if (i % key == 0 && i != 0){
                distribute += "\n";
            }
            distribute += plaintext.substring(i, i + 1);
        }

        rowDistribute = distribute.split("\n");

        String ciphertext = "";

        for (int i = 0; i < rowDistribute[0].length(); i++){
            distribute = "";
            for (int j = 0; i < rowDistribute.length; j++){
                try {
                    distribute += rowDistribute[j].substring(i, i + 1);
                }
                catch (Exception e) {
                    break;
                }
            }

            ciphertext += distribute;
        }

        return ciphertext;
    }

    public static String shiftCipher(String plaintext, int key){
        String ciphertext = "";
        int ord, newOrd;

        for (int i = 0; i < plaintext.length(); i++){
            ord  = (int)plaintext.charAt(i);

            if (ord >= 48 && ord <=57){
                newOrd = 48 + ((ord - 48 + key) % 10);
            }
            else if (ord >= 65 && ord <= 90){
                newOrd = 65 + ((ord - 65 + key) % 26);
            }
            else if (ord >= 97 && ord <= 122){
                newOrd = 97 + ((ord - 97 + key) % 26);
            }
            else{
                newOrd = ord;
            }

            ciphertext += (char)newOrd;            
        }

        return ciphertext;
    }

    public static String ceaser(String plaintext){
        return shiftCipher(plaintext, 3);
    }

    public static String trans(String plaintext, String key){
        int cols = key.length();
        int rows = plaintext.length() / cols;
        rows = (plaintext.length() % cols == 0) ? rows : rows + 1;


        char[][] table = CryptUtil.toTable(plaintext, rows, cols, '*');
        char[][] transposedTable = CryptUtil.transposeTable(table);        
        int[] indices = CryptUtil.map(key);

        String ciphertext = "";

        for (int index : indices) {
            String text = new String(transposedTable[index]);

            if (text.charAt(text.length() - 1) == '*') {
                text = text.substring(0, text.length() - 1);
            }
            ciphertext += text;
        }
        return ciphertext;
    }
}