using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

	public Text scoreLabel;

	public string scoreText = "Score: {0}";
	private uint currentScore = 0;

	void Update () {
		currentScore = GameStateController.Instance.GetCurrentScore ();
        scoreLabel.text = string.Format (scoreText, currentScore);
	}
}
