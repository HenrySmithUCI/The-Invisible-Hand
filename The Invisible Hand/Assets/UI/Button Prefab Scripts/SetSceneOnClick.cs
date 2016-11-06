using UnityEngine;
using System.Collections;

public class SetSceneOnClick : SetOnClick {

  public string sceneName;

	protected override void action() {
    PhaseManager.changePhase(sceneName);
  }
}
