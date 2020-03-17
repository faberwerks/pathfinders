using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// A component to handle interaction with Google Play Games including saving, loading, and achievements.
/// </summary>
public class PlayGamesScript : MonoBehaviour
{
    public static PlayGamesScript Instance { get; private set; }
    private SceneTransition sceneTransition;

    private const string SAVE_NAME = "save.gd";
    private bool isSaving;
    private bool isCloudDataLoaded = false;
    private bool isDone = false;
    private bool isLoading = true;

    private void Awake()
    {
        sceneTransition = GetComponent<SceneTransition>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log("[GPGS BUG] Start entered.");
        DontDestroyOnLoad(gameObject);
        Instance = this;

        //if (!PlayerPrefs.HasKey(SAVE_NAME))
        //{
        //    PlayerPrefs.SetString(SAVE_NAME, "0");
        //}
        // setting default value, if game is played for first time
        if (GameData.Instance.saveData == null)
        {
            Debug.Log("[GPGS BUG] GameData.Instance.saveData set to new save data.");
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
        Debug.Log("[GPGS BUG] SignIn entered.");
        // when authentication process is done (successfuly or not), load cloud data
        Social.localUser.Authenticate(success => { LoadData(); });
    }

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
        Debug.Log("[GPGS BUG] CompareCloudAndLocalSaveData entered.");
        // if first time that game has been launched after installing and successfully log in to GPG
        if (PlayerPrefs.GetInt("IsFirstTime") == 1)
        {
            Debug.Log("[GPGS BUG] CompareCloudAndLocalSaveData: first time.");
            // set playerpref to be 0 (false)
            PlayerPrefs.SetInt("IsFirstTime", 0);
            // cloud save is more up to date
            //Samuel 19 jan 2020 - Commented
            #region commented
            //if (cloudData.Timestamp > localData.Timestamp)
            //{
            //    SaveLocal(cloudData);
            //}
            #endregion

            //change to always get from cloud if there's cloud save
            try
            {
                SaveLocal(cloudData);
            }
            catch (Exception ex)
            {
                Debug.Log("[GPGS BUG] CompareCloudAndLocalSaveData: Exception.");
                Debug.Log(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        // if not first time, start comparing
        else
        {
            Debug.Log("[GPGS BUG] CompareCloudAndLocalSaveData: not first time.");
            // if one Timestamp is higher than other, update it
            #region commented
            //if (localData.Timestamp > cloudData.Timestamp)
            //{
            // update cloud save
            // first set GameData save data to be equal to local data
            //GameData.Instance.saveData = localData;
            //isCloudDataLoaded = true;
            // save updated GameData save data to cloud
            //    SaveData();
            //    return;
            //}
            #endregion

            //Samuel 19 Jan 2020 - Changed validation to fartest lvl
            // compare cloud with local one which one is the fartest one
            if (cloudData.LastLevelNumber > localData.LastLevelNumber)
            {
                Debug.Log("[GPGS BUG] CompareCloudAndLocalSaveData: cloud data is farther than local data.");
                isDone = true;
                GameData.Instance.saveData = cloudData;
                SaveLocal(cloudData);
                Debug.Log("[GPGS BUG] CompareCloudAndLocalSaveData: GameData.Instance.saveData set to cloud data.");
                return;
            }
            // compare cloud with local one which one has most stars
            else if (cloudData.LastLevelNumber == localData.LastLevelNumber)
            {
                Debug.Log("[GPGS BUG] CompareCloudAndLocalSaveData: cloud data is equal to local data.");
                int Cloudstar = 0;
                int localStar = 0;
                foreach (var item in cloudData.levelSaveData)
                {
                    Cloudstar += item.stars;
                }
                foreach (var item in localData.levelSaveData)
                {
                    localStar += item.stars;
                }

                if(Cloudstar > localStar)
                {
                    Debug.Log("[GPGS BUG] CompareCloudAndLocalSaveData: cloud has more stars.");
                    isDone = true;
                    GameData.Instance.saveData = cloudData;
                    SaveLocal(cloudData);
                    Debug.Log("[GPGS BUG] CompareCloudAndLocalSaveData: GameData.Instance.saveData set to cloud data.");
                    return;
                }
            }

            GameData.Instance.saveData = localData;
            Debug.Log("[GPGS BUG] CompareCloudAndLocalSaveData: GameData.Instance.saveData set to local data.");
            isCloudDataLoaded = true;
            SaveData();
            return;
        }
        // if code above doesn't trigger return and code below executes
        // cloud save and local save are identical, can load either one
        GameData.Instance.saveData = cloudData;
        Debug.Log("[GPGS BUG] CompareCloudAndLocalSaveData: GameData.Instance.saveData set to cloud data.");
        isCloudDataLoaded = true;
        isDone = true;
        EndLoading();
    }

    /// <summary>
    /// Loads data from the cloud or locally.
    /// </summary>
    public void LoadData()
    {
        Debug.Log("[GPGS BUG] LoadData entered.");
        // if connected to internet or signed in, do everything on cloud
        if (Social.localUser.authenticated)
        {
            Debug.Log("[GPGS BUG] LoadData: authenticated.");
            isSaving = false;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithManualConflictResolution(SAVE_NAME, DataSource.ReadCacheOrNetwork, true, ResolveConflict, OnSavedGameOpened);
        }
        // will only run on Unity Editor
        // on device, localUser will be authenticated even if not connected to internet (if player is using GPG)
        else
        {
            isDone = true;
            Debug.Log("[GPGS BUG] LoadData: not authenticated.");
            LoadLocal();
        }
    }

    /// <summary>
    /// Loads saved game data locally.
    /// </summary>
    private void LoadLocal()
    {
        Debug.Log("[GPGS BUG] LoadLocal entered.");
        string path = Application.persistentDataPath + "/" + SAVE_NAME;
        if (File.Exists(path))
        {
            GameData.Instance.saveData = DeserialiseSaveData(path);
        }
        else
        {
            Debug.LogError("Save file not found in " + path + ".");
        }

        EndLoading();
    }

    /// <summary>
    /// Saves data to the cloud or locally.
    /// </summary>
    public void SaveData()
    {
        Debug.Log("[GPGS BUG] SaveData entered.");
        // if still running on local data (cloud data has not been loaded yet)
        if (!isCloudDataLoaded)
        {
            Debug.Log("[GPGS BUG] SaveData: cloud data not loaded.");
            isDone = true;
            SaveLocal();
            return;
        }

        // if connected to internet or signed in, do everything on cloud
        if (Social.localUser.authenticated)
        {
            Debug.Log("[GPGS BUG] SaveData: authenticated.");
            isSaving = true;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithManualConflictResolution(SAVE_NAME, DataSource.ReadCacheOrNetwork, true, ResolveConflict, OnSavedGameOpened);
        }
        // will only run on Unity Editor
        // on device, localUser will be authenticated even if not connected to internet (if player is using GPG)
        else
        {
            Debug.Log("[GPGS BUG] SaveData: not authenticated.");
            isDone = true;
            SaveLocal();
        }
    }

    /// <summary>
    /// Saves saved game data locally.
    /// </summary>
    private void SaveLocal()
    {
        Debug.Log("[GPGS BUG] SaveLocal, no args, entered.");
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/" + SAVE_NAME;
        FileStream stream = new FileStream(path, FileMode.Create);

        binaryFormatter.Serialize(stream, GameData.Instance.saveData);
        stream.Close();

        EndLoading();
    }

    /// <summary>
    /// Saves saved game data locally.
    /// </summary>
    /// <param name="saveData">Saved game data to be saved.</param>
    private void SaveLocal(SaveData saveData)
    {
        Debug.Log("[GPGS BUG] SaveLocal, 1 arg, entered.");
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/" + SAVE_NAME;
        FileStream stream = new FileStream(path, FileMode.Create);

        binaryFormatter.Serialize(stream, saveData);
        stream.Close();

        EndLoading();
    }

    /// <summary>
    /// Callback for resolving metadata conflict.
    /// </summary>
    private void ResolveConflict(IConflictResolver resolver, ISavedGameMetadata original, byte[] originalData, ISavedGameMetadata unmerged, byte[] unmergedData)
    {
        Debug.Log("[GPGS BUG] ResolveConflict entered.");
        if (originalData == null)
        {
            Debug.Log("[GPGS BUG] ResolveConflict: originalData null. Choose unmerged.");
            resolver.ChooseMetadata(unmerged);
        }
        else if (unmergedData == null)
        {
            Debug.Log("[GPGS BUG] ResolveConflict: unmergedData null. Choose original.");
            resolver.ChooseMetadata(original);
        }
        else
        {
            // deserialising byte data into Save Data
            SaveData originalSaveData = DeserialiseSaveData(originalData);
            SaveData unmergedSaveData = DeserialiseSaveData(unmergedData);

            #region Old Timestamp-based Validation
            //// getting Timestamp
            //DateTime originalTimestamp = originalSaveData.Timestamp;
            //DateTime unmergedTimestamp = unmergedSaveData.Timestamp;

            //// if original Timestamp is more recent than unmerged Timestamp
            //if (originalTimestamp > unmergedTimestamp)
            //{
            //    resolver.ChooseMetadata(original);
            //    return;
            //}
            //// unmerged Timestamp is more recent than original
            //else if (unmergedTimestamp > originalTimestamp)
            //{
            //    resolver.ChooseMetadata(unmerged);
            //    return;
            //}
            //// if return doesn't get called, original and unmerged are identical
            //// can keep either one
            //resolver.ChooseMetadata(original);
            #endregion

            // getting last level
            int originalLastLevel = originalSaveData.LastLevelNumber;
            int unmergedLastLevel = originalSaveData.LastLevelNumber;

            // if original last level is farther than unmerged last level
            if (originalLastLevel > unmergedLastLevel)
            {
                resolver.ChooseMetadata(original);
                return;
            }
            // if unmerged last level is farther than original last level
            else if (unmergedLastLevel > originalLastLevel)
            {
                resolver.ChooseMetadata(unmerged);
                return;
            }

            // if return doesn't get called, original and unmerged last levels are identical
            // getting stars
            int originalStars = 0;
            int unmergedStars = 0;

            foreach (var levelSaveData in originalSaveData.levelSaveData)
            {
                originalStars += levelSaveData.stars;
            }

            foreach (var levelSaveData in unmergedSaveData.levelSaveData)
            {
                unmergedStars += levelSaveData.stars;
            }

            // if original stars is more than unmerged stars
            if (originalStars > unmergedStars)
            {
                Debug.Log("[GPGS BUG] ResolveConflict: originalStars more. Choose original.");
                resolver.ChooseMetadata(original);
                return;
            }
            // if unmerged stars is more than original stars
            else if (unmergedStars > originalStars)
            {
                Debug.Log("[GPGS BUG] ResolveConflict: unmergedStars more. Choose unmerged.");
                resolver.ChooseMetadata(unmerged);
                return;
            }

            // if return doesn't get called, original and unmerged are identical
            // can choose either one
            Debug.Log("[GPGS BUG] ResolveConflict: identical. Choose original.");
            resolver.ChooseMetadata(original);
        }
    }

    /// <summary>
    /// Callback for opening saved game data.
    /// </summary>
    private void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        Debug.Log("[GPGS BUG] OnSavedGameOpened entered.");
        // if connected to internet
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("[GPGS BUG] OnSavedGameOpened: success.");
            // if LOADING game data
            if (!isSaving)
            {
                Debug.Log("[GPGS BUG] OnSavedGameOpened: loading.");
                LoadGame(game);
            }
            // if SAVING game data
            else
            {
                isDone = true;
                Debug.Log("[GPGS BUG] OnSavedGameOpened: saving.");
                SaveGame(game);
            }
        }
        // if couldn't successfully connect to cloud, runs while on device
        // same code that is in else statements in LoadData() and SaveData()
        else
        {
            Debug.Log("[GPGS BUG] OnSavedGameOpened: failed.");
            if (!isSaving)
            {
                isDone = true;
                Debug.Log("[GPGS BUG] OnSavedGameOpened: loading.");
                LoadLocal();
            }
            else
            {
                isDone = true;
                Debug.Log("[GPGS BUG] OnSavedGameOpened: saving.");
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
        Debug.Log("[GPGS BUG] LoadGame entered.");
        ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(game, OnSavedGameDataRead);
    }

    /// <summary>
    /// Saves saved game data to the cloud.
    /// </summary>
    /// <param name="game"></param>
    private void SaveGame(ISavedGameMetadata game)
    {
        Debug.Log("[GPGS BUG] SaveGame entered.");
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
    /// Force-saves data to the cloud or locally.
    /// </summary>
    public void ForceSaveData()
    {
        Debug.Log("[GPGS BUG] ForceSaveData entered.");
        // if still running on local data (cloud data has not been loaded yet)
        if (!isCloudDataLoaded)
        {
            Debug.Log("[GPGS BUG] ForceSaveData: cloud data not loaded.");
            isDone = false;
            SaveLocal();
            return;
        }

        // if connected to internet or signed in, do everything on cloud
        if (Social.localUser.authenticated)
        {
            Debug.Log("[GPGS BUG] ForceSaveData: authenticated.");
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithManualConflictResolution(SAVE_NAME, DataSource.ReadCacheOrNetwork, true, ResolveConflict, OnForceSavedGameOpened);
        }
        // will only run on Unity Editor
        // on device, localUser will be authenticated even if not connected to internet (if player is using GPG)
        else
        {
            Debug.Log("[GPGS BUG] ForceSaveData: not authenticated.");
            isDone = false;
            SaveLocal();
        }
    }

    /// <summary>
    /// Callback for opening force-saved game data.
    /// </summary>
    private void OnForceSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        Debug.Log("[GPGS BUG] OnForceSavedGameOpened entered.");
        // if connected to internet
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("[GPGS BUG] OnForceSavedGameOpened: success.");
            isDone = false;
            Debug.Log("[GPGS BUG] OnForceSavedGameOpened: saving.");
            SaveGame(game);
        }
        // if couldn't successfully connect to cloud, runs while on device
        // same code that is in else statements in LoadData() and SaveData()
        else
        {
            Debug.Log("[GPGS BUG] OnForceSavedGameOpened: failed.");
            isDone = false;
            Debug.Log("[GPGS BUG] OnForceSavedGameOpened: saving.");
            SaveLocal();
        }
    }

    /// <summary>
    /// Callback for ReadBinaryData().
    /// </summary>
    private void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] savedData)
    {
        Debug.Log("[GPGS BUG] OnSavedGameDataRead entered.");
        // if reading of data successful
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("[GPGS BUG] OnSavedGameDataRead: success.");
            SaveData cloudSaveData;
            // if never played game before, savedData will have length of 0
            if (savedData.Length == 0)
            {
                Debug.Log("[GPGS BUG] OnSavedGameDataRead: save data length 0.");
                // assign new SaveData to cloudSaveData
                cloudSaveData = new SaveData();
            }
            // otherwise take byte[] of data abnd deserialise
            else
            {
                Debug.Log("[GPGS BUG] OnSavedGameDataRead: save data length not 0.");
                cloudSaveData = DeserialiseSaveData(savedData);
            }

            // getting local data
            // (if never played before on this device, localData is already new SaveData
            // no need for checking as with cloudSaveData
            string path = Application.persistentDataPath + "/" + SAVE_NAME;
            if (File.Exists(path))
            {
                Debug.Log("[GPGS BUG] OnSavedGameDataRead: local file exists.");
                SaveData localSaveData = DeserialiseSaveData(path);
                CompareCloudAndLocalSaveData(cloudSaveData, localSaveData);
            }
            else
            {
                isDone = true;
                GameData.Instance.saveData = cloudSaveData;
                SaveLocal(cloudSaveData);
                Debug.Log("[GPGS BUG] OnSavedGameDataRead: GameData.Instance.saveData set to cloud data.");
            }
        }
    }

    /// <summary>
    /// Callback for writing saved game data.
    /// </summary>
    /// <param name="status"></param>
    /// <param name="game"></param>
    private void OnSavedGameDataWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        Debug.Log("[GPGS BUG] OnSavedGameDataWritten entered.");
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

    /// <summary>
    /// Ends the initial Play Games loading.
    /// </summary>
    private void EndLoading()
    {
        if (isDone && isLoading)
        {
            sceneTransition.LoadSceneByName("MainMenu");
            isLoading = false;
        }
    }
}
