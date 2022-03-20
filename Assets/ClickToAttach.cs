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
        StartCoroutine(updateAttach());
    }

    private void OnMouseDown()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (GetComponent<NPC>().isLinked)
            {
            }
            
        }
    }

    public void updateState()
    {

        isAttached = !isAttached;
        StartCoroutine(updateAttach());
    }

    IEnumerator updateAttach()
    {
        yield return null;
        if (isAttached)
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var mouseDir = mousePos - gameObject.transform.position;
            mouseDir.z = 0.0f;
            mouseDir = mouseDir.normalized;
            rb.AddForce(mouseDir * 100f);
            yield return new WaitForSeconds(0.2f);
            rb.bodyType = RigidbodyType2D.Static;
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
