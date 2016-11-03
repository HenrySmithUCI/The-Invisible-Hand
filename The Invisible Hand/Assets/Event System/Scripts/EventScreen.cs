using UnityEngine;
using System.Collections;

public class EventScreen : MonoBehaviour {

  //list of screens this screen will choose from randomly upon play. This will be removed later
  public ScreenObject[] options;
  //prefab for the button to instantiate from
  public EventButton buttonPrefab;
  
  //current background color
  private Color currentColor;
  //current string at the top of the screen
  private string currentText;
  //a list of screens this screen can connect to. will be transformed into buttons later
  private ScreenObject[] currentButtons;

  //text this screen will render on
  private TextMesh childText;
  //background this screen will render on
  private MeshRenderer childRenderer;

  //runs when screen is created
  void Start() {
    childText = GetComponentInChildren<TextMesh>();
    childRenderer = GetComponentInChildren<MeshRenderer>();
    //ScreenObject sc = UpdateScreenObject(options[Random.Range(0,options.Length)]);
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
      EventButton newButton = Instantiate(buttonPrefab);
      newButton.transform.parent = transform;
      newButton.transform.localPosition = new Vector3(-3, i * -1.5f, -1);
      newButton.screenToGoTo = currentButtons[i];
      newButton.text = i.ToString();
    }
  }
}
