using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Screen Object")]
//creates the dynamically changing list corresponding to the resources that is produced and expended
public class ActionObject : ScriptableObject {
  public ResourceAmount[] cost;
  public ResourceAmount[] produced;

}
