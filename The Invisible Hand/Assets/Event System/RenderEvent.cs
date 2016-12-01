using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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

    if (gameEvent.connectedOptions.Length > 0) {
      for (int i = 0; i < 6; i++) {
        GameObject go = transform.FindChild("Option " + (i + 1)).gameObject;
        if (i < gameEvent.connectedOptions.Length) {
          go.SetActive(true);
          go.GetComponent<ChangeEventOnClick>().eventObject = gameEvent.connectedOptions[i];
          go.GetComponentInChildren<Text>().text = gameEvent.connectedOptions[i].title;
        }
      }
    }
    else {
      transform.FindChild("End").gameObject.SetActive(true);
    }
    
  }
}
