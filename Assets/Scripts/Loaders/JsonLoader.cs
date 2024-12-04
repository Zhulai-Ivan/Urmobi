using System;
using System.IO;
using Data;
using UnityEngine;

namespace Loaders
{
    public class JsonLoader: IDataLoader
    {
        public T Load<T>(string levelName)
        {
            if(!levelName.Contains("Levels/"))
                levelName = Path.Combine(Const.LevelsFolderPath, levelName);
            
            if (!File.Exists(levelName))
            {
                throw new FileNotFoundException("File not found: " + levelName);
            }
            
            string json = File.ReadAllText(levelName);

            try
            {
                return JsonUtility.FromJson<T>(json);
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to load data {levelName}: {e.Message}");
            }
        }
    }
}