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
    for (int i = 0; i < CostManager.Instance.availableResources.Length; i++) {
      string resource = CostManager.Instance.availableResources[i];
      RectTransform rt = Instantiate(resourceButtonPrefab).GetComponent<RectTransform>();

      rt.SetParent(this.GetComponent<RectTransform>());

      rt.anchorMin = new Vector2(0.13f, 0.83f - (i * 0.07f));
      rt.anchorMax = new Vector2(0.37f, 0.88f - (i * 0.07f));
      rt.offsetMax = new Vector2(0, 0);
      rt.offsetMin = new Vector2(0, 0);

      UIManager.Instance.makeResourceIconImage(resource, new Rect(0,0,1,1),rt.Find("Cost Text").Find("Icon").GetComponent<RectTransform>());
      rt.Find("Cost Text").GetComponentInChildren<Text>().text = string.Format("{0}G", CostManager.Instance.priceTable[i].amount);

      rt.Find("Buy Button").GetComponent<GetResourceOnClick>().resourceAmount = new ResourceAmount(resource, 1);
      rt.Find("Sell Button").GetComponent<GetResourceOnClick>().resourceAmount = new ResourceAmount(resource, -1);
    }
  }

  void makeBundleButtons() {

  }
}
