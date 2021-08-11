using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class GameStarter : MonoBehaviour
{
    public UnityEvent GameInitializing;
    public UnityEvent GameStarted;

    private void Awake()
    {
        DOTween.Init();
        Initialize();
    }

    public void Initialize()
    {
        GameInitializing?.Invoke();
    }

    public void StartGame()
    {
        GameStarted?.Invoke();
    }
}
