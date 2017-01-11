using UnityEngine;
using System.Collections.Generic;

public class TooManyQuestsException : System.Exception {

}

public class QuestManager : Singleton<QuestManager> {

  protected QuestManager() { }

  public List<QuestObject> currentQuests;
  public int maxQuests = 3;

  public void Awake() {
    currentQuests = new List<QuestObject>();
  }


  public void assignQuest(QuestObject qo) {
    if(currentQuests.Count >= maxQuests) {
      throw new TooManyQuestsException();
    }
    qo.turnsToComplete = qo.maxTurns;
        EventObject.EventGroup eg = new EventObject.EventGroup(qo.OnQuestEnd, qo.maxTurns);
        EventManager.Instance.addTurnEvent(eg);
        currentQuests.Add(qo);
  }

  public void updateQuests() {
    List<QuestObject> toRemove = new List<QuestObject>();

    foreach(QuestObject qo in currentQuests) {
      qo.turnsToComplete -= 1;
      if(qo.turnsToComplete <= 0) {
        toRemove.Add(qo);
      }
    }

    foreach(QuestObject qo in toRemove) {
      currentQuests.Remove(qo);
    }
  }
}
