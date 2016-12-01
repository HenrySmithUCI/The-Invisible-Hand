using UnityEngine;
using System.Collections;

public class NextEventOnClick : SetOnClick {

  protected override void action() {
    EventManager.Instance.nextEvent();
  }
}
