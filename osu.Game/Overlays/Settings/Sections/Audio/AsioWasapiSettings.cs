// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Localisation;
using osu.Game.Configuration;
using osu.Game.Graphics.UserInterface;
using osu.Game.Localisation;

namespace osu.Game.Overlays.Settings.Sections.Audio
{
    public partial class AsioWasapiSettings : SettingsSubsection
    {
        protected override LocalisableString Header => AudioSettingsStrings.AsioWasapiHeader;

        private Bindable<bool> useAsioAudio;
        private Bindable<bool> useWasapiAudio;
        private Bindable<int> asioBufferSize;
        private Bindable<int> asioSampleRate;

        [BackgroundDependencyLoader]
        private void load(OsuConfigManager config)
        {
            useAsioAudio = config.GetBindable<bool>(OsuSetting.UseAsioAudio);
            useWasapiAudio = config.GetBindable<bool>(OsuSetting.UseWasapiAudio);
            asioBufferSize = config.GetBindable<int>(OsuSetting.AsioBufferSize);
            asioSampleRate = config.GetBindable<int>(OsuSetting.AsioSampleRate);

            Children = new Drawable[]
            {
                new SettingsCheckbox
                {
                    LabelText = AudioSettingsStrings.UseAsioAudio,
                    TooltipText = AudioSettingsStrings.UseAsioAudioTooltip,
                    Current = useAsioAudio,
                    Keywords = new[] { "asio", "low latency" }
                },
                new SettingsCheckbox
                {
                    LabelText = AudioSettingsStrings.UseWasapiAudio,
                    TooltipText = AudioSettingsStrings.UseWasapiAudioTooltip,
                    Current = useWasapiAudio,
                    Keywords = new[] { "wasapi", "exclusive", "shared" }
                },
                new SettingsSlider<int>
                {
                    LabelText = AudioSettingsStrings.AsioBufferSize,
                    TooltipText = AudioSettingsStrings.AsioBufferSizeTooltip,
                    Current = asioBufferSize,
                    KeyboardStep = 64,
                    Keywords = new[] { "buffer", "latency", "samples" }
                },
                new SettingsSlider<int>
                {
                    LabelText = AudioSettingsStrings.AsioSampleRate,
                    TooltipText = AudioSettingsStrings.AsioSampleRateTooltip,
                    Current = asioSampleRate,
                    KeyboardStep = 100,
                    Keywords = new[] { "sample rate", "hz", "frequency" }
                }
            };

            // Ensure only one audio mode is selected at a time
            useAsioAudio.ValueChanged += e =>
            {
                if (e.NewValue && useWasapiAudio.Value)
                    useWasapiAudio.Value = false;
            };

            useWasapiAudio.ValueChanged += e =>
            {
                if (e.NewValue && useAsioAudio.Value)
                    useAsioAudio.Value = false;
            };
        }
    }
}
