using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GamePreferences Preferences;

    public static HandScript Instance;

    //Hand Stuff
    public GameObject selected;
    private GameObject target;

    public new List<GameObject> Hand = new List<GameObject>();

    //Moving stuff
    private Vector3 originalPos;
    private Vector3 duckPos;
    private Vector3 targetPos;
    public float duckAmount;
    public float moveSpeed;

    void Awake()
    {
        if(Instance == null)
            Instance=this;
        else
            Destroy(gameObject);
        
        originalPos = transform.position;
        duckPos = new Vector3(originalPos.x, originalPos.y - duckAmount * ScreenCalculations.GetScale(gameObject), originalPos.z);
    }

    void Update()
    {
        RemoveIndexes();
        CenterCards();

        if(Input.GetButtonDown("Fire1"))
        {
            if(selected != null)
                StartCoroutine(Click());
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * ScreenCalculations.GetScale(gameObject) * Time.deltaTime);
        
    }
    public void Select(GameObject card)
    {
        selected = card;
        Duck();
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

    public IEnumerator Click()
    {
        yield return new WaitForSeconds(.15f);

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

    public void OnPointerEnter(PointerEventData eventData)
    {
        Raise();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Duck();
    }

    private void Duck()
    {
        targetPos = duckPos;
    }

    private void Raise()
    {
        targetPos = originalPos;
    }
}
