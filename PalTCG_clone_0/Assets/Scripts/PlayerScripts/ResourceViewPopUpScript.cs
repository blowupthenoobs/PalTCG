using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using Resources;
public class ResourceViewPopUpScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject counterPrefab;
    private List<GameObject> activeCounters = new List<GameObject>();
    private bool isHoveringUI;

    public void SpawnCounters()
    {
        gameObject.SetActive(true);
        var fields = typeof(resources).GetFields();

        foreach(var field in fields)
        {
            var value = field.GetValue(HandScript.Instance.GatheredItems);

            var counter = Instantiate(counterPrefab, transform.position, transform.rotation);
            counter.GetComponent<ResourceCounterScript>().GetHeldField(field.Name);
            counter.transform.SetParent(gameObject.transform);

            activeCounters.Add(counter);
        }
    }

    private void CloseMenu()
    {
        while(activeCounters.Count > 0)
        {
            Destroy(activeCounters[0]);
            activeCounters.RemoveAt(0);
        }
        
        gameObject.SetActive(false);
    }

    void Update()
    {
        if(Input.GetMouseButtonUp(0) && !isHoveringUI)
            CloseMenu();
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
