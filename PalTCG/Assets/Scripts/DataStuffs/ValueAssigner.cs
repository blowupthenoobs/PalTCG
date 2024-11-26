using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


using DefaultUnitData;

public class ValueAssigner : MonoBehaviour
{
    private AccountManager manager;
    private Sprites spritesContainer;
    private PalAbilitySets abilitySets;
    [Header("Sprites")]
    public List<Sprite> lamballSprites;
    public List<Sprite> cattivaSprites;
    public List<Sprite> chikipiSprites;
    public List<Sprite> lifmunkSprites;
    public List<Sprite> tanzeeSprites;
    public List<Sprite> depressoSprites;
    public List<Sprite> daedreamSprites;
    
    void Start()
    {
        manager  = AccountManager.Instance;
        spritesContainer = manager.CardSprites;
        abilitySets = manager.PalAbilities;


        spritesContainer.lamball = lamballSprites;
        spritesContainer.cattiva = cattivaSprites;
        spritesContainer.chikipi = chikipiSprites;
        spritesContainer.lifmunk = lifmunkSprites;
        spritesContainer.tanzee = tanzeeSprites;
        spritesContainer.depresso = depressoSprites;
        spritesContainer.daedream = daedreamSprites;

        abilitySets.daedream.WhenAttack.Add(() => StartCoroutine(StatusEffects.PutToSleep()));
    }
}
