using System.Data;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public bool isAIConstructor = true;

    public AIController ai;

    [Header("UI")]
    public GameObject buildUI;

    [Header("Player Systems")]
    public BlockPlacer playerPlacer;

    void Start()
    {
        if (isAIConstructor)
        {
            PlayerIsArtillery();
        } else
        {
            PlayerIsConstructor();
        }
    }

    public void PlayerIsConstructor()
    {
        ai.SetRole(AIRole.Artillero);

        playerPlacer.isEnabledForPlayer = true;
        buildUI.SetActive(true);
    }

    public void PlayerIsArtillery()
    {
        ai.SetRole(AIRole.Constructor);

        playerPlacer.isEnabledForPlayer = false;
        buildUI.SetActive(false);
    }
}