using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

//class that is in charge of the game's UI
public class UIManager : Singleton<UIManager> {

  protected UIManager() { }
  
  [System.Serializable]
  //creates the GameObject called sceneUI and the corresponding scene name
  public class stringScene {
    public string sceneName;
    public GameObject sceneUI;
  }
  [System.Serializable]
  public class stringSprite {
    public string spriteName;
    public Color color;
    public Sprite sprite;
  }

  //list of available scenes
  public stringScene[] sceneInterfaces;
  public stringSprite[] resourceIcons;
  public RectTransform iconPrefab;

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

  public void makeResourceIconImage(string resource, Rect AnchorPos, RectTransform parent) {
    foreach(stringSprite ss in resourceIcons) {
      if (resource == ss.spriteName) {
        RectTransform img = Instantiate(iconPrefab);
        img.GetComponent<Image>().sprite = ss.sprite;
        img.GetComponent<Image>().color = ss.color;

        img.SetParent(parent);
        img.anchorMin = new Vector2(AnchorPos.xMin, AnchorPos.yMin);
        img.anchorMax = new Vector2(AnchorPos.xMax,AnchorPos.yMax);
        img.offsetMax = new Vector2(0, 0);
        img.offsetMin = new Vector2(0, 0);
      }
    }
  }

 
  //updates the UI dealing with resources, everytime the storage variable in the ResourceStorage class is changed
  public void updateResources() {
    RectTransform resourceBox = GameObject.Find("Resource Ticker").GetComponent<RectTransform>();

    for(int i = 0; i < resourceBox.childCount; i++) {
      ResourceDisplay rd = resourceBox.GetChild(i).GetComponent<ResourceDisplay>();
      if (CostManager.Instance.availableResources.Contains(rd.resource)) {
        rd.display = true;
        if (rd.resource != "") {
          rd.amount = Mathf.FloorToInt(ResourceStorage.Instance.checkResource(rd.resource));
          rd.updateDisplay();
        }
      }
      else {
        rd.display = false;
      }
      
      
    }
  }

  public void displayQuest(QuestObject quest, RectTransform questBox) {

  }

  //updates the UI dealing with the turn counter, everytime the turn variable in TurnManager class is changed
  public void updateTurn() {
    GameObject.Find("Turn Ticker").GetComponentInChildren<Text>().text = PhaseManager.Instance.Turn.ToString();
  }

  public void updateQuests() {

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

    if (GameObject.Find("Resource Ticker") != null) {
      updateResources();
    }

    if (GameObject.Find("Turn Ticker") != null) {;
      updateTurn();
    }

    if (GameObject.Find("Quest Ticker") != null) {
      updateQuests();
    }
  }
}
