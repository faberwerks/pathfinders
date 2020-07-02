using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackHandler : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex == 1)
        {
            Application.Quit();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex == 5)
        {
            SceneManager.LoadScene(1);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex < 4 && SceneManager.GetActiveScene().buildIndex != 0)
        {
            SceneManager.LoadScene(GameData.Instance.previousSceneID);
        }
    }
}
