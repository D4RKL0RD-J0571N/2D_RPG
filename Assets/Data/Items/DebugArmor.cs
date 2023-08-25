using Data.BaseClasses;

namespace Data.Items
{
    public class DebugArmor : BaseArmor
    {
        private void Awake()
        {
            InitializeArmorStats(this);
        }
    }
}