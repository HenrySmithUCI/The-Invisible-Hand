using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : Singleton<UIManager> {

  protected UIManager() { }
  
  [System.Serializable]
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

  public stringScene[] sceneInterfaces;
  public stringSprite[] resourceIcons;
  public RectTransform iconPrefab;

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

  public void updateTurn() {
    GameObject.Find("Turn Ticker").GetComponentInChildren<Text>().text = PhaseManager.Instance.Turn.ToString();
  }

  public void updateQuests() {

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
