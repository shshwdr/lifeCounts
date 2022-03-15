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
}
public class StageLevelManager : Singleton<StageLevelManager>
{
    public int currentLevelId;
    public int maxUnlockedLevel = 0;
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

    
    public void addLevel()
    {
        currentLevelId++;
        maxUnlockedLevel = Mathf.Max(currentLevelId, maxUnlockedLevel);
    }

    public void startNextLevel()
    {
        addLevel();
        SceneManager.LoadScene(currentLevel.sceneName);
    }
    private void Awake()
    {
        levelInfoList = CsvUtil.LoadObjects<LevelInfo>("Level");
        int id = 0;
        foreach (var info in levelInfoList)
        {
            if (info.isDeprecated == 1)
            {
                continue;
            }
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
        
    }
}
