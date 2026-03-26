using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private Text[] texts;

    private void Awake()
    {
        if (GameManager.gameWinner)
        {
            texts[0].text = "survived!";
            texts[1].text = "The Princess Wins!";
        }
        
    }
    public void GoToMainMenu()
    {
        texts[0].text = "was destroyed!";
        texts[1].text = "The Duke wins!";   
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
