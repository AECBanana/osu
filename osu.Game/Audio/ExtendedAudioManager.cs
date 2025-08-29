// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using System;
using osu.Framework;
using osu.Framework.Audio;
using osu.Framework.Bindables;
using osu.Framework.Configuration;
using osu.Framework.IO.Stores;
using osu.Framework.Threading;
using osu.Game.Configuration;
using osu.Framework.Logging;

namespace osu.Game.Audio
{
    public class ExtendedAudioManager : AudioManager
    {
        private readonly OsuConfigManager config;

        private Bindable<bool> useAsioAudio;
        private Bindable<bool> useWasapiAudio;
        private Bindable<int> asioBufferSize;
        private Bindable<int> asioSampleRate;

        public ExtendedAudioManager(AudioThread audioThread, ResourceStore<byte[]> trackStore, ResourceStore<byte[]> sampleStore, OsuConfigManager config)
            : base(audioThread, trackStore, sampleStore)
        {
            this.config = config;

            // Initialize configuration bindings in constructor
            useAsioAudio = config.GetBindable<bool>(OsuSetting.UseAsioAudio);
            useWasapiAudio = config.GetBindable<bool>(OsuSetting.UseWasapiAudio);
            asioBufferSize = config.GetBindable<int>(OsuSetting.AsioBufferSize);
            asioSampleRate = config.GetBindable<int>(OsuSetting.AsioSampleRate);

            // Listen for configuration changes
            useAsioAudio.ValueChanged += _ => onAudioModeChanged();
            useWasapiAudio.ValueChanged += _ => onAudioModeChanged();
            asioBufferSize.ValueChanged += _ => onAsioConfigChanged();
            asioSampleRate.ValueChanged += _ => onAsioConfigChanged();
        }

        private void onAudioModeChanged()
        {
            if (useAsioAudio.Value)
            {
                // Switch to ASIO mode
                Logger.Log("🔈 Switching to ASIO audio mode");
                // This will trigger ASIO initialization through the AudioThread
            }
            else if (useWasapiAudio.Value)
            {
                // Switch to WASAPI mode
                Logger.Log("🔈 Switching to WASAPI audio mode");
                // This will trigger WASAPI initialization through the AudioThread
            }
            else
            {
                // Use default audio mode
                Logger.Log("🔈 Switching to default audio mode");
            }

            // Reinitialize audio device with new settings
            // We'll use the public AudioDevice bindable to trigger reinitialization
            string currentDevice = AudioDevice.Value;
            AudioDevice.Value = string.Empty; // Reset to default
            AudioDevice.Value = currentDevice; // Set back to trigger change
        }

        private void onAsioConfigChanged()
        {
            if (useAsioAudio.Value)
            {
                Logger.Log($"🔈 ASIO configuration changed: BufferSize={asioBufferSize.Value}, SampleRate={asioSampleRate.Value}");
                // Reinitialize ASIO with new settings
                onAudioModeChanged();
            }
        }

        protected override bool InitAsio(int asioDeviceIndex)
        {
            if (!useAsioAudio.Value)
                return false;

            Logger.Log($"🔈 Initializing ASIO device {asioDeviceIndex} with BufferSize={asioBufferSize.Value}, SampleRate={asioSampleRate.Value}");

            // For now, we'll use the base implementation which just logs
            // In a real implementation, we would configure ASIO with the specified parameters
            return base.InitAsio(asioDeviceIndex);
        }

        protected override bool InitBass(int device)
        {
            if (useWasapiAudio.Value)
            {
                // Configure WASAPI-specific settings
                Logger.Log("🔈 Configuring for WASAPI audio");
                // Additional WASAPI configuration could go here
            }

            return base.InitBass(device);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                useAsioAudio?.UnbindAll();
                useWasapiAudio?.UnbindAll();
                asioBufferSize?.UnbindAll();
                asioSampleRate?.UnbindAll();
            }
        }
    }
}
