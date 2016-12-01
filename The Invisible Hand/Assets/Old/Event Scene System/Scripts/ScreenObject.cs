using UnityEngine;
using System.Collections;

//[CreateAssetMenu(fileName = "Screen Object")]
//constructs the class ScreenObject that inherits the class ScriptableObject with the variables background/text/screens initialized
public class ScreenObject : ScriptableObject {

  public Color background;
  public string text;
  public ScreenObject[] screensToConnectTo;

}
