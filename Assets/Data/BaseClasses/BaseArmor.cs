using Data.BaseClasses.Scriptable;
using Data.Global;
using UnityEngine;

namespace Data.BaseClasses
{
    public class BaseArmor : BaseEquippable
    {
        [SerializeField] private ScriptableArmor armorData;

        [SerializeField] private ArmorStat armorStat;

        public ScriptableArmor ArmorData
        {
            get => armorData;
            set => armorData = value;
        }

        public ArmorStat ArmorStat
        {
            get => armorStat;
            set => armorStat = value;
        }
        
        protected internal void InitializeArmorStats(BaseArmor baseArmor)
        {
            BaseStat.InitializeStat(ArmorStat, baseArmor.ArmorData.baseArmor);
            BaseStat.InitializeStat(WeightStat, baseArmor.ArmorData.baseWeight);
        }
        
        public void ModifyArmorValue(BaseArmor armor, float value) // The value is set based on the "armor" skill level of each actor
        {
            armor.ArmorStat.SetCurrentStatValue(value);
        }
    }
}