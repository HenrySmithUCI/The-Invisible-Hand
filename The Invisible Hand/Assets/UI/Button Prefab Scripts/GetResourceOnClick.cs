using UnityEngine;
using System.Collections;

//is called to change the particular resource amount by one
public class GetResourceOnClick : SetOnClick {

  public string resourceToAdd;

  protected override void action() {
    CostManager.Instance.addOneResource(resourceToAdd);
  }
}
