using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ResourceTicker : MonoBehaviour {
  
  public int displaySpots = 10;
    public float buffer = 0.01f;

	public void destroyThenCreateResources() {
    for(int i = 0; i < transform.childCount; i++) {
      Destroy(transform.GetChild(i).gameObject);
    }
        string[] resources = { "Apple", "Wood", "Cloth", "Herb", "Stone", "Leather", "Steel", "Fairy", "Gem" };
        List<string> toDisplay = new List<string>(resources);
        //List<string> toDisplay = new List<string>(CostManager.Instance.availableResources);
        toDisplay.Insert(0, "Gold");

    for(int i = 0; i < toDisplay.Count && i < displaySpots; i++) {
      int amount = Mathf.FloorToInt(ResourceStorage.Instance.checkResource(toDisplay[i]));
      Rect anchorPos = new Rect((i * (1f / displaySpots)) + buffer, 0.05f, ((1f / displaySpots) - buffer), 0.9f);
            //Rect anchorPos = new Rect(i * ((1f / displaySpots) + buffer), 0.05f, ((1f / displaySpots)), 0.9f);
            //print(toDisplay[i]);
            UIManager.Instance.makeResourceDisplay(toDisplay[i], amount, anchorPos, GetComponent<RectTransform>());
            //rd.display = true;
            //rd.updateDisplay();
    }
  }
}
