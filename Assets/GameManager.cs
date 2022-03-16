using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LinkType { spring, distance}
public class GameManager : Singleton<GameManager>
{

    public LinkType linkType;

    public Dictionary<string, int> savedAnimals = new Dictionary<string, int>();

    //protected override void Awake()
    //{
    //    base.Awake();
    //    savedAnimals["normal"] = 2;
    //}
    public void saveAnimalInLevel()
    {
        foreach(NPC npc in GameObject.FindObjectsOfType<NPC>())
        {
            if (npc.isLinked)
            {
                saveAnimal(npc.animalType);
            }
        }
    }

    public void saveAnimal(string type)
    {
        if (!savedAnimals.ContainsKey(type))
        {
            savedAnimals[type] = 0;
        }
        savedAnimals[type] += 1;
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
