using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AIPatternData", menuName = "Boomapult/AI Pattern Data")]
public class AIPatternData : ScriptableObject
{
    public List<StructurePattern> easyPatterns;
    public List<StructurePattern> mediumPatterns;
    public List<StructurePattern> hardPatterns;
}