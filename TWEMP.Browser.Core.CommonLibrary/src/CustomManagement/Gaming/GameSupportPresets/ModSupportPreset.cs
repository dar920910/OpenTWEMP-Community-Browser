﻿// <copyright file="ModSupportPreset.cs" company="The OpenTWEMP Project">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#pragma warning disable SA1600 // ElementsMustBeDocumented

namespace TWEMP.Browser.Core.CommonLibrary.CustomManagement.Gaming.GameSupportPresets;

public record ModSupportPreset
{
    public ModSupportPreset(
        ModHeaderInfo headerInfo,
        ModContentInfo contentInfo,
        ModLauncherInfo launcherInfo,
        ModSocialMediaInfo socialMediaInfo)
    {
        this.HeaderInfo = headerInfo;
        this.ContentInfo = contentInfo;
        this.LauncherInfo = launcherInfo;
        this.SocialMediaInfo = socialMediaInfo;
    }

    public ModHeaderInfo HeaderInfo { get; set; }

    public ModContentInfo ContentInfo { get; set; }

    public ModLauncherInfo LauncherInfo { get; set; }

    public ModSocialMediaInfo SocialMediaInfo { get; set; }

    public static ModSupportPreset CreateDefaultTemplate()
    {
        const string modTitle = "My_Title";
        const string modVersion = "My_Version";

        ModHeaderInfo header = new (modTitle, modVersion);

        const string modLogoFileName = "DEFAULT.png";
        const string modMusicFileName = "My_Background_SoundTrack.mp3";

        ModContentInfo content = new (modLogoFileName, modMusicFileName);

        ModLauncherInfo launcher = ModLauncherInfo.CreateByDefault();

        ModSocialMediaInfo socialmedia = new (new Dictionary<string, string>()
        {
            { "URL1", "https://my-public-mod-url1.twemp/" },
            { "URL2", "https://my-public-mod-url2.twemp" },
            { "URL3", "https://my-public-mod-url3.twemp" },
        });

        return new ModSupportPreset(header, content, launcher, socialmedia);
    }
}

public record CustomizableModPreset
{
    private const string PresetFolderName = ".twemp";
    private const string PresetConfigFileName = "twemp_preset_config.json";

    public CustomizableModPreset(ModSupportPreset preset, string modURI)
    {
        this.Data = preset;

        string directoryPath = Path.Combine(modURI, PresetFolderName);
        this.Location = new DirectoryInfo(directoryPath);

        string filePath = Path.Combine(this.Location.FullName, PresetConfigFileName);
        this.Config = new FileInfo(filePath);
    }

    public ModSupportPreset Data { get; set; }

    public DirectoryInfo Location { get; set; }

    public FileInfo Config { get; set; }

    public static CustomizableModPreset CreateDefaultTemplate(string modURI)
    {
        ModSupportPreset preset = ModSupportPreset.CreateDefaultTemplate();
        return new CustomizableModPreset(preset, modURI);
    }

    public static bool Exists(string modURI)
    {
        string presetConfigFilePath = Path.Combine(modURI, PresetFolderName, PresetConfigFileName);
        return File.Exists(presetConfigFilePath);
    }

    public static CustomizableModPreset ReadCurrentPreset(string modURI)
    {
        string presetConfigFilePath = Path.Combine(modURI, PresetFolderName, PresetConfigFileName);

        CustomizableModPreset preset;
        try
        {
            preset = Serialization.AppSerializer.DeserializeFromJson<CustomizableModPreset>(presetConfigFilePath);
        }
        catch (InvalidOperationException)
        {
            preset = CreateDefaultTemplate(modURI);
        }

        return preset;
    }

#if LEGACY_CUSTOM_PRESET_CREATE_IMPL
    public static CustomModSupportPreset CreatePresetByDefault(string modificationURI)
    {
        // 1. Prepare preset's folder into modification's home directory.
        string presetHomeDirectoryPath = Path.Combine(modificationURI, MOD_PRESET_FOLDERNAME);

        if (!Directory.Exists(presetHomeDirectoryPath))
        {
            Directory.CreateDirectory(presetHomeDirectoryPath);
        }

        // 2. Generate 'mod_support.json' preset configuration file.
        var presetByDefault = new CustomModSupportPreset();
        string presetJsonText = JsonConvert.SerializeObject(presetByDefault, Formatting.Indented);
        string presetFilePath = Path.Combine(presetHomeDirectoryPath, MOD_PRESET_FILENAME);

        File.WriteAllText(presetFilePath, presetJsonText);

        return presetByDefault;
    }
#endif
}

