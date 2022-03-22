using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyClouds : MonoBehaviour
{
    public float moveDistance = 5;
    public float moveTime = 30;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOMoveX(transform.position.x + moveDistance, moveTime).SetLoops(-1, LoopType.Yoyo).SetUpdate(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
