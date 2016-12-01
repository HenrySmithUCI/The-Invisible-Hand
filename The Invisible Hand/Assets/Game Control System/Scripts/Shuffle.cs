using UnityEngine;
using System.Collections.Generic;

public class Shuffle : MonoBehaviour {

  public static List<T> shuffle<T>(List<T> list) //shuffles a list
    {
    List<T> taken = new List<T>(list);
    for (int i = 0; i < list.Count; i++) {
      int k = UnityEngine.Random.Range(0, taken.Count);
      list[i] = taken[k];
      taken.Remove(taken[k]);

    }
    return list;
  }
}
