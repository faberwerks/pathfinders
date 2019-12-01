using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

public class PlayGamesScript : MonoBehaviour
{
    public static PlayGamesScript Instance { get; private set; }

    private const string SAVE_NAME = "save.gd";
    private bool isSaving;
    private bool isCloudDataLoaded = false;

    // Start is called before the first frame update
    private void Start()
    {
        Instance = this;
        
        //if (!PlayerPrefs.HasKey(SAVE_NAME))
        //{
        //    PlayerPrefs.SetString(SAVE_NAME, "0");
        //}
        if (GameData.Instance.saveData == null)
        {
            GameData.Instance.saveData = new SaveData();
        }

        if (!PlayerPrefs.HasKey("IsFirstTime"))
        {
            PlayerPrefs.SetInt("IsFirstTime", 1);
        }

        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

        SignIn();
    }

    private void SignIn()
    {
        Social.localUser.Authenticate(success => { LoadData(); });
    }

    #region SavedGames
    //private string GameDataToString()
    //{
    //    return CloudVariables.Highscore.ToString();
    //}

    //private void StringToGameData(string cloudData, string localData)
    //{
    //    if (PlayerPrefs.GetInt("IsFirstTime") == 1)
    //    {
    //        PlayerPrefs.SetInt("IsFirstTime", 0);
    //        if (int.Parse(cloudData) > int.Parse(localData))
    //        {
    //            PlayerPrefs.SetString(SAVE_NAME, cloudData);
    //        }
    //    }
    //    else
    //    {
    //        if (int.Parse(localData) > int.Parse(cloudData))
    //        {
    //            CloudVariables.Highscore = int.Parse(localData);
    //            AddScoreToLeaderboard(GPGSIds.leaderboard_test_leaderboard, CloudVariables.Highscore);
    //            isCloudDataLoaded = true;
    //            SaveData();
    //            return;
    //        }
    //    }

    //    CloudVariables.Highscore = int.Parse(cloudData);
    //    isCloudDataLoaded = true;
    //}

    //private void StringToGameData(string localData)
    //{
    //    CloudVariables.Highscore = int.Parse(localData);
    //}

    //public void LoadData()
    //{
    //    if (Social.localUser.authenticated)
    //    {
    //        isSaving = false;
    //        ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithManualConflictResolution(SAVE_NAME, DataSource.ReadCacheOrNetwork, true, ResolveConflict, OnSavedGameOpened);
    //    }
    //    else
    //    {
    //        LoadLocal();
    //    }
    //}

    //private byte[] LoadLocal()
    //{
    //    //StringToGameData(PlayerPrefs.GetString(SAVE_NAME));
    //    string path = Application.persistentDataPath + "/" + SAVE_NAME;
    //    if (File.Exists(path))
    //    {
    //        BinaryFormatter formatter = new BinaryFormatter();
    //        FileStream stream = new FileStream(path, FileMode.Open);

    //        SaveData data = formatter.Deserialize(stream) as SaveData;
    //    }
    //    else
    //    {
    //        Debug.LogError("Save file not found in " + path);
    //        return null;
    //    }
    //}

    //public void SaveData()
    //{
    //    if (!isCloudDataLoaded)
    //    {
    //        SaveLocal();
    //    }

    //    if (Social.localUser.authenticated)
    //    {
    //        isSaving = true;
    //        ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithManualConflictResolution(SAVE_NAME, DataSource.ReadCacheOrNetwork, true, ResolveConflict, OnSavedGameOpened);
    //    }
    //    else
    //    {
    //        SaveLocal();
    //    }
    //}

    //private void SaveLocal()
    //{
    //    //PlayerPrefs.SetString(SAVE_NAME, GameDataToString());
    //    BinaryFormatter formatter = new BinaryFormatter();

    //    string path = Application.persistentDataPath + "/" + SAVE_NAME;
    //    FileStream stream = new FileStream(path, FileMode.Create);

    //    SaveData data = new SaveData();

    //    formatter.Serialize(stream, data);
    //    stream.Close();
    //}

