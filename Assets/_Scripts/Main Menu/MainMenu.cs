using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MainMenu : MonoBehaviour
{
    ScreenFade fade;
    [SerializeField] private AudioClip menuMusic;
    private string menuMusicName;

    private void Start()
    {
        GameManager.IsCakeDestroyed = false;

        AudioManager.isMusicLoop = true;
        AudioManager.Instance.PlayMusic(menuMusic,0.3f);
        menuMusicName = menuMusic.name;

        fade = Object.FindAnyObjectByType<ScreenFade>();
    }

    public void StartGame(int gameStartState)
    {
        StartCoroutine(_StartGame(gameStartState));
    }

    public IEnumerator _StartGame(int gameStartState)
    {
        fade.FadeIn();
        yield return new WaitForSeconds(fade.fadeTime);
        AudioManager.Instance.StopSound(menuMusicName);
        GameManager.gameStartState = gameStartState;
        SceneManager.LoadScene("Game Scene");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }


}
