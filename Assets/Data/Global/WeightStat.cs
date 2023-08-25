using System;
using Data.BaseClasses;
using Data.BaseClasses.Scriptable;

namespace Data.Global
{
    [Serializable]  
    public class WeightStat : BaseStat
    {
        public float GetItemBaseWeight(ScriptableItem scriptableItem)
        {
            var initialValue = scriptableItem.baseWeight;
            return initialValue;
        }
    }
}