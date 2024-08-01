using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PalCardScript : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] Resources.StatusEffects statuses;
    public CardData cardData;

    void Awake()
    {
        button = GetComponent<Button>();
    }



    public void ReadyToBePlaced()
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(LookForPalSphere);
    }

    void LookForPalSphere()
    {

    }
    
    public void PlaceOnPalSphere()
    {

    }

}
