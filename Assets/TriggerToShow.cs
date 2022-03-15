using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerToShow : Trigger
{
    public GameObject[] enables;
    public GameObject[] disables;

    public bool shouldSetupDisables = true;
    public bool shouldSetupEnables = true;

    public override void trigger()
    {
        base.trigger();
        {

            foreach (var enable in enables)
            {
                enable.SetActive(true);
            }
        }
        {
            foreach (var enable in disables)
            {
                enable.SetActive(false);
            }
        }
    }
    protected void Start()
    {
        if (shouldSetupEnables)
            foreach (var enable in enables)
            {
                enable.SetActive(false);
            }

        if (shouldSetupDisables)
            foreach (var enable in disables)
            {
                enable.SetActive(true);
            }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
