using System;
using Data.BaseClasses;
using UnityEngine;

namespace Data.Global
{
    [Serializable]
    public class LevelStat : BaseStat
    {
        [SerializeField] private float experience;
        [SerializeField] private float experienceToLevelUpBase;
        [SerializeField] protected float experienceToLevelUpMultiplier;

        public float GetCurrentExperience()
        {
            return experience;
        }

        public float GetExperienceToLevelUp()
        {
            return experienceToLevelUpBase + (experienceToLevelUpMultiplier * GetCurrentExperience());
        }
    }
}