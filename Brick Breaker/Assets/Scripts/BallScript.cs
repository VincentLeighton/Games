using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour
{
	public PaddleScript Paddle;
	private Vector3 VectorDelta;
	private bool HasStarted = false;
	private Rigidbody2D BallBody;
	public float x;
	public float y;
	private float XMid;
	private float YMid;
	public AudioClip Clip;

	// Use this for initialization
	void Start ()
	{
		VectorDelta = this.transform.position - Paddle.transform.position;
		XMid = Screen.width / 2; 
		YMid = Screen.height / 2;
	}
	
	// Update is called once per frame
	void Update ()
	{
		BallBody = this.GetComponent<Rigidbody2D> ();
		if (!HasStarted) {
			this.transform.position = Paddle.transform.position + VectorDelta;
			if (Input.GetMouseButtonDown (0)) {
				HasStarted = true;
				BallBody.velocity = new Vector2 (2f, 2f);
			}
		}
	}

	void OnCollisionExit2D (Collision2D collision)
	{
		if (HasStarted) {
			AudioSource.PlayClipAtPoint (Clip, gameObject.transform.position);
			CheckVelocity (BallBody.velocity);
		}
	}

	void CheckVelocity (Vector2 CurrentVelocity)
	{
		x = CurrentVelocity.x;
		y = CurrentVelocity.y;

		Vector2 NewVelocity = new Vector2 (Random.Range (-.2f, .2f), Random.Range (-.2f, .2f));
		Vector3 temp = BallBody.velocity + NewVelocity;
		//temp.x = Mathf.Clamp (temp.x, -1.5f, 2.5f);
		//temp.y = Mathf.Clamp (temp.y, -1.5f, 2.5f);
		BallBody.velocity = temp;
	}
}
