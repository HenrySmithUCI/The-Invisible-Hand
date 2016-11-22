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
  public RectTransform emptyPrefab;
  public ResourceDisplay rdPrefab;

  //creates Text objects for the resources name/amount and turn count
  private Text resourcesText;
  private Text turnText;

  void Start() {
    updateAll();
  }

  public void seatInside(RectTransform parent, RectTransform child, Rect AnchorPos) {
    child.SetParent(parent);
    child.SetParent(parent);
    child.anchorMin = new Vector2(AnchorPos.xMin, AnchorPos.yMin);
    child.anchorMax = new Vector2(AnchorPos.xMax, AnchorPos.yMax);
    child.offsetMax = new Vector2(0, 0);
    child.offsetMin = new Vector2(0, 0);
  }

  public void makeResourceIconImage(string resource, Rect AnchorPos, RectTransform parent) {
    foreach(stringSprite ss in resourceIcons) {
      if (resource == ss.spriteName) {
        RectTransform img = Instantiate(emptyPrefab);
        img.gameObject.AddComponent<Image>();
        img.GetComponent<Image>().sprite = ss.sprite;
        img.GetComponent<Image>().color = ss.color;

        seatInside(parent, img, AnchorPos);
      }
    }
  }

  public ResourceDisplay makeResourceDisplay(string resource, int amount, Rect AnchorPos, RectTransform parent) {
    ResourceDisplay rd = Instantiate(rdPrefab);
    RectTransform rt = rd.GetComponent<RectTransform>();
    rd.resource = resource;
    rd.gameObject.name = rd.resource + " Display";
    rd.amount = amount;
    rd.display = true;

    seatInside(parent, rt, AnchorPos);

    rd.updateDisplay();
    return rd;
  }

 
  //updates the UI dealing with resources, everytime the storage variable in the ResourceStorage class is changed
  public void updateResources() {
    RectTransform resourceBox = GameObject.Find("Resource Ticker").GetComponent<RectTransform>();

    for(int i = 0; i < resourceBox.childCount; i++) {
      ResourceDisplay rd = resourceBox.GetChild(i).GetComponent<ResourceDisplay>();
      if (CostManager.Instance.availableResources.Contains(rd.resource) || rd.resource == "Gold") {
        rd.display = true;
        if (rd.resource != "") {
          rd.amount = Mathf.FloorToInt(ResourceStorage.Instance.checkResource(rd.resource));
        }
      }
      else {
        rd.display = false;
      }
      rd.updateDisplay();
    }
  }

  //updates the UI dealing with the turn counter, everytime the turn variable in TurnManager class is changed
  public void updateTurn() {
    GameObject.Find("Turn Ticker").GetComponentInChildren<Text>().text = PhaseManager.Instance.Turn.ToString();
  }

  //accesses the list of available scenes and if the input sceneName matches the scene found in the list, the UI updates
  public void changeScene(string sceneName) {

    foreach(stringScene ss in sceneInterfaces) {
      if (ss.sceneName == sceneName) { 
        Instantiate(ss.sceneUI);
      }
    }
    createAll();
  }

  private void createAll() {
    if (GameObject.Find("Resource Ticker") != null) {
      GameObject.Find("Resource Ticker").GetComponent<ResourceTicker>().destroyThenCreateResources();
    }

    if (GameObject.Find("Turn Ticker") != null) {
      updateTurn();
    }

    if (GameObject.Find("Quest Ticker") != null) {
      GameObject.Find("Quest Ticker").GetComponent<QuestTicker>().updateDisplay();
    }
  }


  //on start the given resource list and turn count will be updated
  private void updateAll() {
    if (GameObject.Find("Resource Ticker") != null) {
      GameObject.Find("Resource Ticker").GetComponent<ResourceTicker>().updateResources();
    }

    if (GameObject.Find("Turn Ticker") != null) {;
      updateTurn();
    }

    if (GameObject.Find("Quest Ticker") != null) {
      GameObject.Find("Quest Ticker").GetComponent<QuestTicker>().updateDisplay();
    }
  }
}
