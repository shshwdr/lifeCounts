using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerToSelectLevel : Trigger
{
    public override void trigger()
    {
        base.trigger();

        LevelSelectionView view = GameObject.FindObjectOfType<LevelSelectionView>(true);
        view.showReward();
    }
}
