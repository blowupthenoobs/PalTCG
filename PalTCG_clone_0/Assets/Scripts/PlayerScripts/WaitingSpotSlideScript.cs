using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WaitingSpotSlideScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] WaitingSpace container;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        container.hoveredSlide = gameObject;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(container.hoveredSlide == gameObject)
            container.hoveredSlide = null;
    }
}
