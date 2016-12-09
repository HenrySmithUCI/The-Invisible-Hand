using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NotEnoughEventsException : System.Exception {

}

[System.Serializable]
public class EventListObject
{

    public List<EventObject> events;


}
public class EventStorage : Singleton<EventStorage>
{
    protected EventStorage() { }

    public EventListObject[] turnDependentEvents;
    public List<EventObject> turnIndependentEvents;

    private EventObject ExtractEvent()
    {
        if(turnIndependentEvents.Count == 0) {
            throw new NotEnoughEventsException();
        }
        int index = UnityEngine.Random.Range(0, turnIndependentEvents.Count - 1); //will need to be modified if we decide to add weights to ti events
        EventObject Event = turnIndependentEvents[index];
        turnIndependentEvents.Remove(Event); //NOTE: This method REMOVES a Event object from the turnIndependentEvents member variable
        return Event;



    }

    public List<EventObject> GetEventSet(int turn, int maxEvents)
    {

        List<EventObject> reqEvents;
        if (turn < turnDependentEvents.Length) {
           reqEvents = new List<EventObject>(turnDependentEvents[turn].events); //should be a deep copy
        }
        else {
          reqEvents = new List<EventObject>();
        }
    
        if (maxEvents > reqEvents.Count)
        {
            List<EventObject> nonReqEvents = new List<EventObject>();
            for (int i = 0; i < maxEvents - reqEvents.Count; i++)
            {
                nonReqEvents.Add(ExtractEvent());
            }
            reqEvents.AddRange(nonReqEvents);
        }

        return reqEvents;



    }
}
