using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueAssigner : MonoBehaviour
{
    public List<Sprite> lamball;
    public List<Sprite> cattiva;
    public List<Sprite> chikipi;
    public List<Sprite> lifmunk;
    public List<Sprite> tanzee;
    public List<Sprite> depresso;
    public List<Sprite> daedream;


    void Start()
    {
        GetComponent<GameManager>().CardSprites.lamball = lamball;
        GetComponent<GameManager>().CardSprites.cattiva = cattiva;
        GetComponent<GameManager>().CardSprites.chikipi = chikipi;
        GetComponent<GameManager>().CardSprites.lifmunk = lifmunk;
        GetComponent<GameManager>().CardSprites.tanzee = tanzee;
        GetComponent<GameManager>().CardSprites.depresso = depresso;
        GetComponent<GameManager>().CardSprites.daedream = daedream;
    }
}
