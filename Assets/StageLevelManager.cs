using Pool;
using Sinbad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelInfo
{
    public string sceneName;
    public string displayName;
    public string description;
    public int isDeprecated;
    public int id;
    public string mainTarget;
    public int mainTargetCount;
    public int targetCount;
    public float time;
    public bool isTimeLevel { get { return time > 0; } }
}
public class StageLevelManager : Singleton<StageLevelManager>
{
    public int currentLevelId;
    public int maxUnlockedLevel = 0;
    int rescuedCount = 0;
    int rescuedMainCount = 0;

    float countDownTime = 0;
    float countDownTimer = 0;

    Dictionary<string, int> levelToStarCount = new Dictionary<string, int>();

    public int starCountInLevel(string sceneName)
    {
        if (!levelToStarCount.ContainsKey(sceneName))
        {
            Debug.LogWarning("star not generate");
            return 0;
        }
        return levelToStarCount[sceneName];
    }

    public bool shouldShowCountdown()
    {
        return countDownTime > 0;
    }
    public LevelInfo currentLevel { get { return levelInfoList[currentLevelId]; } }
    public Dictionary<string, LevelInfo> levelInfoByName = new Dictionary<string, LevelInfo>();
    public List<LevelInfo> levelInfoList = new List<LevelInfo>();
    public bool hasNextLevel()
    {
        if (currentLevelId + 1 >= levelInfoList.Count)
        {
            return false;
        }
        return true;
    }

    public void restart()
    {
        startNextLevel();
    }
    public void unlockNextLevel()
    {

        maxUnlockedLevel = Mathf.Max(currentLevelId+1, maxUnlockedLevel);
    }
    public void addLevel()
    {
        currentLevelId++;
        maxUnlockedLevel = Mathf.Max(currentLevelId, maxUnlockedLevel);
    }

    public void returnHome()
    {
        Time.timeScale = 1;

        MusicManager.Instance.playHomeMusic();
        SceneManager.LoadScene("home");
    }

    public int getRescuedCount()
    {
        return rescuedCount;
    }

    public int getRescuedMainCount()
    {
        return rescuedMainCount;
    }

    public bool getMainTargetFinish()
    {
        return rescuedMainCount >= currentLevel.mainTargetCount;
    }

    public int starCount()
    {
        int showStarCount = 0;
        if (getTargetFinish())
        {
            showStarCount = 3;
        }else if (getMainTargetFinish())
        {
            var diff = currentLevel.targetCount - currentLevel.mainTargetCount;
            var expectDiff = diff / 2;

            if(currentLevel.targetCount - rescuedCount< expectDiff)
            {
                showStarCount = 2;

            }
            else
            {
                showStarCount = 1;
            }

        }
        if (!levelToStarCount.ContainsKey(currentLevel.sceneName))
        {
            levelToStarCount[currentLevel.sceneName] = 0;
        }
        if(showStarCount> levelToStarCount[currentLevel.sceneName])
        {
            levelToStarCount[currentLevel.sceneName] = showStarCount;
        }
        return showStarCount;
    }

    public void selectLevel()
    {

        LevelSelectionView view = GameObject.FindObjectOfType<LevelSelectionView>(true);
        view.showReward();
    }
    public void finishLevel()
    {
        Time.timeScale = 0;
        RewardView view = GameObject.FindObjectOfType<RewardView>(true);
        view.showReward();
    }
    public bool getTargetFinish()
    {
        return rescuedCount >= currentLevel.targetCount;
    }
    public void linkAnimal(string type)
    {
        rescuedCount++;
        if(type == currentLevel.mainTarget)
        {
            rescuedMainCount++;
        }
        EventPool.Trigger("linkAnimal");
    }

    public void startLevel(int id)
    {
        currentLevelId = id;
        startNextLevel();
    }

    public void startNextLevel()
    {
        Time.timeScale = 1;
        rescuedCount = 0;
        rescuedMainCount = 0;
        if (currentLevel.isTimeLevel)
        {

            MusicManager.Instance.playUrgentMusic();
        }
        else
        {

            MusicManager.Instance.playLevelMusic();
        }
        countDownTime = currentLevel.time;
        countDownTimer = 0;
        SceneManager.LoadScene(currentLevel.sceneName);
    }
    private void Awake()
    {
        levelInfoList = CsvUtil.LoadObjects<LevelInfo>("Level");
        int id = 0;
        foreach (var info in levelInfoList)
        {
            //if (info.isDeprecated == 1)
            //{
            //    continue;
            //}
            info.id = id++;
            levelInfoByName[info.displayName] = info;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        startNextLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            restart();
        }

        if(countDownTime > 0)
        {
            countDownTimer += Time.deltaTime;
            EventPool.Trigger<int>("updateTimer", (int)(countDownTime - countDownTimer));
            if (countDownTimer >= countDownTime)
            {
                finishLevel();
            }
        }
    }
}
