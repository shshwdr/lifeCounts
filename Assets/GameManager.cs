using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LinkType { spring, distance}
public class GameManager : Singleton<GameManager>
{
    public bool hasGottenHome;

    static public void  popup(Transform target, bool shouldUpdate = false)
    {

        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(target.DOScale(Vector3.one * 1.5f, 0.7f).SetUpdate(shouldUpdate))
          .Append(target.DOScale(Vector3.one * 1f, 0.7f).SetUpdate(shouldUpdate)).SetUpdate(shouldUpdate);
    }
    public LinkType linkType;

    public Dictionary<string, int> savedAnimals = new Dictionary<string, int>();
    //{
    //    {"Wolf",2 },
    //    {"Bird",2 },
    //    {"Boar",2 },
    //    {"Rabbit",2 },
    //    {"Hamming Bird",2 },
    //};

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
   
}
