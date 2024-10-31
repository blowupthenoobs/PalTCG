using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

using DefaultUnitData;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Sprites CardSprites = new Sprites();
    public PalAbilitySets PalAbilities = new PalAbilitySets();


    //Visuals and Confirmation
    public GameObject ConfirmationButtons;
    public TextMeshProUGUI TurnText; 

    public UnityAction StartPlayerTurn;
    public UnityAction StartEnemyTurn;
    public UnityAction StartPlayerAttack;
    public UnityAction StartEnemyAttack;
    public UnityAction EndPlayerTurn;
    public UnityAction EndEnemyTurn;
    public UnityAction testAction;

    public string phase = "PlayerTurn";

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

    void Start()
    {
        PickFirstPlayer();
        StartEnemyTurn += HandScript.Instance.ClearSelection;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) //For Testing Purposes
            TestModes();
    }

    void TestModes()
    {
        if(phase == "PlayerTurn")
            StartPlayerAttack.Invoke();
        else
            StartPlayerTurn.Invoke();
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
        HandScript.Instance.state = "default";
        phase = "PlayerTurn";
        TurnText.text = phase;
    }

    void EnemyPhase()
    {
        phase = "EnemyTurn";
        HandScript.Instance.state = "";
        TurnText.text = phase;
    }

    void PlayerAttackPhase()
    {
        phase = "PlayerAttack";
        HandScript.Instance.state = "choosingAttack";
        TurnText.text = phase;
    }

    void EnemyAttackPhase()
    {
        phase = "EnemyAttack";
        TurnText.text = phase;
    }

    void PickFirstPlayer()
    {
        int chance = Random.Range(0, 2);

        if(chance == 0)
            StartPlayerTurn.Invoke();
        else
            StartEnemyTurn.Invoke();
    }
#endregion
}
