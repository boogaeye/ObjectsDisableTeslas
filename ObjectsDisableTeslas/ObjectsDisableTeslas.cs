namespace ObjectsDisableTeslas
{
    using System;
    using Exiled.API.Features;
    using Exiled.API.Interfaces;

    public class ObjectsDisableTeslas : Plugin<Config, Translations>
    {
        public override Version RequiredExiledVersion { get; } = new Version(2, 10, 0);

        public override string Author { get; } = "BoogaEye";

        public override string Name { get; } = "ObjectsDisableTeslas";

        public override Version Version { get; } = new Version(1, 0, 0);

        public EventHandlers EventHandlers;

        public override void OnEnabled()
        {
            EventHandlers = new EventHandlers(this);
            Exiled.Events.Handlers.Player.TriggeringTesla += EventHandlers.OnTeslaTrigger;
            Exiled.Events.Handlers.Player.ChangingItem += EventHandlers.SwitchItem;
            Exiled.Events.Handlers.Player.UsingMedicalItem += EventHandlers.HealItem;
            Exiled.Events.Handlers.Player.Hurting += EventHandlers.OnHurt;
            Exiled.Events.Handlers.Player.UsingMedicalItem += EventHandlers.HealItem;
            Exiled.Events.Handlers.Server.RestartingRound += EventHandlers.OnRestartingRound;
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.TriggeringTesla -= EventHandlers.OnTeslaTrigger;
            Exiled.Events.Handlers.Player.ChangingItem -= EventHandlers.SwitchItem;
            Exiled.Events.Handlers.Player.UsingMedicalItem -= EventHandlers.HealItem;
            Exiled.Events.Handlers.Player.Hurting -= EventHandlers.OnHurt;
            Exiled.Events.Handlers.Player.UsingMedicalItem -= EventHandlers.HealItem;
            Exiled.Events.Handlers.Server.RestartingRound -= EventHandlers.OnRestartingRound;
            EventHandlers = null;
        }
    }
}
