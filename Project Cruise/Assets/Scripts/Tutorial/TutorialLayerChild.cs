using UnityEngine;

public class TutorialLayerChild : MonoBehaviour
{
    public GameObject childLayer;

    private void OnEnable()
    {
        if(childLayer) childLayer.SetActive(true);
    }

    private void OnDisable()
    {
        if (childLayer) childLayer.SetActive(false);
    }
}
