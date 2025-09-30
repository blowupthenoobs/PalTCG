using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    public bool GameStarted;
    public bool GameEnded;

    [SerializeField] Color normalColor;
    [SerializeField] Color winColor;
    [SerializeField] Color loseColor;
    [SerializeField] GameObject BackToGameButton;

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

    public void WinGame()
    {
        GameEnded = true;
        BackToGameButton.SetActive(false);
        gameObject.GetComponent<Image>().color = winColor;
        gameObject.SetActive(true);
    }

    public void LoseGame()
    {
        GameEnded = true;
        BackToGameButton.SetActive(false);
        gameObject.GetComponent<Image>().color = loseColor;
        gameObject.SetActive(true);
    }
}
