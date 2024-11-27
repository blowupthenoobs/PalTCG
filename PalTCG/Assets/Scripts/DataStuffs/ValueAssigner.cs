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
    [Header("PlayerArts")]
    public Sprite playerDef;
    public Sprite playerAlt;
    public Sprite normalAxel;
    public Sprite normalLily;
    public Sprite normalMarcus;
    public Sprite normalVictor;
    public Sprite normalZoe;
    public Sprite overheadZoe;

    [Header("PalSprites")]
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

        spritesContainer.playerDef = playerDef;
        spritesContainer.playerAlt = playerAlt;
        spritesContainer.normalAxel = normalAxel;
        spritesContainer.normalLily = normalLily;
        spritesContainer.normalMarcus = normalMarcus;
        spritesContainer.normalVictor = normalVictor;
        spritesContainer.normalZoe = normalZoe;
        spritesContainer.overheadZoe = overheadZoe;

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
