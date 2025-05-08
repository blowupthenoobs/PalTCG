using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScript : MonoBehaviour
{
    [SerializeField] GameObject buildingPrefab;
    private List<GameObject> buildings = new List<GameObject>();
    
    public void CreateBuilding()
    {
        
    }
}
