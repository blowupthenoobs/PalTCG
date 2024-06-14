using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    [SerializeField] CardData cardData;
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        cardData.Awake();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
