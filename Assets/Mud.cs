using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mud : MonoBehaviour
{

    public Transform center;
    public float dragModifier = 5;
    public float massModifier = 15;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CharacterController2D>())
        {
            collision.GetComponent<CharacterController2D>().UpdateDragModifier(dragModifier);
            collision.GetComponent<CharacterController2D>().UpdateMassModifier(massModifier);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<CharacterController2D>())
        {
            collision.GetComponent<CharacterController2D>().UpdateDragModifier(1);
            collision.GetComponent<CharacterController2D>().UpdateMassModifier(1);
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
