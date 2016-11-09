using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : Singleton<UIManager> {

  protected UIManager() { }
  
  [System.Serializable]
  public class stringScene {
    public string sceneName;
    public GameObject sceneUI;
  }

  public stringScene[] sceneInterfaces;

  public string resourceBoxName;
  public string turnBoxName;
  public string questBoxName;

  private Text resourcesText;
  private Text turnText;

  void Start() {
    updateAll();
  }

  public void updateResources() {
    resourcesText.text = ResourceStorage.Instance.StorageString();
  }

  public void updateTurn() {
    turnText.text = PhaseManager.Instance.Turn.ToString();
  }

  public void changeScene(string sceneName) {

    foreach(stringScene ss in sceneInterfaces) {
      if (ss.sceneName == sceneName) { 
        Instantiate(ss.sceneUI);
      }
    }
    updateAll();
  }

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
