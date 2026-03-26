using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : PersistentSingleton<GameManager>
{
    /// <summary>
    /// 0 = 1v1
    /// 1 = Vs AI - Duke
    /// 2 = Vs AI - Princess
    /// </summary>
    [NonSerialized]
    public static int gameStartState = 0;

    public static event Action<int> onGameStart;


    /// <summary>
    /// false = duke
    /// true = princess
    /// </summary>
    public static bool gameWinner = false;

    private static bool _isOutOfProyectiles;
    private static bool _isCakeDestroyed;

    public static bool IsCakeDestroyed
    {
        get => _isCakeDestroyed;
        set
        {
            if (_isCakeDestroyed == value) return;

            _isCakeDestroyed = value;

            if (value) 
            {
                gameWinner = false; 
                ShowGameOver();
            }
        }
    }
    public static bool IsOutOfProyectiles
    {
        get => _isOutOfProyectiles;
        set
        {
            if (_isOutOfProyectiles == value) return;

            _isOutOfProyectiles = value;

            if (value)
            {
                gameWinner = true;
                ShowGameOver();
            }
        }
    }

    public static void Setup()
    {
        onGameStart?.Invoke(gameStartState);
    }

    private static void ShowGameOver()
    {
        AudioManager.Instance.StopAllSounds();
        SceneManager.LoadScene("Game Over Scene");
    }

}
