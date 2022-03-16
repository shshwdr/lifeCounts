using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToAttach : MonoBehaviour
{
    Rigidbody2D rb;
    public bool isAttached = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        updateAttach();
    }

    private void OnMouseDown()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (GetComponent<NPC>().isLinked)
            {
                isAttached = !isAttached;
                updateAttach();
            }
            
        }
    }

    void updateAttach()
    {
        if (isAttached)
        {

            rb.bodyType = RigidbodyType2D.Static;
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
