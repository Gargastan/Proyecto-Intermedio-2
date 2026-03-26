using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBuilder : MonoBehaviour
{
    public BlockPlacer placer;
    public Transform cake;
    public AIPatternData patternData;

    public float baseDelay = 0.8f;
    public float spacing = 0.5f;

    private AIDifficulty difficulty;
    private Collider2D cakeCollider;
    private Coroutine buildRoutine;

    public void SetDifficulty(AIDifficulty diff)
    {
        difficulty = diff;
    }

    void OnEnable()
    {
        cakeCollider = cake.GetComponent<Collider2D>();
        buildRoutine = StartCoroutine(BuildRoutine());
    }

    void OnDisable()
    {
        if (buildRoutine != null)
        {
            StopCoroutine(buildRoutine);
            buildRoutine = null;
        }
    }

    IEnumerator BuildRoutine()
    {
        yield return new WaitForSeconds(1f);

        float delay = GetDelay();

        StructurePattern pattern = GetRandomPattern();

        if (pattern == null) yield break;

        foreach (var block in pattern.blocks)
        {
            if (!enabled) yield break;

            Vector2 buildPos = GetBuildPosition(block.offset);
            TryPlace(buildPos, block.blockType);

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
            case AIDifficulty.Easy: return baseDelay;
            case AIDifficulty.Medium: return baseDelay;
            case AIDifficulty.Hard: return baseDelay;
        }

        return baseDelay;
    }

    StructurePattern GetRandomPattern()
    {
        List<StructurePattern> list = null;

        switch (difficulty)
        {
            case AIDifficulty.Easy:
                list = patternData.easyPatterns;
                break;

            case AIDifficulty.Medium:
                list = patternData.mediumPatterns;
                break;

            case AIDifficulty.Hard:
                list = patternData.hardPatterns;
                break;
        }

        if (list == null || list.Count == 0)
        {
            Debug.LogWarning("No hay patrones para esta dificultad");
            return null;
        }

        return list[Random.Range(0, list.Count)];
    }

    void TryPlace(Vector2 position, BlockData blockType)
    {
        BuildSystem.Instance.TryPlaceBlock(blockType, position, Quaternion.identity, true);
    }
}