    //private void ResolveConflict(IConflictResolver resolver, ISavedGameMetadata original, byte[] originalData, ISavedGameMetadata unmerged, byte[] unmergedData)
    //{
    //    if (originalData == null)
    //    {
    //        resolver.ChooseMetadata(unmerged);
    //    }
    //    else if (unmergedData == null)
    //    {
    //        resolver.ChooseMetadata(original);
    //    }
    //    else
    //    {
    //        string originalStr = Encoding.ASCII.GetString(originalData);
    //        string unmergedStr = Encoding.ASCII.GetString(unmergedData);

    //        int originalNum = int.Parse(originalStr);
    //        int unmergedNum = int.Parse(unmergedStr);

    //        if (originalNum > unmergedNum)
    //        {
    //            resolver.ChooseMetadata(original);
    //            return;
    //        }
    //        else if (unmergedNum > originalNum)
    //        {
    //            resolver.ChooseMetadata(unmerged);
    //            return;
    //        }
    //    }

    //    resolver.ChooseMetadata(original);
    //}

    //private void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    //{
    //    if (status == SavedGameRequestStatus.Success)
    //    {
    //        if (!isSaving)
    //        {
    //            LoadGame(game);
    //        }
    //        else
    //        {
    //            SaveGame(game);
    //        }
    //    }
    //    else
    //    {
    //        if (!isSaving)
    //        {
    //            LoadLocal();
    //        }
    //        else
    //        {
    //            SaveLocal();
    //        }
    //    }
    //}

    //private void LoadGame(ISavedGameMetadata game)
    //{
    //    ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(game, OnSavedGameDataRead);
    //}

    //private void SaveGame(ISavedGameMetadata game)
    //{
    //    string stringToSave = GameDataToString();
    //    SaveLocal();

    //    byte[] dataToSave = Encoding.ASCII.GetBytes(stringToSave);

    //    SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder().Build();

    //    ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(game, update, dataToSave, OnSavedGameDataWritten);
    //}

    //private void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] savedData)
    //{
    //    if (status == SavedGameRequestStatus.Success)
    //    {
    //        string cloudDataString;

    //        if (savedData.Length == 0)
    //        {
    //            cloudDataString = "0";
    //        }
    //        else
    //        {
    //            cloudDataString = Encoding.ASCII.GetString(savedData);
    //        }

    //        string localDataString = PlayerPrefs.GetString(SAVE_NAME);

    //        StringToGameData(cloudDataString, localDataString);
    //    }
    //}

    //private void OnSavedGameDataWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    //{

    //}
    #endregion /Saved Games

