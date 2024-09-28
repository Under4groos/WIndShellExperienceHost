using l_winapi.Attributes;

namespace l_winapi.Enums
{
    /// <summary>
    /// https://learn.microsoft.com/ru-ru/windows/uwp/launch-resume/launch-settings-app
    /// </summary>
    public enum EnumSFIcons
    {
        [IconValueAttribute("\ue896", StrMS = "ms-settings:privacy-downloadsfolder")]
        Downloads,
        [IconValueAttribute("\ue7c3")]
        Documents,
        [IconValueAttribute("\ued25")]
        Drives,
        [IconValueAttribute("\ue713", StrMS = "ms-settings:privacy-holographic-environment")]
        Setting,
        [IconValueAttribute("\ue7e8")]
        Power,

        [IconValueAttribute("\ue756", StrMS = "cmd")]
        Console,
        [IconValueAttribute("\ue701", StrMS = "ms-settings:network-wifi")]
        WiFi,
        [IconValueAttribute("\uec41", StrMS = "ms-settings:bluetooth")]
        Bluetooth,
        [IconValueAttribute("\ue767", StrMS = "ms-settings:sound")]
        Sound,
        [IconValueAttribute("\ue7f3", StrMS = "ms-settings:sound-devices")]
        SoundDevices,

    }
}
