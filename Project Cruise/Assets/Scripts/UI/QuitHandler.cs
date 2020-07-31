using UnityEngine;

/// <summary>
/// Component to handle quitting the game.
/// </summary>
public class QuitHandler : MonoBehaviour
{
    /// <summary>
    /// Exits the game.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
