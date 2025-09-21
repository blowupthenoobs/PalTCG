using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

using DefaultUnitData;
public class EnemyPlayerScript : MonoBehaviour
{
    public PhotonView opponentMirror;
    protected Image image;
    public Color normalColor; //Probably Temp
    public Color targetColor;
    public TMP_Text health;

    void Awake()
    {
        image = gameObject.GetComponent<Image>();
    }

    [PunRPC]
    public void SetUpCard(string characterArt)
    {
        string[] dataParts = characterArt.Split("/");
        image.sprite = Pals.LookForPlayerArt(dataParts[1]);
    }

    public void SelectAsTarget()
    {
        if(HandScript.Instance.state == "targeting")
        {
            GameManager.Instance.ShowConfirmationButtons();
            HandScript.Instance.state = "raiding";
            HandScript.Instance.selection.Add(HandScript.Instance.selected);
            HandScript.Instance.selected = gameObject;
            Select();
            HandScript.Instance.updateSelection += VerifyAttack;
            ConfirmationButtons.Instance.Confirmed += StartRaid;
            ConfirmationButtons.Instance.Confirmed += () => HandScript.Instance.StartCoroutine(HandScript.Instance.Attack());
            ConfirmationButtons.Instance.Denied += DisengageAttacks;
            ConfirmationButtons.Instance.Denied += HandScript.Instance.ClearSelection;
        }
        else if(HandScript.Instance.state == "settingAilment")
        {
            HandScript.Instance.Select(gameObject);
        }
    }

    void DisengageAttacks()
    {

        HandScript.Instance.updateSelection -= VerifyAttack;
        ConfirmationButtons.Instance.Confirmed = null;
        ConfirmationButtons.Instance.Denied = null;
        GameManager.Instance.HideConfirmationButtons();
        Deselect();

        HandScript.Instance.state = "choosingAttack";
    }

    void StartRaid()
    {
        HandScript.Instance.updateSelection -= VerifyAttack;
        ConfirmationButtons.Instance.Confirmed = null;
        ConfirmationButtons.Instance.Denied = null;
        GameManager.Instance.HideConfirmationButtons();
        Deselect();

        HandScript.Instance.state = "choosingAttack";
    }

    public void VerifyAttack()
    {
        ConfirmationButtons.Instance.AllowConfirmation(HandScript.Instance.selection.Count > 0);
    }

    public void Select()
    {
        image.color = targetColor;
    }

    public void Deselect()
    {
        image.color = normalColor;
    }

    [PunRPC]
    public void Hurt(int damage)
    {
        opponentMirror.RPC("HurtHeldCard", RpcTarget.Others, damage);
    }

    [PunRPC]
    public void HurtHeldCard(int damage)
    {
        opponentMirror.RPC("HurtHeldCard", RpcTarget.Others, damage);
    }

    [PunRPC]
    public void Rest()
    {
        transform.rotation = Quaternion.Euler(0, 0, -90);
    }

    [PunRPC]
    public void Wake()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void AfterBlockActions()
    {
        opponentMirror.RPC("AfterBlockActions", RpcTarget.Others);
    }

    [PunRPC]
    public void UpdateHealth(int newHealth)
    {
        health.text = newHealth.ToString();
    }

    public void SendRestEffect()
    {
        opponentMirror.RPC("EffectRest", RpcTarget.Others);
    }

    [PunRPC]
    public void Block()
    {
        HandScript.Instance.blocker = gameObject;
    }

    [PunRPC]
    public void OpponentDeath()
    {
        Debug.Log("You win! :)");
    }

    [PunRPC]
    public void NormalRest()
    {
        Rest();
    }
    
    [PunRPC]
    public virtual void PlayerTurnRemoveStatuses()
    {
        // statuses.shocked.Clear();

        // if(statuses.poisoned > 0)
        //     statuses.poisoned--;
    }

    [PunRPC]
    public virtual void EnemyTurnRemoveStatuses()
    {
        // if(statuses.poisoned > 0)
        //     statuses.poisoned--;
        
        // statuses.burning = 0;
    }
}
