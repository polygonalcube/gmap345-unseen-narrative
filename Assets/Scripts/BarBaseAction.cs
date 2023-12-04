using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarBaseAction : Bar
{
    //Cooldown UI for dash & reflect
    public override void Update()
    {
        if (playerScript != null)
            actionCheck = playerScript.canDash;
        base.Update();
    }
}
