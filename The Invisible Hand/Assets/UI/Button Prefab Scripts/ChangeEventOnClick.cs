using UnityEngine;
using System.Collections;

public class ChangeEventOnClick : SetOnClick {

  public EventObject eventObject;

  protected override void action() {
    EventManager.Instance.activateEvent(eventObject);
  }
}
