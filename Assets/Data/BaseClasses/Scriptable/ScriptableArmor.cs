using UnityEngine;

namespace Data.BaseClasses.Scriptable
{
    [CreateAssetMenu(fileName = "New Armor", menuName = "Data/Item/Armor")]
    public class ScriptableArmor : ScriptableItem
    {
        public ScriptableArmor()
        {
            entityType = RecordType.Armor;
        }
        
        [SerializeField] protected internal float baseArmor;
    }
}