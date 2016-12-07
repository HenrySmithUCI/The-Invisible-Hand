using UnityEngine;
using System.Collections;

//class in charged of changing the scene given the corresponding scene name
public class SetSceneOnClick : SetOnClick {

  public string sceneName;

	protected override void action() {
    PhaseManager.Instance.changePhase(sceneName);
  }
}
