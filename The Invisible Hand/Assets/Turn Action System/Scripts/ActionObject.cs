using UnityEngine;
using System.Collections;

<<<<<<< HEAD
//[CreateAssetMenu(fileName = "Screen Object")]
=======
[CreateAssetMenu(fileName = "Screen Object")]
//creates the dynamically changing list corresponding to the resources that is produced and expended
>>>>>>> origin/master
public class ActionObject : ScriptableObject {
  public ResourceAmount[] cost;
  public ResourceAmount[] produced;

}
