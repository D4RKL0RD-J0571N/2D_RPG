using System;
using Data.BaseClasses;

namespace Data.Skill
{
    public class DebugSkill : BaseSkill
    {
        private void Awake()
        {
            InitializeSkillStat(this, 100f);
        }
    }
}