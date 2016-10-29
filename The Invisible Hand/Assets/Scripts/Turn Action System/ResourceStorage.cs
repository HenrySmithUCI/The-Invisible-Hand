using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceStorage : MonoBehaviour {

  
  public List<ResourceAmount> storage;

  void Start() {
    storage = new List<ResourceAmount>();
  }

  public void addResource(string name, int amount) {
    foreach(ResourceAmount resource in storage) {
      if (resource.resourceName == name) {
        resource.amount += amount;
        return;
      }
    }
    ResourceAmount newResource = new ResourceAmount();
    newResource.resourceName = name;
    newResource.amount = amount;
    storage.Add(newResource);
    print("New resource added: " + name);
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

  public void printStorage() {
    foreach (ResourceAmount resource in storage) {
      print("(" + resource.resourceName + ", " + resource.amount + ")");
    }
  }
}
