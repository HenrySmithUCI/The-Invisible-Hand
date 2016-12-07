using UnityEngine;
using System.Collections.Generic;

public class EventManager : Singleton<EventManager> {

  protected EventManager() { }

  public List<EventObject> currentEventList;
  public EventObject currentEvent;

  public void makeCurrentEventList(int turn) {
    currentEventList.Clear();
    currentEventList = EventStorage.Instance.GetEventSet(turn, getNumberOfEvents(turn));
    currentEventList = Shuffle.shuffle<EventObject>(currentEventList);
  }

  public static EventObject getRandomEventFromProbabilities(EventObject.EventGroup[] events) {
    float probSum = 0;
    foreach (EventObject.EventGroup e in events) {
      probSum += e.value;
    }
    float val = Random.Range(0f, probSum);
    float currSum = 0;
    
    foreach (EventObject.EventGroup e in events) {
      if (val <= currSum + e.value) {
        return e.eventObject;
      }
      currSum += e.value;
    }

    //print("No random event was got!");
    return null;
  }

  public void nextEvent() {
    if (currentEventList.Count == 0) {
      PhaseManager.Instance.changePhase("Market Scene");
      return;
    }
    currentEvent = currentEventList[0];
    currentEventList.Remove(currentEvent);
    activateEvent(currentEvent);
  }

  public void activateEvent(EventObject eo) {
    currentEvent = eo;
    foreach (ResourceAmount r in eo.effects) {
      ResourceStorage.Instance.addResource(r.resourceName, r.amount);
    }

    if(eo.assignedQuest != null) {
      try {
        QuestManager.Instance.assignQuest(eo.assignedQuest);
      }
      catch (TooManyQuestsException) {
        activateEvent(eo.onTooManyQuests);
      }
    }

    if (eo.setUpNewEvent.eventObject != null) {
      try {
        EventStorage.Instance.turnDependentEvents[PhaseManager.Instance.Turn + eo.setUpNewEvent.value].events.Add(eo.setUpNewEvent.eventObject);
      }
      catch (System.IndexOutOfRangeException) {
      }
    }

    List<EventObject.EventGroup> availableEvents = new List<EventObject.EventGroup>();
    foreach (EventObject.EventGroup e in eo.redirectEvents) {
      if (e.eventObject.matchesPrerequisites()) {
        availableEvents.Add(e);
      }
    }

    if (availableEvents.Count > 0) {
      EventObject newEO = getRandomEventFromProbabilities(availableEvents.ToArray());
      if (newEO != null) {
        activateEvent(newEO);
      } 
    }

    UIManager.Instance.updateAll();
  }

  public int getNumberOfEvents(int turn) {
    return 1;
  }
}
