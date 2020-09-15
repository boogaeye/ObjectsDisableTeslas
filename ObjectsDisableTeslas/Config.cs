using Exiled.API.Interfaces;
using System.ComponentModel;

namespace ObjectsDisableTeslas
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        [Description("sets what items disable the tesla gates")]
        public ItemType[] teslaDisableItems { get; set; } = { ItemType.KeycardO5, ItemType.KeycardNTFCommander, ItemType.KeycardNTFLieutenant, ItemType.KeycardSeniorGuard };
        [Description("sets when you take out the items what the hint will say")]
        public string[] teslaHints { get; set; } = { "This Item Will Disable tesla gates when held", "This Item Will not trigger tesla gates as long as you hold it" };
        [Description("sets the amount of time it will say the hints")]
        public int teslaHintDuration { get; set; } = 5;
    }
}
