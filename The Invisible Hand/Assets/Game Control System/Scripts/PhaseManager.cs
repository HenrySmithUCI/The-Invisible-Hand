using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class PhaseManager : Singleton<PhaseManager> {

  protected PhaseManager() { }

  //initializes the string startPhase
  public string startPhase;
  private int turn;

  //on start the scene is set based on the given startPhase string
  public void Start() {
    SceneManager.sceneLoaded += delegate { UIManager.Instance.changeScene(SceneManager.GetActiveScene().name); };
    changePhase(startPhase);
  }
  
  //loads the scene specified by the given input
  public static void changePhase(string phase) {
    SceneManager.LoadScene(phase);
  }

  public void nextTurn() {
    turn++;
    UIManager.Instance.updateTurn();
  }

  public int Turn { get { return turn; } }
}
