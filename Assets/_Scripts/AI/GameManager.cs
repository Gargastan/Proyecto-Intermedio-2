using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AIController ai;

    public void PlayerIsConstructor()
    {
        ai.role = AIRole.Artillero;
    }

    public void PlayerIsArtillery()
    {
        ai.role = AIRole.Constructor;
    }
}