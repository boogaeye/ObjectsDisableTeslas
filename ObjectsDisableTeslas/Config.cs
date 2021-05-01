namespace ObjectsDisableTeslas
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Exiled.API.Interfaces;

    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public int HoldTime { get; set; } = 5;

        [Description("if true allows holdable cards to disable teslas for the amount of hold time")]
        public bool HoldTesla { get; set; } = false;

        [Description("how many times Scp207 has to be used in order to make the player immune to tesla gates. set it to -1 to disable")]
        public int Scp207LevelTeslaImmune { get; set; } = -1;

        [Description("if true allows players that used Scp268 to not trigger tesla gates")]
        public bool EnableScp268TeslaDisable { get; set; } = false;

        [Description("defines what roles disable tesla gates")]
        public RoleType[] DisableTeslaRoles { get; set; } = { };

        [Description("sets what objects will disable teslas and if the card is a holdable object or a nonholdable object.(Holdable objects need to be held nonholdable objects need to be in inventory)")]
        public Dictionary<ItemType, bool> TeslaDisableItems { get; set; } = new Dictionary<ItemType, bool>() { { ItemType.KeycardO5, true }, { ItemType.KeycardFacilityManager, true }, { ItemType.KeycardContainmentEngineer, false }, { ItemType.KeycardNTFCommander, false }, { ItemType.KeycardNTFLieutenant, false }, { ItemType.KeycardSeniorGuard, false } };

        [Description("defines hints for items that are holdable")]
        public string[] TeslaHints { get; set; } = { "This Item Will Disable tesla gates when held", "This Item Will not trigger tesla gates as long as you hold it" };

        [Description("defines hints for items that are nonholdable")]
        public string[] TeslaHintsNotHoldable { get; set; } = { "This Item Will Disable tesla gates without needing to hold it", "This Item Will not trigger tesla gates as long as its in your inventory" };

        [Description("sets the amount of time that hints will stay on the players screen")]
        public int TeslaHintDuration { get; set; } = 5;
    }
}
