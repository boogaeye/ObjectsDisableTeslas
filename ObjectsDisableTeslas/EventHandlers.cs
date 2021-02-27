using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.Events.EventArgs;

namespace ObjectsDisableTeslas
{
    public class EventHandlers
    {
        private readonly ObjectsDisableTeslas plugin;
        public EventHandlers(ObjectsDisableTeslas objectsDisableTeslas) => this.plugin = plugin;
        Config config = ObjectsDisableTeslas.instance.Config;
        private readonly Random random = new Random();
        public void SwitchItem(ChangingItemEventArgs ev)
        {
            if (config.teslaDisableItems.ContainsKey(ev.NewItem.id))
            {
                if (config.teslaDisableItems[ev.NewItem.id])
                {
                    ev.Player.ShowHint(config.teslaHintsNotHoldable[random.Next(0, config.teslaHintsNotHoldable.Length)], config.teslaHintDuration);
                }
                else
                {
                    ev.Player.ShowHint(config.teslaHints[random.Next(0, config.teslaHints.Length)], config.teslaHintDuration);
                }
            }
        }
        public void OnTeslaTrigger(TriggeringTeslaEventArgs ev)
        {
            foreach (Inventory.SyncItemInfo i in ev.Player.Inventory.items)
            {
                if (config.teslaDisableItems.ContainsKey(i.id))
                {
                    if (config.teslaDisableItems[i.id])
                    {
                        ev.IsTriggerable = false;
                        break;
                    }
                    else
                    {
                        if (config.teslaDisableItems.ContainsKey(ev.Player.Inventory.curItem))
                        {
                            ev.IsTriggerable = false;
                            break;
                        }
                        else
                        {
                            ev.IsTriggerable = true;
                        }
                    }
                }
                else
                {
                    ev.IsTriggerable = true;
                }
            }
        }
    }
}
