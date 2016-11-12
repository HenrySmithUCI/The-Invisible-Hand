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

    for (int i = 0; i < resourceBox.childCount; i++) {
      resourceBox.GetChild(i).gameObject.SetActive(false);
    }

    RectTransform box = resourceBox.Find("Gold").GetComponent<RectTransform>();
    box.gameObject.SetActive(true);
    makeResourceIconImage("Gold", new Rect(0, 0, 1, 1), box.Find("Icon").GetComponent<RectTransform>());
    box.Find("Amount").GetComponent<Text>().text = Mathf.FloorToInt(ResourceStorage.Instance.checkResource("Gold")).ToString();

    foreach (string resource in CostManager.Instance.availableResources) {
      box = resourceBox.Find(resource).GetComponent<RectTransform>();
      box.gameObject.SetActive(true);
      makeResourceIconImage(resource, new Rect(0, 0, 1, 1), box.Find("Icon").GetComponent<RectTransform>());
      box.Find("Amount").GetComponent<Text>().text = Mathf.FloorToInt(ResourceStorage.Instance.checkResource(resource)).ToString();
    }
    

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
