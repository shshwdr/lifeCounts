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
    public Transform[] stars;
    public Text description;
    public Text title;

    public void showReward()
    {
        panel.SetActive(true);
        //StageLevelManager.Instance.addLevel();
        if (StageLevelManager.Instance.hasNextLevel())
        {
            nextLevelButton.gameObject.SetActive(true);
        }
        else
        {
            nextLevelButton.gameObject.SetActive(false);

        }
        levelText.text = $"Level {StageLevelManager.Instance.currentLevel.displayName}";
        GameManager.Instance.saveAnimalInLevel();
        
        if (StageLevelManager.Instance.getTargetFinish())
        {

            title.text = "Exellent Work";
            description.text = "Every life got saved!";
        }
        else if (StageLevelManager.Instance.getMainTargetFinish())
        {
            title.text = "Great Work";
            description.text = "You saved some lifes but missed some. Try to save them next time!";

        }
        else
        {
            title.text = "Try Again";
            description.text = "Lifes are waiting for you to be saved!";
        }

        int starCount = StageLevelManager.Instance.starCount();
        for(int i = 0; i < starCount; i++)
        {
            stars[i].localScale = new Vector3(1, 1, 1);
            //stars[i].DoScale(new Vector3(1, 1, 1),1);
            // stars[i].transform
        }
    }
    public void hideReward()
    {

        panel.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        nextLevelButton.onClick.AddListener(delegate { StageLevelManager.Instance.addLevel(); StageLevelManager.Instance.startNextLevel(); });
        returnButton.onClick.AddListener(delegate { StageLevelManager.Instance.returnHome(); });
        levelSelectionButton.onClick.AddListener(delegate { StageLevelManager.Instance.selectLevel(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
