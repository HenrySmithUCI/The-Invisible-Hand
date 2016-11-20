using UnityEngine;
using System.Collections;

<<<<<<< HEAD:The Invisible Hand/Assets/Event System/Old/Scripts/ScreenObject.cs
//[CreateAssetMenu(fileName = "Screen Object [deprecated]")]
=======
[CreateAssetMenu(fileName = "Screen Object")]
//constructs the class ScreenObject that inherits the class ScriptableObject with the variables background/text/screens initialized
>>>>>>> origin/master:The Invisible Hand/Assets/Event System/Scripts/ScreenObject.cs
public class ScreenObject : ScriptableObject {

  public Color background;
  public string text;
  public ScreenObject[] screensToConnectTo;

}
