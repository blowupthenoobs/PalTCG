using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

using Resources;
using DefaultUnitData;
public class CraftingMenuScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static CraftingMenuScript Instance;
    [SerializeField] GameObject RecipeContainer;
    [SerializeField] GameObject RecipePrefab;
    [SerializeField] GameObject AffectedItemsList;
    [SerializeField] GameObject ItemCounterPrefab;
    [SerializeField] Image UsedTraitIcon;
    [SerializeField] TMP_Text currentAvailableTrait;
    private Color normalTextColor;
    [SerializeField] Color negativeTextColor;
    private List<List<recipe>> recipeSets = new List<List<recipe>>();
    private List<GameObject> currentRecipeList = new List<GameObject>();
    public List<GameObject> affectedItemIcons = new List<GameObject>();
    private int selectedRecipe;
    private int currentMenu;
    private Traits usedTraits;
    private List<Traits> selectedMenuTrait = new List<Traits>();

    private bool isHoveringUI;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        SetRecipeLists();
        gameObject.SetActive(false);
        normalTextColor = currentAvailableTrait.color;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0) && !isHoveringUI)
            CloseCraftingMenu();
    }

    public void OpenCraftingMenu(int menu)
    {
        currentMenu = menu;
        gameObject.SetActive(true);
        RecipeSelectorScript.nextRecipeIndex = 0;
        SetUpCraftingBench(recipeSets[menu]);
        SetTraitIcon();
    }

    private void SetUpCraftingBench(List<recipe> recipeList) //needs to take in a set of recipes, instantiate their buttons, and place them
    {
        foreach (recipe temp in recipeList)
        {
            currentRecipeList.Add(Instantiate(RecipePrefab, RecipeContainer.transform.position, transform.rotation));
            currentRecipeList[currentRecipeList.Count - 1].transform.SetParent(RecipeContainer.transform);
            currentRecipeList[currentRecipeList.Count - 1].GetComponent<RecipeSelectorScript>().CreateRecipeIcons(temp);
        }
    }

    public void SelectRecipe(int newIndex)
    {
        while (affectedItemIcons.Count > 0)
        {
            Destroy(affectedItemIcons[0]);
            affectedItemIcons.RemoveAt(0);
        }

        currentRecipeList[selectedRecipe].SendMessage("Deselect");
        selectedRecipe = newIndex;
        currentRecipeList[selectedRecipe].SendMessage("Select");
        CreateResourceCounters(recipeSets[currentMenu][selectedRecipe]);
    }

    private void SetTraitIcon()
    {
        UsedTraitIcon.sprite = Pals.GetTraitSprite(GetAllUsedIntTraits(selectedMenuTrait[currentMenu])[0].Name);

        var usedField = GetAllUsedIntTraits(selectedMenuTrait[currentMenu])[0];
        int count = ((int)(usedField.GetValue(BuildingScript.totalTraits)) - (int)(usedField.GetValue(usedTraits)));

        currentAvailableTrait.text = count.ToString();

        if (count >= 0)
            currentAvailableTrait.color = normalTextColor;
        else
            currentAvailableTrait.color = negativeTextColor;
    }

    private void CreateResourceCounters(recipe affectedResources)
    {
        var fields = typeof(resources).GetFields();

        foreach (var field in fields)
        {
            var value = field.GetValue(affectedResources.cost);

            if ((int)value > 0)
            {
                var counter = Instantiate(ItemCounterPrefab, AffectedItemsList.transform.position, transform.rotation);
                counter.GetComponent<ResourceCounterScript>().GetHeldField(field.Name);
                counter.transform.SetParent(AffectedItemsList.transform);

                affectedItemIcons.Add(counter);
            }
        }

        foreach (var field in fields)
        {
            var value = field.GetValue(affectedResources.result);

            if ((int)value > 0)
            {
                var counter = Instantiate(ItemCounterPrefab, AffectedItemsList.transform.position, transform.rotation);
                counter.GetComponent<ResourceCounterScript>().GetHeldField(field.Name);
                counter.transform.SetParent(AffectedItemsList.transform);

                affectedItemIcons.Add(counter);
            }
        }
    }


    public void CraftSelectedRecipe()
    {
        if ((HandScript.Instance.GatheredItems >= recipeSets[currentMenu][selectedRecipe].cost) && ((BuildingScript.totalTraits - usedTraits) >= selectedMenuTrait[currentMenu]) && HandScript.Instance.state == "default")
        {
            HandScript.Instance.GatheredItems -= recipeSets[currentMenu][selectedRecipe].cost;
            HandScript.Instance.GatheredItems += recipeSets[currentMenu][selectedRecipe].result;

            usedTraits += selectedMenuTrait[currentMenu];

            //Play some sort of animation?
            ShowAlteredItemValues();
        }
    }

    public void CloseCraftingMenu()
    {
        while (affectedItemIcons.Count > 0)
        {
            Destroy(affectedItemIcons[0]);
            affectedItemIcons.RemoveAt(0);
        }

        while (currentRecipeList.Count > 0)
        {
            Destroy(currentRecipeList[0]);
            currentRecipeList.RemoveAt(0);
        }

        gameObject.SetActive(false);
    }

    private void SetRecipeLists()
    {
        recipeSets = new List<List<recipe>>
        {
            new List<recipe> //Crafting Bench Recipes
            {
                new recipe
                {
                    cost = new resources{wool = 2},
                    result = new resources{cloth = 1}
                },

                new recipe
                {
                    cost = new resources{wood = 1, stone = 1},
                    result = new resources{normalArrows = 3}
                },

                new recipe
                {
                    cost = new resources{stone = 4},
                    result = new resources{paldium = 1}
                },

                // new recipe
                // {
                //     cost = new resources{wood = 3, stone = 3, paldium = 1},
                //     palSphereLevels = new List<int>{1}
                // },

                new recipe
                {
                    cost = new resources{wood = 1, stone = 1, poisonGland = 1},
                    result = new resources{poisonArrows = 3}
                }
            }
        };

        selectedMenuTrait = new List<Traits>
        {
            new Traits{handyWork = 1},
            new Traits{kindling = 1},
        };
    }

    void ResetUses()
    {
        usedTraits = new Traits();
    }

    public void ShowAlteredItemValues()
    {
        var usedField = GetAllUsedIntTraits(selectedMenuTrait[currentMenu])[0];
        int count = ((int)usedField.GetValue(BuildingScript.totalTraits) - (int)usedField.GetValue(usedTraits + selectedMenuTrait[currentMenu]));

        currentAvailableTrait.text = count.ToString();

        if (count >= 0)
            currentAvailableTrait.color = normalTextColor;
        else
            currentAvailableTrait.color = negativeTextColor;


        var itemChanges = (recipeSets[currentMenu][selectedRecipe].result - recipeSets[currentMenu][selectedRecipe].cost);
        foreach (GameObject icon in affectedItemIcons)
        {
            icon.SendMessage("ShowAlteredValue", itemChanges);
        }
    }

    public void ShowCurrentItemValues()
    {
        var usedField = GetAllUsedIntTraits(selectedMenuTrait[currentMenu])[0];
        int count = ((int)usedField.GetValue(BuildingScript.totalTraits) - (int)usedField.GetValue(usedTraits));

        currentAvailableTrait.text = count.ToString();

        if (count >= 0)
            currentAvailableTrait.color = normalTextColor;
        else
            currentAvailableTrait.color = negativeTextColor;


        foreach (GameObject icon in affectedItemIcons)
        {
            icon.SendMessage("ShowNormalValue");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHoveringUI = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHoveringUI = false;
    }

    private List<FieldInfo> GetAllUsedIntTraits(Traits traitList)
    {
        List<FieldInfo> allUsedTraits = new List<FieldInfo>();
        var fields = typeof(Traits).GetFields();

        foreach (var field in fields)
        {
            var value = field.GetValue(traitList);

            if (value is int)
            {
                if ((int)value > 0)
                    allUsedTraits.Add(field);
            }
        }

        return allUsedTraits;
    }

    public void RefreshTraitUses()
    {
        usedTraits = new Traits();
    }
}
