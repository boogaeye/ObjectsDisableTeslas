namespace ObjectsDisableTeslas
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CustomPlayerEffects;
    using Exiled.API.Features;
    using Exiled.Events.EventArgs;
    using MEC;

    public class EventHandlers
    {
        private readonly Plugin<Config> plugin;

        public EventHandlers(Plugin<Config> plugin) => this.plugin = plugin;

        private readonly Random random = new Random();

        public List<string> teslaLocks = new List<string>();

        public void SwitchItem(ChangingItemEventArgs ev)
        {
            if (this.plugin.Config.TeslaDisableItems.ContainsKey(ev.NewItem.id))
            {
                if (this.plugin.Config.TeslaDisableItems[ev.NewItem.id])
                {
                    if (this.plugin.Config.TeslaHintsNotHoldable.Length == 0)
                    {
                        return;
                    }

                    ev.Player.ShowHint(
                        this.plugin.Config.TeslaHintsNotHoldable[
                            random.Next(0, this.plugin.Config.TeslaHintsNotHoldable.Length)],
                        this.plugin.Config.TeslaHintDuration);
                }
                else
                {
                    if (this.plugin.Config.TeslaHints.Count() == 0)
                        return;

                    ev.Player.ShowHint(
                        this.plugin.Config.TeslaHints[random.Next(0, this.plugin.Config.TeslaHints.Length)],
                        this.plugin.Config.TeslaHintDuration);
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

            if (ev.Player.GetEffectActive<Scp268>() && this.plugin.Config.EnableScp268TeslaDisable)
            {
                ev.IsTriggerable = false;
                return;
            }

            if (this.plugin.Config.DisableTeslaRoles.Contains(ev.Player.Role))
            {
                ev.IsTriggerable = false;

                Timing.RunCoroutine(Swiping(teslaName));

                return;
            }

            foreach (Inventory.SyncItemInfo i in ev.Player.Inventory.items)
            {
                if (this.plugin.Config.TeslaDisableItems.ContainsKey(i.id))
                {
                    if (this.plugin.Config.TeslaDisableItems.TryGetValue(i.id, out bool value) && value)
                    {
                        ev.IsTriggerable = false;
                        Timing.RunCoroutine(Swiping(teslaName));
                        break;
                    }

                    if (this.plugin.Config.TeslaDisableItems.ContainsKey(ev.Player.Inventory.curItem))
                    {
                        ev.IsTriggerable = false;
                        Timing.RunCoroutine(Swiping(teslaName));
                        break;
                    }

                    ev.IsTriggerable = true;
                }
                else
                {
                    ev.IsTriggerable = true;
                }
            }
        }

        public IEnumerator<float> Swiping(string name)
        {
            if (this.plugin.Config.HoldTesla)
            {
                teslaLocks.Add(name);
                yield return Timing.WaitForSeconds(this.plugin.Config.HoldTime);
                teslaLocks.Remove(name);
            }
        }

        public void OnHurt(HurtingEventArgs ev)
        {
            if (this.plugin.Config.Scp207LevelTeslaImmune != -1 && this.plugin.Config.Scp207LevelTeslaImmune <= ev.Target.GetEffectIntensity<Scp207>())
            {
                if (ev.HitInformations.GetDamageType() == DamageTypes.Tesla)
                {
                    ev.IsAllowed = false;
                }
            }
        }

        public void HealItem(UsingMedicalItemEventArgs ev)
        {
            if (this.plugin.Config.Scp207LevelTeslaImmune != -1 && this.plugin.Config.Scp207LevelTeslaImmune - 1 <= ev.Player.GetEffectIntensity<Scp207>() && ev.Item == ItemType.SCP207)
            {
                ev.Player.ShowHint("You are now immune to tesla gates");
            }
        }
    }
}