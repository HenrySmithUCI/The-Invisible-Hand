using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Quest Object")]
public class QuestObject : ScriptableObject {

  public string description;
  public ResourceAmount[] cost;
  public ResourceAmount[] reward;
}
