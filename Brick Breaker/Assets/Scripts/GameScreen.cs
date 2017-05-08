using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameScreen : MonoBehaviour
{
	//public static GameScreen _instance;
	public int NumBricks = 0;
	public Text CounterText;
	public Text ScoreText;
	public int ScoreCount;

	public void Awake ()
	{
		if (CounterText == null) {
			CounterText = this.transform.FindChild ("BrickCounterText").gameObject.GetComponent<Text> ();
		}

		if (ScoreText == null) {
			ScoreText = this.transform.FindChild ("ScoreCounterText").gameObject.GetComponent<Text> ();
		}


	}

	// Use this for initialization
	void Start ()
	{
		
		NumBricks = GameObject.FindGameObjectsWithTag ("Breakable").Length;
	}
	
	// Update is called once per frame
	void Update ()
	{
		CounterText.text = string.Format ("Bricks: {0}", NumBricks);
		ScoreText.text = string.Format ("Score: {0}", ScoreCount);

		if (NumBricks == 0) {
			OnWin ();
		}
	}

	public void OnWin ()
	{
		ScreenManager.Instance.LoadNextScreen ();
	}

	public void DecrementBrickCount ()
	{
		NumBricks--;
	}

	public void UpdateScore (int ScorePoints)
	{
		ScoreCount += ScorePoints;
	}

	private void RemoveGameComponents (Text[] Texts)
	{
		foreach (Text Text in Texts) {
			Destroy (Text);
		}
	}

	private void OnDestroy ()
	{
		
	}
}
