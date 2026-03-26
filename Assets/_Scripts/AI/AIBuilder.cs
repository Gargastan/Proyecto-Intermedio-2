using System.Collections;
using UnityEngine;

public class AIBuilder : MonoBehaviour
{
    public BlockPlacer placer;
    public Transform cake;

    private AIDifficulty difficulty;
    private Collider2D cakeCollider;

    public float baseDelay = 0.4f;
    public float spacing = 0.5f;

    public void SetDifficulty(AIDifficulty diff)
    {
        difficulty = diff;
    }

    void OnEnable()
    {
        cakeCollider = cake.GetComponent<Collider2D>();
        StartCoroutine(BuildRoutine());
    }

    IEnumerator BuildRoutine()
    {
        yield return new WaitForSeconds(1f);

        float delay = GetDelay();

        Vector2[] pattern = GeneratePattern();

        foreach (Vector2 offset in pattern)
        {
            Vector2 buildPos = GetBuildPosition(offset);

            TryPlace(buildPos);

            yield return new WaitForSeconds(delay);
        }
    }

    Vector2 GetBuildPosition(Vector2 offset)
    {
        Vector2 basePos = cake.position;

        if (cakeCollider == null)
            return basePos + offset;

        Bounds bounds = cakeCollider.bounds;

        float x = basePos.x;
        float y = basePos.y;

        if (offset.x > 0)
        {
            x += bounds.extents.x + spacing + offset.x;
        }
        else if (offset.x < 0)
        {
            x -= bounds.extents.x + spacing + Mathf.Abs(offset.x);
        }

        if (offset.y > 0)
        {
            y += bounds.extents.y + spacing + offset.y;
        }
        else
        {
            y += offset.y;
        }

        return new Vector2(x, y);
    }

    float GetDelay()
    {
        switch (difficulty)
        {
            case AIDifficulty.Easy: return baseDelay * 2f;
            case AIDifficulty.Medium: return baseDelay;
            case AIDifficulty.Hard: return baseDelay * 0.5f;
        }

        return baseDelay;
    }

    Vector2[] GeneratePattern()
    {
        if (difficulty == AIDifficulty.Easy)
        {
            return GenerateVerticalWall(-1, 7);
        }

        if (difficulty == AIDifficulty.Medium)
        {
            return GenerateDoubleWall(7);
        }

        return GenerateFullStructure(7);
    }

    Vector2[] GenerateDoubleWall(int height)
    {
        Vector2[] pattern = new Vector2[height * 2];

        for (int i = 0; i < height; i++)
        {
            // Left
            pattern[i] = new Vector2(-1, i);

            // Right
            pattern[i + height] = new Vector2(1, i);
        }

        return pattern;
    }

    Vector2[] GenerateFullStructure(int height)
    {
        System.Collections.Generic.List<Vector2> pattern = new System.Collections.Generic.List<Vector2>();

        for (int i = 0; i < height; i++)
        {
            pattern.Add(new Vector2(-1, i));
            pattern.Add(new Vector2(1, i));
        }

        for (int x = -1; x <= 1; x++)
        {
            pattern.Add(new Vector2(x, height));
        }

        return pattern.ToArray();
    }

    Vector2[] GenerateVerticalWall(int xOffset, int height)
    {
        Vector2[] pattern = new Vector2[height];

        for (int i = 0; i < height; i++)
        {
            pattern[i] = new Vector2(xOffset, i);
        }

        return pattern;
    }

    void TryPlace(Vector2 position)
    {
        if (placer.currentBlock == null) return;

        int cost = placer.currentBlock.cost;

        if (!CurrencyManager.Instance.CanAfford(cost))
            return;

        GameObject placed = Instantiate(placer.currentBlock.prefab, position, Quaternion.identity);

        SpriteRenderer sr = placed.GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.color = Color.white;

        placed.layer = LayerMask.NameToLayer("Placement");

        CurrencyManager.Instance.Spend(cost);
    }
}