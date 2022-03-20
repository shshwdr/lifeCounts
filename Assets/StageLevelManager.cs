using Cinemachine;
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

    bool isFinished;

    bool isHome;


    AudioSource audioSource;
    public bool isGameFinished { get { return isFinished; } }
    public bool isInHome { get { return isHome; } }

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
    public LevelInfo currentLevel { get { if(currentLevelId>=0) return levelInfoList[currentLevelId]; return null; } }
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
        isHome = true;
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
            var expectDiff = (diff+1) / 2;

            Debug.Log($"stars diff {diff}, expect {expectDiff}, compare {currentLevel.targetCount - rescuedCount}");
            if(currentLevel.targetCount - rescuedCount<= expectDiff)
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
        //first disable player move and create car

        var player = GameObject.FindObjectOfType<PlayerController>();
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        Instantiate(Resources.Load<GameObject>("rangerCar"));
        isFinished = true;
        foreach(var camera in GameObject.FindObjectsOfType<CinemachineVirtualCamera>())
        {
            camera.Follow = null;
        }
        //when car finished moving start real level finish
    }

    public void finishLevelReal()
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
        if (isGameFinished)
        {
            return;
        }
        rescuedCount++;
        if(type == currentLevel.mainTarget || currentLevel.mainTarget == "Animal")
        {
            rescuedMainCount++;
        }
        EventPool.Trigger("linkAnimal");
    }

    public void startLevel(int id)
    {
        currentLevelId = id;
        //startNextLevel();
        finishLevel();
    }

    public void startNextLevel()
    {
        isHome = false;
        isFinished = false;
        Time.timeScale = 1;
        rescuedCount = 0;
        rescuedMainCount = 0;
        if (currentLevel == null)
        {
            isHome = true;
            return;
        }
        if (currentLevel.isTimeLevel)
        {

            MusicManager.Instance.playUrgentMusic();
        }
        else
        {
            if (currentLevel.id < 6)
            {

                MusicManager.Instance.playLevelMusic();
            }
            else
            {
                MusicManager.Instance.playLevelMusic2();
            }
        }
        countDownTime = currentLevel.time;
        countDownTimer = 0;
        SceneManager.LoadScene(currentLevel.sceneName);
    }
    protected  void Awake()
    {
        //base.Awake();
        //audioSource = GetComponent<AudioSource>();
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

        if (Input.GetKeyDown(KeyCode.P))
        {
            currentLevelId++;
            restart();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            currentLevelId--;
            restart();
        }

        if (countDownTime > 0 && !isFinished)
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
