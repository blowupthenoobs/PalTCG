using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConfirmationButtons : MonoBehaviour
{
   public static ConfirmationButtons Instance;

   public UnityEvent Confirmed;
   public UnityEvent Denied;

    void Awake()
    {
        if(Instance == null)
            Instance=this;
        else
            Destroy(gameObject);
    }

    public void Confirm()
    {
        Confirmed.Invoke();
    }

    public void Deny()
    {
        Denied.Invoke();
    }
}
