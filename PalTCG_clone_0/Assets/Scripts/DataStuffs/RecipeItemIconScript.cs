using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeItemIconScript : MonoBehaviour
{
    private Image icon;
    [SerializeField] GameObject textBox;
    [SerializeField] TMP_Text countText;

    void Awake()
    {
        icon = gameObject.GetComponent<Image>();
    }

    public void SetIcon(Sprite newLook, int count)
    {
        icon.sprite = newLook;

        if (count > 1)
        {
            countText.text = count.ToString();
        }
        else
            textBox.SetActive(false);
    }
}
