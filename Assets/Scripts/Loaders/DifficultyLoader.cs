using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DifficultyLoader : MonoBehaviour
{
    [SerializeField]
    private LevelLoader _levelLoader;

    [SerializeField]
    private List<DifficultySettings> _difficulties;

    public UnityEvent GameEnded;

    private int _currentDifficulty = 0;

    public void Init()
    {
        if (_levelLoader is null)
            throw new ArgumentNullException(nameof(_levelLoader));
        if (_difficulties.Count < 1)
            throw new ArgumentNullException(nameof(_levelLoader));

        _levelLoader.Init(_difficulties[_currentDifficulty]);
    }

    public void IncreaseDifficulty()
    {
        _currentDifficulty++;

        if (_currentDifficulty == _difficulties.Count)
        {
            _currentDifficulty = 0;
            EndGame();
            return;
        }

        _levelLoader.Init(_difficulties[_currentDifficulty]);
    }

    public void EndGame()
    {
        GameEnded?.Invoke();
    }
}
