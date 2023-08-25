using UnityEngine;

namespace Data.BaseClasses.Scriptable
{
    public class ScriptableMagicEffect : ScriptableEntity
    {
        public ScriptableMagicEffect()
        {
            entityType = RecordType.MagicEffect;
        }

        [SerializeField]
        protected internal Sprite icon;
        [SerializeField]
        protected internal string fullName;
        [SerializeField]
        protected internal string description;
        [SerializeField]
        protected internal float baseValue;
    }
}