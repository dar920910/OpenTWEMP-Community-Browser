﻿// <copyright file="ModConfigSettingsForm.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#pragma warning disable SA1600 // ElementsMustBeDocumented
#pragma warning disable SA1601 // PartialElementsMustBeDocumented
#pragma warning disable SA1101 // PrefixLocalCallsWithThis

#define DISABLED_CFG_OPTIONS
#undef DISABLED_CFG_OPTIONS

#define SKIPPED_IMPLEMENTATION
#undef SKIPPED_IMPLEMENTATION

namespace TWEMP.Browser.App.Classic.CommonLibrary;

using TWEMP.Browser.Core.CommonLibrary;
using TWEMP.Browser.Core.CommonLibrary.CustomManagement.Gaming.Configuration;
using TWEMP.Browser.Core.CommonLibrary.CustomManagement.Gaming.Configuration.Profiles;
using TWEMP.Browser.Core.CommonLibrary.CustomManagement.Gaming.Views;
using TWEMP.Browser.Core.GamingSupport.TotalWarEngine.M2TW.Configuration;
using TWEMP.Browser.Core.GamingSupport.TotalWarEngine.M2TW.Configuration.Backend;
using TWEMP.Browser.Core.GamingSupport.TotalWarEngine.M2TW.Configuration.Backend.DataTypes.Enums;
using TWEMP.Browser.Core.GamingSupport.TotalWarEngine.M2TW.Configuration.Backend.DataTypes;
using TWEMP.Browser.Core.GamingSupport.TotalWarEngine.M2TW.Configuration.Frontend;
using TWEMP.Browser.Core.GamingSupport.TotalWarEngine.M2TW.Configuration.Frontend.SectionStateViews;

public partial class ModConfigSettingsForm : Form
{
    private static readonly (string Text, byte Obj)[] CfgVideoWaterBuffersPerNodeItems;
    private static readonly (string Text, byte Obj)[] CfgVideoTextureFilteringItems;
    private static readonly (string Text, byte Obj)[] CfgVideoSpriteBuffersPerNodeItems;
    private static readonly (string Text, byte Obj)[] CfgVideoModelBuffersPerNodeItems;
    private static readonly (string Text, byte Obj)[] CfgVideoGroundCoverBuffersPerNodeItems;
    private static readonly (string Text, byte Obj)[] CfgVideoGroundBuffersPerNodeItems;
    private static readonly (string Text, byte Obj)[] CfgVideoDepthShadowsResolutionItems;
    private static readonly (string Text, byte Obj)[] CfgVideoDepthShadowsItems;

    private static readonly (string Text, M2TW_UnitSize Obj)[] CfgGameUnitSizeItems;
    private static readonly (string Text, M2TW_Boolean Obj)[] CfgGameAiFactionsItems;
    private static readonly (string Text, M2TW_Boolean Obj)[] CfgGameUiFactionsItems;
    private static readonly (string Text, M2TW_BattleCameraStyle Obj)[] CfgControlsDefaultInBattleItems;
    private static readonly (string Text, M2TW_QualityLevel Obj)[] CfgControlsKeysetItems;
    private static readonly (string Text, M2TW_QualityLevel Obj)[] CfgVideoUnitDetailItems;
    private static readonly (string Text, M2TW_QualityLevel Obj)[] CfgVideoTerrainQualityItems;
    private static readonly (string Text, M2TW_QualityLevel Obj)[] CfgVideoShaderItems;
    private static readonly (string Text, M2TW_QualityLevel Obj)[] CfgVideoGrassDistanceItems;
    private static readonly (string Text, M2TW_QualityLevel Obj)[] CfgVideoEffectQualityItems;
    private static readonly (string Text, M2TW_QualityLevel Obj)[] CfgVideoBuildingDetailItems;
    private static readonly (string Text, M2TW_QualityLevel Obj)[] CfgVideoAntialiasingItems;
    private static readonly (string Text, M2TW_QualityLevel Obj)[] CfgVideoAntiAliasModeItems;
    private static readonly (string Text, M2TW_QualityLevel Obj)[] CfgVideoAnisotropicLevelItems;

    private static readonly (string Text, M2TW_DisplayResolution Obj)[] CfgVideoDisplayResolutionItems;

    private readonly GameModificationInfo currentGameModificationInfo;
    private readonly GameConfigProfile currentGameConfigProfile;
    private readonly GameConfigProfileCreateForm? currentCallingForm;

    private readonly M2TWGameConfigStateView gameConfigStateView;

    static ModConfigSettingsForm()
    {
        CfgVideoWaterBuffersPerNodeItems =
        [
            (Text: "0", Obj: 0),
            (Text: "1", Obj: 1),
            (Text: "2", Obj: 2),
            (Text: "3", Obj: 3),
            (Text: "4", Obj: 4),
        ];

        CfgVideoTextureFilteringItems =
        [
            (Text: "0", Obj: 0),
            (Text: "1", Obj: 1),
            (Text: "2", Obj: 2),
        ];

        CfgVideoSpriteBuffersPerNodeItems =
        [
            (Text: "0", Obj: 0),
            (Text: "1", Obj: 1),
            (Text: "2", Obj: 2),
            (Text: "3", Obj: 3),
            (Text: "4", Obj: 4),
        ];

        CfgVideoModelBuffersPerNodeItems =
        [
            (Text: "0", Obj: 0),
            (Text: "1", Obj: 1),
            (Text: "2", Obj: 2),
            (Text: "3", Obj: 3),
            (Text: "4", Obj: 4),
        ];

        CfgVideoGroundCoverBuffersPerNodeItems =
        [
            (Text: "0", Obj: 0),
            (Text: "1", Obj: 1),
            (Text: "2", Obj: 2),
            (Text: "3", Obj: 3),
            (Text: "4", Obj: 4),
        ];

        CfgVideoGroundBuffersPerNodeItems =
        [
            (Text: "0", Obj: 0),
            (Text: "1", Obj: 1),
            (Text: "2", Obj: 2),
            (Text: "3", Obj: 3),
            (Text: "4", Obj: 4),
        ];

        CfgVideoDepthShadowsResolutionItems =
        [
            (Text: "0", Obj: 0),
            (Text: "1", Obj: 1),
            (Text: "2", Obj: 2),
            (Text: "3", Obj: 3),
            (Text: "4", Obj: 4),
        ];

        CfgVideoDepthShadowsItems =
        [
            (Text: "0", Obj: 0),
            (Text: "1", Obj: 1),
            (Text: "2", Obj: 2),
            (Text: "3", Obj: 3),
            (Text: "4", Obj: 4),
        ];

        CfgGameUnitSizeItems =
        [
            (M2TW_UnitSize.Small, new M2TW_UnitSize(M2TW_Size.Small)),
            (M2TW_UnitSize.Normal, new M2TW_UnitSize(M2TW_Size.Normal)),
            (M2TW_UnitSize.Large, new M2TW_UnitSize(M2TW_Size.Large)),
            (M2TW_UnitSize.Huge, new M2TW_UnitSize(M2TW_Size.Huge)),
        ];

        CfgGameAiFactionsItems =
        [
            (M2TW_Boolean.M2TW_Deprecated_AI_False, new M2TW_Boolean(M2TW_Deprecated_AI_Boolean.Skip)),
            (M2TW_Boolean.M2TW_Deprecated_AI_True, new M2TW_Boolean(M2TW_Deprecated_AI_Boolean.Follow)),
        ];

        CfgGameUiFactionsItems =
        [
            (M2TW_Boolean.M2TW_Deprecated_UI_False, new M2TW_Boolean(M2TW_Deprecated_UI_Boolean.Hide)),
            (M2TW_Boolean.M2TW_Deprecated_UI_True, new M2TW_Boolean(M2TW_Deprecated_UI_Boolean.Show)),
        ];

        CfgControlsDefaultInBattleItems =
        [
            (M2TW_BattleCameraStyle.Default_Camera, new M2TW_BattleCameraStyle(M2TW_BattleCamera.Default)),
            (M2TW_BattleCameraStyle.Generals_Camera, new M2TW_BattleCameraStyle(M2TW_BattleCamera.Generals)),
            (M2TW_BattleCameraStyle.RTS_Camera, new M2TW_BattleCameraStyle(M2TW_BattleCamera.RTS)),
        ];

        CfgControlsKeysetItems =
        [
            (M2TW_QualityLevel.M2TW_KeySet_0, new M2TW_QualityLevel(M2TW_KeySet.KeySet_0)),
            (M2TW_QualityLevel.M2TW_KeySet_1, new M2TW_QualityLevel(M2TW_KeySet.KeySet_1)),
            (M2TW_QualityLevel.M2TW_KeySet_2, new M2TW_QualityLevel(M2TW_KeySet.KeySet_2)),
            (M2TW_QualityLevel.M2TW_KeySet_3, new M2TW_QualityLevel(M2TW_KeySet.KeySet_3)),
        ];

        CfgVideoUnitDetailItems =
        [
            (M2TW_QualityLevel.Low, new M2TW_QualityLevel(M2TW_Quality.Low)),
            (M2TW_QualityLevel.Medium, new M2TW_QualityLevel(M2TW_Quality.Medium)),
            (M2TW_QualityLevel.High, new M2TW_QualityLevel(M2TW_Quality.High)),
            (M2TW_QualityLevel.Highest, new M2TW_QualityLevel(M2TW_Quality.Highest)),
        ];

        CfgVideoTerrainQualityItems =
        [
            (M2TW_QualityLevel.Low, new M2TW_QualityLevel(M2TW_Quality.Low)),
            (M2TW_QualityLevel.Medium, new M2TW_QualityLevel(M2TW_Quality.Medium)),
            (M2TW_QualityLevel.High, new M2TW_QualityLevel(M2TW_Quality.High)),
            (M2TW_QualityLevel.Highest, new M2TW_QualityLevel(M2TW_Quality.Highest)),
        ];

        CfgVideoShaderItems =
        [
            (M2TW_QualityLevel.M2TW_ShaderVersion_v1, new M2TW_QualityLevel(M2TW_ShaderLevel.ShaderVersion_v1)),
            (M2TW_QualityLevel.M2TW_ShaderVersion_v2, new M2TW_QualityLevel(M2TW_ShaderLevel.ShaderVersion_v2)),
        ];

        CfgVideoGrassDistanceItems =
        [
            (M2TW_QualityLevel.M2TW_GrassDistanceLevel_0, new M2TW_QualityLevel(M2TW_GrassDistance.Level_0)),
            (M2TW_QualityLevel.M2TW_GrassDistanceLevel_1, new M2TW_QualityLevel(M2TW_GrassDistance.Level_1)),
            (M2TW_QualityLevel.M2TW_GrassDistanceLevel_2, new M2TW_QualityLevel(M2TW_GrassDistance.Level_2)),
            (M2TW_QualityLevel.M2TW_GrassDistanceLevel_3, new M2TW_QualityLevel(M2TW_GrassDistance.Level_3)),
        ];

        CfgVideoEffectQualityItems =
        [
            (M2TW_QualityLevel.Low, new M2TW_QualityLevel(M2TW_Quality.Low)),
            (M2TW_QualityLevel.Medium, new M2TW_QualityLevel(M2TW_Quality.Medium)),
            (M2TW_QualityLevel.High, new M2TW_QualityLevel(M2TW_Quality.High)),
            (M2TW_QualityLevel.Highest, new M2TW_QualityLevel(M2TW_Quality.Highest)),
        ];

        CfgVideoBuildingDetailItems =
        [
            (M2TW_QualityLevel.Low, new M2TW_QualityLevel(M2TW_Quality.Low)),
            (M2TW_QualityLevel.Medium, new M2TW_QualityLevel(M2TW_Quality.Medium)),
            (M2TW_QualityLevel.High, new M2TW_QualityLevel(M2TW_Quality.High)),
            (M2TW_QualityLevel.Highest, new M2TW_QualityLevel(M2TW_Quality.Highest)),
        ];

        CfgVideoAntialiasingItems =
        [
            (M2TW_QualityLevel.M2TW_AntiAliasing_None, new M2TW_QualityLevel(M2TW_AntiAliasing.AntiAliasMode_None)),
            (M2TW_QualityLevel.M2TW_AntiAliasing_x2, new M2TW_QualityLevel(M2TW_AntiAliasing.AntiAliasMode_x2)),
            (M2TW_QualityLevel.M2TW_AntiAliasing_x4, new M2TW_QualityLevel(M2TW_AntiAliasing.AntiAliasMode_x4)),
        ];

        CfgVideoAntiAliasModeItems =
        [
            (M2TW_QualityLevel.M2TW_AntiAliasing_OffMode, new M2TW_QualityLevel(M2TW_AntiAliasMode.AntiAliasMode_Off)),
            (M2TW_QualityLevel.M2TW_AntiAliasing_x2, new M2TW_QualityLevel(M2TW_AntiAliasMode.AntiAliasMode_x2)),
            (M2TW_QualityLevel.M2TW_AntiAliasing_x4, new M2TW_QualityLevel(M2TW_AntiAliasMode.AntiAliasMode_x4)),
        ];

        CfgVideoAnisotropicLevelItems =
        [
            (M2TW_QualityLevel.M2TW_AF_Bilinear, new M2TW_QualityLevel(M2TW_AnisotropicFilteringLevel.Bilinear)),
            (M2TW_QualityLevel.M2TW_AF_Trilinear, new M2TW_QualityLevel(M2TW_AnisotropicFilteringLevel.Trilinear)),
            (M2TW_QualityLevel.M2TW_AF_x2, new M2TW_QualityLevel(M2TW_AnisotropicFilteringLevel.AF_x2)),
            (M2TW_QualityLevel.M2TW_AF_x4, new M2TW_QualityLevel(M2TW_AnisotropicFilteringLevel.AF_x4)),
            (M2TW_QualityLevel.M2TW_AF_x8, new M2TW_QualityLevel(M2TW_AnisotropicFilteringLevel.AF_x8)),
            (M2TW_QualityLevel.M2TW_AF_x16, new M2TW_QualityLevel(M2TW_AnisotropicFilteringLevel.AF_x16)),
        ];

        CfgVideoDisplayResolutionItems =
        [
            (M2TW_DisplayResolution.W640_H480, new M2TW_DisplayResolution(M2TW_DisplayResolution.Display_640x480)),
            (M2TW_DisplayResolution.W800_H600, new M2TW_DisplayResolution(M2TW_DisplayResolution.Display_800x600)),
            (M2TW_DisplayResolution.W1024_H768, new M2TW_DisplayResolution(M2TW_DisplayResolution.Display_1024x768)),
            (M2TW_DisplayResolution.W1280_H720, new M2TW_DisplayResolution(M2TW_DisplayResolution.Display_1280x720)),
            (M2TW_DisplayResolution.W1280_H1024, new M2TW_DisplayResolution(M2TW_DisplayResolution.Display_1280x1024)),
            (M2TW_DisplayResolution.W1366_H768, new M2TW_DisplayResolution(M2TW_DisplayResolution.Display_1366x768)),
            (M2TW_DisplayResolution.W1600_H900, new M2TW_DisplayResolution(M2TW_DisplayResolution.Display_1600x900)),
            (M2TW_DisplayResolution.W1600_H1200, new M2TW_DisplayResolution(M2TW_DisplayResolution.Display_1600x1200)),
            (M2TW_DisplayResolution.W1920_H1080, new M2TW_DisplayResolution(M2TW_DisplayResolution.Display_1920x1080)),
            (M2TW_DisplayResolution.W2048_H1536, new M2TW_DisplayResolution(M2TW_DisplayResolution.Display_2048x1536)),
            (M2TW_DisplayResolution.W2560_H1440, new M2TW_DisplayResolution(M2TW_DisplayResolution.Display_2560x1440)),
            (M2TW_DisplayResolution.W3072_H1728, new M2TW_DisplayResolution(M2TW_DisplayResolution.Display_3072x1728)),
            (M2TW_DisplayResolution.W3200_H1800, new M2TW_DisplayResolution(M2TW_DisplayResolution.Display_3200x1800)),
            (M2TW_DisplayResolution.W3840_H2160, new M2TW_DisplayResolution(M2TW_DisplayResolution.Display_3840x2160)),
        ];
    }

