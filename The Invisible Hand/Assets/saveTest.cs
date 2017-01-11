/*
using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;


/* https://docs.unity3d.com/ScriptReference/EditorUtility.OpenFilePanel.html and https://docs.unity3d.com/ScriptReference/EditorUtility.SaveFilePanel.html
 * Ripped it from Unity docs
 * 
 * this is how the saveSystem would be implelemted to save and load files. 
 * 
 * I still need to connect variables in saveStatistics class (in the saveSystem.cs file) to the attributes (current turn, current resources, etc)
 * instiniate a saveSystem object (which requires passing in a saveStatistics object) for saving and loading with serialization (using saveData() and loadData() )
 * 
 * 
 * */
 /*
public class saveTest : MonoBehaviour {
  


     //in actual implementation this value would come from another script (like gameController)
    public saveSystem saveProfile;
    

    
    
    public string saveFile;
    

    
    void Start() {
        savedStatistics profile = new savedStatistics();
        saveProfile = new saveSystem(profile);
        

        

    }

   
    public void saveGame()
    {

        try
        {
            
            saveProfile.SaveData(saveFile);

        }

        catch
        {
            newGame();
        }

    }

   public  void loadGame()
    {
        try
        {
            
            string filePath = EditorUtility.OpenFilePanel("newGame", Path.GetDirectoryName("/The-Invisble-Hand/Saves"), "TIH");
            saveProfile.LoadData(filePath);
            saveFile = filePath;
        }

        catch
        {
            Debug.Log("Error loading from file path");
        }
    }

    public void newGame()
    {

        try
        {
            
            string filePath = EditorUtility.SaveFilePanel("newGame",Path.GetDirectoryName( "/The-Invisible-Hand/Saves"), "myNewGame", "TIH"); //I am assuming this method reutrns a correct path regardless of OS 
            saveProfile.SaveData(filePath);
            saveFile = filePath;
        }

        catch
        {
            Debug.Log("Failed to save");
        }
        
    }

    
        

        

	
	// Update is called once per frame
	void Update () {
	
	}
}
*/