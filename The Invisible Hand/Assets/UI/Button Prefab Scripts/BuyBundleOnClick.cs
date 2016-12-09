using UnityEngine;
using System.Collections;

public class BuyBundleOnClick : SetOnClick {

  public Bundle bundle;

  protected override void action() {
    if (BundleManager.Instance.toBuy.Contains(bundle)) {
      BundleManager.Instance.undoBuy(bundle);
    }
    else {
      BundleManager.Instance.setToBuy(bundle);
    }
  }
}
