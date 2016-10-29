using UnityEngine;
using System.Collections;

public class TurnActionButton : MonoBehaviour {

  public ActionObject action;
  public ResourceStorage storage;

  public void OnMouseDown() {

    try {
      foreach (ResourceAmount resource in action.cost) {
        if (storage.checkResource(resource.resourceName) < resource.amount) {
          print("Not enough " + resource.resourceName);
          return;
        }
      }
    }
    catch(ResourceStorage.noResourceFoundError){
      print("One or more resources are not available yet");
      return;
    }

    foreach (ResourceAmount resource in action.cost) {
      storage.addResource(resource.resourceName, resource.amount * -1);
      print("Resource removed: " + resource.resourceName + " " + resource.amount);
    }

    foreach(ResourceAmount resource in action.produced) {
      storage.addResource(resource.resourceName, resource.amount);
      print("Resource gained: " + resource.resourceName + " " + resource.amount);
    }

    storage.printStorage();
  }
}
