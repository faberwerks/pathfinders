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
        CheckControllerType();
    }

    public void CheckControllerType()
    {
        // diagonalArrowSetting is set to TRUE  if using Default Controller (thus need to have the diagonal arrows)
        //                 else is set to FALSE if using Classic Controller (no need for the diagonal arrows)

        //                             If Classic Controller is used, the PlayerPref will return 1
        var diagonalArrowSetting = PlayerPrefs.GetInt("ClassicController", 0) == 1 ? false : true;

        foreach (var arrow in diagonalArrows)
        {
            arrow.SetActive(diagonalArrowSetting);
        }
    }
}
