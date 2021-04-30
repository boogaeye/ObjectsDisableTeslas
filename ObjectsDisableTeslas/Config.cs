using Exiled.API.Interfaces;
using System.ComponentModel;
using System.Collections.Generic;
using Exiled.Loader;

namespace ObjectsDisableTeslas
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public int HoldTime { get; set; } = 5;
        [Description("Disables the tesla gate for the Hold Time")]
        public bool HoldTesla { get; set; } = false;
        [Description("the level scp207 has to be to be immune to tesla gates. set it to -1 to disable")]
        public int SCP207LevelTeslaImmune { get; set; } = -1;
        [Description("makes it where if your invisable you cant trigger teslas")]
        public bool enableScp268TeslaDisable { get; set; } = false;
        [Description("sets certain roles that wont trigger tesla gates however personally I recommend leaving this at its default value")]
        public RoleType[] disableTeslaRoles { get; set; } = { };
        [Description("Item thats allowed to disable teslas : if set true then Allow to be nonholdable")]
        public Dictionary<ItemType, bool> teslaDisableItems { get; set; } = new Dictionary<ItemType, bool>() { { ItemType.KeycardO5, true }, { ItemType.KeycardFacilityManager, true }, { ItemType.KeycardContainmentEngineer, false }, { ItemType.KeycardNTFCommander, false }, { ItemType.KeycardNTFLieutenant, false }, { ItemType.KeycardSeniorGuard, false } };
        [Description("sets when you take out the items what the hint will say")]
        public string[] teslaHints { get; set; } = { "This Item Will Disable tesla gates when held", "This Item Will not trigger tesla gates as long as you hold it" };
        [Description("sets when you take out the items what the hint will say")]
        public string[] teslaHintsNotHoldable { get; set; } = { "This Item Will Disable tesla gates without needing to hold it", "This Item Will not trigger tesla gates as long as its in your inventory" };
        [Description("sets the amount of time it will say the hints")]
        public int teslaHintDuration { get; set; } = 5;
    }
}
