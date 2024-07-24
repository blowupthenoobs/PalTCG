using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    //Visuals and Confirmation
    public GameObject ConfirmationButtons;

    void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        HideConfirmationButtons();
    }

    public void HideConfirmationButtons()
    {
        ConfirmationButtons.SetActive(false);
    }

    public void ShowConfirmationButtons()
    {
        ConfirmationButtons.SetActive(true);
    }
}
