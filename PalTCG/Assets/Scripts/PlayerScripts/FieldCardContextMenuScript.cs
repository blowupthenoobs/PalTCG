using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FieldCardContextMenuScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static FieldCardContextMenuScript Instance;
    public int pallSkillUses;
    public GameObject activeCard;

    private bool isHovered;
    private bool switchingCard;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (!isHovered && Input.GetMouseButtonUp(0))
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
    }


    public void EjectCardFromSpot()
    {
        activeCard.SendMessage("EjectFromSpot");
    }


    private bool CanUsePalSkills()
    {
        return pallSkillUses > 0;
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
