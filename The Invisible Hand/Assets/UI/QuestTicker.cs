using UnityEngine;
using System.Collections;

public class QuestTicker : MonoBehaviour {

  public QuestDisplay questPrefab;

	public void updateDisplay() {
    int maxQuests = QuestManager.Instance.maxQuests;

    for(int i = 0; i < transform.childCount; i++) {
      Destroy(transform.GetChild(i).gameObject);
    }

    for (int i = 0; i < QuestManager.Instance.currentQuests.Count; i++) {
      QuestDisplay qd = Instantiate(questPrefab);
      qd.quest = QuestManager.Instance.currentQuests[i];
      Rect r = new Rect();
      r.xMin = 0;
      r.yMin = (1f - (1f / maxQuests)) - ((1f / maxQuests) * i);
      r.xMax = 1;
      r.yMax = 1 - ((1f / maxQuests) * i);

      UIManager.Instance.seatInside(this.GetComponent<RectTransform>(), qd.GetComponent<RectTransform>(), r);
      qd.UpdateDisplay();
    }
  }
}
