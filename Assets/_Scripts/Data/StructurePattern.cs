using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StructurePattern", menuName = "Boomapult/Structure Pattern")]
public class StructurePattern : ScriptableObject
{
    public List<PatternBlock> blocks;
}

[System.Serializable]
public class PatternBlock
{
    public Vector2 offset;
    public BlockData blockType;
}