using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.WSA;

public class DataPersistenceManager : MonoBehaviour
{
    [SerializeField] private string fileName;
    
    private GameData gameData;
    
    private List<IDataPersistence> dataPersistenceList;

    public static DataPersistenceManager instance { get; private set; }

    private FileDataHandler dataHandler;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("many persistence manager");
        }
        instance = this;
    }

    public void Start()
    {
        this.dataHandler = new FileDataHandler(UnityEngine.Application.persistentDataPath, fileName);
        this.dataPersistenceList = FindAllDataPersistenceObjects();
        LoadGame();
    }    

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        this.gameData = dataHandler.Load();

        if (this.gameData == null)
        {
            Debug.Log("no data, going to defaults");
            NewGame();
        }

        foreach (IDataPersistence dataPersistence in dataPersistenceList)
        {
            dataPersistence.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        foreach (IDataPersistence dataPersistence in dataPersistenceList)
        {
            dataPersistence.SaveData(ref gameData);
        }

        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceList = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceList);
    }
}
