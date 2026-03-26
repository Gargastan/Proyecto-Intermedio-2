using UnityEngine;

public class AIArtillery : MonoBehaviour
{
    private AIDifficulty difficulty;

    public void SetDifficulty(AIDifficulty diff)
    {
        difficulty = diff;
    }
}