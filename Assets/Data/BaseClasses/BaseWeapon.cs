using Data.BaseClasses.Scriptable;
using Data.Global;
using UnityEngine;
using UnityEngine.Serialization;

namespace Data.BaseClasses
{
    public class BaseWeapon : BaseEquippable
    {
        [SerializeField] private ScriptableWeapon weaponData;

        [SerializeField] protected internal DamageStat damageStat;
        
        public ScriptableWeapon WeaponData
        {
            get => weaponData;
            set => weaponData = value;
        }

        public DamageStat DamageStat
        {
            get => damageStat;
            set => damageStat = value;
        }

        protected internal void InitializeWeaponStats(BaseWeapon baseWeapon)
        {
            BaseStat.InitializeStat(DamageStat, baseWeapon.WeaponData.baseDamage);
            BaseStat.InitializeStat(WeightStat, WeaponData.baseWeight);
        }

        public void ModifyWeaponValue(BaseWeapon weapon, float value) // The value is set based on the "armor" skill level of each actor
        {
            weapon.DamageStat.SetCurrentStatValue(value);
        }
    }
}