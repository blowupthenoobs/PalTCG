using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

using Resources;
public class BuildingScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image progressBar;
    public BuildingData data;
    private float heldTime;
    private bool isHoveringUI;
    private bool heldClick;


    public static Traits totalTraits;

    void Awake()
    {
        data = (BuildingData)ScriptableObject.CreateInstance(typeof(BuildingData));
    }

    void Update()
    {
        heldClick = Input.GetMouseButton(0);
        if(isHoveringUI && heldClick)
            heldTime = Mathf.MoveTowards(heldTime, data.timeToHold, Time.deltaTime);
        else
            heldTime = Mathf.MoveTowards(heldTime, 0, Time.deltaTime);

        progressBar.fillAmount = (float)(heldTime / data.timeToHold);

        if((heldTime >= data.timeToHold) && (data.timeToHold > 0))
        {
            heldTime = 0;
            data.buildingFunction.Invoke();
        }
    }

    public void Click()
    {
        if(data.timeToHold == 0)
            data.buildingFunction.Invoke();
        else
        {
            if(data.timeToHold > heldTime)
                heldTime += Time.deltaTime;
            else
            {
                heldTime = 0;
                data.buildingFunction.Invoke();
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHoveringUI = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHoveringUI = false;
    }
}
