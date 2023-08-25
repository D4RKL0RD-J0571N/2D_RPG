using Data.BaseClasses;

namespace Data.Items
{
    public class DebugConsumable : BaseConsumableItem
    {
        private void Awake()
        {
            InitializeItemStats(this);
        }
    }
}