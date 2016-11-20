using UnityEngine;
using System.Collections;

public class NextTurnOnClick : SetOnClick {

  protected override void action() {
    PhaseManager.Instance.nextTurn();
  }
}
