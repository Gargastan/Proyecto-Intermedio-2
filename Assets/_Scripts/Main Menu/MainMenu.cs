using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MainMenu : MonoBehaviour
{
    ScreenFade fade;
    [SerializeField] private AudioClip menuMusic;

    private void Start()
    {
        AudioManager.isMusicLoop = true;
        AudioManager.Instance.PlayMusic(menuMusic);

        fade = Object.FindAnyObjectByType<ScreenFade>();
    }//Busca el efecto de fade.

    public void StartGame()
    {
        StartCoroutine(_StartGame());
    }//Lamada al inicio del juego.

    public IEnumerator _StartGame()
    {
        AudioManager.Instance.StopSound("Menu Music");
        AudioManager.isMusicLoop = false;
        fade.FadeIn();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Game Scene");
    }//Inicia el juego con efectos de fade.

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }//Te devuelve al menú principal.

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }//Cierra el juego.

    private void Update()
    {
        if (!AudioManager.Instance._musicSource.isPlaying && AudioManager.isMusicLoop)
        {
            AudioManager.Instance.PlayMusic(menuMusic);
        }
    }
}
