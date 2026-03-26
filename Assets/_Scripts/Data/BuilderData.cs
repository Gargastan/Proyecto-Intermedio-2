using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuilderData", menuName = "Boomapult/Builder Data")]
public class BuilderData : ScriptableObject
{
    public List<BlockData> availableBlocks;
}