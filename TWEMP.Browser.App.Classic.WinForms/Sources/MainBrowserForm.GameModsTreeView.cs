﻿// <copyright file="MainBrowserForm.GameModsTreeView.cs" company="The OpenTWEMP Project">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#pragma warning disable SA1600 // ElementsMustBeDocumented
#pragma warning disable SA1601 // PartialElementsMustBeDocumented

#define DISABLE_LEGACY_GAMEMOD_VISUALIZATION
#undef DISABLE_LEGACY_GAMEMOD_VISUALIZATION

namespace TWEMP.Browser.App.Classic;

using TWEMP.Browser.App.Classic.WinForms.Properties;
using TWEMP.Browser.Core.CommonLibrary;
using TWEMP.Browser.Core.CommonLibrary.AppGuiAbstractions;
using TWEMP.Browser.Core.CommonLibrary.CustomManagement.Gaming;
using TWEMP.Browser.Core.CommonLibrary.CustomManagement.Gaming.Collections;
using TWEMP.Browser.Core.CommonLibrary.CustomManagement.Gaming.Views;
using TWEMP.Browser.Core.CommonLibrary.CustomManagement.GUI;
using TWEMP.Browser.Core.GamingSupport.TotalWarEngine.M2TW.Configuration.Frontend;

internal partial class MainBrowserForm : IUpdatableBrowser
{
    public void UpdateModificationsTreeView()
    {
        this.treeViewGameMods.Enabled = false;

        if (BrowserKernel.SyncUserDataForAllGameMods())
        {
            this.UpdateAllModificationsInTreeView();
            this.UpdateCustomCollectionsInTreeView();
            this.UpdateFavoriteCollectionInTreeView();

            this.DisableModUIControls();
        }

        this.treeViewGameMods.Enabled = true;
    }

    private void UpdateAllModificationsInTreeView()
    {
        TreeNode allModsNode = this.treeViewGameMods.Nodes[2];
        allModsNode.Nodes.Clear();

        List<GameSetupInfo> gameInstallations = BrowserKernel.GameInstallations;
        foreach (GameSetupInfo gameInstallation in gameInstallations)
        {
            TreeNode gameInstallationNode = new TreeNode(gameInstallation.Name);
            allModsNode.Nodes.Add(gameInstallationNode);

            List<ModCenterInfo> modCenters = gameInstallation.AttachedModCenters;
            foreach (ModCenterInfo modcenter in modCenters)
            {
                TreeNode modcenterNode = new TreeNode(modcenter.Name);
                gameInstallationNode.Nodes.Add(modcenterNode);

                List<GameModificationInfo> mods = modcenter.InstalledModifications;
                foreach (GameModificationInfo mod in mods)
                {
                    TreeNode modNode = this.CreateCollectionChildNode(mod.ShortName);
                    modcenterNode.Nodes.Add(modNode);
                }
            }
        }
    }

    private void UpdateFavoriteCollectionInTreeView()
    {
        TreeNode favoriteCollectionNode = this.treeViewGameMods.Nodes[0];
        favoriteCollectionNode.Nodes.Clear();
        this.CreateFavoriteCollectionChildNodes(BrowserKernel.FavoriteModsCollection, favoriteCollectionNode);
    }

    private void CreateFavoriteCollectionChildNodes(CustomModsCollection favoriteCollection, TreeNode favoriteCollectionRootNode)
    {
        foreach (KeyValuePair<string, string> modPair in favoriteCollection.Modifications)
        {
            var modNode = this.CreateCollectionChildNode(modPair.Value);
            favoriteCollectionRootNode.Nodes.Add(modNode);
        }
    }

