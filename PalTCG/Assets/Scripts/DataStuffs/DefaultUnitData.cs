using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Resources;

namespace DefaultUnitData
{
    public class Sprites
    {
        public List<Sprite> lamball;
        public List<Sprite> cattiva;
        public List<Sprite> chikipi;
        public List<Sprite> lifmunk;
        public List<Sprite> tanzee;
        public List<Sprite> depresso;
        public List<Sprite> daedream;
    }

    public class PalAbilitySets
    {
        public CardAbilities lamball = new CardAbilities
        (
            OnDestroy: new List<UnityAction>(),
            WhenAttack: new List<UnityAction>(),
            OnAttack: new List<UnityAction>(),
            OnHurt: new List<UnityAction>(),
            OnPlay: new List<UnityAction>(),
            PalSkill: new List<UnityAction>()
        );
        public CardAbilities cattiva = new CardAbilities
        (
            OnDestroy: new List<UnityAction>(),
            WhenAttack: new List<UnityAction>(),
            OnAttack: new List<UnityAction>(),
            OnHurt: new List<UnityAction>(),
            OnPlay: new List<UnityAction>(),
            PalSkill: new List<UnityAction>()
        );
        public CardAbilities chikipi = new CardAbilities
        (
            OnDestroy: new List<UnityAction>(),
            WhenAttack: new List<UnityAction>(),
            OnAttack: new List<UnityAction>(),
            OnHurt: new List<UnityAction>(),
            OnPlay: new List<UnityAction>(),
            PalSkill: new List<UnityAction>()
        );
        public CardAbilities lifmunk = new CardAbilities
        (
            OnDestroy: new List<UnityAction>(),
            WhenAttack: new List<UnityAction>(),
            OnAttack: new List<UnityAction>(),
            OnHurt: new List<UnityAction>(),
            OnPlay: new List<UnityAction>(),
            PalSkill: new List<UnityAction>()
        );
        public CardAbilities tanzee = new CardAbilities
        (
            OnDestroy: new List<UnityAction>(),
            WhenAttack: new List<UnityAction>(),
            OnAttack: new List<UnityAction>(),
            OnHurt: new List<UnityAction>(),
            OnPlay: new List<UnityAction>(),
            PalSkill: new List<UnityAction>()
        );
        public CardAbilities depresso = new CardAbilities
        (
            OnDestroy: new List<UnityAction>(),
            WhenAttack: new List<UnityAction>(),
            OnAttack: new List<UnityAction>(),
            OnHurt: new List<UnityAction>(),
            OnPlay: new List<UnityAction>(),
            PalSkill: new List<UnityAction>()
        );
        public CardAbilities daedream = new CardAbilities
        (
            OnDestroy: new List<UnityAction>(),
            WhenAttack: new List<UnityAction>(),
            OnAttack: new List<UnityAction>(),
            OnHurt: new List<UnityAction>(),
            OnPlay: new List<UnityAction>(),
            PalSkill: new List<UnityAction>()
        );
    }

    [System.Serializable]
    public struct CardAbilities
    {
        public List<UnityAction> OnDestroy;
        public List<UnityAction> WhenAttack;
        public List<UnityAction> OnAttack;
        public List<UnityAction> OnHurt;
        public List<UnityAction> OnPlay;
        public List<UnityAction> PalSkill;

        public CardAbilities(List<UnityAction> OnDestroy, List<UnityAction> WhenAttack, List<UnityAction> OnAttack, List<UnityAction> OnHurt, List<UnityAction> OnPlay, List<UnityAction> PalSkill)
        {
            this.OnDestroy = OnDestroy;
            this.WhenAttack = WhenAttack;
            this.OnAttack = OnAttack;
            this.OnHurt = OnHurt;
            this.OnPlay = OnPlay;
            this.PalSkill = PalSkill;
        }
    }
    
    [System.Serializable]
    public struct DefaultPal
    {
        public int cost;
        public Element element;
        public Traits traits;
        public int size;
        public int attackPower;
        public int maxHp;
        public List<Sprite> cardArt;
        public CardAbilities abilities;

        public DefaultPal(int cost, Element element, Traits traits, int size, int attackPower, int maxHp, List<Sprite> cardArt, CardAbilities abilities)
        {
            this.cost = cost;
            this.element = element;
            this.traits = traits;
            this.size = size;
            this.attackPower = attackPower;
            this.maxHp = maxHp;
            this.cardArt = cardArt;
            this.abilities = abilities;
        }
    }

    [System.Serializable]
    public struct PalData
    {
        public int cost;
        public Element element;
        public Traits traits;
        public int size;
        public int attackPower;
        public int maxHp;
        public Sprite cardArt;
        public CardAbilities abilities;

        public PalData(DefaultPal originalData, int artIndex)
        {
            this.cost = originalData.cost;
            this.element = originalData.element;
            this.traits = originalData.traits;
            this.size = originalData.size;
            this.attackPower = originalData.attackPower;
            this.maxHp = originalData.maxHp;
            this.abilities = originalData.abilities;
            this.cardArt = originalData.cardArt[artIndex];
        }
    }


    public class Pals
    {

#region BasicPals
        public DefaultPal lamball = new DefaultPal
        (
            cost: 1,
            element: Element.Basic,
            traits: new Traits
            (
                ranch: 1,
                handyWork: 1,
                foraging: 0,
                gardening: 0,
                watering: 0,
                mining: 0,
                lumber: 0,
                transportation: 1,
                medicine: 0,
                kindling: 0,
                electric: 0,
                freezing: 0,
                dragon: 0,
                bird: false
            ),
            size: 1,
            attackPower: 1,
            maxHp: 9,
            cardArt: AccountManager.Instance.CardSprites.lamball,
            abilities: AccountManager.Instance.PalAbilities.lamball
        );

