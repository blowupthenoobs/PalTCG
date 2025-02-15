using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Resources
{
    public enum Element{Basic, Fire, Grass, Water, Ice, Electric, Dark}
    public struct resources
    {
        public int wood;
        public int stone;
        public int wool;
    }
    public struct StatusEffects
    {
        public int burning;
        public int poisoned;
        public GameObject shocked;
    }
    public struct Traits
    {
        public int ranch;
        public int handyWork;
        public int foraging;
        public int gardening;
        public int watering;
        public int mining;
        public int lumber;
        public int transportation;
        public int medicine;
        public int kindling;
        public int electric;
        public int freezing;
        public int dragon;
        public bool bird;

        public bool blocker;
        public bool tank;

        public Traits(int ranch, int handyWork, int foraging, int gardening, int watering, int mining, int lumber, int transportation, int medicine, int kindling, int electric, int freezing, int dragon, bool bird, bool blocker, bool tank)
        {
            this.ranch = ranch;
            this.handyWork = handyWork;
            this.foraging = foraging;
            this.gardening = gardening;
            this.watering = watering;
            this.mining = mining;
            this.lumber = lumber;
            this.transportation = transportation;
            this.medicine = medicine;
            this.kindling = kindling;
            this.electric = electric;
            this.freezing = freezing;
            this.dragon = dragon;
            this.bird = bird;
            this.blocker = blocker;
            this.tank = tank;
        }
    }

    public class ResourceProcesses
    {
        public static bool PalPaymentIsCorrect()
        {
            var data = (PalCardData)HandScript.Instance.selected.GetComponent<CardScript>().cardData;
            var costAmount = data.cost;

            if(data.element == Element.Basic && HandScript.Instance.selection.Count == costAmount)
                return true;
            else if(HandScript.Instance.selection.Count == costAmount)
            {
                var typesAreCorrect = true;
                var cardColor = ((PalCardData)HandScript.Instance.selected.GetComponent<CardScript>().cardData).element;

                for(int i = 0; i < HandScript.Instance.selection.Count; i++)
                {
                    var paymentColor = ((PalCardData)HandScript.Instance.selection[i].GetComponent<CardScript>().cardData).element;

                    if(cardColor != paymentColor && paymentColor != Element.Basic)
                        typesAreCorrect = false;

                }

                return typesAreCorrect;
            }
            else
                return false;
        }
    }
    
}
