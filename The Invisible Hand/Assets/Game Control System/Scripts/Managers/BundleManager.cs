using UnityEngine;
using System.Collections.Generic;

public class BundleManager : Singleton<BundleManager> {

  protected BundleManager() { }

  public List<Bundle> availableBundles;

  public void Start() {
    availableBundles = new List<Bundle>();
    BundleManager.Instance.generateBundles();
  }

  public void generateBundles() {
    availableBundles.Clear();
    int turn = PhaseManager.Instance.Turn;
    int numOfBundles = getNumberOfBundles(turn);
    for (int i = 0; i < numOfBundles; i++) {
      availableBundles.Add(new Bundle(getResourceList(turn), getMinPrice(turn)));
    }
  }

  public List<string> getResourceList(int turn) {
    int num = Random.Range(2, 5);

    List<string> ret = new List<string>(CostManager.Instance.availableResources);
    ret = Shuffle.shuffle<string>(ret);
    
    return ret.GetRange(0,num);
  }

  public int getNumberOfBundles(int turn) {
    return 5;
  }

  public int getMinPrice(int turn) {
    return Random.Range(10, 1500);
  }
}
