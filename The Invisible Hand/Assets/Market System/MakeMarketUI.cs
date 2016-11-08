using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MakeMarketUI : MonoBehaviour {

  public CanvasRenderer resourceButtonPrefab;
  public CanvasRenderer bundleButtonPrefab;

  private RectTransform rectTransform;

	void Start () {
    print("hoi");
    rectTransform = GetComponent<RectTransform>();

    makeResourceButtons();
    makeBundleButtons();
	}

  void makeResourceButtons() {
    for (int i = 0; i < CostManager.Instance.priceTable.Length; i++) {
      RectTransform rt = Instantiate(resourceButtonPrefab).GetComponent<RectTransform>();
      rt.SetParent(this.GetComponent<RectTransform>());
      rt.anchoredPosition = Vector2.zero;
      rt.anchorMin = new Vector2(0.15f, 0.8f - (i * 0.07f));
      rt.anchorMax = new Vector2(0.4f, 0.85f - (i * 0.07f));

      rt.GetComponentInChildren<Text>().text = CostManager.Instance.priceTable[i].resourceName;
      rt.GetComponent<GetResourceOnClick>().resourceToAdd = CostManager.Instance.priceTable[i].resourceName;
    }
  }

  void makeBundleButtons() {

  }
}
