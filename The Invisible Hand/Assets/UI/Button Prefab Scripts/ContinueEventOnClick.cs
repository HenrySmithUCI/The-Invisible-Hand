using UnityEngine;
using System.Collections;

public class ContinueEventOnClick : SetOnClick {

  protected override void action() {
    GameObject.Find("Event UI(Clone)").transform.FindChild("Event Body").GetComponent<RenderEvent>().nextTextEvent();
  }
}
