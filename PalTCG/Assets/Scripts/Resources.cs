using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    public enum Element{Basic, Fire, Grass, Water, Ice, Electric, Dark}
    public struct resources{
        public int wood;
        public int stone;
        public int wool;
    }
    public struct StatusEffects{
        public int burning;
        public int poisoned;
    }
    public enum Traits{HandyWork, Gardening}
}
