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

    for(int i = 0; i < quest.cost.Length; i++) {
      float bottom = 0.5f * ((i / 2) % 2);
      float right = 0.5f * (i % 2);

      Rect r = new Rect();
      r.xMin = 0 + right;
      r.yMin = 0.5f - bottom;
      r.xMax = 0.5f + right;
      r.yMax = 1f - bottom;

      UIManager.Instance.makeResourceDisplay(quest.cost[i].resourceName, Mathf.CeilToInt(quest.cost[i].amount), r, cost);
    }

    for (int i = 0; i < quest.reward.Length; i++) {
      float bottom = 0.5f * ((i / 2) % 2);
      float right = 0.5f * (i % 2);

      Rect r = new Rect();
      r.xMin = 0 + right;
      r.yMin = 0.5f - bottom;
      r.xMax = 0.5f + right;
      r.yMax = 1f - bottom;

      UIManager.Instance.makeResourceDisplay(quest.reward[i].resourceName, Mathf.CeilToInt(quest.reward[i].amount), r, reward);
    }
  }
}
