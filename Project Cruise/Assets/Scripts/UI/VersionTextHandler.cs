using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VersionTextHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        GetComponent<TMP_Text>().text = "version " + Application.version;
    }
}
