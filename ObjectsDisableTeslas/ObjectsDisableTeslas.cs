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
        public ObjectsDisableTeslas()
        {

        }
        Handlers.Player player;
        public override void OnEnabled()
        {
            player = new Handlers.Player();
            Exiled.Events.Handlers.Player.TriggeringTesla += player.OnTeslaTrigger;
            Exiled.Events.Handlers.Player.ChangingItem += player.SwitchItem;
        }
        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.TriggeringTesla -= player.OnTeslaTrigger;
            Exiled.Events.Handlers.Player.ChangingItem -= player.SwitchItem;
        }
    }
}
