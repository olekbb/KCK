package javaapplication2;

/**
 * Created by Maciej on 31.12.14.
 */

import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.util.ArrayList;

/**
 * The fileReader class is reading commands from text file
 */

public class fileReader {

    private ArrayList<String> listaZdan = new ArrayList<String>();
    private ArrayList<String> listaWyrazow = new ArrayList<String>();

    /**
     * KONSTRUKTOR
     * Wpisuje odczytane zdanie do @listaZdan a nastepnie dzieli na osobne
     * wyrazy i wpisuje do @listaWyrazow
     */

    public fileReader() {

        try {

            String line = null;

            File myFile = new File("C://Users//aleks_000//Desktop//KCKC//8-48//8-48//bin//Debug//komendy.txt");    //\\files\students\s396393\Desktop\NOEW\netbeans\JavaApplication2\src\javaapplication2\komendy.txt
            //"C:\Users\Maciej\Desktop\KCK PROJEKT\MovmentInterpreter[v.1.0]\src\plik.txt"
            FileReader fileReader = new FileReader(myFile);
            BufferedReader reader = new BufferedReader(fileReader);

            while ((line = reader.readLine()) != null) {

                if (line.trim().length() == 0) {
                    continue;  // Skip blank lines & blank lines with sapce
                }

                listaZdan.add(line.toLowerCase());

                for (String words : line.split(" ")) {
                    listaWyrazow.add(words.toLowerCase());
                }
            }
            reader.close();
        } catch (Exception ex) {
            ex.printStackTrace();
        }
        for (int i = 0; i < listaZdan.size(); i++) {
            System.out.println(listaZdan.get(i)); // Wypisuje kolejne pola z @listaZdan
        }
        System.out.println(listaZdan); // Wypisuje wszystkie zdania wpisane do ArrayList @listaZdan
        System.out.println(listaWyrazow); // Wypisuje podzielone wyrazy z ArrayList @listaWyrazow w ktorej bede szukal ketwords
    }

    //  Getter do @listyWyrazow

    public ArrayList<String> getListaWyrazow() {
        return listaWyrazow;
    }

}



