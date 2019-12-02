using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A wrapper component for the Reset Progress method.
/// </summary>
public class ResetProgressHandler : MonoBehaviour
{
    /// <summary>
    /// Resets save data.
    /// </summary>
    public void ResetProgress()
    {
        GameData.Instance.saveData.ResetProgress();
    }
}
