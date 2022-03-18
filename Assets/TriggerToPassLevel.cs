using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerToPassLevel : Trigger
{
    public override void trigger()
    {
        base.trigger();


        StageLevelManager.Instance.finishLevel();

        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
