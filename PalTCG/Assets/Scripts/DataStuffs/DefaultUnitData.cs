using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultUnitData : MonoBehaviour
{
    public struct DefaultPal
    {
        public int cost;
        public Resources.Element element;
        public Resources.Traits traits;
        public int size;

        public DefaultPal(int cost, Resources.Element element, Resources.Traits traits, int size)
        {
            this.cost = cost;
            this.element = element;
            this.traits = traits;
            this.size = size;
        }
    }

#region BasicPals

    public DefaultPal lamball = new DefaultPal
    (
        cost: 0,
        element: Resources.Element.Basic,
        traits: new Resources.Traits
        (
            ranch: 1,
            handyWork: 1,
            foraging: 0,
            gardening: 0,
            watering: 0,
            mining: 0,
            lumber: 0,
            transportation: 1,
            medicine: 0,
            kindling: 0,
            electric: 0,
            freezing: 0,
            dragon: 0,
            bird: false
        ),
        size: 1
    );

#endregion BasicPals
}