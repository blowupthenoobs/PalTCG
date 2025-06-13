using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        CraftingMenuScript.Instance.ShowAlteredItemValues();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CraftingMenuScript.Instance.ShowCurrentItemValues();
    }
}
