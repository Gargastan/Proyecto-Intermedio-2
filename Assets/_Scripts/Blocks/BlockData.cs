using UnityEngine;

[CreateAssetMenu(fileName = "BlockData", menuName = "Boomapult/Block Data")]
public class BlockData : ScriptableObject
{
    public string blockName;
    public Sprite icon;

    [Header("Stats")]
    public int cost;
    public float resistance;
    public float weight;

    [Header("Placement Rules")]
    public bool canPlaceOnTop = true;

    [Header("Visual")]
    public GameObject prefab;
}