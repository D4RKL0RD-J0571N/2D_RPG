using System;
using Data.BaseClasses;
using Data.BaseClasses.Scriptable;

namespace Data.Global
{
    [Serializable]
    public class ArmorStat : BaseStat
    {
        public float GetArmorInitialValue(ScriptableArmor scriptableArmor)
        {
            var initialValue = scriptableArmor.baseArmor;
            return initialValue;
        }
    }
}