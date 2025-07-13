using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Boosters.Interfaces;
using UnityEngine;

namespace DefaultNamespace.Statics
{
    public class BuffColorBlender
    {
        public static Color MixModifierColors(List<IPlantModifier> modifiers)
        {
            if (modifiers.Count == 0) return Color.white;
            if (modifiers.Count == 1) return modifiers[0].Color;

            float r = 0, g = 0, b = 0;
            foreach (var m in modifiers)
            {
                r += m.Color.r;
                g += m.Color.g;
                b += m.Color.b;
            }
            int count = modifiers.Count;
            return new Color(r / count, g / count, b / count, 1f);
        }
    }
}