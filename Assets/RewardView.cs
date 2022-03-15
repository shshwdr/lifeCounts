using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardView : MonoBehaviour
{

    public Button nextLevelButton;
    public Button returnButton;
    public Button levelSelectionButton;
    public Text levelText;
    public GameObject panel;

    public void showReward()
    {
        panel.SetActive(true);
        if (StageLevelManager.Instance.hasNextLevel())
        {
            nextLevelButton.gameObject.SetActive(true);
        }
        else
        {
            nextLevelButton.gameObject.SetActive(false);

        }
        levelText.text = $"You Win Level {StageLevelManager.Instance.currentLevel.displayName}";
    }
    public void hideReward()
    {

        panel.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        nextLevelButton.onClick.AddListener(delegate { StageLevelManager.Instance.startNextLevel(); } );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
