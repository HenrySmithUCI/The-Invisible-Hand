using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Event")]
public class EventObject : ScriptableObject {

  public string title;
  public string text;
  public EventObject[] connectedOptions;
  public ResourceAmount[] prerequisites;
  public ResourceAmount[] effects;
  public Color tint;
  public Sprite frontImage;

}