    public ModConfigSettingsForm(GameModificationInfo gameModificationInfo)
    {
        this.currentGameModificationInfo = gameModificationInfo;
        this.currentGameConfigProfile = GameConfigProfile.CreateDefaultTemplate(this.currentGameModificationInfo);
        this.currentCallingForm = null;

        this.gameConfigStateView = M2TWGameConfigStateView.CreateByDefault(this.currentGameModificationInfo);

        InitializeComponent();
        InitializeConfigControls();
    }

    public ModConfigSettingsForm(
        GameModificationInfo gameModificationInfo,
        GameConfigProfile gameConfigProfile,
        GameConfigProfileCreateForm callingForm)
    {
        this.currentGameModificationInfo = gameModificationInfo;
        this.currentGameConfigProfile = gameConfigProfile;
        this.currentCallingForm = callingForm;

        this.gameConfigStateView = M2TWGameConfigStateView.CreateByDefault(this.currentGameModificationInfo);

        InitializeComponent();
        InitializeConfigControls();
    }

    private static void InitializeComboBoxControlByPredefinedItems<T>(ComboBox control, (string Text, T Obj)[] items)
    {
        control.Items.Clear();

        foreach ((string Text, T Obj) item in items)
        {
            control.Items.Add(item.Text);
        }
    }

    private static T GetPredefinedItemByIndex<T>((string Text, T Obj)[] items, int index)
    {
        T defaultItem = items[0].Obj;

        if (items.Length < index)
        {
            return defaultItem;
        }

        T targetItem = items[index].Obj;

        return (targetItem != null) ? targetItem : defaultItem;
    }

    private void SaveConfigSettingsButton_Click(object sender, EventArgs e)
    {
        M2TWGameConfigStateView gameConfigStateView = CreateGameConfigStateView();
        M2TWGameConfigurator gameConfigurator = new (this.currentGameModificationInfo, gameConfigStateView);
        BrowserKernel.CurrentConfigurator = gameConfigurator;

        GameCfgSection[] settings = gameConfigStateView.RetrieveCurrentSettings();
        this.currentGameConfigProfile.ConfigState.CurrentSettings = settings;

        MessageBox.Show(
            text: "Your new game config is READY!",
            caption: "SUCCESS",
            buttons: MessageBoxButtons.OK,
            icon: MessageBoxIcon.Information);

        this.Close();

        if (this.currentCallingForm != null)
        {
            this.currentCallingForm.ReturnToConfigProfilesForm();
        }
    }

    private void ResetConfigSettingsButton_Click(object sender, EventArgs e)
    {
        MessageBox.Show("RESET CONFIG SETTINGS");
    }

    private void ExitConfigSettingsButton_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void ExportConfigSettingsButton_Click(object sender, EventArgs e)
    {
        const string exportFileName = "config.json";

        M2TWGameConfigStateView gameConfigStateView = CreateGameConfigStateView();
        GameCfgSection[] cfgSettingSections = gameConfigStateView.RetrieveCurrentSettings();

        BrowserKernel.ExportConfigSettingsToFile(cfgSettingSections, exportFileName);

        MessageBox.Show(
            text: $"Config settings were exported to the file:\n{exportFileName}",
            caption: "<EXPORT CFG>",
            buttons: MessageBoxButtons.OK,
            icon: MessageBoxIcon.Information);
    }

    private void ImportConfigSettingsButton_Click(object sender, EventArgs e)
    {
        OpenFileDialog importFileDialog = new ();
        importFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
        DialogResult result = importFileDialog.ShowDialog();

        if (result == DialogResult.OK)
        {
            string importFileFullName = importFileDialog.FileName;

#if TESTING_IMPORT_FEATURE
            // TODO: Fix an exception when deserialization GameCfgSection objects!
            GameCfgSection[] gameCfgSections = BrowserKernel.ImportConfigSettingsFromFile(importFileFullName);
            this.InitializeConfigControls(gameCfgSections); // TODO: Implement this method in future.
#endif

            MessageBox.Show(
                text: $"Config settings were imported from the file:\n{importFileFullName}",
                caption: "<IMPORT CFG [TESTING]>",
                buttons: MessageBoxButtons.OK,
                icon: MessageBoxIcon.Information);
        }
    }

    private void InitializeItemsForComboBoxControls()
    {
        InitializeComboBoxControlByPredefinedItems(this.cfgGameUnitSizeComboBox, CfgGameUnitSizeItems);
        InitializeComboBoxControlByPredefinedItems(this.cfgGameAiFactionsComboBox, CfgGameAiFactionsItems);
        InitializeComboBoxControlByPredefinedItems(this.cfgControlsDefaultInBattleComboBox, CfgControlsDefaultInBattleItems);
        InitializeComboBoxControlByPredefinedItems(this.cfgVideoWaterBuffersPerNodeComboBox, CfgVideoWaterBuffersPerNodeItems);
        InitializeComboBoxControlByPredefinedItems(this.cfgVideoUnitDetailComboBox, CfgVideoUnitDetailItems);
        InitializeComboBoxControlByPredefinedItems(this.cfgVideoTextureFilteringComboBox, CfgVideoTextureFilteringItems);
        InitializeComboBoxControlByPredefinedItems(this.cfgVideoTerrainQualityComboBox, CfgVideoTerrainQualityItems);
        InitializeComboBoxControlByPredefinedItems(this.cfgVideoSpriteBuffersPerNodeComboBox, CfgVideoSpriteBuffersPerNodeItems);
        InitializeComboBoxControlByPredefinedItems(this.cfgVideoShaderComboBox, CfgVideoShaderItems);
        InitializeComboBoxControlByPredefinedItems(this.cfgVideoModelBuffersPerNodeComboBox, CfgVideoModelBuffersPerNodeItems);
        InitializeComboBoxControlByPredefinedItems(this.cfgVideoGroundCoverBuffersPerNodeComboBox, CfgVideoGroundCoverBuffersPerNodeItems);
        InitializeComboBoxControlByPredefinedItems(this.cfgVideoGroundBuffersPerNodeComboBox, CfgVideoGroundBuffersPerNodeItems);
        InitializeComboBoxControlByPredefinedItems(this.cfgVideoGrassDistanceComboBox, CfgVideoGrassDistanceItems);
        InitializeComboBoxControlByPredefinedItems(this.cfgVideoEffectQualityComboBox, CfgVideoEffectQualityItems);
        InitializeComboBoxControlByPredefinedItems(this.cfgVideoDepthShadowsResolutionComboBox, CfgVideoDepthShadowsResolutionItems);
        InitializeComboBoxControlByPredefinedItems(this.cfgVideoDepthShadowsComboBox, CfgVideoDepthShadowsItems);
        InitializeComboBoxControlByPredefinedItems(this.cfgVideoBattleResolutionComboBox, CfgVideoDisplayResolutionItems);
        InitializeComboBoxControlByPredefinedItems(this.cfgVideoCampaignResolutionComboBox, CfgVideoDisplayResolutionItems);
        InitializeComboBoxControlByPredefinedItems(this.cfgVideoBuildingDetailComboBox, CfgVideoBuildingDetailItems);
        InitializeComboBoxControlByPredefinedItems(this.cfgVideoAntialiasingComboBox, CfgVideoAntialiasingItems);
        InitializeComboBoxControlByPredefinedItems(this.cfgVideoAntiAliasModeComboBox, CfgVideoAntiAliasModeItems);
        InitializeComboBoxControlByPredefinedItems(this.cfgVideoAnisotropicLevelComboBox, CfgVideoAnisotropicLevelItems);
    }

