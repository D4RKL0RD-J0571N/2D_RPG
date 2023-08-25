using UnityEngine;

namespace Data.BaseClasses.Scriptable
{
    [CreateAssetMenu(fileName = "New Actor", menuName = "Data/Actor")]
    public class ScriptableActor : ScriptableEntity
    {
        public ScriptableActor()
        {
            entityType = RecordType.NonPlayerCharacter;
        }
        
        [SerializeField] protected internal string fullName;
        [SerializeField] protected internal string description;
        [SerializeField] protected internal bool unique;
        [SerializeField] protected internal uint initialLevel;
        [SerializeField] protected internal float initialHealth;
        [SerializeField] protected internal float initialStamina;
        [SerializeField] protected internal float initialMagic;
        
    }
}