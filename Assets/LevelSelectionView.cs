using Doozy.Engine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionView : MonoBehaviour
{
    public Button nextLevelButton;
    public Button returnButton;
    public Button levelSelectionButton;
    public Text levelText;
    public GameObject panel;
    public void showReward()
    {
        panel.SetActive(true);
        GetComponent<UIView>().Show();
        var levelButtons = GetComponentsInChildren<LevelSelectionCell>(); 
        int i = 0;
        for (; i <= StageLevelManager.Instance.maxUnlockedLevel;i++)
        {
            levelButtons[i].gameObject.SetActive(true);
            levelButtons[i].init(StageLevelManager.Instance.levelInfoList[i]);
        }
        for(;i< levelButtons.Length; i++)
        {
            levelButtons[i].gameObject.SetActive(false);
        }
    }
    public void hideReward()
    {

        GetComponent<UIView>().Hide();
        panel.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        nextLevelButton.onClick.AddListener(delegate { StageLevelManager.Instance.startNextLevel(); });
        returnButton.onClick.AddListener(delegate { hideReward(); });
    }
}
