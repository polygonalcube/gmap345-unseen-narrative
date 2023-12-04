using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarTotemPower : Bar
{
    //Cooldown UI for totem powers
    public override void Update()
    {
        if (playerScript != null)
            actionCheck = playerScript.totemActive;
        base.Update();
    }
}
