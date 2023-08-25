using UnityEngine;

namespace Data.BaseClasses.Scriptable
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Data/Item/Weapon")]
    public class ScriptableWeapon : ScriptableItem
    {
        public ScriptableWeapon()
        {
            entityType = RecordType.Weapon;
        }
        
        [SerializeField] protected internal float baseDamage;
    }
}