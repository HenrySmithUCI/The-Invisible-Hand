using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

//sources for code: http://nielson.io/2015/09/saving-games-in-unity/ and https://www.sitepoint.com/saving-and-loading-player-game-data-in-unity/
//This is how to implement these classes together:
/*      savedStatistics profile = new savedStatistics();
 *      profile.curTurn = turn of the game
 *      profile.gameEvents = the list of all game events
 *      profile.attribute = some attirbute etc
 *       . . . 
 *      
        saveSystem saver = new saveSystem(profile);
        if (some condition is satisifed):

        saver.SaveData();

        else if(some other condition is satisfied):

        saver.LoadData();
        I can add more parameters such as passing a string that is the file name instead of saving and loading the same file 
 * 
 * 
 * 
 * 
 * 
 * */
[System.Serializable]
public abstract class saveGame
{

}
[System.Serializable]
public class savedStatistics : saveGame {

    public int curTurn;
    public EventListObject gameEvents;
    public List<QuestObject> allQuests;
    public List<ResourceAmount> playerResources;
    public int test = 300; //just for testing

   

}

public class saveSystem
{
    public savedStatistics localCopy;
    public bool loadedScene = false;
    public saveSystem(savedStatistics localCopy)
    {
        this.localCopy = localCopy;
    }

    public void SaveData()
    {
        if (!Directory.Exists("Saves"))
            Directory.CreateDirectory("Saves");

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Create("Saves/save.binary");

        localCopy = new savedStatistics();

        formatter.Serialize(saveFile, localCopy);

        saveFile.Close();
    }

    public void LoadData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Open("Saves/save.binary", FileMode.Open);

        localCopy = (savedStatistics)formatter.Deserialize(saveFile);
        Debug.Log(localCopy.test); //this was just for testing
        saveFile.Close();
    }
}
