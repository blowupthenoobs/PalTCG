using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandScript : MonoBehaviour
{
    [SerializeField] GamePreferences Preferences;

    public static HandScript Instance;

    public GameObject selected;
    private GameObject target;

    public new List<GameObject> Hand = new List<GameObject>();

    void Awake()
    {
        if(Instance == null)
            Instance=this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        RemoveIndexes();
        CenterCards();

        if(Input.GetButtonDown("Fire1"))
            Click();
    }
    public void Select(GameObject card)
    {
        selected = card;
    }

    // public void TargetEnemy(GameObject clicked)
    // {
    //     if(selected != null)
    //         if(selected.GetComponent<ManaCardScript>().targetEnemy)
    //             Attack(clicked);
    // }

    // public void TargetTeam(GameObject clicked)
    // {
    //     if(selected != null)
    //         if(!selected.GetComponent<ManaCardScript>().targetEnemy)
    //             UseSkill(clicked);
    // }

    public void Click()
    {
        if(selected != null)
        {
            selected.SendMessage("Deselect");
            selected = null;
        }
    }

    // public void Attack(GameObject target)
    // {
    //     selected.SendMessage("Effect", target);

    //     Hand.Remove(selected);
    //     selected.SendMessage("Discard");
    // }

    // public void UseSkill(GameObject target)
    // {
    //     selected.SendMessage("Effect", target);
        
    //     selected.SendMessage("Discard");
    // }

    public void CenterCards()
    {
        float spacing = Preferences.spacing * ScreenCalculations.GetScale(gameObject);

        float speed = Preferences.cardMoveSpeed * ScreenCalculations.GetScale(gameObject);

        for(int i = 0; i < Hand.Count; i++)
        {
            RectTransform rectTransform = Hand[i].GetComponent<RectTransform>();
            Vector3 newPosition = rectTransform.localPosition;
            newPosition.x = spacing * ((i+1) - (float)(Hand.Count + 1) / 2 );
            newPosition.x = Mathf.Lerp(Hand[i].GetComponent<RectTransform>().localPosition.x, newPosition.x, speed * Time.deltaTime);
            rectTransform.localPosition = newPosition;
        }
    }

    public void Draw(GameObject newCard)
    {
        Hand.Add(newCard);

        newCard.transform.SetParent(gameObject.transform);

        RectTransform rectTransform = newCard.GetComponent<RectTransform>();
        Vector3 newPosition = rectTransform.localPosition;
        newPosition.y = 0;
        rectTransform.localPosition = newPosition;
    }

    private void RemoveIndexes()
    {
        for(int i = 0; i < Hand.Count; i++)
        {
            if(Hand[i] == null)
                Hand.RemoveAt(i);
        }
    }
}
