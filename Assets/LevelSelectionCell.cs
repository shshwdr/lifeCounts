using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionCell : MonoBehaviour
{
    public Button button;
    public Text text;

    public void init(LevelInfo info) {
        text.text = info.displayName;
        button.onClick.AddListener(delegate
        {
            StageLevelManager.Instance.startLevel(info.id);
        });
    
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
