using UnityEngine;
using System.Collections.Generic;

public class BundleManager : Singleton<BundleManager> {

  protected BundleManager() { }

  public List<Bundle> availableBundles;
  public List<Bundle> toBuy;

  public void Awake() {
    availableBundles = new List<Bundle>();
    toBuy = new List<Bundle>();
  }

  public bool isBuyable(Bundle bundle) {
    return bundle.totalPrice <= ResourceStorage.Instance.checkResource("Gold");
  }

  public void generateBundles() {
    availableBundles.Clear();
    toBuy.Clear();
    int turn = PhaseManager.Instance.Turn;
    int numOfBundles = getNumberOfBundles(turn);
    for (int i = 0; i < numOfBundles; i++) {
      availableBundles.Add(new Bundle(getResourceList(turn, i), getMinPrice(turn, i)));
    }
  }

  public void setToBuy(Bundle bundle) {
    if (isBuyable(bundle)) {
      toBuy.Add(bundle);
      ResourceStorage.Instance.addResource("Gold", -bundle.totalPrice);
    }
  }

  public void undoBuy(Bundle bundle) {
    if (toBuy.Contains(bundle)) {
      toBuy.Remove(bundle);
      ResourceStorage.Instance.addResource("Gold", bundle.totalPrice);
    }
  }

  public void completeAllBuys() {
    foreach(Bundle bundle in toBuy) {
      completeBuyBundle(bundle);
    }
  }

  public void completeBuyBundle(Bundle bundle) {
    foreach(string key in bundle.bundle.Keys) {
      ResourceStorage.Instance.addResource(key, bundle.bundle[key]);
    }
  }

  public List<string> getResourceList(int turn, int bundleNumber) {
    if (turn == 0 && bundleNumber == 0) {
      string[] retArray = { "Wood" };
      return new List<string>(retArray);
    }

    int num = Random.Range(2, Mathf.Min(5,CostManager.Instance.availableResources.Count + 1));

    List<string> ret = new List<string>(CostManager.Instance.availableResources);
    ret = Shuffle.shuffle<string>(ret);
    
    return ret.GetRange(0,num);
  }

  public int getNumberOfBundles(int turn) {
    if (turn == 0) {
      return 2;
    }


    return CostManager.Instance.availableResources.Count;
  }

  public int getMinPrice(int turn, int bundleNumber) {
    if (turn == 0 && bundleNumber == 0) {
      return 20;
    }

    return Random.Range(30, 100);
  }
}
