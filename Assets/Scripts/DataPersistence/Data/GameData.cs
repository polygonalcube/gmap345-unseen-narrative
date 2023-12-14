using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData
{
    public int deathCount;
    public List<string> totemsHeld;

    public GameData()
    {
        this.deathCount = 0;
        this.totemsHeld = new List<string>();
    }
}
