using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
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

        public Traits(int ranch, int handyWork, int foraging, int gardening, int watering, int mining, int lumber, int transportation, int medicine, int kindling, int electric, int freezing, int dragon, bool bird)
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
        }
    }

    public static bool PaymentIsCorrect()
    {
        var data = (PalCardData)HandScript.Instance.selected.GetComponent<CardScript>().cardData;
        var costAmount = data.cost;

        if(data.element == Resources.Element.Basic && HandScript.Instance.selection.Count == costAmount)
            return true;
        else
            return false;
    }
}
