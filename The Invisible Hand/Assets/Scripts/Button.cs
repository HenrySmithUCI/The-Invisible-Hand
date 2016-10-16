using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

  public ScreenObject screenToGoTo;
  public string text;
  public Screen parentScreen;

  private TextMesh buttonText;

  void Start() {
    buttonText = GetComponentInChildren<TextMesh>();
    UpdateLook();
  }

  public void UpdateLook() {
    buttonText.text = text;
  }

  void OnMouseDown() {
    parentScreen.UpdateScreenObject(screenToGoTo);
  }
}
