using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Resources;
public class BuildingScript : MonoBehaviour
{
    public BuildingData data;
    private float heldTime;

    public static Traits totalTraits = new Traits
    (
        ranch: 0,
        handyWork: 1,
        foraging: 1,
        gardening: 1,
        watering: 1,
        mining: 1,
        lumber: 1,
        transportation: 1,
        medicine: 0,
        kindling: 0,
        electric: 0,
        freezing: 0,
        dragon: 0,
        bird: false,
        blocker: false,
        tank: false
    );

    void Awake()
    {
        data = (BuildingData)ScriptableObject.CreateInstance(typeof(BuildingData));
    }

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
