using System;
using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    /// <summary>
    /// 0 = 1v1
    /// 1 = Vs AI - Duke
    /// 2 = Vs AI - Princess
    /// </summary>
    [NonSerialized]
    public int gameStartState = 0;
}
