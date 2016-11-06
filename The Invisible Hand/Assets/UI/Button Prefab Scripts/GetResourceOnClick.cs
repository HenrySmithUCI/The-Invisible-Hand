using UnityEngine;
using System.Collections;

public class GetResourceOnClick : SetOnClick {

  public string resourceToAdd;

  protected override void action() {
    CostManager.Instance.addOneResource(resourceToAdd);
  }
}
