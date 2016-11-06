using UnityEngine;
using System.Collections;

public class TurnManager : Singleton<TurnManager> {

  protected TurnManager() { }

  private int turn;

	public void nextTurn() {
    turn++;
    UIManager.Instance.updateTurn();
  }

  public int Turn { get { return turn; } }
}
