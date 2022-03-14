using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashWall : MonoBehaviour
{
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
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
        if(collision.tag == "Breakable")
        {
            Debug.Log("velocity " + rb.velocity.magnitude);
            if (rb.velocity.magnitude > 5f)
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
