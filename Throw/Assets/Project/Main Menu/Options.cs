using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class Options
{
    public static string path = Application.dataPath + Path.DirectorySeparatorChar;

    public static void SaveGameOptions(GameOptions gameOptions, string gameOptionsFileName)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = new FileStream(path + gameOptionsFileName + ".dat", FileMode.Create);
        
        bf.Serialize(file, gameOptions);
        file.Close();
    }

    public static GameOptions LoadGameOptions(string gameOptionsFileName)
    {
        if (File.Exists(path + gameOptionsFileName + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path + gameOptionsFileName + ".dat", FileMode.Open);

            GameOptions savedGameOptions = (GameOptions)bf.Deserialize(file);
            file.Close();

            return savedGameOptions;
        }

        Debug.LogError("File DNE, cannot load");
        return null;
    }

    public static void GameOptionsCheck(string gameOptionsFileName, GameOptions defaultGameOptions)
    {
        if (!File.Exists(path + gameOptionsFileName + ".dat"))
        {
            SaveGameOptions(defaultGameOptions, gameOptionsFileName);
        }
    }

    public static void SaveGUI(int sizeIndex, string gameOptionsFileName)
    {
        float[] UISizes = MainMenu.Instance.UIScales;
        GameOptions gameOptions = LoadGameOptions(gameOptionsFileName);
        gameOptions.UIScale = UISizes[sizeIndex];
        SaveGameOptions(gameOptions, gameOptionsFileName);
    }

    [Serializable]
    public class GameOptions
    {
        public float UIScale;
        public string PlayerName;

        public GameOptions(float UISize, string PlayerName)
        {
            this.UIScale = UISize;
            this.PlayerName = PlayerName;
        }
    }
}
