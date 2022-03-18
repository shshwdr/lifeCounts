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
}
public class StageLevelManager : Singleton<StageLevelManager>
{
    public int currentLevelId;
    public int maxUnlockedLevel = 0;
    int rescuedCount = 0;
    int rescuedMainCount = 0;
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
    
    public void addLevel()
    {
        currentLevelId++;
        maxUnlockedLevel = Mathf.Max(currentLevelId, maxUnlockedLevel);
    }

    public void returnHome()
    {

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
        rescuedCount = 0;
        rescuedMainCount = 0;
        MusicManager.Instance.playLevelMusic();
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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            restart();
        }
    }
}
