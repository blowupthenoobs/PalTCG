using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Resources;

namespace DefaultUnitData
{
    [System.Serializable]
    public class Sprites
    {
        [Header("PlayerCards")]
        public Sprite playerDef;
        public Sprite playerAlt;
        public Sprite normalAxel;
        public Sprite normalLily;
        public Sprite normalMarcus;
        public Sprite normalVictor;
        public Sprite normalZoe;
        public Sprite overheadZoe;

        [Header("Buildings")]
        public Sprite craftingBench;
        public Sprite furnace;
        public Sprite loggingCamp;
        public Sprite miningPit;
        public Sprite ranchBuilding;
        public Sprite feedingBox;
        public Sprite berryPlantation;
        public Sprite crusher;


        [Header("Pals")]
        public List<Sprite> lamball;
        public List<Sprite> cattiva;
        public List<Sprite> chikipi;
        public List<Sprite> lifmunk;
        public List<Sprite> tanzee;
        public List<Sprite> foxsparks;
        public List<Sprite> sparkit;
        public List<Sprite> depresso;
        public List<Sprite> daedream;
        public List<Sprite> fuddler;
        public List<Sprite> dumud;
        public List<Sprite> loupmoon;

        [Header("Tools")]
        public List<Sprite> pickaxe;
        public List<Sprite> axe;
        public List<Sprite> saddle;
        [Header("Icons")]
        public Sprite wood;
        public Sprite stone;
        public Sprite paldium;
        public Sprite wool;
        public Sprite cloth;
        public Sprite poisonGland;
        public Sprite normalArrows;
        public Sprite poisonArrows;


        public Sprite ranch;
        public Sprite handyWork;
        public Sprite foraging;
        public Sprite gardening;
        public Sprite watering;
        public Sprite mining;
        public Sprite lumber;
        public Sprite transportation;
        public Sprite medicine;
        public Sprite kindling;
        public Sprite electric;
        public Sprite freezing;
        public Sprite dragon;
        public Sprite bird;
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

        public DefaultPal(int cost, Element element, Traits traits, int size, int attackPower, int maxHp, List<Sprite> cardArt)
        {
            this.cost = cost;
            this.element = element;
            this.traits = traits;
            this.size = size;
            this.attackPower = attackPower;
            this.maxHp = maxHp;
            this.cardArt = cardArt;
        }
    }

    [System.Serializable]
    public struct DefaultTool
    {
        public resources cost;
        public string toolType;
        public Traits traits;
        public int size;
        public int attackPower;
        public int maxHp;
        public List<Sprite> cardArt;

        public DefaultTool(resources cost, string toolType, Traits traits, int size, int attackPower, int maxHp, List<Sprite> cardArt)
        {
            this.cost = cost;
            this.toolType = toolType;
            this.traits = traits;
            this.size = size;
            this.attackPower = attackPower;
            this.maxHp = maxHp;
            this.cardArt = cardArt;
        }

    }
    [System.Serializable]
    public struct BuildingPreset
    {
        public resources cost;
        public Sprite cardArt;
        public UnityAction buildingFunction;
        public float timeToHold;

        public BuildingPreset(resources cost, Sprite cardArt, UnityAction buildingFunction, float timeToHold)
        {
            this.cost = cost;
            this.cardArt = cardArt;
            this.buildingFunction = buildingFunction;
            this.timeToHold = timeToHold;
        }

        public BuildingPreset(Sprite cardArt, UnityAction buildingFunction, float timeToHold)
        {
            this.cost = new resources();
            this.cardArt = cardArt;
            this.buildingFunction = buildingFunction;
            this.timeToHold = timeToHold;
        }
    }

    [System.Serializable]
    public struct PalData
    {
        public string cardID;
        public int cost;
        public Element element;
        public Traits traits;
        public int size;
        public int attackPower;
        public int maxHp;
        public Sprite cardArt;

        public PalData(DefaultPal originalData, int artIndex, string cardID)
        {
            this.cardID = cardID;
            this.cost = originalData.cost;
            this.element = originalData.element;
            this.traits = originalData.traits;
            this.size = originalData.size;
            this.attackPower = originalData.attackPower;
            this.maxHp = originalData.maxHp;
            this.cardArt = originalData.cardArt[artIndex];
        }

