using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class PhaseManager : Singleton<PhaseManager> {

  public void Start() {
    changePhase("Main Menu");
  }

  public void changePhase(string phase) {
    SceneManager.LoadScene(phase);
    UIManager.Instance.changeScene(phase);
  }
}
