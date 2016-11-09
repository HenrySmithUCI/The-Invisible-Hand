using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MakeMarketUI : MonoBehaviour {

  public CanvasRenderer resourceButtonPrefab;
  public CanvasRenderer bundleButtonPrefab;

	void Start () {
    makeResourceButtons();
    makeBundleButtons();
	}

  void makeResourceButtons() {
    for (int i = 0; i < CostManager.Instance.priceTable.Length; i++) {
      RectTransform rt = Instantiate(resourceButtonPrefab).GetComponent<RectTransform>();

      rt.SetParent(this.GetComponent<RectTransform>());

      rt.anchorMin = new Vector2(0.15f, 0.83f - (i * 0.07f));
      rt.anchorMax = new Vector2(0.4f, 0.88f - (i * 0.07f));
      rt.offsetMax = new Vector2(0, 0);
      rt.offsetMin = new Vector2(0, 0);

      rt.FindChild("Cost Text").GetComponentInChildren<Text>().text = string.Format("{0} : {1}G", CostManager.Instance.priceTable[i].resourceName, CostManager.Instance.priceTable[i].amount);

      rt.FindChild("Buy Button").GetComponent<GetResourceOnClick>().resourceAmount = CostManager.Instance.priceTable[i];
      rt.FindChild("Sell Button").GetComponent<GetResourceOnClick>().resourceAmount = new ResourceAmount(CostManager.Instance.priceTable[i].resourceName, CostManager.Instance.priceTable[i].amount * -1);
    }
  }

  void makeBundleButtons() {

  }
}
