using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceStorage : Singleton<ResourceStorage> {

  protected ResourceStorage() { }
  
  public List<ResourceAmount> storage;

  public void addResource(string name, int amount) {
    foreach(ResourceAmount resource in storage) {
      if (resource.resourceName == name) {
        resource.amount += amount;
        UIManager.Instance.updateResources();
        return;
      }
    }
    ResourceAmount newResource = new ResourceAmount();
    newResource.resourceName = name;
    newResource.amount = amount;
    storage.Add(newResource);
    UIManager.Instance.updateResources();
  }

  public class noResourceFoundError : System.Exception { }

  public int checkResource(string name) {
    foreach (ResourceAmount resource in storage) {
      if (resource.resourceName == name) {
        return resource.amount;
      }
    }

    throw new noResourceFoundError();
  }

  public string StorageString() {
    string ret = "";

    foreach (ResourceAmount resource in storage) {
      ret += resource.resourceName + ":" + resource.amount + " ";
    }

    return ret;
  }
}
