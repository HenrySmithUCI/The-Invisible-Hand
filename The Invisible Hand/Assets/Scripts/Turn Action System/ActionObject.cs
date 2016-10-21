using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Screen Object")]
public class ActionObject : ScriptableObject {

  [System.Serializable]
	public class ResourceAmmount {
    public string resourceName;
    public int ammount;
  }

  public ResourceAmmount[] cost;
  public ResourceAmmount[] produced;

}
