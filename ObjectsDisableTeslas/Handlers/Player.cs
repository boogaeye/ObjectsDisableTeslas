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
        Random random = new Random();
        public void SwitchItem(ChangingItemEventArgs ev)
        {
            if (ObjectsDisableTeslas.instance.Config.teslaDisableItems.Contains<ItemType>(ev.NewItem.id))
            {
                ev.Player.ShowHint(ObjectsDisableTeslas.instance.Config.teslaHints[random.Next(0, ObjectsDisableTeslas.instance.Config.teslaHints.Length)], ObjectsDisableTeslas.instance.Config.teslaHintDuration);
            }
        }
    }
}
