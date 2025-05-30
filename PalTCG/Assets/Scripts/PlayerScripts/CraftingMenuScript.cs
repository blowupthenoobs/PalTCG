using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Resources;
public class CraftingMenuScript : MonoBehaviour
{
    public static CraftingMenuScript Instance;
    [SerializeField] GameObject RecipeContainer;
    [SerializeField] GameObject RecipePrefab;
    private List<List<recipe>> recipeSets = new List<List<recipe>>();
    private List<GameObject> currentRecipeList = new List<GameObject>();
    private int handyWorkUsed;
    private int kindlingUsed;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        SetRecipeLists();
        gameObject.SetActive(false);
    }

    public void OpenCraftingMenu(int menu)
    {
        gameObject.SetActive(true);
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

    public void CloseCraftingMenu()
    {
        while (currentRecipeList.Count > 0)
        {
            Destroy(currentRecipeList[0]);
            currentRecipeList.RemoveAt(0);
        }
    }

    private void SetRecipeLists()
    {
        recipeSets = new List<List<recipe>>
        {
            new List<recipe>
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
}
