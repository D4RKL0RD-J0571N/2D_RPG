using Data.BaseClasses.Scriptable;
using Data.Global;
using UnityEngine;

namespace Data.BaseClasses
{
    public class BaseSkill : BaseEntity
    {
        [SerializeField] private ScriptableSkill skillData;
        [SerializeField] private SkillStat skillStat;

        public ScriptableSkill SkillData
        {
            get => skillData;
            set => skillData = value;
        }

        public SkillStat Stat
        {
            get => skillStat;
            set => skillStat = value;
        }

        public ScriptableSkill GetScriptableSkill( BaseSkill baseSkill)
        {
            return baseSkill.SkillData;
        }

        protected internal void InitializeSkillStat(BaseSkill baseSkill, float initialSkillValue)
        {
            BaseStat.InitializeStat(Stat, initialSkillValue);
        }
    }
}