        public DefaultPal cattiva = new DefaultPal
        (
            cost: 1,
            element: Element.Basic,
            traits: new Traits
            (
                ranch: 0,
                handyWork: 1,
                foraging: 1,
                gardening: 0,
                watering: 0,
                mining: 1,
                lumber: 0,
                transportation: 1,
                medicine: 0,
                kindling: 0,
                electric: 0,
                freezing: 0,
                dragon: 0,
                bird: false
            ),
            size: 1,
            attackPower: 2,
            maxHp: 8,
            cardArt: AccountManager.Instance.CardSprites.cattiva,
            abilities: AccountManager.Instance.PalAbilities.cattiva
        );

        public DefaultPal chikipi = new DefaultPal
        (
            cost: 1,
            element: Element.Basic,
            traits: new Traits
            (
                ranch: 1,
                handyWork: 0,
                foraging: 1,
                gardening: 0,
                watering: 0,
                mining: 0,
                lumber: 0,
                transportation: 0,
                medicine: 0,
                kindling: 0,
                electric: 0,
                freezing: 0,
                dragon: 0,
                bird: false
            ),
            size: 1,
            attackPower: 2,
            maxHp: 7,
            cardArt: AccountManager.Instance.CardSprites.chikipi,
            abilities: AccountManager.Instance.PalAbilities.chikipi
        );

#endregion BasicPals

#region GrassPals
        public DefaultPal lifmunk = new DefaultPal
        (
            cost: 1,
            element: Element.Grass,
            traits: new Traits
            (
                ranch: 0,
                handyWork: 1,
                foraging: 1,
                gardening: 1,
                watering: 0,
                mining: 0,
                lumber: 1,
                transportation: 0,
                medicine: 1,
                kindling: 0,
                electric: 0,
                freezing: 0,
                dragon: 0,
                bird: false
            ),
            size: 1,
            attackPower: 3,
            maxHp: 7,
            cardArt: AccountManager.Instance.CardSprites.lifmunk,
            abilities: AccountManager.Instance.PalAbilities.lifmunk
        );

        public DefaultPal tanzee = new DefaultPal
        (
            cost: 1,
            element: Element.Grass,
            traits: new Traits
            (
                ranch: 0,
                handyWork: 1,
                foraging: 1,
                gardening: 1,
                watering: 0,
                mining: 0,
                lumber: 1,
                transportation: 1,
                medicine: 0,
                kindling: 0,
                electric: 0,
                freezing: 0,
                dragon: 0,
                bird: false
            ),
            size: 1,
            attackPower: 2,
            maxHp: 8,
            cardArt: AccountManager.Instance.CardSprites.tanzee,
            abilities: AccountManager.Instance.PalAbilities.tanzee
        );

#endregion GrassPals

#region DarkPals
        public DefaultPal depresso = new DefaultPal
        (
            cost: 1,
            element: Element.Dark,
            traits: new Traits
            (
                ranch: 1,
                handyWork: 1,
                foraging: 0,
                gardening: 0,
                watering: 0,
                mining: 1,
                lumber: 0,
                transportation: 1,
                medicine: 0,
                kindling: 0,
                electric: 0,
                freezing: 0,
                dragon: 0,
                bird: false
            ),
            size: 1,
            attackPower: 2,
            maxHp: 8,
            cardArt: AccountManager.Instance.CardSprites.depresso,
            abilities: AccountManager.Instance.PalAbilities.depresso
        );

        public DefaultPal daedream = new DefaultPal
        (
            cost: 1,
            element: Element.Dark,
            traits: new Traits
            (
                ranch: 0,
                handyWork: 1,
                foraging: 1,
                gardening: 0,
                watering: 0,
                mining: 0,
                lumber: 0,
                transportation: 1,
                medicine: 0,
                kindling: 0,
                electric: 0,
                freezing: 0,
                dragon: 0,
                bird: false
            ),
            size: 1,
            attackPower: 2,
            maxHp: 7,
            cardArt: AccountManager.Instance.CardSprites.daedream,
            abilities: AccountManager.Instance.PalAbilities.daedream
        );

#endregion DarkPals


        public PalData FindPalData(string palName, int artIndex)
        {
            switch (palName)
            {
                case "lamball":
                    return new PalData(lamball, artIndex);
                break;
                case "cattiva":
                    return new PalData(cattiva, artIndex);
                break;
                case "chikipi":
                    return new PalData(chikipi, artIndex);
                break;
                case "lifmunk":
                    return new PalData(lifmunk, artIndex);
                break;
                case "tanzee":
                    return new PalData(tanzee, artIndex);
                break;
                case "depresso":
                    return new PalData(depresso, artIndex);
                break;
                case "daedream":
                    return new PalData(daedream, artIndex);
                break;
                default:
                    Debug.Log("doesn't have assigned pal");
                    return new PalData(cattiva, artIndex);
                break;
            }
        }

        public static CardData ConvertToCardData(string datatype)
        {
            string[] dataParts = datatype.Split("/");

            CardData data = null;

            switch(dataParts[0])
            {
                case "p":
                    data = (PalCardData)ScriptableObject.CreateInstance(typeof(PalCardData));
                    ((PalCardData)data).DecomposeData(new Pals().FindPalData(dataParts[1], int.Parse(dataParts[2])));
                break;
                case "t":
                    Debug.Log("look for items");
                break;
                case "h":
                    Debug.Log("look for player/hero");
                break;
            }

            return data;
        }
    }
    

}