    private void InitializeBoundValuesForNumericUpDownControls()
    {
        this.cfgGameChatMsgDurationNumericUpDown.Maximum = M2TW_Integer.ExtendedMaxValue;
        this.cfgGameChatMsgDurationNumericUpDown.Minimum = M2TW_Integer.MinValue;

        this.cfgGameCampaignMapSpeedUpNumericUpDown.Maximum = M2TW_Integer.MaxValue;
        this.cfgGameCampaignMapSpeedUpNumericUpDown.Minimum = M2TW_Integer.MinValue;

        this.cfgGameCampaignMapGameSpeedNumericUpDown.Maximum = M2TW_Integer.MaxValue;
        this.cfgGameCampaignMapGameSpeedNumericUpDown.Minimum = M2TW_Integer.MinValue;

        this.cfgAudioSpeechNumericUpDown.Maximum = M2TW_Integer.MaxValue;
        this.cfgAudioSpeechNumericUpDown.Minimum = M2TW_Integer.MinValue;

        this.cfgAudioSfxNumericUpDown.Maximum = M2TW_Integer.MaxValue;
        this.cfgAudioSfxNumericUpDown.Minimum = M2TW_Integer.MinValue;

        this.cfgAudioSoundCardProviderNumericUpDown.Maximum = M2TW_Integer.MaxValue;
        this.cfgAudioSoundCardProviderNumericUpDown.Minimum = M2TW_Integer.MinValue;

        this.cfgAudioMusicVolumeNumericUpDown.Maximum = M2TW_Integer.MaxValue;
        this.cfgAudioMusicVolumeNumericUpDown.Minimum = M2TW_Integer.MinValue;

        this.cfgAudioMasterVolumeNumericUpDown.Maximum = M2TW_Integer.MaxValue;
        this.cfgAudioMasterVolumeNumericUpDown.Minimum = M2TW_Integer.MinValue;

        this.cfgCameraRotateNumericUpDown.Maximum = M2TW_Integer.MaxValue;
        this.cfgCameraRotateNumericUpDown.Minimum = M2TW_Integer.MinValue;

        this.cfgCameraMoveNumericUpDown.Maximum = M2TW_Integer.MaxValue;
        this.cfgCameraMoveNumericUpDown.Minimum = M2TW_Integer.MinValue;

        this.cfgControlsScrollMinZoomNumericUpDown.Maximum = M2TW_Integer.MaxValue;
        this.cfgControlsScrollMinZoomNumericUpDown.Minimum = M2TW_Integer.MinValue;

        this.cfgControlsScrollMaxZoomNumericUpDown.Maximum = M2TW_Integer.MaxValue;
        this.cfgControlsScrollMaxZoomNumericUpDown.Minimum = M2TW_Integer.MinValue;

        this.cfgVideoGammaNumericUpDown.Maximum = M2TW_Integer.MaxValue;
        this.cfgVideoGammaNumericUpDown.Minimum = M2TW_Integer.MinValue;
    }

