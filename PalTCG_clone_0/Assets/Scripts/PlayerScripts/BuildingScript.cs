using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    public Building data;
    private float heldTime;

    void Update()
    {
        if(Input.GetMouseButtonUp(0))
            heldTime = 0;
    }

    public void Click()
    {
        if(data.timeToHold == 0)
            data.buildingFunction.Invoke();
        else
        {

        }
    }
}
