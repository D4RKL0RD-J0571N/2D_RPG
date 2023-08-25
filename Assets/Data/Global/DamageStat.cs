using System;
using Data.BaseClasses;
using Data.BaseClasses.Scriptable;

namespace Data.Global
{
    [Serializable]
    public class DamageStat : BaseStat
    {
        public float GetWeaponInitialValue(ScriptableWeapon scriptableWeapon)
        {
            var initialValue = scriptableWeapon.baseDamage;
            return initialValue;
        }
    }
}