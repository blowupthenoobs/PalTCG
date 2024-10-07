using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultUnitData
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


    public class Pals
    {

#region BasicPals
        public DefaultPal lamball = new DefaultPal
        (
            cost: 1,
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

        public DefaultPal cattiva = new DefaultPal
        (
            cost: 1,
            element: Resources.Element.Basic,
            traits: new Resources.Traits
            (
                ranch: 0,
                handyWork: 1,
                foraging: 1,
                gardening: 0,
                watering: 0,
                mining: 1,
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

        public DefaultPal chikipi = new DefaultPal
        (
            cost: 1,
            element: Resources.Element.Basic,
            traits: new Resources.Traits
            (
                ranch: 1,
                handyWork: 0,
                foraging: 1,
                gardening: 0,
                watering: 0,
                mining: 0,
                lumber: 0,
                transportation: 0,
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

#region PlantPals
        public DefaultPal lifmunk = new DefaultPal
        (
            cost: 1,
            element: Resources.Element.Dark,
            traits: new Resources.Traits
            (
                ranch: 0,
                handyWork: 1,
                foraging: 1,
                gardening: 1,
                watering: 0,
                mining: 0,
                lumber: 1,
                transportation: 0,
                medicine: 1,
                kindling: 0,
                electric: 0,
                freezing: 0,
                dragon: 0,
                bird: false
            ),
            size: 1
        );

        public DefaultPal tanzee = new DefaultPal
        (
            cost: 1,
            element: Resources.Element.Dark,
            traits: new Resources.Traits
            (
                ranch: 0,
                handyWork: 1,
                foraging: 1,
                gardening: 1,
                watering: 0,
                mining: 0,
                lumber: 1,
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

#endregion PlantPals

#region DarkPals
        public DefaultPal depresso = new DefaultPal
        (
            cost: 1,
            element: Resources.Element.Dark,
            traits: new Resources.Traits
            (
                ranch: 1,
                handyWork: 1,
                foraging: 0,
                gardening: 0,
                watering: 0,
                mining: 1,
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

        public DefaultPal daeDream = new DefaultPal
        (
            cost: 1,
            element: Resources.Element.Dark,
            traits: new Resources.Traits
            (
                ranch: 0,
                handyWork: 1,
                foraging: 1,
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

#endregion DarkPals
    }
    

}