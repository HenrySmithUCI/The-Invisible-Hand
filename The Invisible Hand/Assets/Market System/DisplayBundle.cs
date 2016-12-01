using UnityEngine;
using System.Collections.Generic;

public class DisplayBundle : MonoBehaviour {

  public Bundle bundle;

  private RectTransform rt;

  public void Awake() {
    rt = GetComponent<RectTransform>();
  }

	public void updateDisplay() {
    List<string> keyList = new List<string>(bundle.bundle.Keys);
    for(int i = 0; i < bundle.bundle.Count && i < 4; i++) {

      UIManager.Instance.makeResourceDisplay(keyList[i],bundle.bundle[keyList[i]],new Rect(0,0,1,1),rt.GetChild(i).GetComponent<RectTransform>());
    }

    UIManager.Instance.makeResourceDisplay("Gold", bundle.totalPrice, new Rect(0, 0, 1, 1), rt.Find("Cost").GetComponent<RectTransform>());
  }
}
