using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RenderEvent : MonoBehaviour {
  
  public EventObject gameEvent;
    public Sprite defaultImage;

  public void renderAsTextEvent(EventObject.TextEvent te) {
    transform.FindChild("Text").GetComponent<Text>().text = te.text;
    transform.FindChild("Continue").gameObject.SetActive(true);

    for (int i = 0; i < 6; i++) {
      for (int j = 0; j < transform.FindChild("Resources").GetChild(i).childCount; j++) {
        Destroy(transform.FindChild("Resources").GetChild(i).GetChild(j).gameObject);
      }
      transform.FindChild("Resources").GetChild(i).gameObject.SetActive(false);
    }

        transform.FindChild("Resources").FindChild("Resource Extra").gameObject.SetActive(false);
        transform.FindChild("Resources").FindChild("Resource Extra 2").gameObject.SetActive(false);

        if (te.toDisplay.Length > 0) {
      for (int i = 0; i < te.toDisplay.Length && i < 4; i++) {
        transform.FindChild("Resources").FindChild("Resource " + (i + 1)).gameObject.SetActive(true);

        UIManager.Instance.makeResourceDisplay(te.toDisplay[i].resourceName,
          Mathf.FloorToInt(te.toDisplay[i].amount),
          new Rect(0, 0, 1, 1),
          transform.FindChild("Resources").FindChild("Resource " + (i + 1)).GetComponent<RectTransform>());
      }
    }

    if (te.extraToDisplay != null && te.extraToDisplay.resourceName != "") {
      transform.FindChild("Resources").FindChild("Resource Extra").gameObject.SetActive(true);
      UIManager.Instance.makeResourceDisplay(te.extraToDisplay.resourceName,
        Mathf.FloorToInt(te.extraToDisplay.amount),
        new Rect(0, 0, 1, 1),
        transform.FindChild("Resources").FindChild("Resource Extra").GetComponent<RectTransform>());
    }

        if (te.extraToDisplay2 != null && te.extraToDisplay2.resourceName != "")
        {
            transform.FindChild("Resources").FindChild("Resource Extra 2").gameObject.SetActive(true);
            UIManager.Instance.makeResourceDisplay(te.extraToDisplay2.resourceName,
              Mathf.FloorToInt(te.extraToDisplay2.amount),
              new Rect(0, 0, 1, 1),
              transform.FindChild("Resources").FindChild("Resource Extra 2").GetComponent<RectTransform>());
        }
    }

  public void renderAs(EventObject eventObject) {
    gameEvent = eventObject;
    startRender();
  }

  public int currentTextEvent;

  public void startRender() {
    for (int i = 0; i < 6; i++)
      transform.FindChild("Option " + (i + 1)).gameObject.SetActive(false);
    for (int i = 0; i < 5; i++)
      transform.FindChild("Resources").GetChild(i).gameObject.SetActive(false);
    transform.FindChild("End").gameObject.SetActive(false);
    transform.FindChild("Only Event").gameObject.SetActive(false);
    transform.FindChild("Continue").gameObject.SetActive(false);
    transform.FindChild("Title").GetComponent<Text>().text = gameEvent.title;
    if(gameEvent.frontImage != null)
    {
      transform.FindChild("Event Box").FindChild("Event Image").GetComponent<Image>().sprite = gameEvent.frontImage;
    }
        else
        {
            transform.FindChild("Event Box").FindChild("Event Image").GetComponent<Image>().sprite = defaultImage;
        }
    

    if (gameEvent.textEvents.Length > 0) {
      currentTextEvent = 0;
      nextTextEvent();
    }
    else {
      endRender();
    }
  }

  public void nextTextEvent() {
    renderAsTextEvent(gameEvent.textEvents[currentTextEvent]);
    if (currentTextEvent == gameEvent.textEvents.Length - 1) {
      endRender();
    }
    currentTextEvent += 1;
  }

  public void endRender() {
    transform.FindChild("Continue").gameObject.SetActive(false);
        //transform.FindChild("Text").GetComponent<Text>().text = gameEvent.text;
        List<EventObject.EventGroup> potentialEvents;
        if (gameEvent.connectedOptions != null)
        {
            potentialEvents = new List<EventObject.EventGroup>(gameEvent.connectedOptions);
        }
        else
        {
            potentialEvents = new List<EventObject.EventGroup>();
        }
    List<EventObject.EventGroup> availableEvents = new List<EventObject.EventGroup>();
    for (int i = 0; i < potentialEvents.Count; i++) {
      try {
        if (potentialEvents[i].eventObject.matchesPrerequisites()) {
          availableEvents.Add(potentialEvents[i]);
        }
      }
      catch(System.NullReferenceException e){
        print("Something is wrong with event " + gameEvent.ToString() + " at connected option " + i);
        throw (e);
      }
    }

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
