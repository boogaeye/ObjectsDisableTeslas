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
        public override Version RequiredExiledVersion { get; } = new Version(2, 10, 0, 0);
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
