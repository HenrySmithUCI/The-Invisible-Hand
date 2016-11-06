using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CostManager : Singleton<CostManager> {

  protected CostManager() { }

  public Dictionary<string, int> priceTable;

  public void addOneResource(string resource) {
    ResourceStorage.Instance.addResource(resource, 1);
  }
}
