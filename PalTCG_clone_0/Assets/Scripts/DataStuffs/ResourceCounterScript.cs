using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using Resources;
using DefaultUnitData;
public class ResourceCounterScript : MonoBehaviour
{
    [SerializeField] GameObject IconSpot;
    [SerializeField] GameObject IconPrefab;
    [SerializeField] TMP_Text counter;
    private FieldInfo heldValueType;
    private Color normalColor;
    [SerializeField] Color negativeColor;

    void Awake()
    {
        normalColor = counter.color;
    }

    public void GetHeldField(string fieldName)
    {
        heldValueType = typeof(resources).GetField(fieldName);

        var heldIcon = Instantiate(IconPrefab, IconSpot.transform.position, transform.rotation);
        heldIcon.transform.SetParent(IconSpot.transform);
        heldIcon.GetComponent<RecipeItemIconScript>().SetIcon(Pals.GetIconSprite(fieldName), 0);

        ShowNormalValue();
    }

    public void ShowNormalValue()
    {
        counter.text = ((int)heldValueType.GetValue(HandScript.Instance.GatheredItems)).ToString();
        counter.color = normalColor;
    }

    public void ShowAlteredValue(resources alteration)
    {
        int newValue = ((int)heldValueType.GetValue(HandScript.Instance.GatheredItems) + (int)heldValueType.GetValue(alteration));
        counter.text = newValue.ToString();

        if (newValue < 0)
            counter.color = negativeColor;
    }
}
