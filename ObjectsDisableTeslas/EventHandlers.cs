using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Enums;
using Exiled.Events.EventArgs;
using Exiled.API.Features;
using CustomPlayerEffects;
using MEC;

namespace ObjectsDisableTeslas
{
    public class EventHandlers
    {
        private readonly Plugin<Config> plugin;
        public EventHandlers(Plugin<Config> plugin) => this.plugin = plugin;
        private readonly Random random = new Random();
        public List<string> teslaLocks = new List<string>();
        public void SwitchItem(ChangingItemEventArgs ev)
        {
            if (this.plugin.Config.teslaDisableItems.ContainsKey(ev.NewItem.id))
            {
                if (this.plugin.Config.teslaDisableItems[ev.NewItem.id])
                {
                    if (this.plugin.Config.teslaHintsNotHoldable.Count() == 0)
                    {
                        return;
                    }
                    ev.Player.ShowHint(this.plugin.Config.teslaHintsNotHoldable[random.Next(0, this.plugin.Config.teslaHintsNotHoldable.Length)], this.plugin.Config.teslaHintDuration);
                }
                else
                {
                    if (this.plugin.Config.teslaHints.Count() == 0)
                    {
                        return;
                    }
                    ev.Player.ShowHint(this.plugin.Config.teslaHints[random.Next(0, this.plugin.Config.teslaHints.Length)], this.plugin.Config.teslaHintDuration);
                }
            }
        }
        public void OnTeslaTrigger(TriggeringTeslaEventArgs ev)
        {
            string teslaName = ev.Player.CurrentRoom.Name;
            if (teslaLocks.Contains(teslaName))
            {
                ev.IsTriggerable = false;
                return;
            }
            if (ev.Player.GetEffectActive<Scp268>() && this.plugin.Config.enableScp268TeslaDisable)
            {
                ev.IsTriggerable = false;
                return;
            }
            if (this.plugin.Config.disableTeslaRoles.Contains(ev.Player.Role))
            {
                ev.IsTriggerable = false;
                if (this.plugin.Config.HoldTesla)
                {
                    Timing.RunCoroutine(Swiping(teslaName));
                }
                return;
            }
            foreach (Inventory.SyncItemInfo i in ev.Player.Inventory.items)
            {
                if (this.plugin.Config.teslaDisableItems.ContainsKey(i.id))
                {
                    if (this.plugin.Config.teslaDisableItems[i.id])
                    {
                        ev.IsTriggerable = false;
                        if (this.plugin.Config.HoldTesla && ev.Player.Inventory.curItem == i.id)
                        {
                            Timing.RunCoroutine(Swiping(teslaName));
                        }
                        break;
                    }
                    else
                    {
                        if (this.plugin.Config.teslaDisableItems.ContainsKey(ev.Player.Inventory.curItem))
                        {
                            ev.IsTriggerable = false;
                            if (this.plugin.Config.HoldTesla)
                            {
                                Timing.RunCoroutine(Swiping(teslaName));
                            }
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
        public IEnumerator<float> Swiping(string name)
        {
            teslaLocks.Add(name);
            yield return Timing.WaitForSeconds(this.plugin.Config.HoldTime);
            teslaLocks.Remove(name);
        }
        public void OnHurt(HurtingEventArgs ev)
        {
            if (this.plugin.Config.SCP207LevelTeslaImmune != -1 && this.plugin.Config.SCP207LevelTeslaImmune <= ev.Target.GetEffectIntensity<Scp207>())
            {
                if (ev.HitInformations.GetDamageType() == DamageTypes.Tesla)
                {
                    ev.IsAllowed = false;
                }
            }
        }
        public void HealItem(UsingMedicalItemEventArgs ev)
        {
            if (this.plugin.Config.SCP207LevelTeslaImmune != -1 && this.plugin.Config.SCP207LevelTeslaImmune - 1 <= ev.Player.GetEffectIntensity<Scp207>() && ev.Item == ItemType.SCP207)
            {
                ev.Player.ShowHint("You are now immune to tesla gates");
            }
        }
    }
}
