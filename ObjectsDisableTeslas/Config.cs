using Exiled.API.Interfaces;
using System.ComponentModel;

namespace ObjectsDisableTeslas
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        [Description("sets what items disable the tesla gates")]
        public ItemType[] teslaDisableItems { get; set; } = { ItemType.KeycardO5, ItemType.KeycardNTFCommander, ItemType.KeycardNTFLieutenant, ItemType.KeycardSeniorGuard };
    }
}
