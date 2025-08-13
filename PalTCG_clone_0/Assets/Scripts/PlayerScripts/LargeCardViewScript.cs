using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LargeCardViewScript : MonoBehaviour
{
    public static LargeCardViewScript Instance;
    [SerializeField] GameObject leftPosition;
    [SerializeField] GameObject rightPosition;

    private Image picture;
    public GameObject zoomedCard;

    void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        picture = gameObject.GetComponent<Image>();
    }

    public void FocusCard(Sprite image, GameObject smallerCard)
    {
        picture.sprite = image;
        zoomedCard = smallerCard;

        float leftDistToCard = (leftPosition.transform.position - zoomedCard.transform.position).sqrMagnitude;
        float rightDistToCard = (rightPosition.transform.position - zoomedCard.transform.position).sqrMagnitude;

        if(leftDistToCard > rightDistToCard)
            transform.position = leftPosition.transform.position;
        else
            transform.position = rightPosition.transform.position;
        
        gameObject.SetActive(true);
    }

    public void CloseZoom(GameObject smallerCard)
    {
        if(smallerCard == zoomedCard)
            gameObject.SetActive(false);
    }
}
