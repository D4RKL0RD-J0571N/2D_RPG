using System;
using UnityEngine;

namespace Data.BaseClasses
{
    // Stats
    [Serializable]
    public class BaseStat
    {
        
        [SerializeField] private float baseValue; // The value without modifiers, is the base value

        [SerializeField] private float finalValue; // The value after the modifiers the one that will be shown in the actor when equipped it depends on the baseValue

        public float BaseValue
        {
            get => baseValue;
            set => baseValue = value;
        }

        public float FinalValue
        {
            get => finalValue;
            set => finalValue = value;
        }

        public float GetStatValue(BaseStat stat)
        {
            return stat.FinalValue;
        }

        public void SetCurrentStatValue(float value)
        {
            FinalValue += value;
        }
        
        public static void InitializeStat(BaseStat stat, float initialValue)
        {
            stat.BaseValue = initialValue;
            stat.FinalValue = stat.BaseValue;
        }

    }
}