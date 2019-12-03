using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Component to manage level buttons.
/// </summary>
public class LevelButtonManager : MonoBehaviour
{
    public List<GameObject> levelButtons = new List<GameObject>();

    // Start is called before the first frame update
    private void Start()
    {
        AddDescendantsWithTag(transform, "Level Button", levelButtons);
        for (int i = GameData.Instance.saveData.LastLevelNumber; i < levelButtons.Count; i++)
        {
            levelButtons[i].GetComponent<Button>().interactable = false;
        }
    }

    /// <summary>
    /// Adds all of parent's descendants with the specified tag to the list.
    /// </summary>
    /// <param name="parent">Parent game object.</param>
    /// <param name="tag">Tag to filter search.</param>
    /// <param name="list">List to add children to.</param>
    private void AddDescendantsWithTag(Transform parent, string tag, List<GameObject> list)
    {
        foreach (Transform child in parent)
        {
            if (child.gameObject.CompareTag(tag))
            {
                list.Add(child.gameObject);
            }
            AddDescendantsWithTag(child, tag, list);
        }
    }
}
