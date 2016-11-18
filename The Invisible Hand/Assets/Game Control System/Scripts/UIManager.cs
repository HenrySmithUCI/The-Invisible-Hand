using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//class that is in charge of the game's UI
public class UIManager : Singleton<UIManager> {

  protected UIManager() { }
  
  [System.Serializable]
  //creates the GameObject called sceneUI and the corresponding scene name
  public class stringScene {
    public GameObject sceneUI;
    public string sceneName;
  }

  //list of available scenes
  public stringScene[] sceneInterfaces;

  //the following block of statements creates the names of each box in the UI
  public string resourceBoxName;
  public string turnBoxName;
  public string questBoxName;

  //creates Text objects for the resources name/amount and turn count
  private Text resourcesText;
  private Text turnText;

  void Start() {
    updateAll();
  }
 
  //updates the UI dealing with resources, everytime the storage variable in the ResourceStorage class is changed
  public void updateResources() {
    resourcesText.text = ResourceStorage.Instance.StorageString();
  }

  //updates the UI dealing with the turn counter, everytime the turn variable in TurnManager class is changed
  public void updateTurn() {
    turnText.text = TurnManager.Instance.Turn.ToString();
  }

  //accesses the list of available scenes and if the input sceneName matches the scene found in the list, the UI updates
  public void changeScene(string sceneName) {

    foreach(stringScene ss in sceneInterfaces) {
      if (ss.sceneName == sceneName) { 
        Instantiate(ss.sceneUI);
      }
    }
    updateAll();
  }

  //on start the given resource list and turn count will be updated
  private void updateAll() {

    if (GameObject.Find(resourceBoxName) != null) {
      resourcesText = GameObject.Find(resourceBoxName).GetComponentInChildren<Text>();
      updateResources();
    }

    if (GameObject.Find(turnBoxName) != null) {
      turnText = GameObject.Find(turnBoxName).GetComponentInChildren<Text>();
      updateTurn();
    }
    
  }
}
