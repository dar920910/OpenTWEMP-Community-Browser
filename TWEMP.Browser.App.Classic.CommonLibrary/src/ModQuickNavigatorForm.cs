﻿namespace TWEMP.Browser.App.Classic.CommonLibrary;

using TWEMP.Browser.Core.GamingSupport.M2TW;

public partial class ModQuickNavigatorForm : Form
{
    private const string ModLocationPrefix = "[MOD]";

    private readonly string currentModHomeDirectory;
    private readonly string modDataFolderName;
    private readonly string modSavesFolderName;
    private readonly string modLogsFolderName;
    private readonly string dataTextFolderName;
    private readonly string dataLoadingScreenFolderName;
    private readonly string dataUiFolderName;
    private readonly string dataFmvFolderName;
    private readonly string dataSoundsFolderName;
    private readonly string dataUnitModelsFolderName;
    private readonly string dataUnitSpritesFolderName;
    private readonly string dataAnimationsFolderName;
    private readonly string dataBannersFolderName;
    private readonly string dataModelsStratFolderName;
    private readonly string worldMapsBaseFolderName;
    private readonly string worldMapsCampaignFolderName;

    public ModQuickNavigatorForm(string modHomeDirectory)
	{
			InitializeComponent();

			Text = $"Mod Quick Navigation: {modHomeDirectory}";

			currentModHomeDirectory = modHomeDirectory;
			modDataFolderName = GameFolderNames.ModDataFolderName;
			modSavesFolderName = GameFolderNames.ModSavesFolderName;
			modLogsFolderName = GameFolderNames.ModLogsFolderName;
			dataTextFolderName = GameFolderNames.DataTextFolderName;
			dataLoadingScreenFolderName = GameFolderNames.DataLoadingScreenFolderName;
			dataUiFolderName = GameFolderNames.DataUiFolderName;
			dataFmvFolderName = GameFolderNames.DataFmvFolderName;
			dataSoundsFolderName = GameFolderNames.DataSoundsFolderName;
			dataUnitModelsFolderName = GameFolderNames.DataUnitModelsFolderName;
			dataUnitSpritesFolderName = GameFolderNames.DataUnitSpritesFolderName;
			dataAnimationsFolderName = GameFolderNames.DataAnimationsFolderName;
			dataBannersFolderName = GameFolderNames.DataBannersFolderName;
			dataModelsStratFolderName = GameFolderNames.DataModelsStratFolderName;
			worldMapsBaseFolderName = GameFolderNames.WorldMapsBaseFolderName;
			worldMapsCampaignFolderName = GameFolderNames.WorldMapsCampaignFolderName;

			modDataNavigateButton.Text = $"{ModLocationPrefix}\\{modDataFolderName}";
			modSavesNavigateButton.Text = $"{ModLocationPrefix}\\{modSavesFolderName}";
			modLogsNavigateButton.Text = $"{ModLocationPrefix}\\{modLogsFolderName}";
			dataTextNavigateButton.Text = $"{ModLocationPrefix}\\{modDataFolderName}\\{dataTextFolderName}";
			dataLoadingScreenNavigateButton.Text = $"{ModLocationPrefix}\\{modDataFolderName}\\{dataLoadingScreenFolderName}";
			dataUiNavigateButton.Text = $"{ModLocationPrefix}\\{modDataFolderName}\\{dataUiFolderName}";
			dataFmvNavigateButton.Text = $"{ModLocationPrefix}\\{modDataFolderName}\\{dataFmvFolderName}";
			dataSoundsNavigateButton.Text = $"{ModLocationPrefix}\\{modDataFolderName}\\{dataSoundsFolderName}";
			dataUnitModelsNavigateButton.Text = $"{ModLocationPrefix}\\{modDataFolderName}\\{dataUnitModelsFolderName}";
			dataUnitSpritesNavigateButton.Text = $"{ModLocationPrefix}\\{modDataFolderName}\\{dataUnitSpritesFolderName}";
			dataAnimationsNavigateButton.Text = $"{ModLocationPrefix}\\{modDataFolderName}\\{dataAnimationsFolderName}";
			dataBannersNavigateButton.Text = $"{ModLocationPrefix}\\{modDataFolderName}\\{dataBannersFolderName}";
			dataModelsStratNavigateButton.Text = $"{ModLocationPrefix}\\{modDataFolderName}\\{dataModelsStratFolderName}";
			worldMapsBaseNavigateButton.Text = $"{ModLocationPrefix}\\{modDataFolderName}\\{worldMapsBaseFolderName}";
			worldMapsCampaignNavigateButton.Text = $"{ModLocationPrefix}\\{modDataFolderName}\\{worldMapsCampaignFolderName}";
	}

