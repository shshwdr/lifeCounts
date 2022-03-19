using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public bool isLinked = false;
    CinemachineTargetGroup targetGroup;
    public string animalType;
    Animator animator;
    Rigidbody2D rb;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        targetGroup = GameObject.Find("targetGroup").GetComponent<CinemachineTargetGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        //test
        if (Input.GetKeyDown(KeyCode.I))
        {

            animator.SetTrigger("unlink");
        }
    }

    public void link()
    {
        isLinked = true;
        bool found = false;
        for(int i = 0;i< targetGroup.m_Targets.Length; i++)
        {
            if(targetGroup.m_Targets[i].target == null)
            {
                targetGroup.m_Targets[i].target = transform;
                found = true;
                break;
            }
        }

        
        if (!found)
        {
            Debug.LogWarning("camera not removed");
        }
        animator.SetTrigger("link");
        rb.freezeRotation = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        StageLevelManager.Instance.linkAnimal(animalType);
        Debug.Log("link");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isLinked)
        {
            return;
        }
        if (StageLevelManager.Instance.isGameFinished)
        {
            return;
        }
        if (collision.GetComponent<CharacterController2D>())
        {
            link();
            if (GameManager.Instance.linkType == LinkType.distance)
            {
                GetComponent<AnchoredJoint2D>().enabled = true;
                GetComponent<AnchoredJoint2D>().connectedBody = collision.GetComponent<Rigidbody2D>();

            }
            else
            {

                GetComponent<AnchoredJoint2D>().enabled = true;
                GetComponent<AnchoredJoint2D>().connectedBody = collision.GetComponent<Rigidbody2D>();
            }

            if (GetComponent<FixedJoint2D>())
            {
                var dir = transform.position - collision.transform.position;
                dir = Quaternion.Euler(0, 0, -collision.transform.rotation.eulerAngles.z) * dir;
                GetComponent<FixedJoint2D>().connectedAnchor = dir;
            }

            if (GetComponent<PixelCrushers.DialogueSystem.DialogueSystemTrigger>())
            {

                Destroy(GetComponent<PixelCrushers.DialogueSystem.DialogueSystemTrigger>());
            }
            //Destroy(this);
        }
    }
}
