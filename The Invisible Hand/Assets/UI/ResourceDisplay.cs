using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResourceDisplay : MonoBehaviour {

  public string resource;
  public int amount;
  public bool display;

  private RectTransform icon;
  private RectTransform count;

  void Awake () {
    //This errors for some reason but only once? so this just pervents the console from getting cluttered.
    //The error dosent seem to change anything?
    try {
      icon = GetComponent<RectTransform>().Find("Icon").GetComponent<RectTransform>();
      count = GetComponent<RectTransform>().Find("Amount").GetComponent<RectTransform>();
    }
    catch {
      //Yeah, IDK
    }
	}

  public void updateDisplay() {
    hide();
    if (display) {
      icon.gameObject.SetActive(true);
      count.gameObject.SetActive(true);

      UIManager.Instance.makeResourceIconImage(resource, new Rect(0, 0, 1, 1), icon);
      count.GetComponent<Text>().text = amount.ToString();
    }
  }

  public void hide() {
    if (icon.childCount > 0) {
      GameObject.Destroy(icon.GetChild(0).gameObject);
    }

    icon.gameObject.SetActive(false);
    count.gameObject.SetActive(false);
  }
}
