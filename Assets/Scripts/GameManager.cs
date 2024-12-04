using System;
using Data;
using Loaders;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    private LevelLoader _levelLoader;

    [Inject]
    private void InstallBindings(LevelLoader levelLoader)
    {
        _levelLoader = levelLoader;
    }

    private void Awake()
    {
        var data = _levelLoader.Initialize(Const.Level1Path);
    }
}