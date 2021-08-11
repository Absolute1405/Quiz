using UnityEngine;
using UnityEngine.Events;

public class GameFinisher : MonoBehaviour
{
    public UnityEvent GameFinished;
    public UnityEvent GameRestarted;

    public void FinishGame()
    {
        GameFinished?.Invoke();
    }

    public void RestartGame()
    {
        GameRestarted?.Invoke();
    }
}
