using UnityEngine;

public class AIController : MonoBehaviour
{
    public AIRole role;
    public AIDifficulty difficulty;

    public AIBuilder builderAI;
    public AIArtillery artilleryAI;

    void Start()
    {
        ActivateRole();
    }

    void ActivateRole()
    {
        if (role == AIRole.Constructor)
        {
            builderAI.enabled = true;
            artilleryAI.enabled = false;
        }
        else
        {
            builderAI.enabled = false;
            artilleryAI.enabled = true;
        }

        builderAI.SetDifficulty(difficulty);
        artilleryAI.SetDifficulty(difficulty);
    }
}