using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ResourceTicker : MonoBehaviour {
  
  public int displaySpots = 10;

	public void destroyThenCreateResources() {
    for(int i = 0; i < transform.childCount; i++) {
      Destroy(transform.GetChild(i).gameObject);
    }

    List<string> toDisplay = new List<string>(CostManager.Instance.availableResources);
    toDisplay.Insert(0, "Gold");

    for(int i = 0; i < toDisplay.Count && i < displaySpots; i++) {
      int amount = Mathf.FloorToInt(ResourceStorage.Instance.checkResource(toDisplay[i]));
      Rect anchorPos = new Rect((float)i * (1f / (float)displaySpots), 0f, (1f / (float)displaySpots), 1f);

      UIManager.Instance.makeResourceDisplay(toDisplay[i], amount, anchorPos, this.GetComponent<RectTransform>()).updateDisplay();
    }
  }

  public void updateResources() {
    for(int i = 0; i < transform.childCount; i++) {
      transform.GetChild(i).GetComponent<ResourceDisplay>().updateDisplay();
    }
  }
}
