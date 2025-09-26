using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

using DefaultUnitData;
public class PalCardScript : UnitCardScript
{
    public bool palSKillActive;

    public void PlaceOnPalSphere()
    {
        var palSphere = HandScript.Instance.selection[0];

        palSphere.SendMessage("PlaceCard", gameObject);
        GameManager.Instance.HideConfirmationButtons();
        HandScript.Instance.state = "default";

        HandScript.Instance.selected = null;
        HandScript.Instance.selection = new List<GameObject>();

        if(GameManager.Instance.phase == "playerTurn")
            PrepareMainPhase();
        else if (GameManager.Instance.phase == "playerAttack")
            PrepareAttackPhase();
    }

    public void ReadyToBePlaced()
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(LookForPalSphere);
    }

    void LookForPalSphere()
    {
        if(HandScript.Instance.state == "default")
        {
            GameManager.Instance.ShowConfirmationButtons("place on sphere?");
            HandScript.Instance.state = "lookingForSphere";
            HandScript.Instance.Duck();
            HandScript.Instance.updateSelection = VerifyButtons;
            ConfirmationButtons.Instance.Confirmed += PlaceOnPalSphere;
            ConfirmationButtons.Instance.Confirmed += AllowConfirmations.ClearButtonEffects;
            ConfirmationButtons.Instance.Denied += StopLookingForSphere;
            ConfirmationButtons.Instance.Denied += AllowConfirmations.ClearButtonEffects;

            VerifyButtons();
        }
    }

    void StopLookingForSphere()
    {
        HandScript.Instance.state = "default";
    }

    public void PrepareMainPhase()
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(MainPhaseClick);
    }

    private void MainPhaseClick()
    {
        if(HandScript.Instance.state == "default" && FieldCardContextMenuScript.Instance.activeCard != gameObject)
        {
            OpenContextMenu(gameObject);
        }
    }

    public override void GiveCardEventActions()
    {
        base.GiveCardEventActions();
        StartPlayerTurn += PrepareMainPhase;

        if(GameManager.Instance.phase == "PlayerTurn")
            PrepareMainPhase();
    }

    void VerifyButtons()
    {
        ConfirmationButtons.Instance.AllowConfirmation(SphereSelected());
    }

    bool SphereSelected()
    {
        if(HandScript.Instance.selection.Count == 1)
            return true;
        else
            return false;
    }

    public override void SetUpBasicTurnEvents()
    {
        base.SetUpBasicTurnEvents();
        StartPlayerTurn += ResetPalSkill;
    }

    public void UsePalSkill()
    {
        Pals.palSkill[((PalCardData)cardData).palSkill].Invoke();
    }

    public override bool CanUsePalSkills()
    {
        return !palSKillActive && FieldAbilityHandlerScript.CanUseSpecificPalSkill(gameObject) && Pals.palSkill.ContainsKey(((PalCardData)cardData).palSkill); //Last part is just so u can't activate non-existent PalSkills
    }

    public void ResetPalSkill()
    {
        palSKillActive = false;
    }

    public void ActivatePalSkill()
    {
        palSKillActive = true;
        FieldCardContextMenuScript.Instance.pallSkillUses--;
    }

    protected override void Die()
    {
        transform.parent.gameObject.SendMessage("BreakSphere");
        base.Die();
    }

    public void EjectFromSpot()
    {
        SendToPalBox();
    }

}
