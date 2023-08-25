using UnityEngine;

namespace Data.BaseClasses.Scriptable
{
    [CreateAssetMenu(fileName = "New Skill" , menuName = "Data/Skill")]
    public class ScriptableSkill : ScriptableEntity // ParentSkill
    {
        [SerializeField]
        protected internal string fullName;
        [SerializeField]
        protected internal string description;
        
        // Enums or nodes and List of "Perks" that are child skills and a way to set the connections between each other in something like a tree
    }
}