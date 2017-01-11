using UnityEngine;
using System.Collections.Generic;

public class EventManager : Singleton<EventManager> {

  protected EventManager() { }

  public List<EventObject> currentEventList;
  public EventObject currentEvent;

  public void makeCurrentEventList(int turn) {
    currentEventList.Clear();

    try {
      currentEventList = EventStorage.Instance.GetEventSet(turn, getNumberOfEvents(turn));
    }
    catch (NotEnoughEventsException) {
      currentEventList.Add(EventStorage.Instance.defaultEvent);
    }

    currentEventList = Shuffle.shuffle(currentEventList);
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
        if (eo.randomBuy)
        {
            activateEvent(RandomEventGenerator.makeBuy());
        }

        if (eo.randomSell)
        {
            activateEvent(RandomEventGenerator.makeSell());
        }

        if (eo.randomQuest)
        {
            activateEvent(RandomEventGenerator.makeQuest());
        }
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

    if (eo.setUpNewEvent != null && eo.setUpNewEvent.eventObject != null) {
            addTurnEvent(eo.setUpNewEvent);
    }

    List<EventObject.EventGroup> availableEvents = new List<EventObject.EventGroup>();
        if (eo.redirectEvents != null)
        {
            foreach (EventObject.EventGroup e in eo.redirectEvents)
            {
                if (e.eventObject.matchesPrerequisites())
                {
                    availableEvents.Add(e);
                }
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

    public void addTurnEvent(EventObject.EventGroup eo)
    {
        try
        {
            EventStorage.Instance.turnDependentEvents[PhaseManager.Instance.Turn + eo.value].events.Add(eo.eventObject);
        }
        catch (System.ArgumentOutOfRangeException)
        {
            EventListObject elo = new EventListObject();
            elo.events = new List<EventObject>();
            elo.events.Add(eo.eventObject);
            for (int i = 0; i < eo.value - 1; i++)
            {
                EventListObject tempElo = new EventListObject();
                tempElo.events = new List<EventObject>();
                EventStorage.Instance.turnDependentEvents.Add(tempElo);
            }
            EventStorage.Instance.turnDependentEvents.Insert(PhaseManager.Instance.Turn + eo.value, elo);
        }
    }

  public int getNumberOfEvents(int turn) {
        if (turn < 2 || turn == 16)
        {
            return 0;
        }
    return 7;
  }
}
