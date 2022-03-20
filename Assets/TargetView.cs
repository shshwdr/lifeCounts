using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetView : MonoBehaviour
{

    public Text mainTargetLabel;
    public Text targetLabel;
    public Text countdownLabel;
    public Button finishButton;
    GameObject finishTarget;
    public AudioClip levelFinishClip;
    public AudioClip levelFullyFinishClip;

    public Transform mainTargetCheck;
    public Transform targetCheck;

    bool isMainTargetChecked;
    bool isTargetChecked;

    // Start is called before the first frame update
    void Start()
    {
        updateView();
        finishTarget = GameObject.Find("FinishTarget").gameObject;
        finishTarget.SetActive(false);
        EventPool.OptIn("linkAnimal",updateView);
        countdownLabel.gameObject.SetActive( StageLevelManager.Instance.shouldShowCountdown());
        EventPool.OptIn<int>("updateTimer", updateCountdown);
        finishButton.onClick.AddListener(delegate {
            StageLevelManager.Instance.finishLevel(); // finishTarget.SetActive(true);
                                                      });
    }
    void updateCountdown(int t)
    {
        countdownLabel.text = t.ToString();
    }
    void updateView()
    {
        var stageManager = StageLevelManager.Instance;
        var levelInfo = stageManager.currentLevel;

        var mainTargetText = $"Rescue {stageManager.getRescuedMainCount()} / {levelInfo.mainTargetCount} {levelInfo.mainTarget}";
        mainTargetLabel.text = mainTargetText;
        var targetText = $"{stageManager.getRescuedCount()} / {levelInfo.targetCount}";
        targetLabel.text = targetText;
        bool playedMainOne = false;
        if (stageManager.getTargetFinish())
        {
            if (!isTargetChecked)
            {
                //if (!playedMainOne)
                {

                    playedMainOne = true;
                    var audioSource = GameObject.Find("sfx").GetComponent<AudioSource>();
                    audioSource.PlayOneShot(levelFullyFinishClip);
                }
                GameManager.popup(targetCheck);
                //targetCheck.localScale = Vector3.one;
                isTargetChecked = true;
            }
        }
        if (stageManager.getMainTargetFinish())
        {
            if (!isMainTargetChecked)
            {
                if (!playedMainOne)
                {

                    var audioSource = GameObject.Find("sfx").GetComponent<AudioSource>();
                    audioSource.PlayOneShot(levelFinishClip);
                }
                GameManager.popup(mainTargetCheck);
                GameManager.popup(finishButton.transform);
                //mainTargetCheck.localScale = Vector3.one;
                isMainTargetChecked = true;
            }
        }

        //finishButton.gameObject.SetActive(stageManager.getMainTargetFinish());
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
