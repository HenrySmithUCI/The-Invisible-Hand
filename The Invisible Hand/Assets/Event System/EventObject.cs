using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Event")]
public class EventObject : ScriptableObject {

  [System.Serializable]
  public class EventInt {
    public EventObject eventObejct;
    public int value;
  }

  [System.Serializable]
  public class TextEvent {
    public string text;
  }

  public string title;
  public string text;
  public TextEvent[] textEvents;
  public EventInt[] redirectEvents;
  public EventInt setUpNewEvent;
  public QuestObject assignedQuest;
  public EventObject[] connectedOptions;
  public ResourceAmount[] prerequisites;
  public ResourceAmount[] effects;
  public Color tint;
  public Sprite frontImage;

  public bool matchesPrerequisites() {
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
