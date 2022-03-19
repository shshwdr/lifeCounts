using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    public int maxAnimalToShow = 10;
    public Transform positionsParent;


    // Start is called before the first frame update
    void Start()
    {
        if (!GameManager.Instance.hasGottenHome)
        {
            GameManager.Instance.hasGottenHome = true;
            PixelCrushers.DialogueSystem.DialogueManager.StartConversation("home");
        }
        int i = 0;
        int tier = 0;
        var positions = positionsParent.GetComponentsInChildren<Transform>();
        while (i < maxAnimalToShow)
        {
            bool found = false;
            foreach(var pair in GameManager.Instance.savedAnimals)
            {
                if (tier < pair.Value)
                {
                    found = true;
                    GameObject ob = Instantiate(Resources.Load<GameObject>("animals/"+pair.Key), positions[i].position,Quaternion.identity);
                    i++;
                    ob.GetComponent<NPC>().isLinked = true;
                }
            }
            tier++;
            if (found == false)
            {
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
