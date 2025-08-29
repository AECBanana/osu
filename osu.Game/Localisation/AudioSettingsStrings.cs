// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Localisation;

namespace osu.Game.Localisation
{
    public static class AudioSettingsStrings
    {
        private const string prefix = @"osu.Game.Resources.Localisation.AudioSettings";

        /// <summary>
        /// "Audio"
        /// </summary>
        public static LocalisableString AudioSectionHeader => new TranslatableString(getKey(@"audio_section_header"), @"Audio");

        /// <summary>
        /// "Devices"
        /// </summary>
        public static LocalisableString AudioDevicesHeader => new TranslatableString(getKey(@"audio_devices_header"), @"Devices");

        /// <summary>
        /// "Volume"
        /// </summary>
        public static LocalisableString VolumeHeader => new TranslatableString(getKey(@"volume_header"), @"Volume");

        /// <summary>
        /// "Output device"
        /// </summary>
        public static LocalisableString OutputDevice => new TranslatableString(getKey(@"output_device"), @"Output device");

        /// <summary>
        /// "Hitsound stereo separation"
        /// </summary>
        public static LocalisableString PositionalLevel => new TranslatableString(getKey(@"positional_hitsound_audio_level"), @"Hitsound stereo separation");

        /// <summary>
        /// "Master"
        /// </summary>
        public static LocalisableString MasterVolume => new TranslatableString(getKey(@"master_volume"), @"Master");

        /// <summary>
        /// "Master (window inactive)"
        /// </summary>
        public static LocalisableString MasterVolumeInactive => new TranslatableString(getKey(@"master_volume_inactive"), @"Master (window inactive)");

        /// <summary>
        /// "Effect"
        /// </summary>
        public static LocalisableString EffectVolume => new TranslatableString(getKey(@"effect_volume"), @"Effect");

        /// <summary>
        /// "Music"
        /// </summary>
        public static LocalisableString MusicVolume => new TranslatableString(getKey(@"music_volume"), @"Music");

        /// <summary>
        /// "Offset Adjustment"
        /// </summary>
        public static LocalisableString OffsetHeader => new TranslatableString(getKey(@"offset_header"), @"Offset Adjustment");

        /// <summary>
        /// "Audio offset"
        /// </summary>
        public static LocalisableString AudioOffset => new TranslatableString(getKey(@"audio_offset"), @"Audio offset");

        /// <summary>
        /// "Play a few beatmaps to receive a suggested offset!"
        /// </summary>
        public static LocalisableString SuggestedOffsetNote => new TranslatableString(getKey(@"suggested_offset_note"), @"Play a few beatmaps to receive a suggested offset!");

        /// <summary>
        /// "Based on the last {0} play(s), your offset is set correctly!"
        /// </summary>
        public static LocalisableString SuggestedOffsetCorrect(int plays) => new TranslatableString(getKey(@"suggested_offset_correct"), @"Based on the last {0} play(s), your offset is set correctly!", plays);

        /// <summary>
        /// "Based on the last {0} play(s), the suggested offset is {1} ms."
        /// </summary>
        public static LocalisableString SuggestedOffsetValueReceived(int plays, LocalisableString value) => new TranslatableString(getKey(@"suggested_offset_value_received"), @"Based on the last {0} play(s), the suggested offset is {1} ms.", plays, value);

        /// <summary>
        /// "Apply suggested offset"
        /// </summary>
        public static LocalisableString ApplySuggestedOffset => new TranslatableString(getKey(@"apply_suggested_offset"), @"Apply suggested offset");

        /// <summary>
        /// "Offset wizard"
        /// </summary>
        public static LocalisableString OffsetWizard => new TranslatableString(getKey(@"offset_wizard"), @"Offset wizard");

        /// <summary>
        /// "Adjust beatmap offset automatically"
        /// </summary>
        public static LocalisableString AdjustBeatmapOffsetAutomatically => new TranslatableString(getKey(@"adjust_beatmap_offset_automatically"), @"Adjust beatmap offset automatically");

        /// <summary>
        /// "If enabled, the offset suggested from last play on a beatmap is automatically applied."
        /// </summary>
        public static LocalisableString AdjustBeatmapOffsetAutomaticallyTooltip => new TranslatableString(getKey(@"adjust_beatmap_offset_automatically_tooltip"), @"If enabled, the offset suggested from last play on a beatmap is automatically applied.");

        /// <summary>
        /// "ASIO/WASAPI"
        /// </summary>
        public static LocalisableString AsioWasapiHeader => new TranslatableString(getKey(@"asio_wasapi_header"), @"ASIO/WASAPI");

        /// <summary>
        /// "Use ASIO audio"
        /// </summary>
        public static LocalisableString UseAsioAudio => new TranslatableString(getKey(@"use_asio_audio"), @"Use ASIO audio");

        /// <summary>
        /// "Use ASIO for low-latency audio output (requires ASIO compatible sound card)"
        /// </summary>
        public static LocalisableString UseAsioAudioTooltip => new TranslatableString(getKey(@"use_asio_audio_tooltip"), @"Use ASIO for low-latency audio output (requires ASIO compatible sound card)");

        /// <summary>
        /// "Use WASAPI audio"
        /// </summary>
        public static LocalisableString UseWasapiAudio => new TranslatableString(getKey(@"use_wasapi_audio"), @"Use WASAPI audio");

        /// <summary>
        /// "Use Windows Audio Session API for exclusive or shared mode audio"
        /// </summary>
        public static LocalisableString UseWasapiAudioTooltip => new TranslatableString(getKey(@"use_wasapi_audio_tooltip"), @"Use Windows Audio Session API for exclusive or shared mode audio");

        /// <summary>
        /// "ASIO buffer size"
        /// </summary>
        public static LocalisableString AsioBufferSize => new TranslatableString(getKey(@"asio_buffer_size"), @"ASIO buffer size");

        /// <summary>
        /// "Buffer size in samples for ASIO audio (lower = less latency but more CPU usage)"
        /// </summary>
        public static LocalisableString AsioBufferSizeTooltip => new TranslatableString(getKey(@"asio_buffer_size_tooltip"), @"Buffer size in samples for ASIO audio (lower = less latency but more CPU usage)");

        /// <summary>
        /// "ASIO sample rate"
        /// </summary>
        public static LocalisableString AsioSampleRate => new TranslatableString(getKey(@"asio_sample_rate"), @"ASIO sample rate");

        /// <summary>
        /// "Sample rate in Hz for ASIO audio (must match your audio device's capabilities)"
        /// </summary>
        public static LocalisableString AsioSampleRateTooltip => new TranslatableString(getKey(@"asio_sample_rate_tooltip"), @"Sample rate in Hz for ASIO audio (must match your audio device's capabilities)");

        private static string getKey(string key) => $@"{prefix}:{key}";
    }
}
