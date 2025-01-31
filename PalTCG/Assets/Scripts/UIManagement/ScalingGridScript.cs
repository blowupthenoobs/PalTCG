using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScalingGridScript : MonoBehaviour
{
    [SerializeField] Vector2 defaultGridDimensions;
    [SerializeField] Vector2 defaultSpacingDimensions;
    [SerializeField] Vector2 maxGridDimensions;
    private GridLayoutGroup grid;
    [SerializeField] bool isVerticle;
    void Start()
    {
        grid = GetComponent<GridLayoutGroup>();

        UpdateCellSizes();
    }

    void UpdateCellSizes()
    {
        Vector2 gridSize = GetComponent<RectTransform>().rect.size;
        Vector2 gridPadding = new Vector2(GetComponent<GridLayoutGroup>().padding.left + GetComponent<GridLayoutGroup>().padding.right, GetComponent<GridLayoutGroup>().padding.top + GetComponent<GridLayoutGroup>().padding.bottom);

        Vector2 newGridDimensions = new Vector2();
        Vector2 newSpacingDimensions = new Vector2();
        
        if(!isVerticle)
        {
            float percentToMake = (gridSize.x - gridPadding.x) / (defaultGridDimensions.x * maxGridDimensions.x + defaultSpacingDimensions.x * (maxGridDimensions.x - 1));
            newGridDimensions = defaultGridDimensions * percentToMake;
            newSpacingDimensions = defaultSpacingDimensions * percentToMake;
        }


        grid.cellSize = newGridDimensions;
        grid.spacing = newSpacingDimensions;
    }
}
