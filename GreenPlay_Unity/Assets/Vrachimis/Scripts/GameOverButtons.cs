﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverButtons : MonoBehaviour {

	public GUIStyle highScoreStyle;
	public Vector2 highScorePos1;
	public Vector2 highScorePos2;
	public Vector2 highScorePos3;
	public Vector2 highScorePos4;
	public int highScoreSize1 = 1;
	public int highScoreSize2 = 1;
	public int highScoreSize3 = 1;
	public int highScoreSize4 = 1;
	public int highScore = 938;

	public GUIStyle scoreStyle;
	public Vector2 scorePos1;
	public Vector2 scorePos2;
	public Vector2 scorePos3;
	public Vector2 scorePos4;
	public int scoreSize1 = 1;
	public int scoreSize2 = 1;
	public int scoreSize3 = 1;
	public int scoreSize4 = 1;
	public int score = 938;

	public ResizeBackground resizeBackground;
	public GameManager gameManager;
	private Vector3 targetX;
	// Use this for initialization
	void Start () {
		score = PlayerPrefs.GetInt("Score");
		highScore = PlayerPrefs.GetInt("HighScore");
		highScoreStyle.fontSize = Mathf.Min(Screen.height,Screen.width)/10;
		scoreStyle.fontSize = Mathf.Min(Screen.height,Screen.width)/10;

		GameObject resizeBackgroundObject = GameObject.Find("Background");
		if (resizeBackgroundObject != null)
		{
			resizeBackground = resizeBackgroundObject.GetComponent<ResizeBackground>();
		}
		if (resizeBackground == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}

		GameObject gameManagerObject = GameObject.FindWithTag("GameManager");
		if (gameManagerObject != null)
		{
			gameManager = gameManagerObject.GetComponent <GameManager>();
		}
		if (gameManager == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void OnGUI() {
		//highscore
		if (transform.position.x < targetX.x + 0.02f && transform.position.x > targetX.x - 0.04f) {
			score = PlayerPrefs.GetInt("Score");
			highScore = PlayerPrefs.GetInt("HighScore");
			if (highScore.ToString().Length == 1) {
				GUI.Label(new Rect(Screen.width/2 - (Screen.width * highScorePos1.x ), Screen.height/2 - (Screen.height * highScorePos1.y), 500, 100), "" + highScore, highScoreStyle);
			}
			else if (highScore.ToString().Length == 2) {
				GUI.Label(new Rect(Screen.width/2 - (Screen.width * highScorePos2.x ), Screen.height/2 - (Screen.height * highScorePos2.y), 500, 100), "" + highScore, highScoreStyle);
			}
			else if (highScore.ToString().Length == 3) {
				GUI.Label(new Rect(Screen.width/2 - (Screen.width * highScorePos3.x), Screen.height/2 - (Screen.height * highScorePos3.y), 500, 100), "" + highScore, highScoreStyle);
			}
			else if (highScore.ToString().Length == 4) {
				GUI.Label(new Rect(Screen.width/2 - (Screen.width * highScorePos4.x), Screen.height/2 - (Screen.height * highScorePos4.y), 500, 100), "" + highScore, highScoreStyle);
			}

			//score
			if (score.ToString().Length == 1) {
				GUI.Label(new Rect(Screen.width/2 - (Screen.width * scorePos1.x ), Screen.height/2 - (Screen.height * scorePos1.y), 500, 100), "" + score, scoreStyle);
			}
			else if (score.ToString().Length == 2) {
				GUI.Label(new Rect(Screen.width/2 - (Screen.width * scorePos2.x ), Screen.height/2 - (Screen.height * scorePos2.y), 500, 100), "" + score, scoreStyle);
			}
			else if (score.ToString().Length == 3) {
				GUI.Label(new Rect(Screen.width/2 - (Screen.width * scorePos3.x), Screen.height/2 - (Screen.height * scorePos3.y), 500, 100), "" + score, scoreStyle);
			}
			else if (score.ToString().Length == 4) {
				GUI.Label(new Rect(Screen.width/2 - (Screen.width * scorePos4.x), Screen.height/2 - (Screen.height * scorePos4.y), 500, 100), "" + score, scoreStyle);
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		
		targetX = new Vector3(2 * resizeBackground.worldScreenWidth,0,-10);

		if (transform.position.x < targetX.x + 0.02f && transform.position.x > targetX.x - 0.04f) {
			score = PlayerPrefs.GetInt("Score");
			highScore = PlayerPrefs.GetInt("HighScore");
			//high score
			if (highScore.ToString().Length == 1) {
				highScoreStyle.fontSize = Mathf.Min(Screen.height,Screen.width)/highScoreSize1;
			}
			else if (highScore.ToString().Length == 2) {
				highScoreStyle.fontSize = Mathf.Min(Screen.height,Screen.width)/highScoreSize2;
			}
			else if (highScore.ToString().Length == 3) {
				highScoreStyle.fontSize = Mathf.Min(Screen.height,Screen.width)/highScoreSize3;
			}
			else if (highScore.ToString().Length == 4) {
				highScoreStyle.fontSize = Mathf.Min(Screen.height,Screen.width)/highScoreSize4;
			}

			//score
			if (score.ToString().Length == 1) {
				scoreStyle.fontSize = Mathf.Min(Screen.height,Screen.width)/scoreSize1;
			}
			else if (score.ToString().Length == 2) {
				scoreStyle.fontSize = Mathf.Min(Screen.height,Screen.width)/scoreSize2;
			}
			else if (score.ToString().Length == 3) {
				scoreStyle.fontSize = Mathf.Min(Screen.height,Screen.width)/scoreSize3;
			}
			else if (score.ToString().Length == 4) {
				scoreStyle.fontSize = Mathf.Min(Screen.height,Screen.width)/scoreSize4;
			}

			//touch
			foreach (Touch touch in Input.touches)
			{
				if (touch.phase == TouchPhase.Began) {
					Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
					RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
					if (hit != null && hit.collider != null) {
						Debug.Log ("I'm hitting "+hit.collider.name);
						//LATER: make sure to check if hitting pause or mute button
						if (hit.collider.name == "Play") {
							Debug.Log ("PLay");
							//SceneManager.LoadScene(1);
							transform.position = new Vector3(0,0,-10);
							gameManager.startGame();
						}
						else if (hit.collider.name == "NoAds") {
							Debug.Log ("ADS");
							//open No Ads app in the App Store
							Application.OpenURL("https://play.google.com/store/apps/details?id=com.lego.nexoknights.merlok&hl=en");
						}
						else if (hit.collider.name == "Store") {
							Debug.Log ("Shop");
							//Load the store scene
						}
						else if (hit.collider.name == "Leaderboards") {
							Debug.Log("Lead");
							//show leaderboards
						}


					}
				}
			}
		}
	}
}