public record RedistributableModPreset
{
    public RedistributableModPreset(ModSupportPreset preset, ModPresetMetaInfo metadata)
    {
        this.Data = preset;
        this.Metadata = metadata;
    }

    public ModSupportPreset Data { get; set; }

    public ModPresetMetaInfo Metadata { get; set; }

    public static RedistributableModPreset CreateDefaultTemplate()
    {
        ModSupportPreset preset = ModSupportPreset.CreateDefaultTemplate();
        ModPresetMetaInfo metadata = ModPresetMetaInfo.CreateDefaultTemplate();

        return new RedistributableModPreset(preset, metadata);
    }
}

public record ModPresetMetaInfo
{
    public ModPresetMetaInfo(
        Guid guid,
        string version,
        string presetName,
        string packageName,
        string creator)
    {
        this.Guid = guid;
        this.Version = version;
        this.PresetName = presetName;
        this.PackageName = packageName;
        this.Creator = creator;
    }

    public Guid Guid { get; set; }

    public string Version { get; set; }

    public string PresetName { get; set; }

    public string PackageName { get; set; }

    public string Creator { get; set; }

    public static ModPresetMetaInfo CreateDefaultTemplate()
    {
        return new ModPresetMetaInfo(
            guid: Guid.NewGuid(),
            version: "0.0.0",
            presetName: "M2TW Game Engine Modification",
            packageName: "M2TW Mod Support Presets",
            creator: "The OpenTWEMP Project");
    }
}

public record ModHeaderInfo
{
    public ModHeaderInfo(string title, string version)
    {
        this.ModTitle = title;
        this.ModVersion = version;
    }

    public string ModTitle { get; set; }

    public string ModVersion { get; set; }
}

public record ModContentInfo
{
    public ModContentInfo(string logoFilePath, string musicFilePath)
    {
        this.LogotypeImage = logoFilePath;
        this.BackgroundSoundTrack = musicFilePath;
    }

    public string LogotypeImage { get; set; }

    public string BackgroundSoundTrack { get; set; }
}

public record ModLauncherInfo
{
    public ModLauncherInfo(string batch, string setup, string eop)
    {
        this.LauncherProvider_NativeBatch = batch;
        this.LauncherProvider_NativeSetup = setup;
        this.LauncherProvider_M2TWEOP = eop;
    }

    public string LauncherProvider_NativeBatch { get; set; }

    public string LauncherProvider_NativeSetup { get; set; }

    public string LauncherProvider_M2TWEOP { get; set; }

    public static ModLauncherInfo CreateByDefault()
    {
        const string modLauncherNativeBatch = "My_Batch_Script.bat";
        const string modLauncherNativeSetup = "My_Setup_Program.exe";
        const string modLauncherM2TWEOP = "M2TWEOP GUI.exe";

        return new ModLauncherInfo(
            batch: modLauncherNativeBatch,
            setup: modLauncherNativeSetup,
            eop: modLauncherM2TWEOP);
    }
}

public record ModSocialMediaInfo
{
    public ModSocialMediaInfo(Dictionary<string, string> urlPairs)
    {
        this.ModURLs = urlPairs;
    }

    public Dictionary<string, string> ModURLs { get; set; }
}
