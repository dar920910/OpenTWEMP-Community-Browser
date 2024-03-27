﻿// <copyright file="GameConfigState.cs" company="The OpenTWEMP Project">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TWEMP.Browser.Core.CommonLibrary.CustomManagement.Gaming.Configuration;

using TWEMP.Browser.Core.CommonLibrary.CustomManagement.Gaming.GameSupportPresets;

/// <summary>
/// Represents the current state of a game configuration.
/// </summary>
public class GameConfigState
{
    private readonly GameSupportProvider gameSupportProvider;
    private readonly GameModificationInfo gameModificationInfo;

    /// <summary>
    /// Initializes a new instance of the <see cref="GameConfigState"/> class.
    /// </summary>
    /// <param name="provider">A target game support provider type.</param>
    /// <param name="info">Information about a target game modification.</param>
    private GameConfigState(GameSupportProvider provider, GameModificationInfo info)
    {
        this.gameSupportProvider = provider;
        this.gameModificationInfo = info;

        this.CurrentSettings = InitializeSettingsByDefault(
            provider: this.gameSupportProvider, info: this.gameModificationInfo);
    }

    /// <summary>
    /// Gets or sets current configuration settings.
    /// </summary>
    public CfgOptionsSubSet[] CurrentSettings { get; set; }

    /// <summary>
    /// Creates a game configuration state with default settings.
    /// </summary>
    /// <param name="provider">A target game support provider type.</param>
    /// <param name="info">Information about a target game modification.</param>
    /// <returns>A new instance of the <see cref="GameConfigState"/> class.</returns>
    public static GameConfigState CreateByDefault(GameSupportProvider provider, GameModificationInfo info)
    {
        switch (provider.GameEngine)
        {
            case GameEngineSupportType.TWEMP:
            case GameEngineSupportType.M2TW:
            case GameEngineSupportType.RTW:
            default:
                return new GameConfigState(provider, info);
        }
    }

    private static CfgOptionsSubSet[] InitializeSettingsByDefault(
        GameSupportProvider provider, GameModificationInfo info)
    {
        return new CfgOptionsSubSet[] { }; // TODO: Implement using existing M2TW config settings!
    }
}