        public override bool Equals(object obj)
        {
            if(obj is PalData otherData)
            {
                return this.cardID == otherData.cardID;
            }
            return false;
        }

        public override int GetHashCode()
        {
           return cardID?.GetHashCode() ?? 0;
        }

        public static bool operator ==(PalData left, PalData right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PalData left, PalData right)
        {
            return !(left == right);
        }
    }

    [System.Serializable]
    public struct ToolData
    {
        public string cardID;
        public string toolType;
        public resources cost;
        public Traits traits;
        public int size;
        public int attackPower;
        public int maxHp;
        public Sprite cardArt;

        public ToolData(DefaultTool originalData, int artIndex, string cardID)
        {
            this.cardID = cardID;
            this.toolType = originalData.toolType;
            this.cost = originalData.cost;
            this.traits = originalData.traits;
            this.size = originalData.size;
            this.attackPower = originalData.attackPower;
            this.maxHp = originalData.maxHp;
            this.cardArt = originalData.cardArt[artIndex];
        }

        public override bool Equals(object obj)
        {
            if(obj is ToolData otherData)
            {
                return this.cardID == otherData.cardID;
            }
            return false;
        }

        public override int GetHashCode()
        {
           return cardID?.GetHashCode() ?? 0;
        }

        public static bool operator ==(ToolData left, ToolData right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ToolData left, ToolData right)
        {
            return !(left == right);
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
                bird: false,
                tags: new List<string> { "blocker" }
            ),
            size: 1,
            attackPower: 1,
            maxHp: 9,
            cardArt: AccountManager.Instance.CardSprites.lamball
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
                bird: false,
                tags: new List<string>{"mercy"}
            ),
            size: 1,
            attackPower: 2,
            maxHp: 8,
            cardArt: AccountManager.Instance.CardSprites.cattiva
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
                bird: false,
                tags: new List<string> {"chikipi"}
            ),
            size: 1,
            attackPower: 2,
            maxHp: 7,
            cardArt: AccountManager.Instance.CardSprites.chikipi
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
                bird: false,
                tags: new List<string> { }
            ),
            size: 1,
            attackPower: 3,
            maxHp: 7,
            cardArt: AccountManager.Instance.CardSprites.lifmunk
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
                bird: false,
                tags: new List<string> { }
            ),
            size: 1,
            attackPower: 2,
            maxHp: 8,
            cardArt: AccountManager.Instance.CardSprites.tanzee
        );

        #endregion GrassPals

        #region FirePals
        public DefaultPal foxsparks = new DefaultPal
        (
            cost: 1,
            element: Element.Fire,
            traits: new Traits
            (
                ranch: 0,
                handyWork: 0,
                foraging: 0,
                gardening: 0,
                watering: 0,
                mining: 0,
                lumber: 0,
                transportation: 0,
                medicine: 0,
                kindling: 1,
                electric: 0,
                freezing: 0,
                dragon: 0,
                bird: false,
                tags: new List<string> { "burn" }
            ),
            size: 1,
            attackPower: 3,
            maxHp: 7,
            cardArt: AccountManager.Instance.CardSprites.foxsparks
        );
        #endregion FirePals

        #region WaterPals

        #endregion WaterPals

        #region ElectricPals
        public DefaultPal sparkit = new DefaultPal
            (
                cost: 1,
                element: Element.Electric,
                traits: new Traits
                (
                    ranch: 0,
                    handyWork: 1,
                    foraging: 0,
                    gardening: 0,
                    watering: 0,
                    mining: 0,
                    lumber: 0,
                    transportation: 1,
                    medicine: 0,
                    kindling: 0,
                    electric: 1,
                    freezing: 0,
                    dragon: 0,
                    bird: false,
                    tags: new List<string> { "stun" }
                ),
                size: 1,
                attackPower: 2,
                maxHp: 8,
                cardArt: AccountManager.Instance.CardSprites.sparkit
            );
        #endregion ElectricPals

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
                bird: false,
                tags: new List<string> { "toxic" }
            ),
            size: 1,
            attackPower: 2,
            maxHp: 8,
            cardArt: AccountManager.Instance.CardSprites.depresso
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
                bird: false,
                tags: new List<string> { "toxic", "daedream" }
            ),
            size: 1,
            attackPower: 2,
            maxHp: 7,
            cardArt: AccountManager.Instance.CardSprites.daedream
        );

        public DefaultPal loupmoon = new DefaultPal
        (
            cost: 3,
            element: Element.Dark,
            traits: new Traits
            (
                ranch: 1,
                handyWork: 2,
                foraging: 0,
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
                bird: false,
                tags: new List<string> { }
            ),
            size: 2,
            attackPower: 5,
            maxHp: 9,
            cardArt: AccountManager.Instance.CardSprites.loupmoon
        );

        #endregion DarkPals

        #region EarthPals
        public DefaultPal fuddler = new DefaultPal
        (
            cost: 1,
            element: Element.Earth,
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
                bird: false,
                tags: new List<string> { "blocker" }
            ),
            size: 1,
            attackPower: 2,
            maxHp: 7,
            cardArt: AccountManager.Instance.CardSprites.fuddler
        );

        public DefaultPal dumud = new DefaultPal
        (
            cost: 1,
            element: Element.Earth,
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
                bird: false,
                tags: new List<string> { "blocker", "tank" }
            ),
            size: 1,
            attackPower: 2,
            maxHp: 7,
            cardArt: AccountManager.Instance.CardSprites.dumud
        );

        #endregion EarthPals

        #region Tools
        public DefaultTool pickaxe = new DefaultTool
        (
            cost: new resources { wood = 2, stone = 2 },
            toolType: "weapon",
            traits: new Traits
            (
                ranch: 0,
                handyWork: 0,
                foraging: 0,
                gardening: 0,
                watering: 0,
                mining: 2,
                lumber: 0,
                transportation: 0,
                medicine: 0,
                kindling: 0,
                electric: 0,
                freezing: 0,
                dragon: 0,
                bird: false,
                tags: new List<string> { }
            ),
            size: 1,
            attackPower: 2,
            maxHp: 2,
            cardArt: AccountManager.Instance.CardSprites.pickaxe
        );

        public DefaultTool axe = new DefaultTool
        (
            cost: new resources { wood = 2, stone = 2 },
            toolType: "weapon",
            traits: new Traits
            (
                ranch: 0,
                handyWork: 0,
                foraging: 0,
                gardening: 0,
                watering: 0,
                mining: 0,
                lumber: 2,
                transportation: 0,
                medicine: 0,
                kindling: 0,
                electric: 0,
                freezing: 0,
                dragon: 0,
                bird: false,
                tags: new List<string> { }
            ),
            size: 1,
            attackPower: 2,
            maxHp: 2,
            cardArt: AccountManager.Instance.CardSprites.axe
        );

        public DefaultTool saddle = new DefaultTool
        (
            cost: new resources { wood = 1, paldium = 1 },
            toolType: "ride",
            traits: new Traits
            (
                ranch: 0,
                handyWork: 0,
                foraging: 0,
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
                bird: false,
                tags: new List<string> { }
            ),
            size: 1,
            attackPower: 0,
            maxHp: 1,
            cardArt: AccountManager.Instance.CardSprites.saddle
        );
        #endregion

        #region Buildings
        public BuildingPreset craftingBench = new BuildingPreset
        (
            cardArt: AccountManager.Instance.CardSprites.craftingBench,
            buildingFunction: BuildingFunctions.OpenCraftingBenchMenu,
            timeToHold: 0
        );

        public BuildingPreset furnace = new BuildingPreset
        (
            cardArt: AccountManager.Instance.CardSprites.furnace,
            buildingFunction: null, //Need to make a place to store it in the abilities script
            timeToHold: 0
        );

        public BuildingPreset loggingCamp = new BuildingPreset
        (
            cardArt: AccountManager.Instance.CardSprites.loggingCamp,
            buildingFunction: BuildingFunctions.UseLumberFarm,
            timeToHold: 2.0f
        );

        public BuildingPreset miningPit = new BuildingPreset
        (
            cardArt: AccountManager.Instance.CardSprites.miningPit,
            buildingFunction: BuildingFunctions.UseMiningPit,
            timeToHold: 2.0f
        );

        public BuildingPreset ranch = new BuildingPreset
        (
            cardArt: AccountManager.Instance.CardSprites.ranchBuilding,
            buildingFunction: BuildingFunctions.UseMiningPit,
            timeToHold: 2.0f
        );

        public BuildingPreset feedingBox = new BuildingPreset
        (
            cardArt: AccountManager.Instance.CardSprites.feedingBox,
            buildingFunction: BuildingFunctions.UseMiningPit,
            timeToHold: 2.0f
        );

        public BuildingPreset berryPlantation = new BuildingPreset
        (
            cardArt: AccountManager.Instance.CardSprites.berryPlantation,
            buildingFunction: BuildingFunctions.UseMiningPit,
            timeToHold: 2.0f
        );

        public BuildingPreset crusher = new BuildingPreset
        (
            cardArt: AccountManager.Instance.CardSprites.crusher,
            buildingFunction: BuildingFunctions.UseMiningPit,
            timeToHold: 2.0f
        );

        #endregion
        public PalData FindPalData(string palName, int artIndex)
        {
            string cardID = "p/" + palName + "/" + artIndex.ToString();
            switch (palName)
            {
                case "lamball":
                    return new PalData(lamball, artIndex, cardID);
                case "cattiva":
                    return new PalData(cattiva, artIndex, cardID);
                case "chikipi":
                    return new PalData(chikipi, artIndex, cardID);
                case "lifmunk":
                    return new PalData(lifmunk, artIndex, cardID);
                case "tanzee":
                    return new PalData(tanzee, artIndex, cardID);
                case "foxsparks":
                    return new PalData(foxsparks, artIndex, cardID);
                case "sparkit":
                    return new PalData(sparkit, artIndex, cardID);
                case "depresso":
                    return new PalData(depresso, artIndex, cardID);
                case "daedream":
                    return new PalData(daedream, artIndex, cardID);
                case "fuddler":
                    return new PalData(fuddler, artIndex, cardID);
                case "dumud":
                    return new PalData(dumud, artIndex, cardID);
                case "loupmoon":
                    return new PalData(loupmoon, artIndex, cardID);
                default:
                    Debug.Log("doesn't have assigned pal");
                    return new PalData(cattiva, artIndex, "p/cattiva/" + artIndex.ToString());
            }
        }

        public ToolData FindToolData(string toolName, int artIndex)
        {
            string cardID = "t/" + toolName + "/" + artIndex.ToString();
            switch (toolName)
            {
                case "pickaxe":
                    return new ToolData(pickaxe, artIndex, cardID);
                case "axe":
                    return new ToolData(axe, artIndex, cardID);
                case "saddle":
                    return new ToolData(saddle, artIndex, cardID);
                default:
                    Debug.Log("doesn't have assigned tool:" + toolName);
                    return new ToolData(pickaxe, artIndex, "t/pickaxe/" + artIndex.ToString());
            }
        }

        public static Sprite LookForPlayerArt(string artName)
        {
            switch (artName)
            {
                case "playerDef":
                    return AccountManager.Instance.CardSprites.playerDef;
                case "playerAlt":
                    return AccountManager.Instance.CardSprites.playerAlt;
                case "normalAxel":
                    return AccountManager.Instance.CardSprites.normalAxel;
                case "normalLily":
                    return AccountManager.Instance.CardSprites.normalLily;
                case "normalMarcus":
                    return AccountManager.Instance.CardSprites.normalMarcus;
                case "normalVictor":
                    return AccountManager.Instance.CardSprites.normalVictor;
                case "normalZoe":
                    return AccountManager.Instance.CardSprites.normalZoe;
                case "overheadZoe":
                    return AccountManager.Instance.CardSprites.overheadZoe;
                default:
                    Debug.Log("didn't have assigned playerArt");
                    return AccountManager.Instance.CardSprites.playerDef;
            }
        }

        public static CardData ConvertToCardData(string datatype)
        {
            string[] dataParts = datatype.Split("/");

            CardData data = null;

            switch (dataParts[0])
            {
                case "p":
                    data = (PalCardData)ScriptableObject.CreateInstance(typeof(PalCardData));
                    ((PalCardData)data).DecomposeData(new Pals().FindPalData(dataParts[1], int.Parse(dataParts[2])));
                    break;
                case "t":
                    data = (ToolCardData)ScriptableObject.CreateInstance(typeof(ToolCardData));
                    ((ToolCardData)data).DecomposeData(new Pals().FindToolData(dataParts[1], int.Parse(dataParts[2])));
                    break;
                case "h":
                    data = (PalCardData)ScriptableObject.CreateInstance(typeof(PalCardData));
                    data.cardArt = LookForPlayerArt(dataParts[1]);
                    break;
            }

            return data;
        }

        public static BuildingPreset GetBuildingInfo(string buildingName)
        {
            switch (buildingName)
            {
                case "craftingBench":
                    return new Pals().craftingBench;
                case "furnace":
                    return new Pals().furnace;
                case "loggingCamp":
                    return new Pals().loggingCamp;
                case "miningPit":
                    return new Pals().miningPit;
                case "ranch":
                    return new Pals().ranch;
                case "feedingBox":
                    return new Pals().feedingBox;
                case "berryPlantation":
                    return new Pals().berryPlantation;
                case "crusher":
                    return new Pals().crusher;
                default:
                    Debug.Log("building not found");
                    return new Pals().craftingBench;
            }
        }

        public static Sprite GetIconSprite(string name)
        {
            switch (name)
            {
                case "wood":
                    return AccountManager.Instance.CardSprites.wood;
                case "stone":
                    return AccountManager.Instance.CardSprites.stone;
                case "paldium":
                    return AccountManager.Instance.CardSprites.paldium;
                case "wool":
                    return AccountManager.Instance.CardSprites.wool;
                case "cloth":
                    return AccountManager.Instance.CardSprites.cloth;
                case "poisonGland":
                    return AccountManager.Instance.CardSprites.poisonGland;
                case "normalArrows":
                    return AccountManager.Instance.CardSprites.normalArrows;
                case "poisonArrows":
                    return AccountManager.Instance.CardSprites.poisonArrows;
                default:
                    Debug.Log("No icon found for: " + name);
                    return AccountManager.Instance.CardSprites.stone;
            }
        }

        public static Sprite GetTraitSprite(string name)
        {
            switch (name)
            {
                case "ranch":
                    return AccountManager.Instance.CardSprites.ranch;
                case "handyWork":
                    return AccountManager.Instance.CardSprites.handyWork;
                case "foraging":
                    return AccountManager.Instance.CardSprites.foraging;
                case "gardening":
                    return AccountManager.Instance.CardSprites.gardening;
                case "watering":
                    return AccountManager.Instance.CardSprites.watering;
                case "mining":
                    return AccountManager.Instance.CardSprites.mining;
                case "lumber":
                    return AccountManager.Instance.CardSprites.lumber;
                case "transportation":
                    return AccountManager.Instance.CardSprites.transportation;
                case "medicine":
                    return AccountManager.Instance.CardSprites.medicine;
                case "kindling":
                    return AccountManager.Instance.CardSprites.kindling;
                case "electric":
                    return AccountManager.Instance.CardSprites.electric;
                case "freezing":
                    return AccountManager.Instance.CardSprites.freezing;
                case "dragon":
                    return AccountManager.Instance.CardSprites.dragon;
                case "bird":
                    return AccountManager.Instance.CardSprites.bird;
                default:
                    Debug.Log("No icon found for: " + name);
                    return AccountManager.Instance.CardSprites.stone;
            }
        }

        public static Dictionary<string, UnityAction> palSkill = new Dictionary<string, UnityAction>
        {
            { "lamball", () => CardMovement.EquipAsItemPalSkill("weapon")},
            { "chikipi", () => HandFunctions.ChikipiPalSkill()},
            { "foxsparks", () => CardMovement.EquipAsItemPalSkill("weapon")},
            { "lifmunk", () => CardMovement.EquipAsItemPalSkill("weapon")},
        };
    }
}