using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreditsButtonOnClick : SetOnClick {

    bool menuOpen = false;
    public RectTransform creditsText;

	protected override void action()
    {
        if (menuOpen)
        {
            creditsText.gameObject.SetActive(false);
            menuOpen = false;
            transform.GetChild(0).GetComponent<Text>().text = "Credits";
        }
        else
        {
            creditsText.gameObject.SetActive(true);
            menuOpen = true;
            transform.GetChild(0).GetComponent<Text>().text = "Back";
        }
    }
}
