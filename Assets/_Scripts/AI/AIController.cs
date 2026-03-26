using System;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public AIRole role;
    public AIDifficulty difficulty;

    public AIBuilder builderAI;
    public AIArtillery artilleryAI;
        
    void ActivateRole()
    {
        if (role == AIRole.Constructor)
        {
            builderAI.enabled = true;
            artilleryAI.enabled = false;

            builderAI.SetDifficulty(difficulty);
        }
        else
        {
            builderAI.enabled = false;
            artilleryAI.enabled = true;

            artilleryAI.SetDifficulty(difficulty);
        }
    }

    public void SetRole(AIRole newRole)
    {
        role = newRole;
        ActivateRole();
    }
}