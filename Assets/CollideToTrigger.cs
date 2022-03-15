using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideToTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach(Trigger trigger in GetComponents<Trigger>())
        {
            trigger.trigger();
        }
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
