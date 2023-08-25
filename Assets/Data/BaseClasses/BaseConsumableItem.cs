using Data.BaseClasses.Scriptable;
using UnityEngine;
using UnityEngine.Serialization;

namespace Data.BaseClasses
{
    public class BaseConsumableItem : BaseItem
    {
        [SerializeField] private ScriptableConsumableItem consumableData;
        [SerializeField] private BaseStat consumableStat;
        [SerializeField] private float consumableInitialValue;
        
        public BaseStat ConsumableStat
        {
            get => consumableStat;
            set => consumableStat = value;
        }

        public float ConsumableInitialValue
        {
            get => consumableInitialValue;
            set => consumableInitialValue = value;
        }

        public ScriptableConsumableItem ConsumableData
        {
            get => consumableData;
            set => consumableData = value;
        }


        protected internal void InitializeItemStats(BaseConsumableItem baseConsumableItem)
        {
            BaseStat.InitializeStat(ConsumableStat, baseConsumableItem.ConsumableInitialValue );
            BaseStat.InitializeStat(WeightStat, baseConsumableItem.ConsumableData.baseWeight );
            
        }

        public void ModifyItemValue(BaseConsumableItem baseConsumableItem, float value)
        {
            baseConsumableItem.ConsumableStat.SetCurrentStatValue(value);
        }
    }
}