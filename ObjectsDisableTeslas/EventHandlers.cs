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
        private readonly ObjectsDisableTeslas plugin;

        public EventHandlers(ObjectsDisableTeslas plugin) => this.plugin = plugin;

        private readonly Random random = new Random();

        public List<string> teslaLocks = new List<string>();

        public void SwitchItem(ChangingItemEventArgs ev)
        {
            if (plugin.Config.TeslaDisableItems.ContainsKey(ev.NewItem.id))
            {
                if (plugin.Config.TeslaDisableItems.TryGetValue(ev.NewItem.id, out bool value) && value)
                {
                    if (plugin.Config.TeslaHintsNotHoldable.Length == 0)
                    {
                        return;
                    }

                    ev.Player.ShowHint(
                        plugin.Config.TeslaHintsNotHoldable[
                            random.Next(0, plugin.Config.TeslaHintsNotHoldable.Length)],
                        plugin.Config.TeslaHintDuration);
                }
                else
                {
                    if (plugin.Config.TeslaHints.Length == 0)
                        return;

                    ev.Player.ShowHint(
                        plugin.Config.TeslaHints[random.Next(0, plugin.Config.TeslaHints.Length)],
                        plugin.Config.TeslaHintDuration);
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

            if (ev.Player.GetEffectActive<Scp268>() && plugin.Config.EnableScp268TeslaDisable)
            {
                ev.IsTriggerable = false;
                return;
            }

            if (plugin.Config.DisableTeslaRoles.Contains(ev.Player.Role))
            {
                ev.IsTriggerable = false;

                Timing.RunCoroutine(Swiping(teslaName));

                return;
            }

            foreach (Inventory.SyncItemInfo i in ev.Player.Inventory.items)
            {
                if (plugin.Config.TeslaDisableItems.TryGetValue(i.id, out bool value) && value)
                {
                    ev.IsTriggerable = false;
                    break;
                }

                if (plugin.Config.TeslaDisableItems.ContainsKey(ev.Player.Inventory.curItem))
                {
                    ev.IsTriggerable = false;
                    Timing.RunCoroutine(Swiping(teslaName));
                    break;
                }

                ev.IsTriggerable = true;
            }
        }

        public IEnumerator<float> Swiping(string name)
        {
            if (plugin.Config.HoldTesla && !teslaLocks.Contains(name))
            {
                teslaLocks.Add(name);
                yield return Timing.WaitForSeconds(plugin.Config.HoldTime);
                teslaLocks.Remove(name);
            }
        }

        public void OnHurt(HurtingEventArgs ev)
        {
            if (plugin.Config.Scp207LevelTeslaImmune != -1 && plugin.Config.Scp207LevelTeslaImmune <= ev.Target.GetEffectIntensity<Scp207>() && ev.HitInformations.GetDamageType() == DamageTypes.Tesla)
            {

                ev.IsAllowed = false;

            }
        }

        public void HealItem(UsingMedicalItemEventArgs ev)
        {
            if (plugin.Config.Scp207LevelTeslaImmune != -1 && plugin.Config.Scp207LevelTeslaImmune - 1 <= ev.Player.GetEffectIntensity<Scp207>() && ev.Item == ItemType.SCP207)
            {
                ev.Player.ShowHint(plugin.Config.TextTranslations.Scp207TeslaImmuneText);
            }
        }
    }
}