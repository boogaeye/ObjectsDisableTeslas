using Exiled.API.Interfaces;
using System.ComponentModel;
using System.Collections.Generic;
using Exiled.Loader;

namespace ObjectsDisableTeslas
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = true;
        [Description("Item thats allowed to disable teslas : if set true then Allow to be nonholdable")]
        public Dictionary<ItemType, bool> teslaDisableItems { get; set; } = new Dictionary<ItemType, bool>() { { ItemType.KeycardO5, true }, { ItemType.KeycardFacilityManager, true }, { ItemType.KeycardContainmentEngineer, false }, { ItemType.KeycardNTFCommander, false }, { ItemType.KeycardNTFLieutenant, false }, { ItemType.KeycardSeniorGuard, false }, { ItemType.WeaponManagerTablet, true } };
        [Description("sets when you take out the items what the hint will say")]
        public string[] teslaHints { get; set; } = { "This Item Will Disable tesla gates when held", "This Item Will not trigger tesla gates as long as you hold it" };
        [Description("sets when you take out the items what the hint will say")]
        public string[] teslaHintsNotHoldable { get; set; } = { "This Item Will Disable tesla gates without needing to hold it", "This Item Will not trigger tesla gates as long as its in your inventory" };
        [Description("sets the amount of time it will say the hints")]
        public int teslaHintDuration { get; set; } = 5;
    }
}
