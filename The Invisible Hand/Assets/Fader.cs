using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Fader : MonoBehaviour
{

    public AnimationCurve fadeCurve;
    public Image fadeImage;
    public float fadeInTime;
    public float fadeOutTime;

    public IEnumerator fadeOut()
    {
        fadeImage.gameObject.SetActive(true);
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / fadeOutTime;
            Color c = fadeImage.color;
            c.a = fadeCurve.Evaluate(t);
            fadeImage.color = c;
            yield return 0;
        }
    }

    private IEnumerator fadeIn()
    {
        float t = 1f;

        while (t > 0f)
        {
            t -= Time.deltaTime / fadeInTime;
            Color c = fadeImage.color;
            c.a = fadeCurve.Evaluate(t);
            fadeImage.color = c;
            yield return 0;
        }
        fadeImage.gameObject.SetActive(false);
    }
}
