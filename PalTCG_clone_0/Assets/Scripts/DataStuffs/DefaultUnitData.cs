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
        public List<Sprite> depresso;
        public List<Sprite> daedream;
        public List<Sprite> fuddler;
        public List<Sprite> dumud;

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

    public class PalAbilitySets
    {
        public CardAbilities lamball = new CardAbilities
        (
            OnDestroy: new List<UnityAction>(),
            WhenAttack: new List<UnityAction>(),
            OnAttack: new List<UnityAction>(),
            OnHurt: new List<UnityAction>(),
            OnPlay: new List<UnityAction>(),
            EndOfTurn: new List<UnityAction>(),
            OncePerTurn: new List<UnityAction>(),
            PalSkill: new List<UnityAction>(),
            WhenSkillAttack: new List<UnityAction>(),
            OnSkillAtack: new List<UnityAction>(),
            EndOfSkillTurn: new List<UnityAction>()
        );
        public CardAbilities cattiva = new CardAbilities
        (
            OnDestroy: new List<UnityAction>(),
            WhenAttack: new List<UnityAction>(),
            OnAttack: new List<UnityAction>(),
            OnHurt: new List<UnityAction>(),
            OnPlay: new List<UnityAction>(),
            EndOfTurn: new List<UnityAction>(),
            OncePerTurn: new List<UnityAction>(),
            PalSkill: new List<UnityAction>(),
            WhenSkillAttack: new List<UnityAction>(),
            OnSkillAtack: new List<UnityAction>(),
            EndOfSkillTurn: new List<UnityAction>()
        );
        public CardAbilities chikipi = new CardAbilities
        (
            OnDestroy: new List<UnityAction>(),
            WhenAttack: new List<UnityAction>(),
            OnAttack: new List<UnityAction>(),
            OnHurt: new List<UnityAction>(),
            OnPlay: new List<UnityAction>(),
            EndOfTurn: new List<UnityAction>(),
            OncePerTurn: new List<UnityAction>(),
            PalSkill: new List<UnityAction>(),
            WhenSkillAttack: new List<UnityAction>(),
            OnSkillAtack: new List<UnityAction>(),
            EndOfSkillTurn: new List<UnityAction>()
        );
        public CardAbilities lifmunk = new CardAbilities
        (
            OnDestroy: new List<UnityAction>(),
            WhenAttack: new List<UnityAction>(),
            OnAttack: new List<UnityAction>(),
            OnHurt: new List<UnityAction>(),
            OnPlay: new List<UnityAction>(),
            EndOfTurn: new List<UnityAction>(),
            OncePerTurn: new List<UnityAction>(),
            PalSkill: new List<UnityAction>(),
            WhenSkillAttack: new List<UnityAction>(),
            OnSkillAtack: new List<UnityAction>(),
            EndOfSkillTurn: new List<UnityAction>()
        );
        public CardAbilities tanzee = new CardAbilities
        (
            OnDestroy: new List<UnityAction>(),
            WhenAttack: new List<UnityAction>(),
            OnAttack: new List<UnityAction>(),
            OnHurt: new List<UnityAction>(),
            OnPlay: new List<UnityAction>(),
            EndOfTurn: new List<UnityAction>(),
            OncePerTurn: new List<UnityAction>(),
            PalSkill: new List<UnityAction>(),
            WhenSkillAttack: new List<UnityAction>(),
            OnSkillAtack: new List<UnityAction>(),
            EndOfSkillTurn: new List<UnityAction>()
        );
        public CardAbilities depresso = new CardAbilities
        (
            OnDestroy: new List<UnityAction>(),
            WhenAttack: new List<UnityAction>(),
            OnAttack: new List<UnityAction>(),
            OnHurt: new List<UnityAction>(),
            OnPlay: new List<UnityAction>(),
            EndOfTurn: new List<UnityAction>(),
            OncePerTurn: new List<UnityAction>(),
            PalSkill: new List<UnityAction>(),
            WhenSkillAttack: new List<UnityAction>(),
            OnSkillAtack: new List<UnityAction>(),
            EndOfSkillTurn: new List<UnityAction>()
        );
        public CardAbilities daedream = new CardAbilities
        (
            OnDestroy: new List<UnityAction>(),
            WhenAttack: new List<UnityAction>(),
            OnAttack: new List<UnityAction>(),
            OnHurt: new List<UnityAction>(),
            OnPlay: new List<UnityAction>(),
            EndOfTurn: new List<UnityAction>(),
            OncePerTurn: new List<UnityAction>(),
            PalSkill: new List<UnityAction>(),
            WhenSkillAttack: new List<UnityAction>(),
            OnSkillAtack: new List<UnityAction>(),
            EndOfSkillTurn: new List<UnityAction>()
        );
        public CardAbilities fuddler = new CardAbilities
        (
            OnDestroy: new List<UnityAction>(),
            WhenAttack: new List<UnityAction>(),
            OnAttack: new List<UnityAction>(),
            OnHurt: new List<UnityAction>(),
            OnPlay: new List<UnityAction>(),
            EndOfTurn: new List<UnityAction>(),
            OncePerTurn: new List<UnityAction>(),
            PalSkill: new List<UnityAction>(),
            WhenSkillAttack: new List<UnityAction>(),
            OnSkillAtack: new List<UnityAction>(),
            EndOfSkillTurn: new List<UnityAction>()
        );
        public CardAbilities dumud = new CardAbilities
        (
            OnDestroy: new List<UnityAction>(),
            WhenAttack: new List<UnityAction>(),
            OnAttack: new List<UnityAction>(),
            OnHurt: new List<UnityAction>(),
            OnPlay: new List<UnityAction>(),
            EndOfTurn: new List<UnityAction>(),
            OncePerTurn: new List<UnityAction>(),
            PalSkill: new List<UnityAction>(),
            WhenSkillAttack: new List<UnityAction>(),
            OnSkillAtack: new List<UnityAction>(),
            EndOfSkillTurn: new List<UnityAction>()
        );

        public CardAbilities pickaxe = new CardAbilities
        (
            OnDestroy: new List<UnityAction>(),
            WhenAttack: new List<UnityAction>(),
            OnAttack: new List<UnityAction>(),
            OnHurt: new List<UnityAction>(),
            OnPlay: new List<UnityAction>(),
            EndOfTurn: new List<UnityAction>(),
            OncePerTurn: new List<UnityAction>(),
            PalSkill: new List<UnityAction>(),
            WhenSkillAttack: new List<UnityAction>(),
            OnSkillAtack: new List<UnityAction>(),
            EndOfSkillTurn: new List<UnityAction>()
        );

        public CardAbilities axe = new CardAbilities
        (
            OnDestroy: new List<UnityAction>(),
            WhenAttack: new List<UnityAction>(),
            OnAttack: new List<UnityAction>(),
            OnHurt: new List<UnityAction>(),
            OnPlay: new List<UnityAction>(),
            EndOfTurn: new List<UnityAction>(),
            OncePerTurn: new List<UnityAction>(),
            PalSkill: new List<UnityAction>(),
            WhenSkillAttack: new List<UnityAction>(),
            OnSkillAtack: new List<UnityAction>(),
            EndOfSkillTurn: new List<UnityAction>()
        );

        public CardAbilities saddle = new CardAbilities
        (
            OnDestroy: new List<UnityAction>(),
            WhenAttack: new List<UnityAction>(),
            OnAttack: new List<UnityAction>(),
            OnHurt: new List<UnityAction>(),
            OnPlay: new List<UnityAction>(),
            EndOfTurn: new List<UnityAction>(),
            OncePerTurn: new List<UnityAction>(),
            PalSkill: new List<UnityAction>(),
            WhenSkillAttack: new List<UnityAction>(),
            OnSkillAtack: new List<UnityAction>(),
            EndOfSkillTurn: new List<UnityAction>()
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
        public List<UnityAction> EndOfTurn;
        public List<UnityAction> OncePerTurn;


        public List<UnityAction> PalSkill;
        public List<UnityAction> WhenSkillAttack;
        public List<UnityAction> OnSkillAttack;
        public List<UnityAction> EndOfSkillTurn;

        public CardAbilities(List<UnityAction> OnDestroy, List<UnityAction> WhenAttack, List<UnityAction> OnAttack, List<UnityAction> OnHurt, List<UnityAction> OnPlay, List<UnityAction> EndOfTurn, List<UnityAction> OncePerTurn, List<UnityAction> PalSkill, List<UnityAction> WhenSkillAttack, List<UnityAction> OnSkillAtack, List<UnityAction> EndOfSkillTurn)
        {
            this.OnDestroy = OnDestroy;
            this.WhenAttack = WhenAttack;
            this.OnAttack = OnAttack;
            this.OnHurt = OnHurt;
            this.OnPlay = OnPlay;
            this.EndOfTurn = EndOfTurn;
            this.OncePerTurn = OncePerTurn;

            this.PalSkill = PalSkill;
            this.WhenSkillAttack = WhenSkillAttack;
            this.OnSkillAttack = OnSkillAtack;
            this.EndOfSkillTurn = EndOfSkillTurn;
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
    public struct DefaultTool
    {
        public resources cost;
        public string toolType;
        public Traits traits;
        public int size;
        public int attackPower;
        public int maxHp;
        public List<Sprite> cardArt;
        public CardAbilities abilities;

        public DefaultTool(resources cost, string toolType, Traits traits, int size, int attackPower, int maxHp, List<Sprite> cardArt, CardAbilities abilities)
        {
            this.cost = cost;
            this.toolType = toolType;
            this.traits = traits;
            this.size = size;
            this.attackPower = attackPower;
            this.maxHp = maxHp;
            this.cardArt = cardArt;
            this.abilities = abilities;
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
        public CardAbilities abilities;

        public PalData(DefaultPal originalData, int artIndex, string cardID)
        {
            this.cardID = cardID;
            this.cost = originalData.cost;
            this.element = originalData.element;
            this.traits = originalData.traits;
            this.size = originalData.size;
            this.attackPower = originalData.attackPower;
            this.maxHp = originalData.maxHp;
            this.abilities = originalData.abilities;
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
        public CardAbilities abilities;

        public ToolData(DefaultTool originalData, int artIndex, string cardID)
        {
            this.cardID = cardID;
            this.toolType = originalData.toolType;
            this.cost = originalData.cost;
            this.traits = originalData.traits;
            this.size = originalData.size;
            this.attackPower = originalData.attackPower;
            this.maxHp = originalData.maxHp;
            this.abilities = originalData.abilities;
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
                blocker: true,
                tank: false,
                rideable: true
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
                bird: false,
                blocker: false,
                tank: false,
                rideable: true
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
                bird: false,
                blocker: false,
                tank: false,
                rideable: true
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
                bird: false,
                blocker: false,
                tank: false,
                rideable: true
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
                bird: false,
                blocker: false,
                tank: false,
                rideable: true
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
                bird: false,
                blocker: false,
                tank: false,
                rideable: true
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
                bird: false,
                blocker: false,
                tank: false,
                rideable: true
            ),
            size: 1,
            attackPower: 2,
            maxHp: 7,
            cardArt: AccountManager.Instance.CardSprites.daedream,
            abilities: AccountManager.Instance.PalAbilities.daedream
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
                blocker: true,
                tank: false,
                rideable: true
            ),
            size: 1,
            attackPower: 2,
            maxHp: 7,
            cardArt: AccountManager.Instance.CardSprites.fuddler,
            abilities: AccountManager.Instance.PalAbilities.fuddler
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
                blocker: true,
                tank: true,
                rideable: true
            ),
            size: 1,
            attackPower: 2,
            maxHp: 7,
            cardArt: AccountManager.Instance.CardSprites.dumud,
            abilities: AccountManager.Instance.PalAbilities.dumud
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
                blocker: false,
                tank: false,
                rideable: true
            ),
            size: 1,
            attackPower: 2,
            maxHp: 2,
            cardArt: AccountManager.Instance.CardSprites.pickaxe,
            abilities: AccountManager.Instance.PalAbilities.pickaxe
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
                blocker: false,
                tank: false,
                rideable: true
            ),
            size: 1,
            attackPower: 2,
            maxHp: 2,
            cardArt: AccountManager.Instance.CardSprites.axe,
            abilities: AccountManager.Instance.PalAbilities.axe
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
                blocker: false,
                tank: false,
                rideable: true
            ),
            size: 1,
            attackPower: 0,
            maxHp: 1,
            cardArt: AccountManager.Instance.CardSprites.saddle,
            abilities: AccountManager.Instance.PalAbilities.saddle
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
                case "depresso":
                    return new PalData(depresso, artIndex, cardID);
                case "daedream":
                    return new PalData(daedream, artIndex, cardID);
                case "fuddler":
                    return new PalData(fuddler, artIndex, cardID);
                case "dumud":
                    return new PalData(dumud, artIndex, cardID);
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
    }
}