    private void TreeViewGameMods_AfterSelect(object sender, TreeViewEventArgs e)
    {
        if (this.IsNotModificationNode(e.Node!))
        {
            this.ChangeSelectedNodeView(e.Node!);
            this.DisableModUIControls();
            return;
        }
        else
        {
            if (this.IsNodeOfGameModView(e.Node!))
            {
                UpdatableGameModificationView gameModView = this.SelectGameModViewByTreeNode(e.Node!);

                BrowserKernel.CurrentConfigurator = M2TWGameConfigurator.CreateByDefault(gameModView.CurrentInfo);
                BrowserKernel.CurrentGameModView = gameModView;

                this.modMainTitleLabel.Text = gameModView.GetActivePresetFullName();
                this.modStatusLabel.Text = gameModView.GetCustomizablePresetDescription();
                this.UpdateGameModLogoPictureBox(gameModView);
                this.UpdateConfigurationControls(gameModView);

                if (this.WindowState != FormWindowState.Minimized)
                {
                    this.StartGameModBackgroundMusic(gameModView);
                }

#if DISABLE_LEGACY_GAMEMOD_VISUALIZATION
                GameModificationInfo selectedModification = FindModBySelectedNodeFromCollection(e.Node!);

                if (BrowserKernel.UseExperimentalFeatures)
                {
                    modMainTitleLabel.Text = selectedModification.CurrentPreset.ModTitle + " [" + selectedModification.CurrentPreset.ModVersion + "]";
                    modStatusLabel.Text = "Customize Your Mod via Preset Configuration File: " + selectedModification.GetPresetFilePath();

                    string modLogoImageFilePath = selectedModification.GetModPresetLogoImageFilePath();

                    if (File.Exists(modLogoImageFilePath))
                    {
                        if (modLogoPictureBox.Image != null)
                        {
                            modLogoPictureBox.Image.Dispose();
                            modLogoPictureBox.Image = null;
                        }

                        modLogoPictureBox.Load(modLogoImageFilePath);
                    }
                    else
                    {
                        modLogoPictureBox.Image = Resources.OPENTWEMP_LOGO;
                    }

                    if (selectedModification.CanBeLaunchedViaNativeBatch())
                    {
                        radioButtonLauncherProvider_BatchScript.Enabled = true;
                        ColorTheme currentColorTheme = BrowserKernel.SelectCurrentColorTheme();
                        radioButtonLauncherProvider_BatchScript.ForeColor = currentColorTheme.CommonControlsForeColor;
                    }
                    else
                    {
                        radioButtonLauncherProvider_BatchScript.Enabled = false;
                        radioButtonLauncherProvider_BatchScript.ForeColor = Color.DarkRed;
                    }

                    if (selectedModification.CanBeLaunchedViaNativeSetup())
                    {
                        radioButtonLauncherProvider_NativeSetup.Enabled = true;
                        ColorTheme currentColorTheme = BrowserKernel.SelectCurrentColorTheme();
                        radioButtonLauncherProvider_NativeSetup.ForeColor = currentColorTheme.CommonControlsForeColor;
                    }
                    else
                    {
                        radioButtonLauncherProvider_NativeSetup.Enabled = false;
                        radioButtonLauncherProvider_NativeSetup.ForeColor = Color.DarkRed;
                    }

                    if (selectedModification.CanBeLaunchedViaM2TWEOP())
                    {
                        radioButtonLauncherProvider_M2TWEOP.Enabled = true;
                        ColorTheme currentColorTheme = BrowserKernel.SelectCurrentColorTheme();
                        radioButtonLauncherProvider_M2TWEOP.ForeColor = currentColorTheme.CommonControlsForeColor;
                    }
                    else
                    {
                        radioButtonLauncherProvider_M2TWEOP.Enabled = false;
                        radioButtonLauncherProvider_M2TWEOP.ForeColor = Color.DarkRed;
                    }
                }
                else
                {
                    modMainTitleLabel.Text = selectedModification.ShortName;
                    modStatusLabel.Text = selectedModification.Location;
                    modLogoPictureBox.Image = Resources.OPENTWEMP_LOGO;
                }
#endif

                this.EnableModUIControls();
                return;
            }
        }
    }

    private bool IsNotModificationNode(TreeNode node)
    {
        // Is 'node' is selected on the Collection Level ?
        if (this.IsRootNodeOfTreeView(node))
        {
            return true;
        }

        // Is 'node' is selected on the Custom Collection Folders Level ?
        if (this.IsCustomCollectionFolderNode(node))
        {
            return true;
        }

        // Is 'node' is selected on the GameSetup Node Level ?
        if (this.IsGameSetupNode(node))
        {
            return true;
        }

        // Is 'node' is selected on the ModCentet Node Level ?
        if (this.IsModCenterNode(node))
        {
            return true;
        }

        return false;
    }

    private bool IsRootNodeOfTreeView(TreeNode node)
    {
        return node.Level == 0;
    }

    private bool IsCustomCollectionFolderNode(TreeNode node)
    {
        return (node.Level == 1) && node.Parent.Text.Equals("My Mod Collections");
    }

    private bool IsGameSetupNode(TreeNode node)
    {
        return (node.Level == 1) && node.Parent.Text.Equals("All Modifications");
    }

    private bool IsModCenterNode(TreeNode node)
    {
        return (node.Level == 2) && node.Parent.Parent.Text.Equals("All Modifications");
    }

    private bool IsNodeOfGameModView(TreeNode node)
    {
        if (this.IsNodeOfFavoriteCollection(node))
        {
            return true;
        }

        if (this.IsNodeOfModificationFromCustomCollection(node))
        {
            return true;
        }

        if (this.IsNodeOfModificationFromAllModsCollection(node))
        {
            return true;
        }

        return false;
    }

    private bool IsNodeOfFavoriteCollection(TreeNode node)
    {
        if (node.Level == 1)
        {
            if (node.Parent.Text.Equals("My Favorite Mods"))
            {
                return true;
            }
        }

        return false;
    }

    private bool IsNodeOfModificationFromCustomCollection(TreeNode node)
    {
        if (node.Level == 2)
        {
            TreeNode currentCollection = node.Parent;

            if (currentCollection.Parent.Text.Equals("My Mod Collections"))
            {
                return true;
            }
        }

        return false;
    }

