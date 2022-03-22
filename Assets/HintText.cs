using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<Text>().text = StageLevelManager.Instance.currentText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
