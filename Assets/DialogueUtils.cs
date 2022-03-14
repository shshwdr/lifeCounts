using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueUtils : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void startDialogue()
    {
        Time.timeScale = 0;
    }
    public void stopDialogue()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
