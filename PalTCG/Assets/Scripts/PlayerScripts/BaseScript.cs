using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Resources;
public class BaseScript : MonoBehaviour
{
    [SerializeField] GameObject buildingPrefab;
    private List<GameObject> buildings = new List<GameObject>();

    public void CreateBuilding(string buildingName)
    {
        buildings.Add(Instantiate(buildingPrefab, transform.position, transform.rotation));
        buildings[buildings.Count - 1].transform.SetParent(gameObject.transform);

        buildings[buildings.Count - 1].GetComponent<BuildingScript>().data.SetUpBuilding(buildingName, buildings[buildings.Count - 1]);
    }

    void Start()
    {
        CreateBuilding("craftingBench");
        CreateBuilding("miningPit");
        CreateBuilding("loggingCamp");

        BuildingScript.totalTraits = new Traits
        (
            ranch: 0,
            handyWork: 1,
            foraging: 1,
            gardening: 1,
            watering: 1,
            mining: 1,
            lumber: 1,
            transportation: 1,
            medicine: 0,
            kindling: 0,
            electric: 0,
            freezing: 0,
            dragon: 0,
            bird: false,
            blocker: false,
            tank: false
        );
    }
}
