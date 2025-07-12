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
            if (modifiers.Count == 0) return Color.clear;
            if (modifiers.Count == 1) return modifiers[0].Color;
            
            var sortedModifiers = modifiers.OrderByDescending(m => m.Rating).ToList();
            
            var baseColor = sortedModifiers[0].Color;
    
            if (sortedModifiers.Count == 1) return baseColor;
            
            var resultColor = baseColor;
    
            for (int i = 1; i < sortedModifiers.Count; i++)
            {
                var modifier = sortedModifiers[i];
                var blendFactor = modifier.Rating / (float)sortedModifiers[0].Rating * 0.5f; 
                
                resultColor = BlendColors(resultColor, modifier.Color, blendFactor);
            }
    
            return resultColor;
        } 
        
        private static Color BlendColors(Color baseColor, Color blendColor, float factor)
        {
            var screenBlend = new Color(
                1f - (1f - baseColor.r) * (1f - blendColor.r),
                1f - (1f - baseColor.g) * (1f - blendColor.g),
                1f - (1f - baseColor.b) * (1f - blendColor.b),
                Mathf.Lerp(baseColor.a, blendColor.a, factor)
            );
            
            return Color.Lerp(baseColor, screenBlend, factor);
        }
    }
}