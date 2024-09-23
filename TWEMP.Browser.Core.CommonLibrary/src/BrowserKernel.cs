﻿// <copyright file="BrowserKernel.cs" company="The OpenTWEMP Project">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#pragma warning disable SA1600 // ElementsMustBeDocumented

namespace TWEMP.Browser.Core.CommonLibrary;

using TWEMP.Browser.Core.CommonLibrary.CustomManagement;
using TWEMP.Browser.Core.CommonLibrary.CustomManagement.GUI;
using TWEMP.Browser.Core.CommonLibrary.CustomManagement.Localization;

/// <summary>
/// Defines the kernel of any OpenTWEMP Community Browser implementation.
/// </summary>
public static class BrowserKernel
{
    private static readonly AppGuiStyleManager AppGuiStyleManagerInstance;

    static BrowserKernel()
    {
        AppGuiStyleManagerInstance = AppGuiStyleManager.Create();
    }

    public static GuiStyle CurrentGUIStyle
    {
        get
        {
            return AppGuiStyleManagerInstance.CurrentStyle;
        }

        set
        {
            AppGuiStyleManagerInstance.CurrentStyle = value;
        }
    }

    public static bool UseExperimentalFeatures
    {
        get
        {
            return AppSystemSettingsManager.UseExperimentalFeatures;
        }

        set
        {
            AppSystemSettingsManager.UseExperimentalFeatures = value;
        }
    }

    public static ColorTheme SelectCurrentColorTheme()
    {
        return AppGuiStyleManagerInstance.GetCurrentColorTheme();
    }

    public static ColorTheme UpdateCurrentColorTheme(GuiStyle style)
    {
        if (CurrentGUIStyle != style)
        {
            CurrentGUIStyle = style;
        }

        return AppGuiStyleManagerInstance.GetCurrentColorTheme();
    }

    public static bool IsEnabledLocalizationOnEnglish()
    {
        return AppLocalizationManager.IsCurrentLocalizationName(AppLocalization.LOCALE_NAME_ENG);
    }

    public static bool IsEnabledLocalizationOnRussian()
    {
        return AppLocalizationManager.IsCurrentLocalizationName(AppLocalization.LOCALE_NAME_RUS);
    }

    public static void SetLocalizationOnEnglishAsCurrent()
    {
        AppLocalizationManager.SetCurrentLocalizationByName(AppLocalization.LOCALE_NAME_ENG);
    }

    public static void SetLocalizationOnRussianAsCurrent()
    {
        AppLocalizationManager.SetCurrentLocalizationByName(AppLocalization.LOCALE_NAME_RUS);
    }

    public static string GetTextInCurrentLocalization(string formName, string controlName)
    {
        LocaleSnapshot snapshot = AppLocalizationManager.CurrentLocalization.GetFormLocaleSnapshotByKey(formName);
        return snapshot.GetLocalizedValueByKey(controlName);
    }
}
