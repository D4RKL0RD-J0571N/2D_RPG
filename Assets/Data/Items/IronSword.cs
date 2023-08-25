using Data.BaseClasses;

namespace Data.Items
{
    public class IronSword : BaseWeapon
    {
        private void Awake()
        {
            InitializeWeaponStats(this);
        }
    }
}