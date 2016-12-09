using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RenderEvent : MonoBehaviour {
  
  public EventObject gameEvent;

  public void renderAsTextEvent(EventObject.TextEvent te) {
    transform.FindChild("Text").GetComponent<Text>().text = te.text;
    transform.FindChild("Continue").gameObject.SetActive(true);
  }

  public void renderAs(EventObject eventObject) {
    gameEvent = eventObject;
    startRender();
  }

  public int currentTextEvent;

  public void startRender() {
    for (int i = 0; i < 6; i++)
      transform.FindChild("Option " + (i + 1)).gameObject.SetActive(false);
    transform.FindChild("End").gameObject.SetActive(false);
    transform.FindChild("Only Event").gameObject.SetActive(false);
    transform.FindChild("Continue").gameObject.SetActive(false);
    transform.FindChild("Title").GetComponent<Text>().text = gameEvent.title;
    transform.FindChild("Event Box").FindChild("Event Image").GetComponent<Image>().sprite = gameEvent.frontImage;

    if (gameEvent.textEvents.Length > 0) {
      currentTextEvent = 0;
      renderAsTextEvent(gameEvent.textEvents[currentTextEvent]);
    }
    else {
      endRender();
    }
  }

  public void nextTextEvent() {
    currentTextEvent += 1;
    if (gameEvent.textEvents.Length > currentTextEvent) {
      renderAsTextEvent(gameEvent.textEvents[currentTextEvent]);
    }
    else {
      endRender();
    }

  }

  public void endRender() {
    transform.FindChild("Continue").gameObject.SetActive(false);
    transform.FindChild("Text").GetComponent<Text>().text = gameEvent.text;

    List<EventObject.EventGroup> availableEvents = new List<EventObject.EventGroup>(gameEvent.connectedOptions);
    availableEvents = availableEvents.FindAll(x => x.eventObject.matchesPrerequisites());

    if (availableEvents.Count == 1) {
      transform.FindChild("Only Event").gameObject.SetActive(true);
      string buttonText = availableEvents[0].name;
      if (availableEvents[0].name == "") {
        buttonText = availableEvents[0].eventObject.title;
      }
      transform.FindChild("Only Event").GetComponentInChildren<Text>().text = buttonText;
      transform.FindChild("Only Event").GetComponent<ChangeEventOnClick>().eventObject = availableEvents[0].eventObject;
    }
    else if (availableEvents.Count > 1) {
      for (int i = 0; i < 6; i++) {
        GameObject go = transform.FindChild("Option " + (i + 1)).gameObject;
        if (i < availableEvents.Count) {
          go.SetActive(true);
          go.GetComponent<ChangeEventOnClick>().eventObject = availableEvents[i].eventObject;
          string buttonText = availableEvents[i].name;
          if (availableEvents[i].name == "") {
            buttonText = availableEvents[i].eventObject.title;
          }
          go.GetComponentInChildren<Text>().text = buttonText;
        }
      }
    }
    else {
      transform.FindChild("End").gameObject.SetActive(true);
    }
    
  }
}
