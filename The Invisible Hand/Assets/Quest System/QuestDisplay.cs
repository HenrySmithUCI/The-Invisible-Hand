using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuestDisplay : MonoBehaviour {

  public QuestObject quest;

  private Text description;
  private RectTransform cost;
  private RectTransform reward;

  public void Awake() {
    RectTransform rt = GetComponent<RectTransform>();
    description = rt.Find("Description").GetComponent<Text>();
    cost = rt.Find("Cost").GetComponent<RectTransform>();
    reward = rt.Find("Reward").GetComponent<RectTransform>();
  }

  public void UpdateDisplay() {
    description.text = quest.description;

    for (int i = 0; i < cost.childCount; i++) {
      if (1 < quest.cost.Length) {

      }
      else {

      }
    }



  }
}
