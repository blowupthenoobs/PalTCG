using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

using DefaultUnitData;
public class EnemyBaseScript : MonoBehaviour
{
    [SerializeField] GameObject buildingPrefab;
    [SerializeField] PhotonView opponentMirror;

    [PunRPC]
    public void CreateBuilding(string buildingName)
    {
        var newBuilding = Instantiate(buildingPrefab, transform.position, transform.rotation);
        newBuilding.transform.SetParent(gameObject.transform);

        newBuilding.GetComponent<Image>().sprite = Pals.GetBuildingInfo(buildingName).cardArt;
    }
}
