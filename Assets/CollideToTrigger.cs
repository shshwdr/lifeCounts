using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideToTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {

            foreach (Trigger trigger in GetComponents<Trigger>())
            {
                trigger.trigger();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnTriggerEnter2D(collision.collider);
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
