using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckIconButtonScript : MonoBehaviour
{
    [HideInInspector] public DeckBuildingManagerScript manager;
    [HideInInspector] public int indexNumber;

    public void Click()
    {
        manager.SendMessage("SetUpDeckBuilder", indexNumber);
    }
}
