using l_winapi.Attributes;

namespace l_winapi.Enums
{
    public enum EnumSFIcons
    {
        [IconValueAttribute("\ue896")]
        Downloads,
        [IconValueAttribute("\ued25")]
        Drives,
        [IconValueAttribute("\ue713")]
        Setting,
        [IconValueAttribute("\ue7e8")]
        Power,
        // MS -- https://learn.microsoft.com/ru-ru/windows/uwp/launch-resume/launch-settings-app
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
