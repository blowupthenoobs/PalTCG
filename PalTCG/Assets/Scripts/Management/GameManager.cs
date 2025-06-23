using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Photon.Pun;
using TMPro;

using DefaultUnitData;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] GameObject CraftingMenu;
    public Sprites CardSprites;
    public PalAbilitySets PalAbilities;


    //Visuals and Confirmation
    public GameObject CardPileBox;
    public GameObject ConfirmationButtons;
    public TextMeshProUGUI TurnText; 

    //Game References

    //Game Phases
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

        CardSprites = AccountManager.Instance.CardSprites;
        PalAbilities = AccountManager.Instance.PalAbilities;

        StartPlayerTurn += PlayerPhase;
        StartPlayerTurn += () => DrawCards(2);
        StartEnemyTurn += EnemyPhase;
        StartPlayerAttack += PlayerAttackPhase;
        StartEnemyAttack += EnemyAttackPhase;

        CraftingMenu.SetActive(true);
    }

    void Start()
    {
        StartEnemyTurn += HandScript.Instance.ClearSelection;
        StartPlayerTurn += HandScript.Instance.ClearSelection;
        StartPlayerAttack += HandScript.Instance.ClearSelection;
    }

    public void LockInDeck()
    {
        TurnText.text = "Waiting for other player";
        RoomManagerScript.Instance.PlayerLockedIn();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && (phase == "PlayerTurn" || phase == "PlayerAttack")) //For Testing Purposes
            PhotonView.Get(this).RPC("SwitchPhases", RpcTarget.All);
    }

    [PunRPC]
    void SwitchPhases()
    {
        switch(phase)
        {
            case "PlayerTurn":
                StartPlayerAttack.Invoke();
                break;
            case "PlayerAttack":
                StartEnemyTurn.Invoke();
                break;
            case "EnemyTurn":
                StartEnemyAttack.Invoke();
                break;
            case "EnemyAttack":
                StartPlayerTurn.Invoke(); //Results in issues when destroy card
                break;
        }
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
        BuildingFunctions.RefreshBuildingUses();
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

    public void PickFirstPlayer()
    {
        int chance = Random.Range(0, 2);

        if(chance == 0)
        {
            StartWithPlayer();
            PhotonView.Get(this).RPC("StartWithOpponent", RpcTarget.Others);
        }
        else
        {
            StartWithOpponent();
            PhotonView.Get(this).RPC("StartWithPlayer", RpcTarget.Others);
        }
    }

    [PunRPC]
    void StartWithPlayer()
    {
        StartPlayerTurn.Invoke();
    }

    [PunRPC]
    void StartWithOpponent()
    {
        StartEnemyTurn.Invoke();
    }
#endregion
    public void DrawCards(int amount)
    {
        while(amount > 0)
        {
            HandScript.Instance.playerDrawPile.SendMessage("Draw");
            amount--;
        }
    }
}
