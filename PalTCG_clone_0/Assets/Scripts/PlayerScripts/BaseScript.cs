using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

using Resources;
public class BaseScript : MonoBehaviour
{
    public PhotonView opponentMirror;
    [SerializeField] GameObject buildingPrefab;
    private List<GameObject> buildings = new List<GameObject>();

    public void CreateBuilding(string buildingName)
    {
        buildings.Add(Instantiate(buildingPrefab, transform.position, transform.rotation));
        buildings[buildings.Count - 1].transform.SetParent(gameObject.transform);

        buildings[buildings.Count - 1].GetComponent<BuildingScript>().data.SetUpBuilding(buildingName, buildings[buildings.Count - 1]);
        opponentMirror.RPC("CreateBuilding", RpcTarget.OthersBuffered, buildingName);
    }

    public void CreateBuilding(BuildingData data)
    {
        buildings.Add(Instantiate(buildingPrefab, transform.position, transform.rotation));
        buildings[buildings.Count - 1].transform.SetParent(gameObject.transform);

        buildings[buildings.Count - 1].GetComponent<BuildingScript>().data = data;
        data.SetToGameObject(buildings[buildings.Count - 1]);

        opponentMirror.RPC("CreateBuilding", RpcTarget.OthersBuffered, data.buildingType);
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
            tags: new List<string>{}
        );
    }

    public void CheckForCard()
    {
        if(GameManager.Instance.phase == "PlayerTurn")
        {
            if(HandScript.Instance.selected != null && HandScript.Instance.state == "default")
            {
                if(HandScript.Instance.selected.GetComponent<SchematicScript>() != null)
                {
                    GameManager.Instance.ShowConfirmationButtons();
                    HandScript.Instance.state = "awaitingDecision";
                    HandScript.Instance.updateSelection += VerifyButtons;
                    ConfirmationButtons.Instance.Confirmed += PayForCard;
                    ConfirmationButtons.Instance.Denied += DisengagePurchase;
                }

                VerifyButtons();
            }
        }
        else if(HandScript.Instance.selected != null)
        {
            if(HandScript.Instance.selected.GetComponent<CardScript>() != null || HandScript.Instance.selected.GetComponent<SchematicScript>() != null)
            {
                HandScript.Instance.selected.SendMessage("Deselect");
                HandScript.Instance.selected = null;
            }
        }
    }
    private void PayForCard()
    {
        AllowConfirmations.ClearButtonEffects();
        GameManager.Instance.HideConfirmationButtons();

        var data = HandScript.Instance.selected.GetComponent<SchematicScript>().cardData;
        // opponentMirror.RPC("CreateCard", RpcTarget.Others, data.originalData.cardID);

        CreateBuilding(data);

        HandScript.Instance.BuildingDeck.RemoveAt(HandScript.Instance.BuildingDeck.IndexOf(HandScript.Instance.selected));
        Destroy(HandScript.Instance.selected);
        HandScript.Instance.selected = null;

        HandScript.Instance.GatheredItems -= data.cost;

        HandScript.Instance.state = "default";
    }

    void VerifyButtons()
    {
        ConfirmationButtons.Instance.AllowConfirmation(HandScript.Instance.GatheredItems >= HandScript.Instance.selected.GetComponent<SchematicScript>().cardData.cost);
    }
    
    void DisengagePurchase()
    {
        AllowConfirmations.ClearButtonEffects();
        GameManager.Instance.HideConfirmationButtons();

        HandScript.Instance.selected.SendMessage("Deselect");
        HandScript.Instance.selected = null;

        HandScript.Instance.state = "default";
    }
}
