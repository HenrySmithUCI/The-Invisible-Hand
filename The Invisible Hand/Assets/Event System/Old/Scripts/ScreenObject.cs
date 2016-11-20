using UnityEngine;
using System.Collections;

//[CreateAssetMenu(fileName = "Screen Object [deprecated]")]
public class ScreenObject : ScriptableObject {

  public Color background;
  public string text;
  public ScreenObject[] screensToConnectTo;

}