    private bool IsNodeOfModificationFromAllModsCollection(TreeNode node)
    {
        if (node.Level == 3)
        {
            TreeNode currentModCenterNode = node.Parent;
            TreeNode currentGameSetupNode = currentModCenterNode.Parent;

            if (currentGameSetupNode.Parent.Text.Equals("All Modifications"))
            {
                return true;
            }
        }

        return false;
    }

    private UpdatableGameModificationView SelectGameModViewByTreeNode(TreeNode node)
    {
        GameModificationInfo gameModInfo = BrowserKernel.GetActiveModificationInfo(node.Text);
        return BrowserKernel.CurrentGameModsCollectionView.SelectGameModViewByInfo(gameModInfo) !;
    }

    private GameModificationInfo FindModBySelectedNodeFromCollection(TreeNode selectedTreeNode)
    {
        return BrowserKernel.GetActiveModificationInfo(selectedTreeNode.Text);
    }

    private GameModificationInfo FindModificationBySelectedTreeNode(TreeNode selectedTreeNode)
    {
        TreeNode modcenterNode = selectedTreeNode.Parent;
        TreeNode gamesetupNode = modcenterNode.Parent;

        GameSetupInfo currentGameSetup = BrowserKernel.GameInstallations.Find(gamesetup => gamesetup.Name.Equals(gamesetupNode.Text)) !;
        ModCenterInfo currentModCenter = currentGameSetup.AttachedModCenters.Find(modcenter => modcenter.Name.Equals(modcenterNode.Text)) !;

        return currentModCenter.InstalledModifications[selectedTreeNode.Index];
    }

    private void ChangeSelectedNodeView(TreeNode node)
    {
        if (node.IsExpanded)
        {
            node.Collapse();
        }
        else
        {
            node.Expand();
        }
    }

    private void UpdateGameModLogoPictureBox(UpdatableGameModificationView gameModView)
    {
        FileInfo gameModLogoImageFileInfo = BrowserKernel.GetActivePresetLogoImageFileInfo(gameModView);
        this.LoadGameModLogoImageIntoPictureBox(gameModLogoImageFileInfo);
    }

    private void LoadGameModLogoImageIntoPictureBox(FileInfo gameModLogoImage)
    {
        if (gameModLogoImage.Exists)
        {
            if (this.modLogoPictureBox.Image != null)
            {
                this.modLogoPictureBox.Image.Dispose();
                this.modLogoPictureBox.Image = null;
            }

            this.modLogoPictureBox.Load(gameModLogoImage.FullName);
        }
        else
        {
            this.modLogoPictureBox.Image = Resources.OPENTWEMP_LOGO;
        }
    }

    private void UpdateConfigurationControls(UpdatableGameModificationView gameModView)
    {
        if (gameModView.CurrentInfo.CanBeLaunchedViaNativeBatch())
        {
            this.radioButtonLauncherProvider_BatchScript.Enabled = true;
            ColorTheme currentColorTheme = BrowserKernel.SelectCurrentColorTheme();
            this.radioButtonLauncherProvider_BatchScript.ForeColor = currentColorTheme.CommonControlsForeColor;
        }
        else
        {
            this.radioButtonLauncherProvider_BatchScript.Enabled = false;
            this.radioButtonLauncherProvider_BatchScript.ForeColor = Color.DarkRed;
        }

        if (gameModView.CurrentInfo.CanBeLaunchedViaNativeSetup())
        {
            this.radioButtonLauncherProvider_NativeSetup.Enabled = true;
            ColorTheme currentColorTheme = BrowserKernel.SelectCurrentColorTheme();
            this.radioButtonLauncherProvider_NativeSetup.ForeColor = currentColorTheme.CommonControlsForeColor;
        }
        else
        {
            this.radioButtonLauncherProvider_NativeSetup.Enabled = false;
            this.radioButtonLauncherProvider_NativeSetup.ForeColor = Color.DarkRed;
        }

        if (gameModView.CurrentInfo.CanBeLaunchedViaM2TWEOP())
        {
            this.radioButtonLauncherProvider_M2TWEOP.Enabled = true;
            ColorTheme currentColorTheme = BrowserKernel.SelectCurrentColorTheme();
            this.radioButtonLauncherProvider_M2TWEOP.ForeColor = currentColorTheme.CommonControlsForeColor;
        }
        else
        {
            this.radioButtonLauncherProvider_M2TWEOP.Enabled = false;
            this.radioButtonLauncherProvider_M2TWEOP.ForeColor = Color.DarkRed;
        }
    }

    private TreeNode GetCurrentGameModNode()
    {
        return this.treeViewGameMods.SelectedNode;
    }

    private void SetCurrentGameModNode(TreeNode node)
    {
        this.treeViewGameMods.SelectedNode = node;

        if (node.IsSelected)
        {
            this.treeViewGameMods.Focus();
        }
    }
}
