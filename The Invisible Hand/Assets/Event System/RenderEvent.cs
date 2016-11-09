using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RenderEvent : MonoBehaviour {
  
  public EventObject gameEvent;

	void Start () {
    renderAs(gameEvent);
  }

  public void renderAs(EventObject eventObject) {
    transform.FindChild("Title").GetComponent<Text>().text = eventObject.title;
    transform.FindChild("Front Image").GetComponent<Image>().sprite = eventObject.frontImage;
    transform.FindChild("Text").GetComponent<Text>().text = eventObject.text;

    for (int i = 0; i < 6; i++) {
      GameObject go = transform.FindChild("Option " + (i + 1)).gameObject;
      if (i < eventObject.connectedOptions.Length) {
        go.SetActive(true);
        go.GetComponent<ChangeEventOnClick>().eventObject = eventObject.connectedOptions[i];
        go.GetComponentInChildren<Text>().text = eventObject.connectedOptions[i].title;
      }
      else {
        go.SetActive(false);
      }
    }
  }
}
