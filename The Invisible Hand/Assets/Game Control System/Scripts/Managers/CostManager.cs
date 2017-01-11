using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CostManager : Singleton<CostManager> {

  protected CostManager() { }

  //initializes an array of the class ResourceAmount
  public List<ResourceAmount> staticPriceTable;
  public ResourceAmount[] priceTable;
  public List<string> availableResources;

  //returns the total price of a resource
  public float getPrice(string resource) {
    foreach (ResourceAmount re in priceTable) {
      if (re.resourceName == resource) {
        return re.amount;
      }
    }
    throw new System.Exception("Cannot get price of " + resource + " because it does not exist yet!");
  }
    private string[] resources = { "Wood", "Stone", "Steel", "Cloth", "Leather", "Apple", "Herb", "Fairy", "Gem" };
    public void preliminaryUpdate()
    {
        for (int i = 0; i < staticPriceTable.Count; i++)
        {
            ResourceStorage.Instance.addResource(priceTable[i].resourceName + " Price", 1);
        }
    }

  public void updateCosts() {
    
    resources = resources.Except(availableResources).ToArray();

    foreach (string s in resources) {
      try {
        if(ResourceStorage.Instance.checkResource(s + " Available" ) > 0 && !availableResources.Contains(s)) {
          availableResources.Add(s);
        }
      }
      catch (ResourceStorage.noResourceFoundError) {
        continue;
      }
    }

    for (int i = 0; i < staticPriceTable.Count; i++) {
      priceTable[i].amount = staticPriceTable.Find(resource => resource.resourceName == priceTable[i].resourceName).amount;
      priceTable[i].amount *= ResourceStorage.Instance.checkResource(priceTable[i].resourceName + " Price");
    }
    
  }

  public void exchangeResource(ResourceAmount resourceAmount) {
    if (resourceAmount.amount > 0) {
      if (ResourceStorage.Instance.checkResource("Gold") >= getPrice(resourceAmount.resourceName) * resourceAmount.amount) {
        ResourceStorage.Instance.addResource(resourceAmount.resourceName, resourceAmount.amount);
        ResourceStorage.Instance.addResource("Gold", -1 * getPrice(resourceAmount.resourceName) * resourceAmount.amount);
      }
    }
    else {
      if (ResourceStorage.Instance.checkResource(resourceAmount.resourceName) >= -1 * resourceAmount.amount) {
        ResourceStorage.Instance.addResource(resourceAmount.resourceName, resourceAmount.amount);
        ResourceStorage.Instance.addResource("Gold", -1 * getPrice(resourceAmount.resourceName) * resourceAmount.amount);
      }
    }
  }
  
  //increments the specificed resource by one
  public void addOneResource(string resource) {
    ResourceStorage.Instance.addResource(resource, 1);
  }
}
