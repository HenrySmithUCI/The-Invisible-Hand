using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuestDisplay : MonoBehaviour {

  public QuestObject quest;

  private Text description;
  private RectTransform cost;
  private RectTransform reward;
  private RectTransform timer;

  public void Awake() {
    RectTransform rt = GetComponent<RectTransform>();
    description = rt.Find("Description").GetComponent<Text>();
    cost = rt.Find("Cost").GetComponent<RectTransform>();
    reward = rt.Find("Reward").GetComponent<RectTransform>();
    timer = rt.Find("Timer").GetComponent<RectTransform>();

  }

  public void UpdateDisplay() {
    description.text = quest.description;

    for(int i = 0; i < quest.cost.Length; i++) {
      UIManager.Instance.makeResourceDisplay(
        quest.cost[i].resourceName, 
        Mathf.CeilToInt(quest.cost[i].amount), 
        new Rect(0,0,1,1), 
        cost.GetChild(i).GetComponent<RectTransform>());
    }

        UIManager.Instance.makeResourceDisplay(quest.reward.resourceName, Mathf.CeilToInt(quest.reward.amount), new Rect(0, 0, 1, 1), reward);
    UIManager.Instance.makeResourceDisplay("Turn", quest.turnsToComplete, new Rect(0,0,1,1), timer);
  }
}
