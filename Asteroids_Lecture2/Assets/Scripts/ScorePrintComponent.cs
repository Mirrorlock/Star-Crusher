using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePrintComponent : MonoBehaviour {
    public string ScoreToDisplay = "Score: {0}";
    public GameObject score_obj = null;
    public uint score = 99999;
	void Start()
    {
        var text = score_obj.GetComponent<Text>();
        ScoreToDisplay = string.Format(ScoreToDisplay, score);
        text.text = ScoreToDisplay;
    }
}
