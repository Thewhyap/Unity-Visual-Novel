using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CHARACTERS
{
    public class Character_Live2D : Character
    {
        public Character_Live2D(string name, CharacterConfigData config, GameObject prefab, string rootAssetsFolder) : base(name, config, prefab)
        { 

        }

        // Added for Live2D after episode 11

        /*public override void OnReceiveCastingExpression(int layer, string expression)
        {
            SetExpression(expression);
        }*/
    }
}
