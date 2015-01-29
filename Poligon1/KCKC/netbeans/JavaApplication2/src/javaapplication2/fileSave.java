package javaapplication2;


/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

import java.io.BufferedWriter;
import java.io.File;
import java.io.FileWriter;
import java.io.IOException;

/**
 *
 * @author s396393
 */
public class fileSave {
    
    public fileSave(String textToSave, String fileName) {
        
        try {
 
			//String content = "This is the content to write into file";
 
			File file = new File("C://Users//aleks_000//Desktop//KCKC//8-48//8-48//bin//" + fileName + ".txt");
 
			// if file doesnt exists, then create it
			if (!file.exists()) {
				file.createNewFile();
			}
 
			FileWriter fw = new FileWriter(file.getAbsoluteFile());
			BufferedWriter bw = new BufferedWriter(fw);
			bw.write(textToSave);
			bw.close();
 
			System.out.println("File saved");
 
		} catch (IOException e) {
			e.printStackTrace();
		}
    
}
    
}


