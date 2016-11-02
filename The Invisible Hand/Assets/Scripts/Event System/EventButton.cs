using UnityEngine;
using System.Collections;

//Buttons to be used in the event system
public class EventButton : MonoBehaviour {

  //when this button is clicked, this is the scene it will go to
  public ScreenObject screenToGoTo;
  //text displayed on button
  public string text;

  //The mesh where the button's text will be displayed
  private TextMesh buttonText;

  //runs when button is first created
  void Start() {
    //gets a reference to the button's text
    buttonText = GetComponentInChildren<TextMesh>();

    UpdateLook();
  }

  //makes the button look right
  public void UpdateLook() {
    //sets the text of the button to be whatever it's supposed to be
    buttonText.text = text;
  }

  //called when the button is clicked
  void OnMouseDown() {
    //transition from current screen to next screen
	  transform.parent.GetComponent<EventScreen>().UpdateScreenObject(screenToGoTo);
  }
}

//note to self: don't get SCREEN and SCENE mixed up. They're different
