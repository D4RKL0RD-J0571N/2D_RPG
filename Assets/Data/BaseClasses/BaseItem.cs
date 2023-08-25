using Data.Global;
using UnityEngine;

namespace Data.BaseClasses
{
    public class BaseItem : BaseEntity
    {
        [SerializeField] private WeightStat weightStat;
        [SerializeField] private uint itemCount;
        
        
        public uint ItemCount
        {
            get => itemCount;
            set => itemCount = value;
        }

        public WeightStat WeightStat
        {
            get => weightStat;
            set => weightStat = value;
        }
    }
}                       