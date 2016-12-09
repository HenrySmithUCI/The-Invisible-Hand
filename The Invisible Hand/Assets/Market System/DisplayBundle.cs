using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DisplayBundle : MonoBehaviour {

  public Bundle bundle;

  private RectTransform rt;
  private RectTransform buyButton;

  public void Awake() {
    rt = GetComponent<RectTransform>();
    buyButton = rt.Find("Buy Button").GetComponent<RectTransform>();
  }

  public void makeDisplay() {
    List<string> keyList = new List<string>(bundle.bundle.Keys);

    for (int i = 0; i < bundle.bundle.Count && i < 4; i++) {

      UIManager.Instance.makeResourceDisplay(keyList[i], bundle.bundle[keyList[i]], new Rect(0, 0, 1, 1), rt.GetChild(i).GetComponent<RectTransform>());
    }

    UIManager.Instance.makeResourceDisplay("Gold", bundle.totalPrice, new Rect(0, 0, 1, 1), rt.Find("Cost").GetComponent<RectTransform>());
    UIManager.Instance.makeResourceDisplay("Gold", Mathf.CeilToInt(bundle.getEffectivePrice()), new Rect(0, 0, 1, 1), rt.Find("Discount").GetComponent<RectTransform>());

    buyButton.GetComponent<BuyBundleOnClick>().bundle = bundle;
  }

	public void updateDisplay() {
    GameObject check = transform.Find("Check Box").Find("Check").gameObject;

    if (BundleManager.Instance.toBuy.Contains(bundle)) {
      check.SetActive(true);
      buyButton.GetComponentInChildren<Text>().text = "Bought";
    }
    else {
      check.SetActive(false);
      if (BundleManager.Instance.isBuyable(bundle)) {
        buyButton.GetComponentInChildren<Text>().text = "Buy";
        buyButton.GetComponent<Button>().interactable = true;
      }
      else {
        buyButton.GetComponentInChildren<Text>().text = "Too Expensive";
        buyButton.GetComponent<Button>().interactable = false;
      }
    }
  }
}
