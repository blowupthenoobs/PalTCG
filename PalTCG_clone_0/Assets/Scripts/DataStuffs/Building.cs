using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using DefaultUnitData;
public class Building : ScriptableObject
{
    public string buildingType;
    public Sprite cardArt;
    public Image image;
    public GameObject gameObject;
    public UnityAction buildingFunction;
    public float timeToHold;


    private void SetUpBuilding(string name, GameObject holder)
    {
        buildingType = name;
        gameObject = holder;
        image = gameObject.GetComponent<Image>();
        var data = Pals.GetBuildingInfo(buildingType);

        cardArt = data.cardArt;
        image.sprite = cardArt;
    }
}
