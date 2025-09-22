using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FieldCardContextMenuScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static FieldCardContextMenuScript Instance;
    public int pallSkillUses;
    public GameObject activeCard;

    [SerializeField] GameObject PalSkillButton;
    [SerializeField] GameObject ActivatedAbilityButton;
    [SerializeField] GameObject BootFromSlotButton;

    private bool isHovered;
    private bool switchingCard;

    void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        GameManager.Instance.StartPlayerTurn += ResetPalSkills;
    }

    void Update()
    {
        if(!isHovered && Input.GetMouseButtonUp(0))
            CloseContextMenu();
    }

    private void CloseContextMenu()
    {
        if(!switchingCard)
        {
            activeCard = null;
            gameObject.SetActive(false);
        }
    }

    public void OpenContextMenu(GameObject spot, GameObject activator)
    {
        transform.position = spot.transform.position;
        activeCard = activator;
        gameObject.SetActive(true);

        StartCoroutine(CancelDissapear());

        PalSkillButton.GetComponent<Button>().interactable = CanUsePalSkills();
        ActivatedAbilityButton.GetComponent<Button>().interactable = activeCard.GetComponent<UnitCardScript>().CanUseAbilities();
        BootFromSlotButton.GetComponent<Button>().interactable = activeCard.GetComponent<UnitCardScript>().CanBeBooted();
    }



    public void AskToEjectCard()
    {
        GameManager.Instance.ShowConfirmationButtons("Eject card?");
        HandScript.Instance.state = "awaitingDecision";
        ConfirmationButtons.Instance.Confirmed += () => EjectCardFromSpot(activeCard);
        ConfirmationButtons.Instance.Confirmed += AllowConfirmations.ResetState;
        ConfirmationButtons.Instance.Confirmed += AllowConfirmations.ClearButtonEffects;
        ConfirmationButtons.Instance.Denied += AllowConfirmations.ResetState;
        ConfirmationButtons.Instance.Denied += AllowConfirmations.ClearButtonEffects;
        ConfirmationButtons.Instance.AllowConfirmation(true);
    }

    public void EjectCardFromSpot(GameObject activator)
    {
        activator.SendMessage("EjectFromSpot");
    }

    public void ActivateCardAbilities()
    {
        StartCoroutine(activeCard.GetComponent<UnitCardScript>().UseActivatedAbility());
    }

    public void ActivatePalSkillAbilities()
    {
        activeCard.GetComponent<PalCardScript>().UsePalSkill();
    }

    private bool CanUsePalSkills()
    {
        return (pallSkillUses > 0 && activeCard.GetComponent<UnitCardScript>().CanUsePalSkills());
    }

    private void ResetPalSkills()
    {
        pallSkillUses = 1;
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
    }

    private IEnumerator CancelDissapear()
    {
        switchingCard = true;
        yield return new WaitForSeconds(.1f);
        switchingCard = false;
    }
}
