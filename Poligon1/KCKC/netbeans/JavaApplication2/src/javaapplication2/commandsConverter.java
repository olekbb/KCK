package javaapplication2;


/**
 * Created by Maciej on 31.12.14.
 */

import java.util.ArrayList;

/**
 * This class is search keywords in @listaWyrazow and convert them
 * to Unity commands and generate routeCode
 *
 *
 * Vector3 regalLeft = new Vector3(-4.5f, 1.5f);	>>> RL
 * Vector3 regalCenter = new Vector3(0f, 1.5f);         >>> RC
 * Vector3 regalRight = new Vector3(4.5f, 1.5f);        >>> RR

 * Vector3 platformLeft = new Vector3(-4.5f, -1f);      >>> PL
 * Vector3 platformCenter = new Vector3(0f, -1f);       >>> PC
 * Vector3 platformRight = new Vector3(4.5f, -1f);      >>> PR
 *
 */

public class commandsConverter {

    private ArrayList<Integer> indexOfHit = new ArrayList<Integer>(); //Indexy wyrzow opisujacych ktory regal albo ktore pole
    private ArrayList<String> komendyUnity = new ArrayList<String>(); //Tu wpisuje komendy w postaci UNITY_****_***
    private ArrayList<String> routeCode = new ArrayList<String>();  // Kod trasy dla Unity
    private ArrayList<String> listaWyrazow; // Uzupelniam ja w konstruktorze slowami odczytanymi z pliku

    private String[] numberOfShelf = {"lewego" , "srodkowego" , "prawego"}; //Dostepne regaly
    private String[] colorOfField = {"lewe" , "srodkowe" , "prawe"}; //Dostepne pola


    public commandsConverter() {
        fileReader fileReader = new fileReader(); // [IMPORTANT] Start reading file with commands!!!!
        listaWyrazow = fileReader.getListaWyrazow(); // Uzyskanie listy wyrazow odczytanych z pliku
    }

    public void startConvertingCommands() {

        WhichRegal();
        WhichField();

        System.out.println("Spec@IndexChecksum: " + indexOfHit); // Zwraca numery indexow trafionych keywords z tablicy String[] w ArrayList @listaWyrazow
        System.out.println(komendyUnity); // Zwraca przekonwertowane keywords na komendy Unity
        System.out.println(routeCode); // Kod drogi dla Unity

        ArrayListToString_SaveToFile(komendyUnity,"output","\r\n");
        ArrayListToString_SaveToFile(routeCode,"routCode","");
    }

    /**
     * Convert ArrayList to string and save to .txt file
     * @param ArrayList
     * @param filename
     * @param textFormat
     */
    public void ArrayListToString_SaveToFile(ArrayList<String> ArrayList, String filename, String textFormat) {
        String ArrayListString = "";
        for (String s : ArrayList) {
            ArrayListString += s + textFormat;
        }
        new fileSave(ArrayListString, filename);
    }


    public void WhichRegal() {
        for (int i = 0; i < numberOfShelf.length; i++) {
            if (listaWyrazow.contains(numberOfShelf[i])) {
                int indexOfWord = listaWyrazow.indexOf(numberOfShelf[i]);
                indexOfHit.add(indexOfWord);
                System.out.println(listaWyrazow.get(indexOfWord));
                switch (i) {
                    case 0: // REGAL LEFT
                        routeCode.add("RL");
                        komendyUnity.add("UNITY_REGAL_LEFT");
                        break;
                    case 1: // REGAL CENTER
                        routeCode.add("RC");
                        komendyUnity.add("UNITY_REGAL_CENTER");
                        break;
                    case 2: // REGAL RIGHT
                        routeCode.add("RR");
                        komendyUnity.add("UNITY_REGAL_LEFT");
                        break;
                }
            }
        }
    }
    public void WhichField() {
        for (int i = 0; i < colorOfField.length; i++) {
            if (listaWyrazow.contains(colorOfField[i])) {
                int indexOfWord = listaWyrazow.indexOf(colorOfField[i]);
                indexOfHit.add(indexOfWord);
                System.out.println(listaWyrazow.get(indexOfWord));
                switch (i) {
                    case 0: // PLATFORM LEFT
                        routeCode.add("PL");
                        komendyUnity.add("UNITY_PLATFORM_LEFT");
                        break;
                    case 1: // PLATFORM CENTER
                        routeCode.add("PC");
                        komendyUnity.add("UNITY_PLATFORM_CENTER");
                        break;
                    case 2: // PLATFORM RIGHT
                        routeCode.add("PR");
                        komendyUnity.add("UNITY_PLATFORM_RIGHT");
                        break;
                }
            }
        }
    }
}

