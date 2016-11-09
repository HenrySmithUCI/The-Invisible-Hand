using UnityEngine;
using System.Collections;

public class GetResourceOnClick : SetOnClick {

  public ResourceAmount resourceAmount;

  protected override void action() {
    CostManager.Instance.exchangeResource(resourceAmount);
  }
}
