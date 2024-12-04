using System.Collections.Generic;
using System.IO;
using System.Linq;
using Data;
using ModestTree;
using UnityEditor;
using UnityEngine;
using View;

namespace Editor
{
    public class LevelEditorWindow : EditorWindow
    {
        private string _saveFile = "Level";
        private string _outputPath = "Assets/Levels";
        private int _targetLayer = 0;

        private List<GameObject> _objects = new List<GameObject>();

        [MenuItem("Tools/Level Editor")]
        public static void ShowWindow()
        {
            var window = GetWindow<LevelEditorWindow>("Level Editor");
        }

        private void OnGUI()
        {
            GUILayout.Label("Level Export Tool", EditorStyles.boldLabel);

            // Output path
            GUILayout.Label("Output path: ");
            _outputPath = EditorGUILayout.TextField(_outputPath);

            // File name
            GUILayout.Label("File name: ");
            _saveFile = EditorGUILayout.TextField(_saveFile);

            // Layer
            GUILayout.Label("Target Layer: ");
            _targetLayer = EditorGUILayout.LayerField(_targetLayer);

            // Button to collect objects by layer
            if (GUILayout.Button("Collect objects"))
            {
                CollectObjects();
            }

            if (GUILayout.Button("Export objects"))
            {
                Export();
            }

            GUILayout.Label("Import", EditorStyles.boldLabel);
            if (GUILayout.Button("Import objects"))
            {
                Import();
            }
        }

        private void CollectObjects()
        {
            _objects.Clear();
            var allObjects = FindObjectsOfType<GameObject>();

            foreach (var gameObject in allObjects)
            {
                if (gameObject.layer == _targetLayer)
                    _objects.Add(gameObject);
            }

            Debug.Log($"Collected {_objects.Count} objects with {LayerMask.LayerToName(_targetLayer)} layer");
        }

        private void Export()
        {
            LevelData levelData = new LevelData();
            levelData.objects = new List<ObjectData>();
            foreach (var gameObject in _objects)
            {
                ObjectData objectData = new ObjectData()
                {
                    id = gameObject.GetComponent<ObjectView>().Id,
                    name = gameObject.name,
                    type = gameObject.GetComponent<ObjectView>().Type,
                    position = gameObject.transform.position,
                    rotation = gameObject.transform.rotation.eulerAngles,
                    scale = gameObject.transform.localScale,
                    layer = LayerMask.LayerToName(_targetLayer),
                    isActive = gameObject.activeSelf,
                    producedObjectId = gameObject.GetComponent<ProducerObjectView>()?.ProducedObjectId
                };

                levelData.objects.Add(objectData);
            }

            string json = JsonUtility.ToJson(levelData, true);

            if (!Directory.Exists(_outputPath))
            {
                Directory.CreateDirectory(_outputPath);
            }

            File.WriteAllText(Path.Combine(_outputPath, _saveFile), json);
            Debug.Log($"Level data exported to {Path.Combine(_outputPath, _saveFile)}");
        }

        private async void Import()
        {
            string path = EditorUtility.OpenFilePanel("Import Level", "Assets/Levels/", "json");

            if (path.IsEmpty())
            {
                Debug.LogError($"Failed to open {path}");
                return;
            }

            string json = File.ReadAllText(path);
            LevelData levelData = JsonUtility.FromJson<LevelData>(json);

            if (levelData.objects == null || levelData.objects.Count == 0)
            {
                Debug.LogError($"No objects found in {path} file");
                return;
            }

            foreach (var levelObject in levelData.objects)
            {
                var prefab = await LoadResourceEditorUtility.LoadAsset<GameObject>(levelObject.type.ToString());
                var objectView = Instantiate(prefab);

                objectView.gameObject.name = levelObject.name;
                objectView.transform.position = levelObject.position;
                objectView.transform.rotation = Quaternion.Euler(levelObject.rotation);
                objectView.transform.localScale = levelObject.scale;

                if (levelObject.producedObjectId != string.Empty)
                {
                    objectView.gameObject.GetComponent<ProducerObjectView>().Init(levelObject.id, levelObject.type,
                        levelObject.producedObjectId);
                }
                else
                {
                    objectView.gameObject.GetComponent<ObjectView>().Init(levelObject.type, levelObject.id);
                }

                objectView.gameObject.SetActive(levelObject.isActive);
                objectView.gameObject.layer = LayerMask.NameToLayer(levelObject.layer);
            }

            Debug.Log($"Imported {levelData.objects.Count} objects");
        }
    }
}