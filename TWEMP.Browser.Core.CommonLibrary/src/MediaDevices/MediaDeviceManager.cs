﻿// <copyright file="MediaDeviceManager.cs" company="The OpenTWEMP Project">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TWEMP.Browser.Core.CommonLibrary.MediaDevices;

using TWEMP.Browser.Core.CommonLibrary.MediaDevices.Plugins;

/// <summary>
/// Represents a manager for available media devices of the application.
/// </summary>
public class MediaDeviceManager
{
    private const string MediaDeviceHomeFolderName = "support";
    private const string DefaultAudioFileName = "DEFAULT.mp3";

    private static MediaDeviceManager? activeManagerInstance;

    private readonly IAudioPlaybackDevice audioPlaybackDevice;

    private readonly DirectoryInfo mediaDeviceHomeDirectoryInfo;
    private readonly FileInfo defaultAudioFileInfo;

    private bool isInPlaybackInterruptModeAfterGameLaunch;

    /// <summary>
    /// Initializes a new instance of the <see cref="MediaDeviceManager"/> class.
    /// </summary>
    /// <param name="audioPlaybackDevice">An instance of a target audio playback device.</param>
    private MediaDeviceManager(IAudioPlaybackDevice audioPlaybackDevice)
    {
        this.audioPlaybackDevice = audioPlaybackDevice;

        string mediaDeviceHomeDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), MediaDeviceHomeFolderName);
        this.mediaDeviceHomeDirectoryInfo = new DirectoryInfo(mediaDeviceHomeDirectoryPath);

        if (!this.mediaDeviceHomeDirectoryInfo.Exists)
        {
            this.mediaDeviceHomeDirectoryInfo.Create();
        }

        string defaultAudioFilePath = Path.Combine(this.mediaDeviceHomeDirectoryInfo.FullName, DefaultAudioFileName);
        this.defaultAudioFileInfo = new FileInfo(defaultAudioFilePath);

        this.isInPlaybackInterruptModeAfterGameLaunch = false;

        this.MusicPlayerDevice = new GameMusicPlayer(this.audioPlaybackDevice, this.defaultAudioFileInfo);
    }

    /// <summary>
    /// Gets the current game music player device.
    /// </summary>
    public GameMusicPlayer MusicPlayerDevice { get; private set; }

    /// <summary>
    /// Creates a new configured instance of the <see cref="MediaDeviceManager"/> class by default.
    /// </summary>
    /// <returns>A new configured instance of the <see cref="MediaDeviceManager"/> class.</returns>
    public static MediaDeviceManager Create()
    {
        if (activeManagerInstance == null)
        {
            IAudioPlaybackDevice defaultPlaybackDevice = new NAudioSoundPlayer();
            MediaDeviceManager mediaDeviceManager = new (defaultPlaybackDevice);
            activeManagerInstance = mediaDeviceManager;
        }

        return activeManagerInstance;
    }

    /// <summary>
    /// Starts audio playback for a specified audio file.
    /// </summary>
    /// <param name="audioFileInfo">The audio file info to start playback.</param>
    public void StartAudioPlayback(FileInfo audioFileInfo)
    {
        if (this.IsAudioFileInPlaybackInterruptMode(audioFileInfo))
        {
            return;
        }

        this.isInPlaybackInterruptModeAfterGameLaunch = false;

        if (audioFileInfo.Exists)
        {
            this.MusicPlayerDevice.Play(audioFileInfo);
        }
        else
        {
            this.MusicPlayerDevice = new GameMusicPlayer(this.audioPlaybackDevice, this.defaultAudioFileInfo);
        }
    }

    /// <summary>
    /// Interrupts current audio playback.
    /// </summary>
    public void InterruptAudioPlayback()
    {
        if (!this.isInPlaybackInterruptModeAfterGameLaunch)
        {
            this.MusicPlayerDevice.Stop();
        }

        if (this.MusicPlayerDevice.State == GameMusicPlaybackState.Stopped)
        {
            this.MusicPlayerDevice = new GameMusicPlayer(this.audioPlaybackDevice, this.defaultAudioFileInfo);
        }

        this.isInPlaybackInterruptModeAfterGameLaunch = true;
    }

    private bool IsAudioFileInPlaybackInterruptMode(FileInfo audioFileInfo) =>
        this.IsCurrentAudioFile(audioFileInfo) && this.isInPlaybackInterruptModeAfterGameLaunch;

    private bool IsCurrentAudioFile(FileInfo audioFileInfo) =>
        this.MusicPlayerDevice.AudioFile.FullName.Equals(audioFileInfo.FullName);
}
