using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeMenu : MonoBehaviour
{
    public Button startButton;
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(delegate
        {

            LevelSelectionView view = GameObject.FindObjectOfType<LevelSelectionView>(true);
            view.showReward();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