	private static void NavigateToModDirectory(string directoryPath)
	{
#if DISABLE_WHEN_MIGRATION
		SystemToolbox.ShowFileSystemDirectory(directoryPath);
#endif
		MessageBox.Show(directoryPath, "TEST", MessageBoxButtons.OK, MessageBoxIcon.Information);
	}

	private void formExitButton_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void ModDataNavigateButton_Click(object sender, EventArgs e)
	{
		string targetFolderPath = Path.Combine(currentModHomeDirectory, modDataFolderName);
		NavigateToModDirectory(targetFolderPath);
	}

	private void ModSavesNavigateButton_Click(object sender, EventArgs e)
	{
		string targetFolderPath = Path.Combine(currentModHomeDirectory, modSavesFolderName);
		NavigateToModDirectory(targetFolderPath);
	}

	private void ModLogsNavigateButton_Click(object sender, EventArgs e)
	{
		string targetFolderPath = Path.Combine(currentModHomeDirectory, modLogsFolderName);
		NavigateToModDirectory(targetFolderPath);
	}

	private void DataTextNavigateButton_Click(object sender, EventArgs e)
	{
		string targetFolderPath = Path.Combine(currentModHomeDirectory, modDataFolderName, dataTextFolderName);
		NavigateToModDirectory(targetFolderPath);
	}

	private void DataLoadingScreenNavigateButton_Click(object sender, EventArgs e)
	{
		string targetFolderPath = Path.Combine(currentModHomeDirectory, modDataFolderName, dataLoadingScreenFolderName);
		NavigateToModDirectory(targetFolderPath);
	}

	private void DataUiNavigateButton_Click(object sender, EventArgs e)
	{
		string targetFolderPath = Path.Combine(currentModHomeDirectory, modDataFolderName, dataUiFolderName);
		NavigateToModDirectory(targetFolderPath);
	}

	private void DataFmvNavigateButton_Click(object sender, EventArgs e)
	{
		string targetFolderPath = Path.Combine(currentModHomeDirectory, modDataFolderName, dataFmvFolderName);
		NavigateToModDirectory(targetFolderPath);
	}

	private void DataSoundsNavigateButton_Click(object sender, EventArgs e)
	{
		string targetFolderPath = Path.Combine(currentModHomeDirectory, modDataFolderName, dataSoundsFolderName);
		NavigateToModDirectory(targetFolderPath);
	}

	private void DataUnitModelsNavigateButton_Click(object sender, EventArgs e)
	{
		string targetFolderPath = Path.Combine(currentModHomeDirectory, modDataFolderName, dataUnitModelsFolderName);
		NavigateToModDirectory(targetFolderPath);
	}

	private void DataUnitSpritesNavigateButton_Click(object sender, EventArgs e)
	{
		string targetFolderPath = Path.Combine(currentModHomeDirectory, modDataFolderName, dataUnitSpritesFolderName);
		NavigateToModDirectory(targetFolderPath);
	}

	private void DataAnimationsNavigateButton_Click(object sender, EventArgs e)
	{
		string targetFolderPath = Path.Combine(currentModHomeDirectory, modDataFolderName, dataAnimationsFolderName);
		NavigateToModDirectory(targetFolderPath);
	}

	private void DataBannersNavigateButton_Click(object sender, EventArgs e)
	{
		string targetFolderPath = Path.Combine(currentModHomeDirectory, modDataFolderName, dataBannersFolderName);
		NavigateToModDirectory(targetFolderPath);
	}

	private void DataModelsStratNavigateButton_Click(object sender, EventArgs e)
	{
		string targetFolderPath = Path.Combine(currentModHomeDirectory, modDataFolderName, dataModelsStratFolderName);
		NavigateToModDirectory(targetFolderPath);
	}

	private void WorldMapsBaseNavigateButton_Click(object sender, EventArgs e)
	{
		string targetFolderPath = Path.Combine(currentModHomeDirectory, modDataFolderName, worldMapsBaseFolderName);
		NavigateToModDirectory(targetFolderPath);
	}

	private void WorldMapsCampaignNavigateButton_Click(object sender, EventArgs e)
	{
		string targetFolderPath = Path.Combine(currentModHomeDirectory, modDataFolderName, worldMapsCampaignFolderName);
		NavigateToModDirectory(targetFolderPath);
	}
}
