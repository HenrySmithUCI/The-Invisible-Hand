using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CostManager : Singleton<CostManager> {

  protected CostManager() { }

  public ResourceAmount[] priceTable;
  public string[] availableResources;

  public float getPrice(string resource) {
    foreach (ResourceAmount re in priceTable) {
      if (re.resourceName == resource) {
        return re.amount;
      }
    }
    throw new System.Exception("Cannot get price of " + resource + "because it does not exist yet!");
  }

  public void exchangeResource(ResourceAmount resourceAmount) {
    ResourceStorage.Instance.addResource(resourceAmount.resourceName, resourceAmount.amount);
  }
}
