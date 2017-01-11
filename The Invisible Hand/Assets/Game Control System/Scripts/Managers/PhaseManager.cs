using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PhaseManager : Singleton<PhaseManager> {

  protected PhaseManager() { }

  //initializes the string startPhase
  public string startPhase;
  public AnimationCurve fadeCurve;
  public Image fadeImage;
  public float fadeInTime;
  public float fadeOutTime;
  private int turn;

  //on start the scene is set based on the given startPhase string
  public void Start() {
    SceneManager.sceneLoaded += delegate { UIManager.Instance.changeScene(SceneManager.GetActiveScene().name); };
    SceneManager.sceneLoaded += delegate { StartCoroutine(this.fadeIn(SceneManager.GetActiveScene().name)); };
    turn = 0;
        updateData();
    if (fadeImage.gameObject.activeInHierarchy == false) {
      fadeImage.gameObject.SetActive(true);
    }
    SceneManager.LoadScene(startPhase);
  }
  
  //loads the scene specified by the given input
  public void changePhase(string phase) {
    if (phase == "Market Scene") {
      CostManager.Instance.updateCosts();
      BundleManager.Instance.generateBundles();
    }
    StartCoroutine(this.fadeOut(phase));
  }

  public IEnumerator fadeOut(string phase) {
    fadeImage.gameObject.SetActive(true);
    float t = 0f;

    while(t < 1f) {
      t += Time.deltaTime / fadeOutTime;
      Color c = fadeImage.color;
      c.a = fadeCurve.Evaluate(t);
      fadeImage.color = c;
      yield return 0;
    }

    SceneManager.LoadScene(phase);
  }

  public IEnumerator fadeIn(string phase) {
    float t = 1f;

    while (t > 0f) {
      t -= Time.deltaTime / fadeInTime;
      Color c = fadeImage.color;
      c.a = fadeCurve.Evaluate(t);
      fadeImage.color = c;
      yield return 0;
    }

    fadeImage.gameObject.SetActive(false);
  }

  public void updateData() {
        CostManager.Instance.updateCosts();
    BundleManager.Instance.completeAllBuys();
    EventManager.Instance.makeCurrentEventList(turn);
    QuestManager.Instance.updateQuests();
    UIManager.Instance.updateTurn();
    }

  public void nextTurn() {
    turn++;
    updateData();
    changePhase(startPhase);
  }

  public int Turn { get { return turn; } }
}
