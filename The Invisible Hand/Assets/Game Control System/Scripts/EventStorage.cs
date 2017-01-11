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

    public List<EventListObject> turnDependentEvents;
    public List<EventObject> turnIndependentEvents;
    public List<EventObject> endlessEventPool;
    public EventObject defaultEvent;

    private EventObject ExtractEvent()
    {
        int index = UnityEngine.Random.Range(0, turnIndependentEvents.Count - 1); //will need to be modified if we decide to add weights to ti events
        EventObject Event = turnIndependentEvents[index];
        turnIndependentEvents.Remove(Event); //NOTE: This method REMOVES a Event object from the turnIndependentEvents member variable
        return Event;



    }
    public EventObject randomFromPool()
    {
        return endlessEventPool[UnityEngine.Random.Range(0, endlessEventPool.Count)];
    }

    public List<EventObject> GetEventSet(int turn, int maxEvents)
    {

        List<EventObject> reqEvents;
        if (turn < turnDependentEvents.Count) {
           reqEvents = new List<EventObject>(turnDependentEvents[turn].events); //should be a deep copy
        }
        else {
          reqEvents = new List<EventObject>();
        }

        List<EventObject> extraEvents = new List<EventObject>();

        if (turnIndependentEvents.Count > 0)
        {
            extraEvents.Add(ExtractEvent());
        }

        if(endlessEventPool.Count > 0)
        {
            while(maxEvents - extraEvents.Count > 0)
            {
                extraEvents.Add(randomFromPool());
            }
        }
        reqEvents.AddRange(extraEvents);
        return reqEvents;



    }
}
