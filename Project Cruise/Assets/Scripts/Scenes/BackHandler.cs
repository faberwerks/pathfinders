using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackHandler : MonoBehaviour
{
    // Update is called once per frame
    public GameObject QuitCanvas;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex == 1)
        {
            //Application.Quit();
            QuitCanvas.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex == 4)
        {
            SceneManager.LoadScene(3);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex < 4 && SceneManager.GetActiveScene().buildIndex != 0)
        {
            SceneManager.LoadScene(1);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex > 4)
        {
            Blackboard.Instance.LevelManager.Pause(true);
            Blackboard.Instance.LevelManager.pauseCanvas.SetActive(true);
        }
    }
}
