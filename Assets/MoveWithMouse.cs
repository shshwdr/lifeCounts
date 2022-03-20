using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithMouse : MonoBehaviour
{
    public Rigidbody2D rb;
    public int clickForce = 500;
    private Plane plane = new Plane(Vector3.up, Vector3.zero);
    NPC npc;
    public AudioClip flySound;
    AudioSource audioSource;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        npc = GetComponent<NPC>();
        audioSource = GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {
        if (!npc.isLinked)
        {
            return;

        }
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var mouseDir = mousePos - gameObject.transform.position;
        mouseDir.z = 0.0f;
        mouseDir = mouseDir.normalized;

        if (Input.GetMouseButton(0))
        {
            rb.AddForce(mouseDir * clickForce*Time.deltaTime);
        }
        if (Input.GetMouseButtonDown(0))
        {
            audioSource.Play();
        }
        if (Input.GetMouseButtonUp(0))
        {
            audioSource.Stop();
        }
    }
}
