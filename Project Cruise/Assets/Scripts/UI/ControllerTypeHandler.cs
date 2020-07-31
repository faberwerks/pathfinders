using UnityEngine;

/// <summary>
/// Component to handle the control type.
/// </summary>
public class ControllerTypeHandler : MonoBehaviour
{
    public GameObject[] diagonalArrows;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("ClassicController", 0) == 1 ? true : false)
        {
            foreach (var arrow in diagonalArrows)
            {
                arrow.SetActive(false);
            }
        }
    }
}
