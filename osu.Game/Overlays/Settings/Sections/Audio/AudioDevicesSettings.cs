// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using osu.Framework.Localisation;
using osu.Game.Graphics.UserInterface;
using osu.Game.Localisation;
using osu.Framework;

namespace osu.Game.Overlays.Settings.Sections.Audio
{
    public partial class AudioDevicesSettings : SettingsSubsection
    {
        protected override LocalisableString Header => AudioSettingsStrings.AudioDevicesHeader;

        [Resolved]
        private AudioManager audio { get; set; }

        private SettingsDropdown<string> dropdown;

        private void onDeviceChanged(string name) => updateItems();

        private void deviceChanged(ValueChangedEvent<string> e)
        {
            // Set the selected device directly through the AudioDevice bindable
            audio.AudioDevice.Value = e.NewValue;
        }

        private void updateItems()
        {
            var deviceItems = new List<string> { string.Empty };
            deviceItems.AddRange(audio.AudioDeviceNames);

            // Add WASAPI exclusive and shared mode options for each device (Windows only)
            if (RuntimeInfo.OS == RuntimeInfo.Platform.Windows)
            {
                foreach (var deviceName in audio.AudioDeviceNames)
                {
                    if (!string.IsNullOrEmpty(deviceName))
                    {
                        deviceItems.Add($"WASAPI Exclusive: {deviceName}");
                        deviceItems.Add($"WASAPI Shared: {deviceName}");
                    }
                }
            }

            // Add ASIO devices to the list (only available in desktop version)
            var asioDevices = osu.Framework.Audio.Asio.AsioDeviceManager.AvailableDevices.ToList();
            for (int i = 0; i < asioDevices.Count; i++)
            {
                string asioDeviceName = $"ASIO: {asioDevices[i].Name}";
                deviceItems.Add(asioDeviceName);
            }

            string preferredDeviceName = audio.AudioDevice.Value;
            if (deviceItems.All(kv => kv != preferredDeviceName))
                deviceItems.Add(preferredDeviceName);

            // The option dropdown for audio device selection lists all audio
            // device names. Dropdowns, however, may not have multiple identical
            // keys. Thus, we remove duplicate audio device names from
            // the dropdown. BASS does not give us a simple mechanism to select
            // specific audio devices in such a case anyways. Such
            // functionality would require involved OS-specific code.
            dropdown.Items = deviceItems
                             // Dropdown doesn't like null items. Somehow we are seeing some arrive here (see https://github.com/ppy/osu/issues/21271)
                             .Where(i => i != null)
                             .Distinct()
                             .ToList();
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            if (audio != null)
            {
                audio.OnNewDevice -= onDeviceChanged;
                audio.OnLostDevice -= onDeviceChanged;
            }
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            AddRange(new Drawable[]
            {
                dropdown = new AudioDeviceSettingsDropdown
                {
                    LabelText = AudioSettingsStrings.OutputDevice,
                    Current = audio.AudioDevice
                }
            });

            audio.OnNewDevice += onDeviceChanged;
            audio.OnLostDevice += onDeviceChanged;

            updateItems();
        }

        private partial class AudioDeviceSettingsDropdown : SettingsDropdown<string>
        {
            protected override OsuDropdown<string> CreateDropdown() => new AudioDeviceDropdownControl();

            private partial class AudioDeviceDropdownControl : DropdownControl
            {
                protected override LocalisableString GenerateItemText(string item)
                    => string.IsNullOrEmpty(item) ? CommonStrings.Default : base.GenerateItemText(item);
            }
        }
    }
}
