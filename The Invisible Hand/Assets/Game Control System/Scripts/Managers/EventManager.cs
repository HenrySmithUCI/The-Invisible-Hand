using UnityEngine;
using System.Collections.Generic;

public class EventManager : Singleton<EventManager> {

  protected EventManager() { }

  public List<EventObject> currentEventList;
  public EventObject currentEvent;

  public void makeCurrentEventList(int turn) {
    currentEventNum = 0;
    currentEventList.Clear();
    currentEventList = EventStorage.Instance.GetEventSet(turn, getNumberOfEvents(turn));
    currentEventList = Shuffle.shuffle<EventObject>(currentEventList);
  }

  int currentEventNum;

  public static EventObject getRandomEventFromProbabilities(EventObject.EventInt[] events) {
    float probSum = 0;
    foreach (EventObject.EventInt e in events) {
      probSum += e.value;
    }

    float val = Random.Range(0f, probSum);

    float currSum = 0;
    foreach (EventObject.EventInt e in events) {
      if (val < currSum + e.value) {
        return e.eventObejct;
      }
      currSum += e.value;
    }

    return null;
  }

  public void nextEvent() {
    activateEvent(currentEventList[currentEventNum]);
    UIManager.Instance.setCurrentEvent(currentEventList[currentEventNum]);
    currentEventNum += 1;
  }

  public void activateEvent(EventObject eo) {
    foreach (ResourceAmount r in eo.effects) {
      ResourceStorage.Instance.addResource(r.resourceName, r.amount);
    }

    if (eo.redirectEvents.Length > 0) {
      getRandomEventFromProbabilities(eo.redirectEvents);
    }


  }

  public int getNumberOfEvents(int turn) {
    return 1;
  }
}
