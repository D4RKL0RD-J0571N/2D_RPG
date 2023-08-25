using Data.BaseClasses;

namespace Data.Items
{
    public class DebugSword : BaseWeapon
    {
        private void Awake()
        {
            InitializeWeaponStats(this);
        }
    }
}