    private M2TWGameConfigStateView CreateGameConfigStateView()
    {
        M2TWGameConfigStateView gameConfigStateView = new (this.gameConfigStateView);

        // [GAME] ModGameplaySection
        gameConfigStateView.ModGameplaySection!.GameUseQuickchat = new M2TW_Boolean(this.cfgGameUseQuickchatCheckBox.Checked);
        gameConfigStateView.ModGameplaySection.GameUnlimitedMenOnBattlefield = new M2TW_Boolean(this.cfgGameUnlimitedMenOnBattlefieldCheckBox.Checked);
        gameConfigStateView.ModGameplaySection.GameNoCampaignBattleTimeLimit = new M2TW_Boolean(this.cfgGameNoCampaignBattleTimeLimitCheckBox.Checked);
        gameConfigStateView.ModGameplaySection.GameMuteAdvisor = new M2TW_Boolean(this.cfgGameMuteAdvisorCheckBox.Checked);
        gameConfigStateView.ModGameplaySection.GameMorale = new M2TW_Boolean(this.cfgGameMoraleCheckBox.Checked);
        gameConfigStateView.ModGameplaySection.GameMicromanageAllSettlements = new M2TW_Boolean(this.cfgGameMicromanageAllSettlementsCheckBox.Checked);
        gameConfigStateView.ModGameplaySection.GameLabelSettlements = new M2TW_Boolean(this.cfgGameLabelSettlementsCheckBox.Checked);
        gameConfigStateView.ModGameplaySection.GameLabelCharacters = new M2TW_Boolean(this.cfgGameLabelCharactersCheckBox.Checked);
        gameConfigStateView.ModGameplaySection.GameGamespySavePasswrd = new M2TW_Boolean(this.cfgGameGamespySavePasswrdCheckBox.Checked);
        gameConfigStateView.ModGameplaySection.GameFirstTimePlay = new M2TW_Boolean(this.cfgGameFirstTimePlayCheckBox.Checked);
        gameConfigStateView.ModGameplaySection.GameFatigue = new M2TW_Boolean(this.cfgGameFatigueCheckBox.Checked);
        gameConfigStateView.ModGameplaySection.GameEventCutscenes = new M2TW_Boolean(this.cfgGameEventCutscenesCheckBox.Checked);
        gameConfigStateView.ModGameplaySection.GameEnglish = new M2TW_Boolean(this.cfgGameEnglishCheckBox.Checked);
        gameConfigStateView.ModGameplaySection.GameDisableEvents = new M2TW_Boolean(this.cfgGameDisableEventsCheckBox.Checked);
        gameConfigStateView.ModGameplaySection.GameDisableArrowMarkers = new M2TW_Boolean(this.cfgGameDisableArrowMarkersCheckBox.Checked);
        gameConfigStateView.ModGameplaySection.GameBlindAdvisor = new M2TW_Boolean(this.cfgGameBlindAdvisorCheckBox.Checked);
        gameConfigStateView.ModGameplaySection.GameAutoSave = new M2TW_Boolean(this.cfgGameAutoSaveCheckBox.Checked);
        gameConfigStateView.ModGameplaySection.GameAllUsers = new M2TW_Boolean(this.cfgGameAllUsersCheckBox.Checked);
        gameConfigStateView.ModGameplaySection.GameAdvisorVerbosity = new M2TW_Boolean(this.cfgGameAdvisorVerbosityCheckBox.Checked);
        gameConfigStateView.ModGameplaySection.GameAdvancedStatsAlways = new M2TW_Boolean(this.cfgGameAdvancedStatsAlwaysCheckBox.Checked);
        gameConfigStateView.ModGameplaySection.GameCampaignMapSpeedUp = new M2TW_Integer(Convert.ToByte(this.cfgGameCampaignMapSpeedUpNumericUpDown.Text));
        gameConfigStateView.ModGameplaySection.GameCampaignMapGameSpeed = new M2TW_Integer(Convert.ToByte(this.cfgGameCampaignMapGameSpeedNumericUpDown.Text));
        gameConfigStateView.ModGameplaySection.GamePrefFactionsPlayed = Convert.ToInt32(this.cfgGamePrefFactionsPlayedTextBox.Text);
        gameConfigStateView.ModGameplaySection.GameTutorialPath = this.cfgGameTutorialPathTextBox.Text;
        gameConfigStateView.ModGameplaySection.GameCampaignNumTimePlay = new M2TW_Integer(Convert.ToByte(this.cfgGameCampaignNumTimePlayTextBox.Text));
        gameConfigStateView.ModGameplaySection.GameChatMsgDuration = new M2TW_Integer(Convert.ToUInt16(this.cfgGameChatMsgDurationNumericUpDown.Value));

        gameConfigStateView.ModGameplaySection.GameUnitSize = GetPredefinedItemByIndex(
            items: CfgGameUnitSizeItems, index: this.cfgGameUnitSizeComboBox.SelectedIndex);

        gameConfigStateView.ModGameplaySection.GameAiFactions = GetPredefinedItemByIndex(
            items: CfgGameAiFactionsItems, index: this.cfgGameAiFactionsComboBox.SelectedIndex);

        // [AUDIO] GameAudioCfgSectionStateView
        gameConfigStateView.GameAudioCfgSection!.SpeechVolume = new M2TW_Integer(Convert.ToByte(this.cfgAudioSpeechNumericUpDown.Text));
        gameConfigStateView.GameAudioCfgSection.SoundEffectsVolume = new M2TW_Integer(Convert.ToByte(this.cfgAudioSfxNumericUpDown.Text));
        gameConfigStateView.GameAudioCfgSection.SpeechVolume = new M2TW_Integer(Convert.ToByte(this.cfgAudioSoundCardProviderNumericUpDown.Text));
        gameConfigStateView.GameAudioCfgSection.AudioMusicVolume = new M2TW_Integer(Convert.ToByte(this.cfgAudioMusicVolumeNumericUpDown.Text));
        gameConfigStateView.GameAudioCfgSection.AudioMasterVolume = new M2TW_Integer(Convert.ToByte(this.cfgAudioMasterVolumeNumericUpDown.Text));
        gameConfigStateView.GameAudioCfgSection.SpeechEnable = new M2TW_Boolean(this.cfgAudioSpeechEnableCheckBox.Checked);
        gameConfigStateView.GameAudioCfgSection.AudioEnable = new M2TW_Boolean(this.cfgAudioEnableCheckBox.Checked);
        gameConfigStateView.GameAudioCfgSection.SubFactionAccents = new M2TW_Boolean(this.cfgAudioSubFactionAccentsEnableCheckBox.Checked);

        // [CAMERA] GameCameraCfgSectionStateView
        gameConfigStateView.GameCameraCfgSection!.CameraDefaultInBattle = GetPredefinedItemByIndex(
            items: CfgControlsDefaultInBattleItems, index: this.cfgControlsDefaultInBattleComboBox.SelectedIndex);

        gameConfigStateView.GameCameraCfgSection.CameraRotate = new M2TW_Integer(Convert.ToByte(this.cfgCameraRotateNumericUpDown.Text));
        gameConfigStateView.GameCameraCfgSection.CameraMove = new M2TW_Integer(Convert.ToByte(this.cfgCameraMoveNumericUpDown.Text));
        gameConfigStateView.GameCameraCfgSection.CameraRestrict = new M2TW_Boolean(this.cfgCameraRestrictCheckBox.Checked);

        // [CONTROLS] GameControlsCfgSectionStateView
        gameConfigStateView.GameControlsCfgSection!.KeySet = GetPredefinedItemByIndex(
            items: CfgControlsKeysetItems, index: this.cfgControlsKeysetComboBox.SelectedIndex);

        gameConfigStateView.GameControlsCfgSection.CampaignScrollMinZoom = Convert.ToByte(this.cfgControlsScrollMinZoomNumericUpDown.Text);
        gameConfigStateView.GameControlsCfgSection.CampaignScrollMaxZoom = Convert.ToByte(this.cfgControlsScrollMaxZoomNumericUpDown.Text);

        // [HOTSEAT] ModHotseatSectionStateView
        gameConfigStateView.HotseatSection!.HotseatGameName = this.cfgHotseatGameNameTextBox.Text;
        gameConfigStateView.HotseatSection.HotseatAdminPassword = new M2TW_Boolean(this.cfgHotseatAdminPasswordCheckBox.Checked);
        gameConfigStateView.HotseatSection.HotseatPasswords = new M2TW_Boolean(this.cfgHotseatPasswordsCheckBox.Checked);
        gameConfigStateView.HotseatSection.HotseatValidateDiplomacy = new M2TW_Boolean(this.cfgHotseatValidateDiplomacyCheckBox.Checked);
        gameConfigStateView.HotseatSection.HotseatAllowValidationFailures = new M2TW_Boolean(this.cfgHotseatAllowValidationFailuresCheckBox.Checked);
        gameConfigStateView.HotseatSection.HotseatValidateData = new M2TW_Boolean(this.cfgHotseatValidateDataCheckBox.Checked);
        gameConfigStateView.HotseatSection.HotseatCloseAfterSave = new M2TW_Boolean(this.cfgHotseatCloseAfterSaveCheckBox.Checked);
        gameConfigStateView.HotseatSection.HotseatSaveConfig = new M2TW_Boolean(this.cfgHotseatSaveConfigCheckBox.Checked);
        gameConfigStateView.HotseatSection.HotseatAutoSave = new M2TW_Boolean(this.cfgHotseatAutosaveCheckBox.Checked);
        gameConfigStateView.HotseatSection.HotseatUpdateAiCamera = new M2TW_Boolean(this.cfgHotseatUpdateAiCameraCheckBox.Checked);
        gameConfigStateView.HotseatSection.HotseatSavePrefs = new M2TW_Boolean(this.cfgHotseatSavePrefsCheckBox.Checked);
        gameConfigStateView.HotseatSection.HotseatDisablePapalElections = new M2TW_Boolean(this.cfgHotseatDisablePapalElectionsCheckBox.Checked);
        gameConfigStateView.HotseatSection.HotseatDisableConsole = new M2TW_Boolean(this.cfgHotseatDisableConsoleCheckBox.Checked);
        gameConfigStateView.HotseatSection.HotseatTurns = new M2TW_Boolean(this.cfgHotseatTurnsCheckBox.Checked);
        gameConfigStateView.HotseatSection.HotseatScroll = new M2TW_Boolean(this.cfgHotseatScrollCheckBox.Checked);
        gameConfigStateView.HotseatSection.HotseatAutoresolveBattles = new M2TW_Boolean(this.cfgHotseatAutoresolveBattlesCheckBox.Checked);

        // [NETWORK]
        gameConfigStateView.HotseatSection.NetworkUsePort = Convert.ToUInt16(this.cfgNetworkUsePortTextBox.Text);
        gameConfigStateView.HotseatSection.NetworkUseIp = new M2TW_IpAddress(127, 0, 0, 1); // REPLACE: this.cfgNetworkUseIpTextBox.Text;

        // [MISC]
#if DISABLED_CFG_OPTIONS
        gameConfigStateView.HotseatSection.BypassToStrategySave = this.cfgMiscBypassToStrategySaveTextBox.Text;
#endif
        gameConfigStateView.ModGameplaySection.UnlockCampaign = new M2TW_Boolean(this.cfgMiscUnlockCampaignCheckBox.Checked);

        // [IO] ModSettingsSectionStateView
        gameConfigStateView.ModCoreSettingsSection!.FileFirst = new M2TW_Boolean(this.cfgIOFileFirstCheckBox.Checked);
        gameConfigStateView.ModCoreSettingsSection.Editor = new M2TW_Boolean(this.cfgFeaturesEditorCheckBox.Checked);

        // [LOG]
        gameConfigStateView.ModDiagnosticSection!.LogTo = this.cfgLogLocationTextBox.Text;

        // [UI] GameUICfgSectionStateView
        gameConfigStateView.GameUICfgSection!.UiUnitCards = new M2TW_Boolean(this.cfgUiUnitCardsCheckBox.Checked);
        gameConfigStateView.GameUICfgSection.UiShowTooltips = new M2TW_Boolean(this.cfgUiShowTooltipsCheckBox.Checked);
        gameConfigStateView.GameUICfgSection.UiRadar = new M2TW_Boolean(this.cfgUiRadarCheckBox.Checked);
        gameConfigStateView.GameUICfgSection.UiFullBattleHud = new M2TW_Boolean(this.cfgUiFullBattleHudCheckBox.Checked);
        gameConfigStateView.GameUICfgSection.UiButtons = new M2TW_Boolean(this.cfgUiButtonsCheckBox.Checked);
        gameConfigStateView.GameUICfgSection.UiSaCards = new M2TW_Boolean(this.cfgUiSaCardsCheckBox.Checked);

        // [VIDEO] GameVideoCfgSectionStateView
        gameConfigStateView.GameVideoCfgSection!.VideoGamma = new M2TW_Integer(Convert.ToByte(this.cfgVideoGammaNumericUpDown.Text));

        gameConfigStateView.GameVideoCfgSection.VideoWaterBuffersPerNode = new M2TW_Integer(GetPredefinedItemByIndex(
            items: CfgVideoWaterBuffersPerNodeItems, index: this.cfgVideoWaterBuffersPerNodeComboBox.SelectedIndex));

        gameConfigStateView.GameVideoCfgSection.VideoTextureFiltering = new M2TW_Integer(GetPredefinedItemByIndex(
            items: CfgVideoTextureFilteringItems, index: this.cfgVideoTextureFilteringComboBox.SelectedIndex));

        gameConfigStateView.GameVideoCfgSection.VideoSpriteBuffersPerNode = new M2TW_Integer(GetPredefinedItemByIndex(
            items: CfgVideoSpriteBuffersPerNodeItems, index: this.cfgVideoSpriteBuffersPerNodeComboBox.SelectedIndex));

        gameConfigStateView.GameVideoCfgSection.VideoModelBuffersPerNode = new M2TW_Integer(GetPredefinedItemByIndex(
            items: CfgVideoModelBuffersPerNodeItems, index: this.cfgVideoModelBuffersPerNodeComboBox.SelectedIndex));

        gameConfigStateView.GameVideoCfgSection.VideoGroundCoverBuffersPerNode = new M2TW_Integer(GetPredefinedItemByIndex(
            items: CfgVideoGroundCoverBuffersPerNodeItems, index: this.cfgVideoGroundCoverBuffersPerNodeComboBox.SelectedIndex));

        gameConfigStateView.GameVideoCfgSection.VideoGroundBuffersPerNode = new M2TW_Integer(GetPredefinedItemByIndex(
            items: CfgVideoGroundBuffersPerNodeItems, index: this.cfgVideoGroundBuffersPerNodeComboBox.SelectedIndex));

        gameConfigStateView.GameVideoCfgSection.VideoDepthShadowsResolution = new M2TW_Integer(GetPredefinedItemByIndex(
            items: CfgVideoDepthShadowsResolutionItems, index: this.cfgVideoDepthShadowsResolutionComboBox.SelectedIndex));

        gameConfigStateView.GameVideoCfgSection.VideoDepthShadows = new M2TW_Integer(GetPredefinedItemByIndex(
            items: CfgVideoDepthShadowsItems, index: this.cfgVideoDepthShadowsComboBox.SelectedIndex));

        gameConfigStateView.GameVideoCfgSection.VideoWindowedMode = new M2TW_Boolean(this.cfgVideoWindowedCheckBox.Checked);
        gameConfigStateView.GameVideoCfgSection.VideoWidescreenMode = new M2TW_Boolean(this.cfgVideoWidescreenCheckBox.Checked);
        gameConfigStateView.GameVideoCfgSection.VideoVsync = new M2TW_Boolean(this.cfgVideoVsyncCheckBox.Checked);
        gameConfigStateView.GameVideoCfgSection.VideoVegetation = new M2TW_Boolean(this.cfgVideoVegetationCheckBox.Checked);
        gameConfigStateView.GameVideoCfgSection.VideoSubtitles = new M2TW_Boolean(this.cfgVideoSubtitlesCheckBox.Checked);
        gameConfigStateView.GameVideoCfgSection.VideoStencilShadows = new M2TW_Boolean(this.cfgVideoStencilShadowsCheckBox.Checked);
        gameConfigStateView.GameVideoCfgSection.VideoSplashes = new M2TW_Boolean(this.cfgVideoSplashesCheckBox.Checked);
        gameConfigStateView.GameVideoCfgSection.VideoSkipMipLevels = new M2TW_Boolean(this.cfgVideoSkipMipLevelsChecBox.Checked);
        gameConfigStateView.GameVideoCfgSection.VideoShowPackageLitter = new M2TW_Boolean(this.cfgVideoShowPackageLitterCheckBox.Checked);
        gameConfigStateView.GameVideoCfgSection.VideoShowBanners = new M2TW_Boolean(this.cfgVideoShowBannersCheckBox.Checked);
        gameConfigStateView.GameVideoCfgSection.VideoSabotageMovies = new M2TW_Boolean(this.cfgVideoSabotageMoviesCheckBox.Checked);
        gameConfigStateView.GameVideoCfgSection.VideoReflection = new M2TW_Boolean(this.cfgVideoReflectionCheckBox.Checked);
        gameConfigStateView.GameVideoCfgSection.VideoNoBackgroundFmv = new M2TW_Boolean(this.cfgVideoNoBackgroundFmvCheckBox.Checked);
        gameConfigStateView.GameVideoCfgSection.VideoMovies = new M2TW_Boolean(this.cfgVideoMoviesCheckBox.Checked);
        gameConfigStateView.GameVideoCfgSection.VideoInfiltrationMovies = new M2TW_Boolean(this.cfgVideoInfiltrationMoviesCheckBox.Checked);
        gameConfigStateView.GameVideoCfgSection.VideoEventMovies = new M2TW_Boolean(this.cfgVideoEventMoviesCheckBox.Checked);
        gameConfigStateView.GameVideoCfgSection.VideoBloom = new M2TW_Boolean(this.cfgVideoBloomCheckBox.Checked);
        gameConfigStateView.GameVideoCfgSection.VideoAutodetect = new M2TW_Boolean(this.cfgVideoAutodetectCheckBox.Checked);
        gameConfigStateView.GameVideoCfgSection.VideoAssassinationMovies = new M2TW_Boolean(this.cfgVideoAssassinationMoviesCheckBox.Checked);

        gameConfigStateView.GameVideoCfgSection.VideoCampaignResolution = GetPredefinedItemByIndex(
            items: CfgVideoDisplayResolutionItems, index: this.cfgVideoCampaignResolutionComboBox.SelectedIndex);

        gameConfigStateView.GameVideoCfgSection.VideoBattleResolution = GetPredefinedItemByIndex(
            items: CfgVideoDisplayResolutionItems, index: this.cfgVideoBattleResolutionComboBox.SelectedIndex);

        gameConfigStateView.GameVideoCfgSection.VideoUnitDetail = GetPredefinedItemByIndex(
            items: CfgVideoUnitDetailItems, index: this.cfgVideoUnitDetailComboBox.SelectedIndex);

        gameConfigStateView.GameVideoCfgSection.VideoTerrainQuality = GetPredefinedItemByIndex(
            items: CfgVideoTerrainQualityItems, index: this.cfgVideoTerrainQualityComboBox.SelectedIndex);

        gameConfigStateView.GameVideoCfgSection.VideoShader = GetPredefinedItemByIndex(
            items: CfgVideoShaderItems, index: this.cfgVideoShaderComboBox.SelectedIndex);

        gameConfigStateView.GameVideoCfgSection.VideoGrassDistance = GetPredefinedItemByIndex(
            items: CfgVideoGrassDistanceItems, index: this.cfgVideoGrassDistanceComboBox.SelectedIndex);

        gameConfigStateView.GameVideoCfgSection.VideoEffectQuality = GetPredefinedItemByIndex(
            items: CfgVideoEffectQualityItems, index: this.cfgVideoEffectQualityComboBox.SelectedIndex);

        gameConfigStateView.GameVideoCfgSection.VideoBuildingDetail = GetPredefinedItemByIndex(
            items: CfgVideoBuildingDetailItems, index: this.cfgVideoBuildingDetailComboBox.SelectedIndex);

        gameConfigStateView.GameVideoCfgSection.VideoAntialiasing = GetPredefinedItemByIndex(
            items: CfgVideoAntialiasingItems, index: this.cfgVideoAntialiasingComboBox.SelectedIndex);

        gameConfigStateView.GameVideoCfgSection.VideoAntiAliasMode = GetPredefinedItemByIndex(
            items: CfgVideoAntiAliasModeItems, index: this.cfgVideoAntiAliasModeComboBox.SelectedIndex);

        gameConfigStateView.GameVideoCfgSection.VideoAnisotropicLevel = GetPredefinedItemByIndex(
            items: CfgVideoAnisotropicLevelItems, index: this.cfgVideoAnisotropicLevelComboBox.SelectedIndex);

        return gameConfigStateView;
    }

