using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : Singleton<UIManager> {

  protected UIManager() { }

  public string resourceBoxName;
  public string turnBoxName;
  public string questBoxName;

  private Text resourcesText;
  private Text turnText;

  void Start() {

    resourcesText = GameObject.Find(resourceBoxName).GetComponentInChildren<Text>();
    turnText = GameObject.Find(turnBoxName).GetComponentInChildren<Text>();

    updateResources();
    updateTurn();
  }

  public void updateResources() {
    resourcesText.text = ResourceStorage.Instance.StorageString();
  }

  public void updateTurn() {
    turnText.text = TurnManager.Instance.Turn.ToString();
  }
}
