using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


using DefaultUnitData;

public class ValueAssigner : MonoBehaviour
{
    public List<Sprite> lamballSprites;
    public List<Sprite> cattivaSprites;
    public List<Sprite> chikipiSprites;
    public List<Sprite> lifmunkSprites;
    public List<Sprite> tanzeeSprites;
    public List<Sprite> depressoSprites;
    public List<Sprite> daedreamSprites;
    
    void Start()
    {
        GetComponent<GameManager>().CardSprites.lamball = lamballSprites;
        GetComponent<GameManager>().CardSprites.cattiva = cattivaSprites;
        GetComponent<GameManager>().CardSprites.chikipi = chikipiSprites;
        GetComponent<GameManager>().CardSprites.lifmunk = lifmunkSprites;
        GetComponent<GameManager>().CardSprites.tanzee = tanzeeSprites;
        GetComponent<GameManager>().CardSprites.depresso = depressoSprites;
        GetComponent<GameManager>().CardSprites.daedream = daedreamSprites;
    }
}
