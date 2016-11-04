using UnityEngine;
using System.Collections;

public class CostManager : Singleton<CostManager> {

  protected CostManager() { }

  public void addOneResource(string resource) {
    ResourceStorage.Instance.addResource(resource, 1);
  }
}
