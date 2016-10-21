using UnityEngine;
using System.Collections;

public class Screen : MonoBehaviour {

  public ScreenObject[] options;
  public Button buttonPrefab;
  
  private Color currentColor;
  private string currentText;
  private ScreenObject[] currentButtons;

  private TextMesh childText;
  private MeshRenderer childRenderer;

  void Start() {
    childText = GetComponentInChildren<TextMesh>();
    childRenderer = GetComponentInChildren<MeshRenderer>();
    ScreenObject sc = UpdateScreenObject(options[Random.Range(0,options.Length)]);
  }

  public ScreenObject UpdateScreenObject(ScreenObject newCurrentScreenObject) {
    currentColor = newCurrentScreenObject.background;
    currentText = newCurrentScreenObject.text;
    currentButtons = newCurrentScreenObject.screensToConnectTo;

    UpdateLook();
    return newCurrentScreenObject;
  }

  void UpdateLook() {
    childText.text = currentText;
    childRenderer.material.color = currentColor;

    for (int i = 0; i < transform.childCount; i++) {
      if (transform.GetChild(i).tag == "Button") {
        Destroy(transform.GetChild(i).gameObject);
      }
    }



    for (int i = 0; i < currentButtons.Length; i++) {
      Button newButton = Instantiate(buttonPrefab);
      newButton.transform.parent = transform;
      newButton.transform.localPosition = new Vector3(-3, i * -1.5f, -1);
      newButton.screenToGoTo = currentButtons[i];
      newButton.text = i.ToString();
    }
  }
}
