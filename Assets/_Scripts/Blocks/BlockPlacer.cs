using UnityEngine;

public class BlockPlacer : MonoBehaviour
{
    [Header("References")]
    public Camera cam;
    public GridSystem grid;

    public BlockData currentBlock;
    public BuildTimer buildTimer;

    [Header("Placement Settings")]
    public LayerMask placementLayer;
    public Vector2 checkSize = new Vector2(0.9f, 0.1f);
    public float checkDistance = 0.6f;

    #region Preview
    private GameObject previewObject;
    private float previewRotation = 0f;
    #endregion

    void Update()
    {
        HandleRotation();
        HandlePreview();
        HandlePlacement();
    }

    void HandleRotation()
    {
        // Rotar preview 90° con clic derecho
        if (Input.GetMouseButtonDown(1) && previewObject != null)
        {
            previewRotation += 90f;
            if (previewRotation >= 360f) previewRotation = 0f;
            previewObject.transform.rotation = Quaternion.Euler(0, 0, previewRotation);
        }
    }

    void HandlePreview()
    {
        if (currentBlock == null) return;

        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mouseWorldPos.x, mouseWorldPos.y);
        Vector2 snappedPos = grid.GetSnappedPosition(mousePos2D);

        if (previewObject == null)
        {
            previewObject = Instantiate(currentBlock.prefab);
            Destroy(previewObject.GetComponent<Rigidbody2D>());
            Destroy(previewObject.GetComponent<Collider2D>());
            Collider2D[] c2 = previewObject.GetComponentsInChildren<Collider2D>();
            for (int i = 0; i < c2.Length; i++) Destroy(c2[i]);
            SetPreviewTransparency(previewObject, 0.5f);
        }

        SpriteRenderer sr = previewObject.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            float spriteBottomOffset = sr.bounds.extents.y;
            previewObject.transform.position = new Vector3(snappedPos.x, snappedPos.y + spriteBottomOffset, 0);
        }
        else
        {
            previewObject.transform.position = new Vector3(snappedPos.x, snappedPos.y, 0);
        }

        previewObject.transform.rotation = Quaternion.Euler(0, 0, previewRotation);
    }

    void HandlePlacement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!buildTimer.canBuild)
            {
                Debug.Log("Ya no puedes construir");
                return;
            }

            if (previewObject == null || currentBlock == null) return;

            Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mouseWorldPos.x, mouseWorldPos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Placement"))
                {
                    Debug.Log("No se puede colocar aquí, clic sobre objeto inválido");
                    return;
                }
            }

            Vector3 pos = previewObject.transform.position;

            int cost = currentBlock.cost;

            if (CurrencyManager.Instance.CanAfford(cost))
            {
                SetPreviewTransparency(previewObject, 1f);
                GameObject placed = Instantiate(currentBlock.prefab, pos, previewObject.transform.rotation);
                SpriteRenderer sr = placed.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.color = Color.white;
                }
                placed.layer = LayerMask.NameToLayer("Placement");
                CurrencyManager.Instance.Spend(cost);
            }
            else
            {
                Debug.Log("No hay dinero");
            }
        }
    }

    bool CanPlaceBlock(Vector2 position)
    {
        Vector2 checkPos = position + Vector2.down * checkDistance;
        Collider2D hit = Physics2D.OverlapBox(checkPos, checkSize, 0f, placementLayer);
        return hit != null;
    }

    void SetPreviewTransparency(GameObject obj, float alpha)
    {
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            Color c = sr.color;
            c.a = alpha;
            sr.color = c;
        }
    }

    public void SetCurrentBlock(BlockData newBlock)
    {
        currentBlock = newBlock;
        if (previewObject != null)
        {
            Destroy(previewObject);
        }
        previewRotation = 0f;
    }
}