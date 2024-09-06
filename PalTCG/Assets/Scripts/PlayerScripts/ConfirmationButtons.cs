using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ConfirmationButtons : MonoBehaviour
{
   public static ConfirmationButtons Instance;

   public UnityAction Confirmed;
   public UnityAction Denied;
   private bool canConfirm;

   [SerializeField] GameObject confirmButton;
   [SerializeField] GameObject denyButton;

   [SerializeField] float greyValue;

    void Awake()
    {
        if(Instance == null)
            Instance=this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        GameManager.Instance.StartPlayerTurn += Deny;
        GameManager.Instance.StartEnemyTurn += Deny;
        GameManager.Instance.StartPlayerAttack += Deny;
        GameManager.Instance.StartEnemyAttack += Deny;
    }

    public void Confirm()
    {
        if(canConfirm)
            Confirmed.Invoke();
    }

    public void Deny()
    {
        if(Denied != null)
            Denied.Invoke();
    }

    public void AllowConfirmation(bool input)
    {
        canConfirm = input;

        if(!canConfirm)
        {
            Color newColor = confirmButton.GetComponent<Image>().color;
            newColor.a = greyValue;
            confirmButton.GetComponent<Image>().color = newColor;
        }
        else
        {
            Color newColor = confirmButton.GetComponent<Image>().color;
            newColor.a = 1;
            confirmButton.GetComponent<Image>().color = newColor;
        }
    }
}
