using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Resources;
public class CraftingMenuScript : MonoBehaviour
{
    [SerializeField] GameObject RecipeContainer;
    [SerializeField] GameObject RecipePrefab;
    private List<GameObject> currentRecipeList = new List<GameObject>();
    private int handyWorkUsed;
    private int kindlingUsed;

    public void SetUpCraftingBench()
    {
        
    }

    void ResetUses()
    {
        handyWorkUsed = 0;
        kindlingUsed = 0;
    }
}
