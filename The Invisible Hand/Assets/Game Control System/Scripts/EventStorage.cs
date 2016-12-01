using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
        int index = UnityEngine.Random.Range(0, turnIndependentEvents.Count - 1); //will need to be modified if we decide to add weights to ti events
        EventObject Event = turnIndependentEvents[index];
        turnIndependentEvents.Remove(Event); //NOTE: This method REMOVES a Event object from the turnIndependentEvents member variable
        return Event;



    }

    public List<EventObject> GetEventSet(int turn, int maxEvents)
    {
        List<EventObject> reqEvents = new List<EventObject>(turnDependentEvents[turn].events); //should be a deep copy
        if (maxEvents > reqEvents.Count)
        {

            for (int i = 0; i < maxEvents - reqEvents.Count; i++)
            {
                reqEvents.Add(ExtractEvent());
            }

        }

        return reqEvents;



    }
}
