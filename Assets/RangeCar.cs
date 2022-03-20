using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCar : MonoBehaviour
{
    public float rangeShowDistance = 15;
    public float moveTime = 3;
    private void Awake()
    {
        StartCoroutine(moveCar());
    }

    IEnumerator moveCar()
    {
        var player = GameObject.FindObjectOfType<PlayerController>();
        transform.position = player.transform.position - new Vector3(rangeShowDistance, 0, 0);
        Debug.Log($"move car {player.transform.position} {rangeShowDistance}, {Time.timeScale}");
        transform.DOMoveX(player.transform.position.x+ rangeShowDistance, moveTime).SetEase(Ease.Linear);
        yield return new WaitForSecondsRealtime(moveTime);
        if (StageLevelManager.Instance.isInHome)
        {
            StageLevelManager.Instance.startNextLevel();
        }
        else
        {

            StageLevelManager.Instance.finishLevelReal();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            var player = collision.GetComponent<PlayerController>();
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            player.GetComponent<FixedJoint2D>().enabled = true;
            player.GetComponent<FixedJoint2D>().connectedBody = GetComponent<Rigidbody2D>();
            foreach (var col in player.GetComponents<Collider2D>())
            {

                col.isTrigger = true;
            }
            foreach (var collider in GameObject.FindObjectsOfType<NPC>())
            {
                if (collider.isLinked)
                {
                    foreach (var col in collider.GetComponents<Collider2D>())
                    {

                        col.isTrigger = true;
                    }
                }
            }
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
