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
    for (int i = 0; i < CostManager.Instance.availableResources.Count; i++) {
      string resource = CostManager.Instance.availableResources[i];
      RectTransform rt = Instantiate(resourceButtonPrefab).GetComponent<RectTransform>();

      Vector2 trCorner = new Vector2(0.13f, 0.87f);
      Vector2 rectSize = new Vector2(0.33f, 0.07f);
      float spacer = 0.02f;

      Rect r = new Rect();
      r.xMin = trCorner.x;
      r.yMin = (trCorner.y - rectSize.y) - (i * (spacer + rectSize.y));
      r.xMax = trCorner.x + rectSize.x;
      r.yMax = trCorner.y - (i * (spacer + rectSize.y));

      UIManager.Instance.seatInside(this.GetComponent<RectTransform>(), rt, r);

      RectTransform rdReceive = rt.Find("Cost Text").Find("Receive").GetComponent<RectTransform>();
      RectTransform rdGive = rt.Find("Cost Text").Find("Give").GetComponent<RectTransform>();

      UIManager.Instance.makeResourceDisplay(resource, 1, new Rect(0,0,1,1), rdReceive);
      UIManager.Instance.makeResourceDisplay("Gold", Mathf.CeilToInt(CostManager.Instance.getPrice(resource)), new Rect(0, 0, 1, 1), rdGive);

      rt.Find("Buy Button").GetComponent<GetResourceOnClick>().resourceAmount = new ResourceAmount(resource, 1);
      rt.Find("Sell Button").GetComponent<GetResourceOnClick>().resourceAmount = new ResourceAmount(resource, -1);
      rt.name = resource + " Buttons";
    }

    for (int i = 0; i < CostManager.Instance.availableResources.Count; i++) {
      string resource = CostManager.Instance.availableResources[i];
      RectTransform rt = transform.Find(resource + " Buttons").GetComponent<RectTransform>();
      rt.Find("Buy Button").GetComponent<Button>().interactable = ResourceStorage.Instance.checkResource("Gold") >= CostManager.Instance.getPrice(resource);
      rt.Find("Sell Button").GetComponent<Button>().interactable = ResourceStorage.Instance.checkResource(resource) >= 1;
    }
  }

  public void updateVisuals() {
    for (int i = 0; i < CostManager.Instance.availableResources.Count; i++) {
      string resource = CostManager.Instance.availableResources[i];
      RectTransform rt = transform.Find(resource + " Buttons").GetComponent<RectTransform>();
      rt.Find("Buy Button").GetComponent<Button>().interactable = ResourceStorage.Instance.checkResource("Gold") >= CostManager.Instance.getPrice(resource);
      rt.Find("Sell Button").GetComponent<Button>().interactable = ResourceStorage.Instance.checkResource(resource) >= 1;
    }
    
    for (int i = 0; i < BundleManager.Instance.availableBundles.Count; i++) {
      transform.Find("Bundle " + i).GetComponent<DisplayBundle>().updateDisplay();
    }
  }

  void makeBundleButtons() {
    for(int i = 0; i < BundleManager.Instance.availableBundles.Count; i++) {
      RectTransform rt = Instantiate(bundleButtonPrefab).GetComponent<RectTransform>();

      Vector2 trCorner = new Vector2(0.50f, 0.87f);
      float spacer = 0.02f;
      Vector2 rectSize = new Vector2(0.2f, 0.15f);
      int bundlesPerRow = 5;
      if (i < bundlesPerRow) {
        trCorner += new Vector2(0f,-(i * (spacer + rectSize.y)));
      }
      else {
        trCorner += new Vector2(rectSize.x + spacer,-((i - bundlesPerRow) * (spacer + rectSize.y)));
      }

      Rect r = new Rect();
      r.xMin = trCorner.x;
      r.yMin = trCorner.y - rectSize.y;
      r.xMax = trCorner.x + rectSize.x;
      r.yMax = trCorner.y;

      UIManager.Instance.seatInside(this.GetComponent<RectTransform>(), rt, r);
      rt.GetComponent<DisplayBundle>().bundle = BundleManager.Instance.availableBundles[i];
      rt.GetComponent<DisplayBundle>().makeDisplay();
      rt.GetComponent<DisplayBundle>().updateDisplay();
      rt.name = "Bundle " + i;
    }
  }
}
