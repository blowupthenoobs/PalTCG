using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Resources;
using DefaultUnitData;
public class RecipeSelectorScript : MonoBehaviour
{
    [SerializeField] GameObject recipeCosts;
    [SerializeField] GameObject recipeResults;
    [SerializeField] GameObject recipeItemIcon;
    private Image background;

    public void CreateRecipeIcons(recipe icons)
    {
        var fields = typeof(resources).GetFields();

        foreach (var field in fields)
        {
            var value = field.GetValue(icons.cost);

            if ((int)value > 0)
            {
                var iconObj = Instantiate(recipeItemIcon, recipeCosts.transform.position, transform.rotation);
                iconObj.GetComponent<RecipeItemIconScript>().SetIcon(Pals.GetIconSprite(field.Name), (int)value);
                iconObj.transform.SetParent(recipeCosts.transform);
            }
        }

        foreach (var field in fields)
        {
            var value = field.GetValue(icons.result);

            if ((int)value > 0)
            {
                var iconObj = Instantiate(recipeItemIcon, recipeResults.transform.position, transform.rotation);
                iconObj.GetComponent<RecipeItemIconScript>().SetIcon(Pals.GetIconSprite(field.Name), (int)value);
                iconObj.transform.SetParent(recipeResults.transform);
            }
        }

        //Make palsphere logic later
    }
}
