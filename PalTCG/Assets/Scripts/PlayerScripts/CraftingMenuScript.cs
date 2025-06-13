using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using Resources;
public class CraftingMenuScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static CraftingMenuScript Instance;
    [SerializeField] GameObject RecipeContainer;
    [SerializeField] GameObject RecipePrefab;
    [SerializeField] GameObject AffectedItemsList;
    [SerializeField] GameObject ItemCounterPrefab;
    private List<List<recipe>> recipeSets = new List<List<recipe>>();
    private List<GameObject> currentRecipeList = new List<GameObject>();
    public List<GameObject> affectedItemIcons = new List<GameObject>();
    private int selectedRecipe;
    private int currentMenu;
    private int handyWorkUsed;
    private int kindlingUsed;

    private bool isHoveringUI;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        SetRecipeLists();
        gameObject.SetActive(false);
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

    public void CloseCraftingMenu()
    {
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
    }

    void ResetUses()
    {
        handyWorkUsed = 0;
        kindlingUsed = 0;
    }

    public void ShowAlteredItemValues()
    {
        var itemChanges = (recipeSets[currentMenu][selectedRecipe].result - recipeSets[currentMenu][selectedRecipe].cost);
        foreach (GameObject icon in affectedItemIcons)
        {
            icon.SendMessage("ShowAlteredValue", itemChanges);
        }
    }

    public void ShowCurrentItemValues()
    {
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
}
