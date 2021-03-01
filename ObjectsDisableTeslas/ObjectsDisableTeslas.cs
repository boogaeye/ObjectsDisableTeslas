using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using Exiled.API.Enums;

namespace ObjectsDisableTeslas
{
    public class ObjectsDisableTeslas : Plugin<Config>
    {
        private static readonly Lazy<ObjectsDisableTeslas> LazyInstance = new Lazy<ObjectsDisableTeslas>(valueFactory: () => new ObjectsDisableTeslas());
        static public ObjectsDisableTeslas instance => LazyInstance.Value;
        public override PluginPriority Priority { get; } = PluginPriority.Lowest;
        public override string Author { get; } = "BoogaEye";
        public EventHandlers EventHandlers;
        public ObjectsDisableTeslas()
        {

        }
        public override void OnEnabled()
        {
            EventHandlers = new EventHandlers(this);
            Exiled.Events.Handlers.Player.TriggeringTesla += EventHandlers.OnTeslaTrigger;
            Exiled.Events.Handlers.Player.ChangingItem += EventHandlers.SwitchItem;
            Exiled.Events.Handlers.Player.UsingMedicalItem += EventHandlers.HealItem;
            Exiled.Events.Handlers.Player.Hurting += EventHandlers.OnHurt;
            Exiled.Events.Handlers.Player.UsingMedicalItem += EventHandlers.HealItem;
        }
        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.TriggeringTesla -= EventHandlers.OnTeslaTrigger;
            Exiled.Events.Handlers.Player.ChangingItem -= EventHandlers.SwitchItem;
            Exiled.Events.Handlers.Player.UsingMedicalItem -= EventHandlers.HealItem;
            Exiled.Events.Handlers.Player.Hurting -= EventHandlers.OnHurt;
            Exiled.Events.Handlers.Player.UsingMedicalItem -= EventHandlers.HealItem;
        }
    }
}
