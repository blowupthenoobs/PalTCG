using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    //Visuals and Confirmation
    public GameObject ConfirmationButtons;

    public UnityAction StartPlayerTurn;
    public UnityAction StartEnemyTurn;
    public UnityAction StartPlayerAttack;
    public UnityAction StartEnemyAttack;
    public UnityAction EndPlayerTurn;
    public UnityAction EndEnemyTurn;

    public string phase;

    void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        HideConfirmationButtons();

        StartPlayerTurn += PlayerPhase;
        StartEnemyTurn += EnemyPhase;
        StartPlayerAttack += PlayerAttackPhase;
        StartEnemyAttack += EnemyAttackPhase;
    }

    public void HideConfirmationButtons()
    {
        ConfirmationButtons.SetActive(false);
    }

    public void ShowConfirmationButtons()
    {
        ConfirmationButtons.SetActive(true);
    }


#region GameStates
    void PlayerPhase()
    {
        phase = "PlayerTurn";
    }

    void EnemyPhase()
    {
        phase = "EnemyTurn";
    }

    void PlayerAttackPhase()
    {
        phase = "PlayerAttack";
    }

    void EnemyAttackPhase()
    {
        phase = "EnemyAttack";
    }
#endregion
}
