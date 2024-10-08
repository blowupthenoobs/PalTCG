using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    public struct DefaultPal
    {
        public int cost;
        public Element element;
        public Traits traits;
        public int size;
        public List<Sprite> cardArt;

        public DefaultPal(int cost, Element element, Traits traits, int size, List<Sprite> cardArt)
        {
            this.cost = cost;
            this.element = element;
            this.traits = traits;
            this.size = size;
            this.cardArt = cardArt;
        }
    }

    public struct PalData
    {
        public int cost;
        public Element element;
        public Traits traits;
        public int size;
        public Sprite cardArt;

        public PalData(DefaultPal originalData, int artIndex)
        {
            this.cost = originalData.cost;
            this.element = originalData.element;
            this.traits = originalData.traits;
            this.size = originalData.size;
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
            cardArt: GameManager.Instance.CardSprites.lamball
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
            cardArt: GameManager.Instance.CardSprites.cattiva
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
            cardArt: GameManager.Instance.CardSprites.chikipi
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
            cardArt: GameManager.Instance.CardSprites.lifmunk
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
            cardArt: GameManager.Instance.CardSprites.tanzee
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
            cardArt: GameManager.Instance.CardSprites.depresso
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
            cardArt: GameManager.Instance.CardSprites.daedream
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


    }
    

}