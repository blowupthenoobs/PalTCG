using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ScreenCalculations : MonoBehaviour
{

    public static float GetAspectRatio(GameObject UIelement)
    {
        float referenceRatio = (float)GetCanvas(UIelement).GetComponent<CanvasScaler>().referenceResolution.x/(float)GetCanvas(UIelement).GetComponent<CanvasScaler>().referenceResolution.y;
        float screenRatio = (float)Screen.height/(float)Screen.width;

        return referenceRatio * screenRatio;
    }

    public static float GetScale(GameObject UIelement)
    {
        return GetCanvas(UIelement).GetComponent<Canvas>().scaleFactor;
    }

    static GameObject GetCanvas(GameObject currentObject)
    {
        GameObject target = null;

        while(target == null)
        {
            if(currentObject.GetComponent<Canvas>() != null)
                target = currentObject;
            else
                currentObject = currentObject.transform.parent.gameObject;
        }

        return target;
    }

    public static GameObject TopUI()
    {
        return GameObject.Find("TopElements");
    }
}
