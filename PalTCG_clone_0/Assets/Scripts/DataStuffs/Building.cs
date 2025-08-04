using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using Resources;
using DefaultUnitData;
public class BuildingData : ScriptableObject
{
    public string buildingType;
    public resources cost;
    public Sprite cardArt;
    public Image image;
    public GameObject gameObject;
    public UnityAction buildingFunction;
    public float timeToHold;


    public void SetUpBuilding(string name, GameObject holder)
    {
        gameObject = holder;
        image = gameObject.GetComponent<Image>();
        SetUpData(name);
        image.sprite = cardArt;
    }

    public void SetUpData(string name)
    {
        buildingType = name;
        var data = Pals.GetBuildingInfo(buildingType);

        cardArt = data.cardArt;
        buildingFunction += data.buildingFunction;
        timeToHold = data.timeToHold;
    }

    public void SetToGameObject(GameObject holder)
    {
        gameObject = holder;
        image = gameObject.GetComponent<Image>();
        image.sprite = cardArt;
    }
}
