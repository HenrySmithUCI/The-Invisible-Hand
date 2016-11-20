using UnityEngine;
using System.Collections;

public class ChangeEventOnClick : SetOnClick {

  public EventObject eventObject;

  protected override void action() {
    GameObject.Find("Event UI(Clone)").transform.FindChild("Event Body").GetComponent<RenderEvent>().renderAs(eventObject);
  }
}
