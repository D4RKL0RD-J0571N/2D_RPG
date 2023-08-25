using System;
using Data.BaseClasses.Scriptable;
using UnityEngine;

namespace Data.BaseClasses
{
    // Magic Effects
    [Serializable]
    public class BaseMagicEffect
    {
        [SerializeField] private float baseBaseValue;
        
        [SerializeField] private ScriptableMagicEffect scriptableMagicEffect;

        public ScriptableMagicEffect MagicEffect
        {
            get => scriptableMagicEffect;
            set => scriptableMagicEffect = value;
        }

        public float BaseValue
        {
            get => baseBaseValue;
            set => baseBaseValue = value;
        }

        public ScriptableMagicEffect GetScriptableEffect(BaseMagicEffect baseMagicEffect)
        {
            return baseMagicEffect.MagicEffect;
        }

        public float GetCurrentEffectValue(BaseMagicEffect baseMagicEffect)
        {
            return baseMagicEffect.BaseValue;

        }
        
        public float GetMagicEffectValue(BaseMagicEffect baseMagicEffect)
        {
            return baseMagicEffect.MagicEffect.baseValue;
        }

        public void ModifyMagicEffectValue(BaseMagicEffect magicEffect, float value)
        {
            magicEffect.BaseValue += value;
        }
    }
}