using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Rigidbody2D>())
        {
            GetComponent<SpringJoint2D>().enabled = true;
            GetComponent<SpringJoint2D>().connectedBody = collision.GetComponent<Rigidbody2D>();
            Destroy(this);
        }
    }
}
