using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/*
	The class JsonUtility serializes and deserializes JSON data. Because users can edit this data at any time when working with files, it should not be fully trusted. It can be a starting point to create data formats using other tools, but anything read from files should be verified for acceptable ranges of values.
	
	used these two:
	    https://blog.logrocket.com/why-should-shouldnt-save-data-unitys-playerprefs/#:~:text=In%20essence%2C%20all%20we%20need,JavaScript%20Object%20Notation%20(JSON)
	    	this one has a problem when loading the data on the first load

	    		FileNotFoundException: Could not find file "/Users/pavankatepalli/Library/Application Support/DefaultCompany/countdown timer/GameData.json"

		https://videlais.com/2021/02/25/using-jsonutility-in-unity-to-save-and-load-game-data/
			this one checks if the file exists

*/

public class DataManager : MonoBehaviour
{
    private Data gameData;
    private static string dataFilePath = Path.Combine(Application.persistentDataPath, "GameData.json");

    public DataManager(int level = 0)
    {
        gameData = new Data();
        gameData.level = level;
    }

    // Here we set our level with some sort of GameManager
    public void SetLevel(int level)
    {
        if (gameData == null)
        {
            gameData = new Data();
        }

        gameData.level = level;
    }

    // The method to return the loaded game data when needed
    public Data GetGameData()
    {
        return gameData;
    }

    public void Save()
    {
        // This creates a new StreamWriter to write to a specific file path
        using (StreamWriter writer = new StreamWriter(dataFilePath))
        {
            // This will convert our Data object into a string of JSON
            string dataToWrite = JsonUtility.ToJson(gameData);

            // This is where we actually write to the file
            writer.Write(dataToWrite);
        }
    }

    public void Load()
    {
    	Debug.Log("line 63 Debug.Log(dataFilePath)");
    	Debug.Log(dataFilePath);
    	Debug.Log("line 63 log dataFilePath");

    	// Does the file exist?
    	if (File.Exists(dataFilePath)){
    		Debug.Log("line 70 if (File.Exists(dataFilePath)){");

    		// This creates a StreamReader, which allows us to read the data from the specified file path
    		using (StreamReader reader = new StreamReader(dataFilePath))
    		{
    		    // We read in the file as a string
    		    string dataToLoad = reader.ReadToEnd();

    		    // Here we convert the JSON formatted string into an actual Object in memory
    		    gameData = JsonUtility.FromJson<Data>(dataToLoad);
    		}
    	}else{
    		Debug.Log("line 81 }else{");
    	}
    }

    [System.Serializable]
    public class Data
    {
        // The actual data we want to save goes here, for this example we'll only use an integer to represent the level
        public int level = 0;
    }
}