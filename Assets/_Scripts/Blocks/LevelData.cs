using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelData", menuName = "Boomapult/Level Data")]
public class LevelData : ScriptableObject
{
    public string levelName;
    public List<BlockData> availableBlocks;
}