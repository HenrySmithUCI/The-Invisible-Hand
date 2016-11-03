using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class PhaseManager : MonoBehaviour {

  public void changePhase(string phase) {
    SceneManager.LoadScene(phase);
  }
}
