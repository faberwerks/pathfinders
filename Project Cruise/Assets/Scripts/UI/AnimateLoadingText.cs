using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimateLoadingText : MonoBehaviour
{
    public float delay = 0.5f;

    private TMP_Text loadingText;
    // Start is called before the first frame update
    void Start()
    {
        loadingText = GetComponent<TMP_Text>();
        loadingText.text = "Loading";

        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        while (true)
        {
            if (loadingText.text != "Loading...")
            {
                loadingText.text += ".";
            }
            else
            {
                loadingText.text = "Loading";
            }
            
            yield return new WaitForSeconds(delay);
        }
    }
}
