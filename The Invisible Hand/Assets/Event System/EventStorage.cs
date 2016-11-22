using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class TurnDependentArray
{

    public List<EventObject> events;


}
public class EventStorage : MonoBehaviour
{

    //td: turn dependent , ti: turn independent. Creative I know

    public TurnDependentArray[] tdEvents;
    public List<EventObject> tiEvents;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private EventObject removeTiEvent()
    {
        int index = UnityEngine.Random.Range(0, tiEvents.Count - 1); //will need to be modified if we decide to add weights to ti events
        EventObject Event = tiEvents[index];
        tiEvents.Remove(Event); //NOTE: This method REMOVES a Event object from the tiEvents member variable
        return Event;



    }

    public List<EventObject> GetQuestSet(int turn, int maxEvents)
    {
        List<EventObject> reqEvents = new List<EventObject>(tdEvents[turn].events); //should be a deep copy
        if (maxEvents > reqEvents.Count)
        {

            for (int i = 0; i < maxEvents - reqEvents.Count; i++)
            {
                reqEvents.Add(removeTiEvent());
            }

        }

        return reqEvents;



    }
}
