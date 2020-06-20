using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RelicNotification : MonoBehaviour
{
    public Image relicImage;
    public TMP_Text relicText;

    public float duration = 3.0f;

    private void OnEnable()
    {
        StartCoroutine(CFadeOutImage());
        StartCoroutine(CFadeOutText());
    }

    private IEnumerator CFadeOutImage()
    {
        Color colour = relicImage.color;
        float startOpacity = relicImage.color.a;

        float t = 0;

        while (t < duration)
        {
            t += Time.deltaTime;

            float blend = Mathf.Clamp01(t / duration);

            colour.a = Mathf.Lerp(startOpacity, 0.0f, blend);

            relicImage.color = colour;

            yield return null;
        }
    }

    private IEnumerator CFadeOutText()
    {
        Color colour = relicText.color;
        float startOpacity = relicText.color.a;

        float t = 0;

        while (t < duration)
        {
            t += Time.deltaTime;

            float blend = Mathf.Clamp01(t / duration);

            colour.a = Mathf.Lerp(startOpacity, 0.0f, blend);

            relicText.color = colour;

            yield return null;
        }
    }
}
