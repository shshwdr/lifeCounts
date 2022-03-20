using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToJump : MonoBehaviour
{
    Rigidbody2D rb;
    CharacterController2D controller2d;
    public bool isAttached = false;

    public float originalJumpForce = 50;
    public float linkedJumpForce = 600f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controller2d = GetComponent<CharacterController2D>();
        controller2d.m_JumpForce = originalJumpForce;
    }

    private void OnMouseDown()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<NPC>().isLinked)
        {

            controller2d.m_JumpForce = linkedJumpForce;
        }
        //if (Input.GetMouseButtonDown(1))
        //{
        //    if (GetComponent<NPC>().isLinked)
        //    {

        //        isAttached = !isAttached;
        //    }

        //}
        if (isAttached)
        {
            GetComponent<CharacterController2D>().Move(0, false, true);
        }
    }

    void updateAttach()
    {
        //if (isAttached)
        //{

        //    rb.bodyType = RigidbodyType2D.Static;
        //}
        //else
        //{
        //    rb.bodyType = RigidbodyType2D.Dynamic;
        //}
    }
}
