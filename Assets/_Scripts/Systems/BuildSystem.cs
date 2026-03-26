using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    public static BuildSystem Instance;

    void Awake()
    {
        Instance = this;
    }

    public bool TryPlaceBlock(BlockData block, Vector2 pos, Quaternion rot, bool ignoreCost = false)
    {
        if (block == null) return false;

        if (!ignoreCost)
        {
            if (!CurrencyManager.Instance.CanAfford(block.cost))
                return false;

            CurrencyManager.Instance.Spend(block.cost);
        }

        GameObject placed = Instantiate(block.prefab, pos, rot);

        SpriteRenderer sr = placed.GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.color = Color.white;

        placed.layer = LayerMask.NameToLayer("Placement");

        return true;
    }
}