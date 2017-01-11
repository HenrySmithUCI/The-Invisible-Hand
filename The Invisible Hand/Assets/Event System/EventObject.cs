using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Event")]
public class EventObject : ScriptableObject {

  [System.Serializable]
  public class EventGroup {
    public EventObject eventObject;
    public string name;
    public int value;

    public EventGroup(EventObject eo, string name)
    {
            this.name = name;
            eventObject = eo;
            value = 0;
    }

    public EventGroup(EventObject eo, int value)
    {
            eventObject = eo;
            this.value = value;
            name = "";
    }
        public EventGroup(EventObject eo, int value, string name)
        {
            eventObject = eo;
            this.value = value;
            this.name = name;
        }
    }

  [System.Serializable]
  public class TextEvent {
    [Header("Resource Displays")]
    public ResourceAmount[] toDisplay;
    public ResourceAmount extraToDisplay;
        public ResourceAmount extraToDisplay2;
        [TextArea]
    public string text;

    public TextEvent(string text, ResourceAmount[] toDisplay, ResourceAmount extraToDisplay, ResourceAmount extraToDisplay2)
    {
            this.text = text;
            this.toDisplay = toDisplay;
            this.extraToDisplay = extraToDisplay;
            this.extraToDisplay2 = extraToDisplay2;
    }
  }

  [Header("Title")]
  public string title;
  [Space(1)]
  [Header("Text")]
  public TextEvent[] textEvents;
  [Space(1)]
  [Header("Effects")]
  public EventGroup[] redirectEvents;
  public EventGroup setUpNewEvent;
  public QuestObject assignedQuest;
  public ResourceAmount[] effects;
  [Space(1)]
  [Header("Prerequisites")]
  public ResourceAmount[] prerequisites;
  [Space(1)]
  [Header("Options")]
  public EventGroup[] connectedOptions;
  [Space(1)]
  [Header("Image")]
  public Color tint;
  public Sprite frontImage;
  [Space(1)]
  [Header("If there are too many quests")]
  public EventObject onTooManyQuests;
  [Space(1)]
  [Header("Ask to replace with random event")]
  public bool randomQuest;
  public bool randomSell;
  public bool randomBuy;

  public bool matchesPrerequisites() {
        if(prerequisites == null)
        {
            return true;
        }
    foreach(ResourceAmount r in prerequisites) {
      try {
        if (ResourceStorage.Instance.checkResource(r.resourceName) < r.amount) {
          return false;
        }
      }
      catch (ResourceStorage.noResourceFoundError){
        return false;
      }
    }

    return true;
  }

  public static EventObject getGenericEvent() {
    EventObject a = new EventObject();

    return a;
  }

}
