using UnityEngine;

namespace Data.BaseClasses.Scriptable
{
    public class ScriptableItem : ScriptableEntity
    {
        [SerializeField]
        protected internal Sprite icon;
        [SerializeField]
        protected internal string fullName;
        [SerializeField]
        protected internal string description;
        [SerializeField]
        protected internal bool isEquipable;
        [SerializeField]
        protected internal bool isUsable;
        [SerializeField] 
        protected internal float baseWeight;
    }
}