    private void InitializeConfigControls()
    {
        this.InitializeItemsForComboBoxControls();
        this.InitializeBoundValuesForNumericUpDownControls();

        this.Text = $"M2TW Config Settings: \"{this.currentGameConfigProfile.Name}\" [ {this.currentGameModificationInfo.Location} ]";

        // [GAME] ModGameplaySection
        this.cfgGameUseQuickchatCheckBox.Checked = this.gameConfigStateView.ModGameplaySection!.GameUseQuickchat!.GetValue();
        this.cfgGameUnlimitedMenOnBattlefieldCheckBox.Checked = this.gameConfigStateView.ModGameplaySection.GameUnlimitedMenOnBattlefield!.GetValue();
        this.cfgGameNoCampaignBattleTimeLimitCheckBox.Checked = this.gameConfigStateView.ModGameplaySection.GameNoCampaignBattleTimeLimit!.GetValue();
        this.cfgGameMuteAdvisorCheckBox.Checked = this.gameConfigStateView.ModGameplaySection.GameMuteAdvisor!.GetValue();
        this.cfgGameMoraleCheckBox.Checked = this.gameConfigStateView.ModGameplaySection.GameMorale!.GetValue();
        this.cfgGameMicromanageAllSettlementsCheckBox.Checked = this.gameConfigStateView.ModGameplaySection.GameMicromanageAllSettlements!.GetValue();
        this.cfgGameLabelSettlementsCheckBox.Checked = this.gameConfigStateView.ModGameplaySection.GameLabelSettlements!.GetValue();
        this.cfgGameLabelCharactersCheckBox.Checked = this.gameConfigStateView.ModGameplaySection.GameLabelCharacters!.GetValue();
        this.cfgGameGamespySavePasswrdCheckBox.Checked = this.gameConfigStateView.ModGameplaySection.GameGamespySavePasswrd!.GetValue();
        this.cfgGameFirstTimePlayCheckBox.Checked = this.gameConfigStateView.ModGameplaySection.GameFirstTimePlay!.GetValue();
        this.cfgGameFatigueCheckBox.Checked = this.gameConfigStateView.ModGameplaySection.GameFatigue!.GetValue();
        this.cfgGameEventCutscenesCheckBox.Checked = this.gameConfigStateView.ModGameplaySection.GameEventCutscenes!.GetValue();
        this.cfgGameEnglishCheckBox.Checked = this.gameConfigStateView.ModGameplaySection.GameEnglish!.GetValue();
        this.cfgGameDisableEventsCheckBox.Checked = this.gameConfigStateView.ModGameplaySection.GameDisableEvents!.GetValue();
        this.cfgGameDisableArrowMarkersCheckBox.Checked = this.gameConfigStateView.ModGameplaySection.GameDisableArrowMarkers!.GetValue();
        this.cfgGameBlindAdvisorCheckBox.Checked = this.gameConfigStateView.ModGameplaySection.GameBlindAdvisor!.GetValue();
        this.cfgGameAutoSaveCheckBox.Checked = this.gameConfigStateView.ModGameplaySection.GameAutoSave!.GetValue();
        this.cfgGameAllUsersCheckBox.Checked = this.gameConfigStateView.ModGameplaySection.GameAllUsers!.GetValue();
        this.cfgGameAdvisorVerbosityCheckBox.Checked = this.gameConfigStateView.ModGameplaySection.GameAdvisorVerbosity!.GetValue();
        this.cfgGameAdvancedStatsAlwaysCheckBox.Checked = this.gameConfigStateView.ModGameplaySection.GameAdvancedStatsAlways!.GetValue();
        this.cfgGameUnitSizeComboBox.Text = this.gameConfigStateView.ModGameplaySection.GameUnitSize!.Value;
        this.cfgGameChatMsgDurationNumericUpDown.Text = this.gameConfigStateView.ModGameplaySection.GameChatMsgDuration!.Value;
        this.cfgGameCampaignMapSpeedUpNumericUpDown.Text = this.gameConfigStateView.ModGameplaySection.GameCampaignMapSpeedUp!.Value;
        this.cfgGameCampaignMapGameSpeedNumericUpDown.Text = this.gameConfigStateView.ModGameplaySection.GameCampaignMapGameSpeed!.Value;
        this.cfgGamePrefFactionsPlayedTextBox.Text = this.gameConfigStateView.ModGameplaySection.GamePrefFactionsPlayed.ToString();
        this.cfgGameTutorialPathTextBox.Text = this.gameConfigStateView.ModGameplaySection.GameTutorialPath;
        this.cfgGameAiFactionsComboBox.Text = this.gameConfigStateView.ModGameplaySection.GameAiFactions!.BooleanValue;
        this.cfgGameCampaignNumTimePlayTextBox.Text = this.gameConfigStateView.ModGameplaySection.GameCampaignNumTimePlay!.Value;

        // [AUDIO] GameAudioCfgSectionStateView
        this.cfgAudioSpeechNumericUpDown.Text = this.gameConfigStateView.GameAudioCfgSection!.SpeechVolume!.Value;
        this.cfgAudioSfxNumericUpDown.Text = this.gameConfigStateView.GameAudioCfgSection.SoundEffectsVolume!.Value;
        this.cfgAudioSoundCardProviderNumericUpDown.Text = this.gameConfigStateView.GameAudioCfgSection.SpeechVolume.Value;
        this.cfgAudioMusicVolumeNumericUpDown.Text = this.gameConfigStateView.GameAudioCfgSection.AudioMusicVolume!.Value;
        this.cfgAudioMasterVolumeNumericUpDown.Text = this.gameConfigStateView.GameAudioCfgSection.AudioMasterVolume!.Value;
        this.cfgAudioSpeechEnableCheckBox.Checked = this.gameConfigStateView.GameAudioCfgSection.SpeechEnable!.GetValue();
        this.cfgAudioEnableCheckBox.Checked = this.gameConfigStateView.GameAudioCfgSection.AudioEnable!.GetValue();
        this.cfgAudioSubFactionAccentsEnableCheckBox.Checked = this.gameConfigStateView.GameAudioCfgSection.SubFactionAccents!.GetValue();

        // [CAMERA] GameCameraCfgSectionStateView
        this.cfgControlsDefaultInBattleComboBox.Text = this.gameConfigStateView.GameCameraCfgSection!.CameraDefaultInBattle!.Value;
        this.cfgCameraRotateNumericUpDown.Text = this.gameConfigStateView.GameCameraCfgSection.CameraRotate!.Value;
        this.cfgCameraMoveNumericUpDown.Text = this.gameConfigStateView.GameCameraCfgSection.CameraMove!.Value;
        this.cfgCameraRestrictCheckBox.Checked = this.gameConfigStateView.GameCameraCfgSection.CameraRestrict!.GetValue();

        // [CONTROLS] GameControlsCfgSectionStateView
        this.cfgControlsKeysetComboBox.Text = this.gameConfigStateView.GameControlsCfgSection!.KeySet!.Value;
        this.cfgControlsScrollMinZoomNumericUpDown.Text = this.gameConfigStateView.GameControlsCfgSection.CampaignScrollMinZoom.ToString();
        this.cfgControlsScrollMaxZoomNumericUpDown.Text = this.gameConfigStateView.GameControlsCfgSection.CampaignScrollMaxZoom.ToString();

        // [HOTSEAT] ModHotseatSectionStateView
        this.cfgHotseatGameNameTextBox.Text = this.gameConfigStateView.HotseatSection!.HotseatGameName;
        this.cfgHotseatAdminPasswordCheckBox.Checked = this.gameConfigStateView.HotseatSection.HotseatAdminPassword!.GetValue();
        this.cfgHotseatPasswordsCheckBox.Checked = this.gameConfigStateView.HotseatSection.HotseatPasswords!.GetValue();
        this.cfgHotseatValidateDiplomacyCheckBox.Checked = this.gameConfigStateView.HotseatSection.HotseatValidateDiplomacy!.GetValue();
        this.cfgHotseatAllowValidationFailuresCheckBox.Checked = this.gameConfigStateView.HotseatSection.HotseatAllowValidationFailures!.GetValue();
        this.cfgHotseatValidateDataCheckBox.Checked = this.gameConfigStateView.HotseatSection.HotseatValidateData!.GetValue();
        this.cfgHotseatCloseAfterSaveCheckBox.Checked = this.gameConfigStateView.HotseatSection.HotseatCloseAfterSave!.GetValue();
        this.cfgHotseatSaveConfigCheckBox.Checked = this.gameConfigStateView.HotseatSection.HotseatSaveConfig!.GetValue();
        this.cfgHotseatAutosaveCheckBox.Checked = this.gameConfigStateView.HotseatSection.HotseatAutoSave!.GetValue();
        this.cfgHotseatUpdateAiCameraCheckBox.Checked = this.gameConfigStateView.HotseatSection.HotseatUpdateAiCamera!.GetValue();
        this.cfgHotseatSavePrefsCheckBox.Checked = this.gameConfigStateView.HotseatSection.HotseatSavePrefs!.GetValue();
        this.cfgHotseatDisablePapalElectionsCheckBox.Checked = this.gameConfigStateView.HotseatSection.HotseatDisablePapalElections!.GetValue();
        this.cfgHotseatDisableConsoleCheckBox.Checked = this.gameConfigStateView.HotseatSection.HotseatDisableConsole!.GetValue();
        this.cfgHotseatTurnsCheckBox.Checked = this.gameConfigStateView.HotseatSection.HotseatTurns!.GetValue();
        this.cfgHotseatScrollCheckBox.Checked = this.gameConfigStateView.HotseatSection.HotseatScroll!.GetValue();
        this.cfgHotseatAutoresolveBattlesCheckBox.Checked = this.gameConfigStateView.HotseatSection.HotseatAutoresolveBattles!.GetValue();

        // [NETWORK]
        this.cfgNetworkUsePortTextBox.Text = this.gameConfigStateView.HotseatSection.NetworkUsePort.ToString();
        this.cfgNetworkUseIpTextBox.Text = this.gameConfigStateView.HotseatSection.NetworkUseIp!.Value;

        // [MISC]
#if DISABLED_CFG_OPTIONS
        this.cfgMiscBypassToStrategySaveTextBox.Text = this.gameConfigStateView.HotseatSection.BypassToStrategySave;
#endif
        this.cfgMiscUnlockCampaignCheckBox.Checked = this.gameConfigStateView.ModGameplaySection.UnlockCampaign!.GetValue();

        // [IO] ModSettingsSectionStateView
        this.cfgIOFileFirstCheckBox.Checked = this.gameConfigStateView.ModCoreSettingsSection!.FileFirst!.GetValue();
        this.cfgFeaturesEditorCheckBox.Checked = this.gameConfigStateView.ModCoreSettingsSection.Editor!.GetValue();

        // [LOG]
        this.cfgLogLocationTextBox.Text = this.gameConfigStateView.ModDiagnosticSection!.LogTo;

        // [UI] GameUICfgSectionStateView
        this.cfgUiUnitCardsCheckBox.Checked = this.gameConfigStateView.GameUICfgSection!.UiUnitCards!.GetValue();
        this.cfgUiShowTooltipsCheckBox.Checked = this.gameConfigStateView.GameUICfgSection.UiShowTooltips!.GetValue();
        this.cfgUiRadarCheckBox.Checked = this.gameConfigStateView.GameUICfgSection.UiRadar!.GetValue();
        this.cfgUiFullBattleHudCheckBox.Checked = this.gameConfigStateView.GameUICfgSection.UiFullBattleHud!.GetValue();
        this.cfgUiButtonsCheckBox.Checked = this.gameConfigStateView.GameUICfgSection.UiButtons!.GetValue();
        this.cfgUiSaCardsCheckBox.Checked = this.gameConfigStateView.GameUICfgSection.UiSaCards!.GetValue();

        // [VIDEO] GameVideoCfgSectionStateView
        this.cfgVideoGammaNumericUpDown.Text = this.gameConfigStateView.GameVideoCfgSection!.VideoGamma!.Value;
        this.cfgVideoWaterBuffersPerNodeComboBox.Text = this.gameConfigStateView.GameVideoCfgSection.VideoWaterBuffersPerNode!.Value;
        this.cfgVideoUnitDetailComboBox.Text = this.gameConfigStateView.GameVideoCfgSection.VideoUnitDetail!.Value;
        this.cfgVideoTextureFilteringComboBox.Text = this.gameConfigStateView.GameVideoCfgSection.VideoTextureFiltering!.Value;
        this.cfgVideoTerrainQualityComboBox.Text = this.gameConfigStateView.GameVideoCfgSection.VideoTerrainQuality!.Value;
        this.cfgVideoSpriteBuffersPerNodeComboBox.Text = this.gameConfigStateView.GameVideoCfgSection.VideoSpriteBuffersPerNode!.Value;
        this.cfgVideoShaderComboBox.Text = this.gameConfigStateView.GameVideoCfgSection.VideoShader!.Value;
        this.cfgVideoModelBuffersPerNodeComboBox.Text = this.gameConfigStateView.GameVideoCfgSection.VideoModelBuffersPerNode!.Value;
        this.cfgVideoGroundCoverBuffersPerNodeComboBox.Text = this.gameConfigStateView.GameVideoCfgSection.VideoGroundCoverBuffersPerNode!.Value;
        this.cfgVideoGroundBuffersPerNodeComboBox.Text = this.gameConfigStateView.GameVideoCfgSection.VideoGroundBuffersPerNode!.Value;
        this.cfgVideoGrassDistanceComboBox.Text = this.gameConfigStateView.GameVideoCfgSection.VideoGrassDistance!.Value;
        this.cfgVideoEffectQualityComboBox.Text = this.gameConfigStateView.GameVideoCfgSection.VideoEffectQuality!.Value;
        this.cfgVideoDepthShadowsResolutionComboBox.Text = this.gameConfigStateView.GameVideoCfgSection.VideoDepthShadowsResolution!.Value;
        this.cfgVideoDepthShadowsComboBox.Text = this.gameConfigStateView.GameVideoCfgSection.VideoDepthShadows!.Value;
        this.cfgVideoCampaignResolutionComboBox.Text = this.gameConfigStateView.GameVideoCfgSection.VideoCampaignResolution!.Value;
        this.cfgVideoBuildingDetailComboBox.Text = this.gameConfigStateView.GameVideoCfgSection.VideoBuildingDetail!.Value;
        this.cfgVideoBattleResolutionComboBox.Text = this.gameConfigStateView.GameVideoCfgSection.VideoBattleResolution!.Value;
        this.cfgVideoAntialiasingComboBox.Text = this.gameConfigStateView.GameVideoCfgSection.VideoAntialiasing!.Value;
        this.cfgVideoAntiAliasModeComboBox.Text = this.gameConfigStateView.GameVideoCfgSection.VideoAntiAliasMode!.Value;
        this.cfgVideoAnisotropicLevelComboBox.Text = this.gameConfigStateView.GameVideoCfgSection.VideoAnisotropicLevel!.Value;
        this.cfgVideoWindowedCheckBox.Checked = this.gameConfigStateView.GameVideoCfgSection.VideoWindowedMode!.GetValue();
        this.cfgVideoWidescreenCheckBox.Checked = this.gameConfigStateView.GameVideoCfgSection.VideoWidescreenMode!.GetValue();
        this.cfgVideoVsyncCheckBox.Checked = this.gameConfigStateView.GameVideoCfgSection.VideoVsync!.GetValue();
        this.cfgVideoVegetationCheckBox.Checked = this.gameConfigStateView.GameVideoCfgSection.VideoVegetation!.GetValue();
        this.cfgVideoSubtitlesCheckBox.Checked = this.gameConfigStateView.GameVideoCfgSection.VideoSubtitles!.GetValue();
        this.cfgVideoStencilShadowsCheckBox.Checked = this.gameConfigStateView.GameVideoCfgSection.VideoStencilShadows!.GetValue();
        this.cfgVideoSplashesCheckBox.Checked = this.gameConfigStateView.GameVideoCfgSection.VideoSplashes!.GetValue();
        this.cfgVideoSkipMipLevelsChecBox.Checked = this.gameConfigStateView.GameVideoCfgSection.VideoSkipMipLevels!.GetValue();
        this.cfgVideoShowPackageLitterCheckBox.Checked = this.gameConfigStateView.GameVideoCfgSection.VideoShowPackageLitter!.GetValue();
        this.cfgVideoShowBannersCheckBox.Checked = this.gameConfigStateView.GameVideoCfgSection.VideoShowBanners!.GetValue();
        this.cfgVideoSabotageMoviesCheckBox.Checked = this.gameConfigStateView.GameVideoCfgSection.VideoSabotageMovies!.GetValue();
        this.cfgVideoReflectionCheckBox.Checked = this.gameConfigStateView.GameVideoCfgSection.VideoReflection!.GetValue();
        this.cfgVideoNoBackgroundFmvCheckBox.Checked = this.gameConfigStateView.GameVideoCfgSection.VideoNoBackgroundFmv!.GetValue();
        this.cfgVideoMoviesCheckBox.Checked = this.gameConfigStateView.GameVideoCfgSection.VideoMovies!.GetValue();
        this.cfgVideoInfiltrationMoviesCheckBox.Checked = this.gameConfigStateView.GameVideoCfgSection.VideoInfiltrationMovies!.GetValue();
        this.cfgVideoEventMoviesCheckBox.Checked = this.gameConfigStateView.GameVideoCfgSection.VideoEventMovies!.GetValue();
        this.cfgVideoBloomCheckBox.Checked = this.gameConfigStateView.GameVideoCfgSection.VideoBloom!.GetValue();
        this.cfgVideoAutodetectCheckBox.Checked = this.gameConfigStateView.GameVideoCfgSection.VideoAutodetect!.GetValue();
        this.cfgVideoAssassinationMoviesCheckBox.Checked = this.gameConfigStateView.GameVideoCfgSection.VideoAssassinationMovies!.GetValue();
    }

#if TESTING
    private void TestConfigSettings()
    {
        // [GAME]

        this.cfgGameUseQuickchatCheckBox/* CheckBox(); */ // this.gameConfigStateView.ModGameplaySection.GameUseQuickchat = new M2TW_Boolean(false),
        this.cfgGameUnlimitedMenOnBattlefieldCheckBox/* CheckBox(); */ // this.gameConfigStateView.ModGameplaySection.GameUnlimitedMenOnBattlefield = new M2TW_Boolean(true),
        this.cfgGameNoCampaignBattleTimeLimitCheckBox/* CheckBox(); */ // this.gameConfigStateView.ModGameplaySection.GameNoCampaignBattleTimeLimit = new M2TW_Boolean(true),
        this.cfgGameMuteAdvisorCheckBox/* CheckBox(); */ // this.gameConfigStateView.ModGameplaySection.GameMuteAdvisor = new M2TW_Boolean(false),
        this.cfgGameMoraleCheckBox/* CheckBox(); */ // this.gameConfigStateView.ModGameplaySection.GameMorale = new M2TW_Boolean(true),
        this.cfgGameMicromanageAllSettlementsCheckBox/* CheckBox(); */ // this.gameConfigStateView.ModGameplaySection.GameMicromanageAllSettlements = new M2TW_Boolean(true),
        this.cfgGameLabelSettlementsCheckBox/* CheckBox(); */ // this.gameConfigStateView.ModGameplaySection.GameLabelSettlements = new M2TW_Boolean(true),
        this.cfgGameLabelCharactersCheckBox/* CheckBox(); */ // this.gameConfigStateView.ModGameplaySection.GameLabelCharacters = new M2TW_Boolean(false),
        this.cfgGameGamespySavePasswrdCheckBox/* CheckBox(); */ // this.gameConfigStateView.ModGameplaySection.GameGamespySavePasswrd = new M2TW_Boolean(true),
        this.cfgGameFirstTimePlayCheckBox/* CheckBox(); */ // this.gameConfigStateView.ModGameplaySection.GameFirstTimePlay = new M2TW_Boolean(false),
        this.cfgGameFatigueCheckBox/* CheckBox(); */ // this.gameConfigStateView.ModGameplaySection.GameFatigue = new M2TW_Boolean(true),
        this.cfgGameEventCutscenesCheckBox/* CheckBox(); */ // this.gameConfigStateView.ModGameplaySection.GameEventCutscenes = new M2TW_Boolean(true),
        this.cfgGameEnglishCheckBox/* CheckBox(); */ // this.gameConfigStateView.ModGameplaySection.GameEnglish = new M2TW_Boolean(false),
        this.cfgGameDisableEventsCheckBox/* CheckBox(); */ // this.gameConfigStateView.ModGameplaySection.GameDisableEvents = new M2TW_Boolean(false),
        this.cfgGameDisableArrowMarkersCheckBox/* CheckBox(); */ // this.gameConfigStateView.ModGameplaySection.GameDisableArrowMarkers = new M2TW_Boolean(false),
        this.cfgGameBlindAdvisorCheckBox/* CheckBox(); */ // this.gameConfigStateView.ModGameplaySection.GameBlindAdvisor = new M2TW_Boolean(false),
        this.cfgGameAutoSaveCheckBox/* CheckBox(); */ // this.gameConfigStateView.ModGameplaySection.GameAutoSave = new M2TW_Boolean(true),
        this.cfgGameAllUsersCheckBox/* CheckBox(); */ // this.gameConfigStateView.ModGameplaySection.GameAllUsers = new M2TW_Boolean(true),
        this.cfgGameAdvisorVerbosityCheckBox/* CheckBox(); */ // this.gameConfigStateView.ModGameplaySection.GameAdvisorVerbosity = new M2TW_Boolean(false),
        this.cfgGameAdvancedStatsAlwaysCheckBox/* CheckBox(); */ // this.gameConfigStateView.ModGameplaySection.GameAdvancedStatsAlways = new M2TW_Boolean(false),
        this.cfgGameUnitSizeComboBox/* ComboBox(); */ // this.gameConfigStateView.ModGameplaySection.GameUnitSize = new M2TW_UnitSize(M2TW_Size.Huge),
        this.cfgGameChatMsgDurationNumericUpDown/* NumericUpDown(); */ // this.gameConfigStateView.ModGameplaySection.GameChatMsgDuration = new M2TW_Integer(M2TW_Integer.ExtendedMaxValue),
        this.cfgGameCampaignMapSpeedUpNumericUpDown/* NumericUpDown(); */ // this.gameConfigStateView.ModGameplaySection.GameCampaignMapSpeedUp = new M2TW_Integer(1),
        this.cfgGameCampaignMapGameSpeedNumericUpDown/* NumericUpDown(); */ // this.gameConfigStateView.ModGameplaySection.GameCampaignMapGameSpeed = new M2TW_Integer(10),
        this.cfgGamePrefFactionsPlayedTextBox/* TextBox(); */ // this.gameConfigStateView.ModGameplaySection.GamePrefFactionsPlayed = 4177855,
        this.cfgGameTutorialPathTextBox/* TextBox(); */ // this.gameConfigStateView.ModGameplaySection.GameTutorialPath = "norman_prologue/battle_of_hastings",
        this.cfgGameAiFactionsComboBox/* ComboBox(); */ // this.gameConfigStateView.ModGameplaySection.GameAiFactions = new M2TW_Boolean(M2TW_Deprecated_AI_Boolean.Follow),
        this.cfgGameCampaignNumTimePlayTextBox/* TextBox(); */ // this.gameConfigStateView.ModGameplaySection.GameCampaignNumTimePlay = new M2TW_Integer(252),

        // [AUDIO] GameAudioCfgSectionStateView

        this.cfgAudioSpeechNumericUpDown/* NumericUpDown(); */ // this.gameConfigStateView.GameAudioCfgSection.SpeechVolume = new M2TW_Integer(85),
        this.cfgAudioSfxNumericUpDown/* NumericUpDown(); */ // this.gameConfigStateView.GameAudioCfgSection.SoundEffectsVolume = new M2TW_Integer(80),
        this.cfgAudioSoundCardProviderNumericUpDown/* NumericUpDown(); */ // this.gameConfigStateView.GameAudioCfgSection.SpeechVolume = new M2TW_Integer(85),
        this.cfgAudioMusicVolumeNumericUpDown/* NumericUpDown(); */ // this.gameConfigStateView.GameAudioCfgSection.AudioMusicVolume = new M2TW_Integer(70),
        this.cfgAudioMasterVolumeNumericUpDown/* NumericUpDown(); */ // this.gameConfigStateView.GameAudioCfgSection.AudioMasterVolume = new M2TW_Integer(85),
        this.cfgAudioSpeechEnableCheckBox/* CheckBox(); */ // this.gameConfigStateView.GameAudioCfgSection.SpeechEnable = new M2TW_Boolean(true),
        this.cfgAudioEnableCheckBox/* CheckBox(); */ // this.gameConfigStateView.GameAudioCfgSection.AudioEnable = new M2TW_Boolean(true),
        this.cfgAudioSubFactionAccentsEnableCheckBox/* CheckBox(); */ // this.gameConfigStateView.GameAudioCfgSection.SubFactionAccents = new M2TW_Boolean(true),

        // [CAMERA] GameCameraCfgSectionStateView

        this.cfgControlsDefaultInBattleComboBox/* ComboBox(); */ // this.gameConfigStateView.GameCameraCfgSection.CameraDefaultInBattle = new M2TW_BattleCameraStyle(M2TW_BattleCamera.RTS),
        this.cfgCameraRotateNumericUpDown/* NumericUpDown(); */ // this.gameConfigStateView.GameCameraCfgSection.CameraRotate = new M2TW_Integer(30),
        this.cfgCameraMoveNumericUpDown/* NumericUpDown(); */ // this.gameConfigStateView.GameCameraCfgSection.CameraMove = new M2TW_Integer(70),
        this.cfgCameraRestrictCheckBox/* CheckBox(); */ // this.gameConfigStateView.GameCameraCfgSection.CameraRestrict = new M2TW_Boolean(false),

        // [CONTROLS] GameControlsCfgSectionStateView

        this.cfgControlsKeysetNumericUpDown/* NumericUpDown(); */ // this.gameConfigStateView.GameControlsCfgSection.KeySet = new M2TW_QualityLevel(M2TW_KeySet.KeySet_0),
        this.cfgControlsScrollMinZoomNumericUpDown/* NumericUpDown(); */ // this.gameConfigStateView.GameControlsCfgSection.CampaignScrollMinZoom = Convert.ToByte(30),
        this.cfgControlsScrollMaxZoomNumericUpDown/* NumericUpDown(); */ // this.gameConfigStateView.GameControlsCfgSection.CampaignScrollMaxZoom = Convert.ToByte(30),

        // [HOTSEAT] ModHotseatSectionStateView

        this.cfgHotseatGameNameTextBox/* TextBox(); */ // this.gameConfigStateView.HotseatSection.HotseatGameName = "hotseat_gamename.sav",
        this.cfgHotseatAdminPasswordTextBox/* TextBox(); */ // this.gameConfigStateView.HotseatSection.HotseatAdminPassword = string.Empty,
        this.cfgHotseatPasswordsTextBox/* TextBox(); */ // this.gameConfigStateView.HotseatSection.HotseatPasswords = new M2TW_Boolean(false),
        this.cfgHotseatValidateDiplomacyCheckBox/* CheckBox(); */ // this.gameConfigStateView.HotseatSection.HotseatValidateDiplomacy = new M2TW_Boolean(false),
        this.cfgHotseatAllowValidationFailuresCheckBox/* CheckBox(); */ // this.gameConfigStateView.HotseatSection.HotseatAllowValidationFailures = new M2TW_Boolean(false),
        this.cfgHotseatValidateDataCheckBox/* CheckBox(); */ // this.gameConfigStateView.HotseatSection.HotseatValidateData = new M2TW_Boolean(false),
        this.cfgHotseatCloseAfterSaveCheckBox/* CheckBox(); */ // this.gameConfigStateView.HotseatSection.HotseatCloseAfterSave = new M2TW_Boolean(false),
        this.cfgHotseatSaveConfigCheckBox/* CheckBox(); */ // this.gameConfigStateView.HotseatSection.HotseatSaveConfig = new M2TW_Boolean(true),
        this.cfgHotseatAutosaveCheckBox/* CheckBox(); */ // this.gameConfigStateView.HotseatSection.HotseatAutoSave = new M2TW_Boolean(true),
        this.cfgHotseatUpdateAiCameraCheckBox/* CheckBox(); */ // this.gameConfigStateView.HotseatSection.HotseatUpdateAiCamera = new M2TW_Boolean(true),
        this.cfgHotseatSavePrefsCheckBox/* CheckBox(); */ // this.gameConfigStateView.HotseatSection.HotseatSavePrefs = new M2TW_Boolean(true),
        this.cfgHotseatDisablePapalElectionsCheckBox/* CheckBox(); */ // this.gameConfigStateView.HotseatSection.HotseatDisablePapalElections = new M2TW_Boolean(true),
        this.cfgHotseatDisableConsoleCheckBox/* CheckBox(); */ // this.gameConfigStateView.HotseatSection.HotseatDisableConsole = new M2TW_Boolean(false),
        this.cfgHotseatTurnsCheckBox/* CheckBox(); */ // this.gameConfigStateView.HotseatSection.HotseatTurns = new M2TW_Boolean(false),
        this.cfgHotseatScrollCheckBox/* CheckBox(); */ // this.gameConfigStateView.HotseatSection.HotseatScroll = new M2TW_Boolean(false),
        this.cfgHotseatAutoresolveBattlesCheckBox/* CheckBox(); */ // this.gameConfigStateView.HotseatSection.HotseatAutoresolveBattles = new M2TW_Boolean(false),

        // [NETWORK]

        this.cfgNetworkUsePortTextBox/* TextBox(); */ // this.gameConfigStateView.HotseatSection.NetworkUsePort = Convert.ToUInt16(M2TW_IpAddress.DefaultPort),
        this.cfgNetworkUseIpTextBox/* TextBox(); */ // this.gameConfigStateView.HotseatSection.NetworkUseIp = new M2TW_IpAddress(127, 0, 0, 1),

        // [MISC]

        this.cfgMiscBypassToStrategySaveTextBox/* TextBox(); */ // this.gameConfigStateView.BypassToStrategySave = "game_name.sav",
        this.cfgMiscUnlockCampaignCheckBox/* CheckBox(); */ // this.gameConfigStateView.UnlockCampaign = new M2TW_Boolean(false),

        // [IO] ModSettingsSectionStateView

        this.cfgIOFileFirstCheckBox/* CheckBox(); */ // this.gameConfigStateView.ModCoreSettingsSection.FileFirst = new M2TW_Boolean(true),
        this.cfgFeaturesEditorCheckBox/* CheckBox(); */ // this.gameConfigStateView.ModCoreSettingsSection.Editor = new M2TW_Boolean(true),

        // [UI] GameUICfgSectionStateView

        this.cfgUiUnitCardsCheckBox/* CheckBox(); */ // this.gameConfigStateView.GameUICfgSection.UiUnitCards = new M2TW_Boolean(M2TW_Deprecated_UI_Boolean.Show),
        this.cfgUiShowTooltipsCheckBox/* CheckBox(); */ // this.gameConfigStateView.GameUICfgSection.UiShowTooltips = new M2TW_Boolean(true),
        this.cfgUiRadarCheckBox/* CheckBox(); */ // this.gameConfigStateView.GameUICfgSection.UiRadar = new M2TW_Boolean(M2TW_Deprecated_UI_Boolean.Show),
        this.cfgUiFullBattleHudCheckBox/* CheckBox(); */ // this.gameConfigStateView.GameUICfgSection.UiFullBattleHud = new M2TW_Boolean(true),
        this.cfgUiButtonsCheckBox/* CheckBox(); */ // this.gameConfigStateView.GameUICfgSection.UiButtons = new M2TW_Boolean(M2TW_Deprecated_UI_Boolean.Show),
        this.cfgUiSaCardsCheckBox/* CheckBox(); */ // this.gameConfigStateView.GameUICfgSection.UiSaCards = new M2TW_Boolean(M2TW_Deprecated_UI_Boolean.Show),

        // [VIDEO] GameVideoCfgSectionStateView

        this.cfgVideoGammaNumericUpDown/* NumericUpDown(); */ // this.gameConfigStateView.GameVideoCfgSection.VideoGamma = new M2TW_Integer(120),
        this.cfgVideoWaterBuffersPerNodeComboBox/* ComboBox(); */ // this.gameConfigStateView.GameVideoCfgSection.VideoWaterBuffersPerNode = new M2TW_Integer(4),
        this.cfgVideoUnitDetailComboBox/* ComboBox(); */ // this.gameConfigStateView.GameVideoCfgSection.VideoUnitDetail = new M2TW_QualityLevel(M2TW_Quality.Highest),
        this.cfgVideoTextureFilteringComboBox/* ComboBox(); */ // this.gameConfigStateView.GameVideoCfgSection.VideoTextureFiltering = new M2TW_Integer(2),
        this.cfgVideoTerrainQualityComboBox/* ComboBox(); */ // this.gameConfigStateView.GameVideoCfgSection.VideoTerrainQuality = new M2TW_QualityLevel(M2TW_Quality.High),
        this.cfgVideoSpriteBuffersPerNodeComboBox/* ComboBox(); */ // this.gameConfigStateView.GameVideoCfgSection.VideoSpriteBuffersPerNode = new M2TW_Integer(4),
        this.cfgVideoShaderComboBox/* ComboBox(); */ // this.gameConfigStateView.GameVideoCfgSection.VideoShader = new M2TW_QualityLevel(M2TW_ShaderLevel.ShaderVersion_v2),
        this.cfgVideoModelBuffersPerNodeComboBox/* ComboBox(); */ // this.gameConfigStateView.GameVideoCfgSection.VideoModelBuffersPerNode = new M2TW_Integer(4),
        this.cfgVideoGroundCoverBuffersPerNodeComboBox/* ComboBox(); */ // this.gameConfigStateView.GameVideoCfgSection.VideoGroundCoverBuffersPerNode = new M2TW_Integer(4),
        this.cfgVideoGroundBuffersPerNodeComboBox/* ComboBox(); */ // this.gameConfigStateView.GameVideoCfgSection.VideoGroundBuffersPerNode = new M2TW_Integer(4),
        this.cfgVideoGrassDistanceComboBox/* ComboBox(); */ // this.gameConfigStateView.GameVideoCfgSection.VideoGrassDistance = new M2TW_QualityLevel(M2TW_GrassDistance.Level_1),
        this.cfgVideoEffectQualityComboBox/* ComboBox(); */ // this.gameConfigStateView.GameVideoCfgSection.VideoEffectQuality = new M2TW_QualityLevel(M2TW_Quality.Highest),
        this.cfgVideoDepthShadowsResolutionComboBox/* ComboBox(); */ // this.gameConfigStateView.GameVideoCfgSection.VideoDepthShadowsResolution = new M2TW_Integer(3),
        this.cfgVideoDepthShadowsComboBox/* ComboBox(); */ // this.gameConfigStateView.GameVideoCfgSection.VideoDepthShadows = new M2TW_Integer(2),
        this.cfgVideoCampaignResolutionComboBox/* ComboBox(); */ // this.gameConfigStateView.GameVideoCfgSection.VideoCampaignResolution = new M2TW_DisplayResolution(M2TW_DisplayResolution.Display_1024x768),
        this.cfgVideoBuildingDetailComboBox/* ComboBox(); */ // this.gameConfigStateView.GameVideoCfgSection.VideoBuildingDetail = new M2TW_QualityLevel(M2TW_Quality.High),
        this.cfgVideoBattleResolutionComboBox/* ComboBox(); */ // this.gameConfigStateView.GameVideoCfgSection.VideoBattleResolution = new M2TW_DisplayResolution(M2TW_DisplayResolution.Display_1024x768),
        this.cfgVideoAntialiasingComboBox/* ComboBox(); */ // this.gameConfigStateView.GameVideoCfgSection.VideoAntialiasing = new M2TW_QualityLevel(M2TW_AntiAliasing.AntiAliasMode_x4),
        this.cfgVideoAntiAliasModeComboBox/* ComboBox(); */ // this.gameConfigStateView.GameVideoCfgSection.VideoAntiAliasMode = new M2TW_QualityLevel(M2TW_AntiAliasMode.AntiAliasMode_x4),
        this.cfgVideoAnisotropicLevelComboBox/* ComboBox(); */ // this.gameConfigStateView.GameVideoCfgSection.VideoAnisotropicLevel = new M2TW_QualityLevel(M2TW_AnisotropicFilteringLevel.AF_x16),
        this.cfgVideoWindowedCheckBox/* CheckBox(); */ // this.gameConfigStateView.GameVideoCfgSection.VideoWindowedMode = new M2TW_Boolean(false),
        this.cfgVideoWidescreenCheckBox/* CheckBox(); */ // this.gameConfigStateView.GameVideoCfgSection.VideoWidescreenMode = new M2TW_Boolean(true),
        this.cfgVideoVsyncCheckBox/* CheckBox(); */ // this.gameConfigStateView.GameVideoCfgSection.VideoVsync = new M2TW_Boolean(false),
        this.cfgVideoVegetationCheckBox/* CheckBox(); */ // this.gameConfigStateView.GameVideoCfgSection.VideoVegetation = new M2TW_Boolean(false),
        this.cfgVideoSubtitlesCheckBox/* CheckBox(); */ // this.gameConfigStateView.GameVideoCfgSection.VideoSubtitles = new M2TW_Boolean(false),
        this.cfgVideoStencilShadowsCheckBox/* CheckBox(); */ // this.gameConfigStateView.GameVideoCfgSection.VideoStencilShadows = new M2TW_Boolean(true),
        this.cfgVideoSplashesCheckBox/* CheckBox(); */ // this.gameConfigStateView.GameVideoCfgSection.VideoSplashes = new M2TW_Boolean(true),
        this.cfgVideoSkipMipLevelsChecBox/* CheckBox(); */ // this.gameConfigStateView.GameVideoCfgSection.VideoSkipMipLevels = new M2TW_Boolean(false),
        this.cfgVideoShowPackageLitterCheckBox/* CheckBox(); */ // this.gameConfigStateView.GameVideoCfgSection.VideoShowPackageLitter = new M2TW_Boolean(true),
        this.cfgVideoShowBannersCheckBox/* CheckBox(); */ // this.gameConfigStateView.GameVideoCfgSection.VideoShowBanners = new M2TW_Boolean(false),
        this.cfgVideoSabotageMoviesCheckBox/* CheckBox(); */ // this.gameConfigStateView.GameVideoCfgSection.VideoSabotageMovies = new M2TW_Boolean(false),
        this.cfgVideoReflectionCheckBox/* CheckBox(); */ // this.gameConfigStateView.GameVideoCfgSection.VideoReflection = new M2TW_Boolean(true),
        this.cfgVideoNoBackgroundFmvCheckBox/* CheckBox(); */ // this.gameConfigStateView.GameVideoCfgSection.VideoNoBackgroundFmv = new M2TW_Boolean(true),
        this.cfgVideoMoviesCheckBox/* CheckBox(); */ // this.gameConfigStateView.GameVideoCfgSection.VideoMovies = new M2TW_Boolean(true),
        this.cfgVideoInfiltrationMoviesCheckBox/* CheckBox(); */ // this.gameConfigStateView.GameVideoCfgSection.VideoInfiltrationMovies = new M2TW_Boolean(false),
        this.cfgVideoEventMoviesCheckBox/* CheckBox(); */ // this.gameConfigStateView.GameVideoCfgSection.VideoEventMovies = new M2TW_Boolean(true),
        this.cfgVideoBloomCheckBox/* CheckBox(); */ // this.gameConfigStateView.GameVideoCfgSection.VideoBloom = new M2TW_Boolean(true),
        this.cfgVideoAutodetectCheckBox/* CheckBox(); */ // this.gameConfigStateView.GameVideoCfgSection.VideoAutodetect = new M2TW_Boolean(false),
        this.cfgVideoAssassinationMoviesCheckBox/* CheckBox(); */ // this.gameConfigStateView.GameVideoCfgSection.VideoAssassinationMovies = new M2TW_Boolean(false),

        // [LOG]

        this.checkBoxLogHistory/* CheckBox(); */ // this.gameConfigStateView.ModDiagnosticSection // OVERRIDE!!!
        this.radioButtonLogErrorAndTrace/* RadioButton(); */ // this.gameConfigStateView.ModDiagnosticSection.LogLevel = new M2TW_LoggingLevel(M2TW_LoggingMode.Error), // OVERRIDE!!!
        this.radioButtonLogOnlyTrace/* RadioButton(); */ // this.gameConfigStateView.ModDiagnosticSection.LogLevel = new M2TW_LoggingLevel(M2TW_LoggingMode.Error), // OVERRIDE!!!
        this.radioButtonLogOnlyError/* RadioButton(); */ // this.gameConfigStateView.ModDiagnosticSection.LogLevel = new M2TW_LoggingLevel(M2TW_LoggingMode.Error), // OVERRIDE!!!
        this.cfgLogLocationTextBox/* TextBox(); */ // this.gameConfigStateView.ModDiagnosticSection.LogTo = mod.LogFileRelativePath,
    }
#endif

#if SKIPPED_IMPLEMENTATION


