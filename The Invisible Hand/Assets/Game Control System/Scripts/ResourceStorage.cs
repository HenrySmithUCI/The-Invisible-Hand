﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceStorage : Singleton<ResourceStorage> {

  protected ResourceStorage() { }
  
  //initializes a variable called storage that is a list containg variables of class ResourceAmount
  public List<ResourceAmount> storage;

  //changes a certain resource's amount in the storage by the specified amount input.
  public void addResource(string name, float amount) {
    foreach(ResourceAmount resource in storage) {
      if (resource.resourceName == name) {
        resource.amount += amount;
        UIManager.Instance.updateAll();
        return;
      }
    }
  //**if the resource is not initialized this will create a newResource and adds to the storage list
    ResourceAmount newResource = new ResourceAmount();
    newResource.resourceName = name;
    newResource.amount = amount;
    storage.Add(newResource);
    UIManager.Instance.updateAll();
  }

  //initializes the exception which is raised when a specified resource is not found
  public class noResourceFoundError : System.Exception { }

  //returns amount for each resource in storage
  public float checkResource(string name) {
    foreach (ResourceAmount resource in storage) {
      if (resource.resourceName == name) {
        return resource.amount;
      }
    }

        //throw new noResourceFoundError();
        return 0;
  }

  public bool isResourceStored(string value) {
    try {
      checkResource(value);
      return true;
    }
    catch (noResourceFoundError) {
      return false;
    }
  }

  //returns a string for each resource type in storage to the following '{name} : {amount}'
  public string StorageString() {
    string ret = "";

    foreach (ResourceAmount resource in storage) {
      ret += resource.resourceName + ":" + resource.amount + " ";
    }

    return ret;
  }
}
