using UnityEngine;
using System.Collections;

public class PaddleScript : MonoBehaviour
{
	private float Min = .22f;
	private float Max = 7.78f;
	private float Range;
	public bool Auto = true;
	private BallScript Ball;

	public float MouseX {
		get{ return Input.mousePosition.x; }
	}

	public float MousePer {
		get{ return MouseX / Screen.width; }
	}

	// Use this for initialization
	void Start ()
	{
		Range = Max;
		Ball = GameObject.FindObjectOfType<BallScript> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		if (!Auto) {
			float clamp = Mathf.Clamp (CalculateX (MousePer), Min, Max);
			this.transform.position = new Vector3 (clamp, this.transform.position.y, 0);
		} else {
			AutoPlay ();
		}
	}

	private void AutoPlay ()
	{
		gameObject.transform.position = new Vector3 (Ball.transform.position.x, gameObject.transform.position.y);
	}

	private float CalculateX (float per)
	{
		return Range * per;
	}
}
