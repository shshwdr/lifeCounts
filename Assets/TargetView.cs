using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetView : MonoBehaviour
{

    public Text mainTargetLabel;
    public Text targetLabel;
    public Button finishButton;
    GameObject finishTarget;

    // Start is called before the first frame update
    void Start()
    {
        updateView();
        finishTarget = GameObject.Find("FinishTarget").gameObject;
        finishTarget.SetActive(false);
        EventPool.OptIn("linkAnimal",updateView);
        finishButton.onClick.AddListener(delegate { finishTarget.SetActive(true); });
    }

    void updateView()
    {
        var stageManager = StageLevelManager.Instance;
        var levelInfo = stageManager.currentLevel;

        var mainTargetText = $"Rescue {stageManager.getRescuedCount()} / {levelInfo.mainTargetCount} {levelInfo.mainTarget}";
        mainTargetLabel.text = mainTargetText;
        var targetText = $"{stageManager.getRescuedMainCount()} / {levelInfo.targetCount}";
        targetLabel.text = targetText;

        finishButton.gameObject.SetActive(stageManager.getMainTargetFinish());
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
