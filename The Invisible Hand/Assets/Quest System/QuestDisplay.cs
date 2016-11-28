﻿using UnityEngine;
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
      UIManager.Instance.makeResourceDisplay(
        quest.cost[i].resourceName, 
        Mathf.CeilToInt(quest.cost[i].amount), 
        new Rect(0,0,1,1), 
        cost.GetChild(i).GetComponent<RectTransform>());
    }

    for (int i = 0; i < quest.reward.Length; i++) {
      UIManager.Instance.makeResourceDisplay(quest.reward[i].resourceName,
        Mathf.CeilToInt(quest.reward[i].amount),
        new Rect(0,0,1,1),
        reward.GetChild(i).GetComponent<RectTransform>());
    }
  }
}
