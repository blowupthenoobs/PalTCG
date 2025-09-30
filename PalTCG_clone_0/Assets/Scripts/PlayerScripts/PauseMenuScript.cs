using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public bool GameStarted;
    public bool GameEnded;

    public void OpenPauseMenu()
    {
        gameObject.SetActive(true);
    }

    public void ClosePauseMenu()
    {
        gameObject.SetActive(false);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