    #region Saved Games
    // USELESS?
    private byte[] SaveDataToByteArray()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        MemoryStream memoryStream = new MemoryStream();
        binaryFormatter.Serialize(memoryStream, GameData.Instance.saveData);
        return memoryStream.ToArray();
    }

    private void ByteArrayToSaveData(byte[] cloudData, byte[] localData)
    {
        SaveData cloudSaveData = DeserialiseSaveData(cloudData);
        SaveData localSaveData = DeserialiseSaveData(localData);

        if (PlayerPrefs.GetInt("IsFirstTime") == 1)
        {
            PlayerPrefs.SetInt("IsFirstTime", 0);
            if (cloudSaveData.timestamp > localSaveData.timestamp)
            {
                GameData.Instance.saveData = cloudSaveData;
            }
        }
        else
        {
            if (localSaveData.timestamp > cloudSaveData.timestamp)
            {
                GameData.Instance.saveData = localSaveData;
                isCloudDataLoaded = true;
                SaveData();
                return;
            }
        }
        GameData.Instance.saveData = cloudSaveData;
        isCloudDataLoaded = true;
    }

    private void ByteArrayToSaveData(byte[] localData)
    {
        GameData.Instance.saveData = DeserialiseSaveData(localData); ;
    }

    public void LoadData()
    {
        if (Social.localUser.authenticated)
        {
            isSaving = false;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithManualConflictResolution(SAVE_NAME, DataSource.ReadCacheOrNetwork, true, ResolveConflict, OnSavedGameOpened);
        }
        else
        {
            LoadLocal();
        }
    }

    private void LoadLocal()
    {
        string path = Application.persistentDataPath + "/" + SAVE_NAME;
        if (File.Exists(path))
        {
            GameData.Instance.saveData = DeserialiseSaveData(path);
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
        }
    }

    public void SaveData()
    {
        if (!isCloudDataLoaded)
        {
            SaveLocal();
            return;
        }

        if (Social.localUser.authenticated)
        {
            isSaving = true;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithManualConflictResolution(SAVE_NAME, DataSource.ReadCacheOrNetwork, true, ResolveConflict, OnSavedGameOpened);
        }
        else
        {
            SaveLocal();
        }
    }

    private void SaveLocal()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/" + SAVE_NAME;
        FileStream stream = new FileStream(path, FileMode.Create);

        binaryFormatter.Serialize(stream, GameData.Instance.saveData);
        stream.Close();
    }

    private void ResolveConflict(IConflictResolver resolver, ISavedGameMetadata original, byte[] originalData, ISavedGameMetadata unmerged, byte[] unmergedData)
    {
        if (originalData == null)
        {
            resolver.ChooseMetadata(unmerged);
        }
        else if (unmergedData == null)
        {
            resolver.ChooseMetadata(original);
        }
        else
        {
            SaveData originalSaveData = DeserialiseSaveData(originalData);
            SaveData unmergedSaveData = DeserialiseSaveData(unmergedData);

            DateTime originalTimestamp = originalSaveData.timestamp;
            DateTime unmergedTimestamp = unmergedSaveData.timestamp;

            if (originalTimestamp > unmergedTimestamp)
            {
                resolver.ChooseMetadata(original);
                return;
            }
            else if (unmergedTimestamp > originalTimestamp)
            {
                resolver.ChooseMetadata(unmerged);
                return;
            }
            resolver.ChooseMetadata(original);
        }
    }

    private void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            if (!isSaving)
            {
                LoadGame(game);
            }
            else
            {
                SaveGame(game);
            }
        }
        else
        {
            if (!isSaving)
            {
                LoadLocal();
            }
            else
            {
                SaveLocal();
            }
        }
    }

    private void LoadGame(ISavedGameMetadata game)
    {
        ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(game, OnSavedGameDataRead);
    }

    private void SaveGame(ISavedGameMetadata game)
    {
        SaveLocal();

        byte[] dataToSave = SaveDataToByteArray();

        SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder().Build();

        ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(game, update, dataToSave, OnSavedGameDataWritten);
    }

    private void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] savedData)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            SaveData cloudSaveData;
            if (savedData.Length == 0)
            {
                cloudSaveData = new SaveData();
            }
            else
            {
                cloudSaveData = DeserialiseSaveData(savedData);
            }
            
            SaveData localSaveData = GameData.Instance.saveData;

            ByteArrayToSaveData(cloudSaveData, localSaveData);
        }
    }

    private SaveData DeserialiseSaveData(byte[] data)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        MemoryStream memoryStream = new MemoryStream(data);
        SaveData saveData = binaryFormatter.Deserialize(memoryStream) as SaveData;
        memoryStream.Close();
        return saveData;
    }

    private SaveData DeserialiseSaveData(string path)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);

        SaveData saveData = binaryFormatter.Deserialize(stream) as SaveData;
        stream.Close();
        return saveData;
    }
    #endregion

    #region Achievements
    public static void UnlockAchievement(string id)
    {
        Social.ReportProgress(id, 100, success => { });
    }

    public static void IncrementAchievement(string id, int stepsToIncrement)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(id, stepsToIncrement, success => { });
    }

    public static void ShowAchievementsUI()
    {
        Social.ShowAchievementsUI();
    }
    #endregion /Achievements

    #region Leaderboards
    public static void AddScoreToLeaderboard(string leaderboardId, long score)
    {
        Social.ReportScore(score, leaderboardId, success => { });
    }

    public static void ShowLeaderboardsUI()
    {
        Social.ShowLeaderboardUI();
    }
    #endregion /Leaderboards
}
