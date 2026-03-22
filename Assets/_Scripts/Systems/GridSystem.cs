using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public float gridSize = 1f;

    public Vector2 GetSnappedPosition(Vector2 position)
    {
        float x = Mathf.Round(position.x / gridSize) * gridSize;
        float y = Mathf.Round(position.y / gridSize) * gridSize;

        return new Vector2(x, y);
    }
}