    private void InitializeAdditionalUIControls() // TODO: Implement this method later...
    {
        // [GAME]
        // GameTutorialBattlePlayed = new M2TW_Boolean(false),

        // [AUDIO]
        // SoundCardProvider = "Miles Fast 2D Positional Audio",

        // [HOTSEAT]
        // MultiplayerPlayable = new M2TW_Boolean(true), ???

        // [VIDEO]
        // VideoVegetationQuality = new M2TW_QualityLevel(M2TW_Quality.High),
        // VideoBorderlessWindow = new M2TW_Boolean(false),

        // ADDITIONAL CONFIG SETTINGS

        // [LOG]
        this.checkBoxLogHistory/* CheckBox(); */ // this.gameConfigStateView.ModDiagnosticSection
        this.radioButtonLogErrorAndTrace/* RadioButton(); */ // this.gameConfigStateView.ModDiagnosticSection.LogLevel = new M2TW_LoggingLevel(M2TW_LoggingMode.Error)
        this.radioButtonLogOnlyTrace/* RadioButton(); */ // this.gameConfigStateView.ModDiagnosticSection.LogLevel = new M2TW_LoggingLevel(M2TW_LoggingMode.Error)
        this.radioButtonLogOnlyError/* RadioButton(); */ // this.gameConfigStateView.ModDiagnosticSection.LogLevel = new M2TW_LoggingLevel(M2TW_LoggingMode.Error)
        this.cfgLogLocationTextBox/* TextBox(); */ // this.gameConfigStateView.ModDiagnosticSection.LogTo = mod.LogFileRelativePath,
    }
#endif
}
