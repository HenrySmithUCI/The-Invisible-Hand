using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//**not really sure what this does
public class SetOnClick : MonoBehaviour {

  private Button button;

	void Start () {
    button = GetComponent<Button>();
    button.onClick.AddListener(delegate { action(); });
	}

  protected virtual void action() { }

}
