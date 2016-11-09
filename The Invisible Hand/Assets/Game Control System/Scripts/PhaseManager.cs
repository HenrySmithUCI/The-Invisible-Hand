using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class PhaseManager : Singleton<PhaseManager> {

  public string startPhase;
  private int turn;

  public void Start() {
    changePhase(startPhase);
    SceneManager.sceneLoaded += delegate { UIManager.Instance.changeScene(SceneManager.GetActiveScene().name); };
  }

  public static void changePhase(string phase) {
    SceneManager.LoadScene(phase);
  }

  public void nextTurn() {
    turn++;
    UIManager.Instance.updateTurn();
  }

  public int Turn { get { return turn; } }
}
