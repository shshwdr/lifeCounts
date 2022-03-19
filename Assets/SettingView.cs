using Doozy.Engine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingView : MonoBehaviour
{

    public Button nextLevelButton;
    public Button returnButton;
    public Button levelSelectionButton;
    public Button resumeButton;
    public Text levelText;
    public GameObject panel;
    public void showReward()
    {
        Time.timeScale = 0;
        GetComponent<UIView>().Show();
        panel.SetActive(true);
        if (StageLevelManager.Instance.getMainTargetFinish())
        {

            StageLevelManager.Instance.unlockNextLevel();
        }
        //StageLevelManager.Instance.addLevel();
        if (StageLevelManager.Instance.hasNextLevel())
        {
            nextLevelButton.gameObject.SetActive(true);
        }
        else
        {
            nextLevelButton.gameObject.SetActive(false);

        }
        levelText.text = $"LEVEL {StageLevelManager.Instance.currentLevel.displayName}";
        GameManager.Instance.saveAnimalInLevel();

    }
    public void hideReward()
    {
        Time.timeScale = 1;

        GetComponent<UIView>().Hide();
        panel.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        nextLevelButton.onClick.AddListener(delegate {
            StageLevelManager.Instance.startNextLevel();
        });
        resumeButton.onClick.AddListener(delegate {
            hideReward();
        });
        returnButton.onClick.AddListener(delegate { StageLevelManager.Instance.returnHome(); });
        levelSelectionButton.onClick.AddListener(delegate { StageLevelManager.Instance.selectLevel(); });
    }
}
