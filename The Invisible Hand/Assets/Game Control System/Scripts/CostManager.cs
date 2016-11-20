using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CostManager : Singleton<CostManager> {

  protected CostManager() { }

  //initializes an array of the class ResourceAmount
  public ResourceAmount[] priceTable;
  public List<string> availableResources;

  //returns the total price of a resource
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
  
  //increments the specificed resource by one
  public void addOneResource(string resource) {
    ResourceStorage.Instance.addResource(resource, 1);
  }
}
