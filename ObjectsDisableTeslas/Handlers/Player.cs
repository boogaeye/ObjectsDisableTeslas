using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.Events.EventArgs;
using CustomPlayerEffects;

namespace ObjectsDisableTeslas.Handlers
{
    class Player
    {
        public void OnTeslaTrigger(TriggeringTeslaEventArgs ev)
        {
            if (ObjectsDisableTeslas.instance.Config.teslaDisableItems.Contains<ItemType>(ev.Player.CurrentItem.id))
            {
                ev.IsTriggerable = false;
            }
        }
        public void SwitchItem(ChangingItemEventArgs ev)
        {
            if (ObjectsDisableTeslas.instance.Config.teslaDisableItems.Contains<ItemType>(ev.NewItem.id))
            {
                ev.Player.ShowHint("This Item Will Disable tesla gates when held", 5);
            }
        }
    }
}
