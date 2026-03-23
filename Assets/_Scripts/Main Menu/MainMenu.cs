using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MainMenu : MonoBehaviour
{
    ScreenFade fade;
    [SerializeField] private AudioClip menuMusic,gameMusic;
    private string menuMusicName;

    private void Start()
    {
        AudioManager.isMusicLoop = true;
        AudioManager.Instance.PlayMusic(menuMusic,0.3f);
        menuMusicName = menuMusic.name;

        fade = Object.FindAnyObjectByType<ScreenFade>();
    }

    public void StartGame()
    {
        StartCoroutine(_StartGame());
    }

    public IEnumerator _StartGame()
    {
        fade.FadeIn();
        yield return new WaitForSeconds(fade.fadeTime);
        AudioManager.Instance.StopSound(menuMusicName);
        AudioManager.Instance.PlayMusic(gameMusic, 0.4f);
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
