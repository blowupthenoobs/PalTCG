using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScript : MonoBehaviour
{
    [SerializeField] GameObject buildingPrefab;
    private List<GameObject> buildings = new List<GameObject>();
    
    public void CreateBuilding(string buildingName)
    {
        buildings.Add(Instantiate(buildingPrefab, transform.position, transform.rotation));
        buildings[buildings.Count -1].transform.parent = gameObject.transform;

        buildings[buildings.Count -1].GetComponent<BuildingScript>().data.SetUpBuilding(buildingName, buildings[buildings.Count -1]);
    }

    void Start()
    {
        CreateBuilding("craftingBench");
    }
}
