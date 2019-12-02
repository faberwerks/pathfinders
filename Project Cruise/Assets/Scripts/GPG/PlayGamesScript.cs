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
        // setting default value, if game is played for first time
        if (GameData.Instance.saveData == null)
        {
            GameData.Instance.saveData = new SaveData();
        }

        // tells us if first time that this game has been launched after install
        // - 0 = no
        // - 1 = yes
        if (!PlayerPrefs.HasKey("IsFirstTime"))
        {
            PlayerPrefs.SetInt("IsFirstTime", 1);
        }

        // load local data first because loading from cloud can take time, if user progresses while using local data, it will all
        // sync in comparison in [???]
        LoadLocal();

        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

        SignIn();
    }

    /// <summary>
    /// Sign in to Google Play Games.
    /// </summary>
    private void SignIn()
    {
        // when authentication process is done (successfuly or not), load cloud data
        Social.localUser.Authenticate(success => { LoadData(); });
    }

    #region Old Saved Games
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
    /// <summary>
    /// Serialises Save Data into byte array.
    /// </summary>
    /// <returns>Serialised Save Data as byte array.</returns>
    private byte[] SerialiseSaveData()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        MemoryStream memoryStream = new MemoryStream();
        binaryFormatter.Serialize(memoryStream, GameData.Instance.saveData);
        return memoryStream.ToArray();
    }

    /// <summary>
    /// Deserialises byte data into Save Data.
    /// </summary>
    /// <param name="data">Byte data to be deserialised.</param>
    /// <returns>Deserialised byte data as Save Data.</returns>
    private SaveData DeserialiseSaveData(byte[] data)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        MemoryStream memoryStream = new MemoryStream(data);
        SaveData saveData = binaryFormatter.Deserialize(memoryStream) as SaveData;
        memoryStream.Close();
        return saveData;
    }

    /// <summary>
    /// Deserialises serialised data into Save Data.
    /// </summary>
    /// <param name="path">Path of serialised save data</param>
    /// <returns>Deserialised byte data as Save Data.</returns>
    private SaveData DeserialiseSaveData(string path)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);

        SaveData saveData = binaryFormatter.Deserialize(stream) as SaveData;
        stream.Close();
        return saveData;
    }

    /// <summary>
    /// Assigns save data to game data and decides whether to use local or cloud save data.
    /// </summary>
    private void CompareCloudAndLocalSaveData(SaveData cloudData, SaveData localData)
    {
        //SaveData cloudSaveData = DeserialiseSaveData(cloudData);
        //SaveData localSaveData = DeserialiseSaveData(localData);

        // if first time that game has been launched after installing and successfully log in to GPG
        if (PlayerPrefs.GetInt("IsFirstTime") == 1)
        {
            // set playerpref to be 0 (false)
            PlayerPrefs.SetInt("IsFirstTime", 0);
            // cloud save is more up to date
            if (cloudData.timestamp > localData.timestamp)
            {
                SaveLocal(cloudData);
            }
        }
        // if not first time, start comparing
        else
        {
            // if one timestamp is higher than other, update it
            if (localData.timestamp > cloudData.timestamp)
            {
                // update cloud save
                // first set GameData save data to be equal to local data
                GameData.Instance.saveData = localData;
                isCloudDataLoaded = true;
                // save updated GameData save data to cloud
                SaveData();
                return;
            }
        }
        // if code above doesn't trigger return and code below executes
        // cloud save and local save are identical, can load either one
        GameData.Instance.saveData = cloudData;
        isCloudDataLoaded = true;
    }

    /// <summary>
    /// Loads data from the cloud or locally.
    /// </summary>
    public void LoadData()
    {
        // if connected to internet or signed in, do everything on cloud
        if (Social.localUser.authenticated)
        {
            isSaving = false;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithManualConflictResolution(SAVE_NAME, DataSource.ReadCacheOrNetwork, true, ResolveConflict, OnSavedGameOpened);
        }
        // will only run on Unity Editor
        // on device, localUser will be authenticated even if not connected to internet (if player is using GPG)
        else
        {
            LoadLocal();
        }
    }

    /// <summary>
    /// Loads saved game data locally.
    /// </summary>
    private void LoadLocal()
    {
        string path = Application.persistentDataPath + "/" + SAVE_NAME;
        if (File.Exists(path))
        {
            GameData.Instance.saveData = DeserialiseSaveData(path);
        }
        else
        {
            Debug.LogError("Save file not found in " + path + ".");
        }
    }

    /// <summary>
    /// Saves data to the cloud or locally.
    /// </summary>
    public void SaveData()
    {
        // if still running on local data (cloud data has not been loaded yet)
        if (!isCloudDataLoaded)
        {
            SaveLocal();
            return;
        }

        // if connected to internet or signed in, do everything on cloud
        if (Social.localUser.authenticated)
        {
            isSaving = true;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithManualConflictResolution(SAVE_NAME, DataSource.ReadCacheOrNetwork, true, ResolveConflict, OnSavedGameOpened);
        }
        // will only run on Unity Editor
        // on device, localUser will be authenticated even if not connected to internet (if player is using GPG)
        else
        {
            SaveLocal();
        }
    }

    /// <summary>
    /// Saves saved game data locally.
    /// </summary>
    private void SaveLocal()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/" + SAVE_NAME;
        FileStream stream = new FileStream(path, FileMode.Create);

        binaryFormatter.Serialize(stream, GameData.Instance.saveData);
        stream.Close();
    }

    /// <summary>
    /// Saves saved game data locally.
    /// </summary>
    /// <param name="saveData">Saved game data to be saved.</param>
    private void SaveLocal(SaveData saveData)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/" + SAVE_NAME;
        FileStream stream = new FileStream(path, FileMode.Create);

        binaryFormatter.Serialize(stream, saveData);
        stream.Close();
    }

    /// <summary>
    /// Callback for resolving metadata conflict.
    /// </summary>
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
            // deserialising byte data into Save Data
            SaveData originalSaveData = DeserialiseSaveData(originalData);
            SaveData unmergedSaveData = DeserialiseSaveData(unmergedData);

            // getting timestamp
            DateTime originalTimestamp = originalSaveData.timestamp;
            DateTime unmergedTimestamp = unmergedSaveData.timestamp;

            // if original timestamp is more recent than unmerged timestamp
            if (originalTimestamp > unmergedTimestamp)
            {
                resolver.ChooseMetadata(original);
                return;
            }
            // unmerged timestamp is more recent than original
            else if (unmergedTimestamp > originalTimestamp)
            {
                resolver.ChooseMetadata(unmerged);
                return;
            }
            // if return doesn't get called, original and unmerged are identical
            // can keep either one
            resolver.ChooseMetadata(original);
        }
    }

    /// <summary>
    /// Callback for opening saved game data.
    /// </summary>
    private void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        // if connected to internet
        if (status == SavedGameRequestStatus.Success)
        {
            // if LOADING game data
            if (!isSaving)
            {
                LoadGame(game);
            }
            // if SAVING game data
            else
            {
                SaveGame(game);
            }
        }
        // if couldn't successfully connect to cloud, runs while on device
        // same code that is in else statements in LoadData() and SaveData()
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

    /// <summary>
    /// Loads saved game data from the cloud.
    /// </summary>
    /// <param name="game"></param>
    private void LoadGame(ISavedGameMetadata game)
    {
        ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(game, OnSavedGameDataRead);
    }

    /// <summary>
    /// Saves saved game data to the cloud.
    /// </summary>
    /// <param name="game"></param>
    private void SaveGame(ISavedGameMetadata game)
    {
        // also saving locally
        SaveLocal();

        // serialises to byte array
        byte[] dataToSave = SerialiseSaveData();
        // updating metadata with new description
        SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder().Build();
        // uploading data to cloud
        ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(game, update, dataToSave, OnSavedGameDataWritten);
    }

    /// <summary>
    /// Callback for ReadBinaryData().
    /// </summary>
    private void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] savedData)
    {
        // if reading of data successful
        if (status == SavedGameRequestStatus.Success)
        {
            SaveData cloudSaveData;
            // if never played game before, savedData will have length of 0
            if (savedData.Length == 0)
            {
                // assign new SaveData to cloudSaveData
                cloudSaveData = new SaveData();
            }
            // otherwise take byte[] of data abnd deserialise
            else
            {
                cloudSaveData = DeserialiseSaveData(savedData);
            }

            // getting local data
            // (if never played before on this device, localData is already new SaveData
            // no need for checking as with cloudSaveData
            string path = Application.persistentDataPath + "/" + SAVE_NAME;
            SaveData localSaveData = DeserialiseSaveData(path);

            CompareCloudAndLocalSaveData(cloudSaveData, localSaveData);
        }
    }

    /// <summary>
    /// Callback for writing saved game data.
    /// </summary>
    /// <param name="status"></param>
    /// <param name="game"></param>
    private void OnSavedGameDataWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {

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
    //public static void AddScoreToLeaderboard(string leaderboardId, long score)
    //{
    //    Social.ReportScore(score, leaderboardId, success => { });
    //}

    //public static void ShowLeaderboardsUI()
    //{
    //    Social.ShowLeaderboardUI();
    //}
    #endregion /Leaderboards
}
