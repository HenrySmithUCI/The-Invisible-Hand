using UnityEngine;
using System.Collections;

[System.Serializable]

//constructs the class that keeps track of the resource's name and amount
public class ResourceAmount {
  public string resourceName;
  public float amount;

  public ResourceAmount() { }

  public ResourceAmount(string resourceName, float amount) {
    this.resourceName = resourceName;
    this.amount = amount;
  }
}
