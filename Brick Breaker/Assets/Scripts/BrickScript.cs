using UnityEngine;
using System.Collections;

public class BrickScript : MonoBehaviour
{
	public int MaxHits = 1;
	public int Hits = 0;
	public GameScreen Screen;
	public Sprite Undamaged;
	public Sprite Damaged;
	public Sprite Damaged2;
	public AudioClip Clip;
	public ParticleSystem Smoke;

	private void Awake ()
	{
		if (Screen == null) {
			Screen = GameObject.FindObjectOfType<Canvas> ().GetComponent<GameScreen> ();
		}
		gameObject.GetComponent<SpriteRenderer> ().sprite = Undamaged;
	}
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnCollisionExit2D (Collision2D collision)
	{
		Hits++;
		if (gameObject.tag == "Breakable") {
			CalculateHit ();
		}
	}

	private void CalculateHit ()
	{
		if (Hits >= MaxHits) {
			ParticleSystem TempSmoke = Instantiate (Smoke, transform.position, Quaternion.identity) as ParticleSystem;
			TempSmoke.Play ();
			DestroyBrick (gameObject);
		}
		if (Hits == 1) {
			gameObject.GetComponent<SpriteRenderer> ().sprite = Damaged;
		} else {
			gameObject.GetComponent<SpriteRenderer> ().sprite = Damaged2;
		}
	}

	void DestroyBrick (GameObject brick)
	{
		AudioSource.PlayClipAtPoint (Clip, gameObject.transform.position);
		Destroy (gameObject);
		Screen.DecrementBrickCount ();
		Screen.UpdateScore (MaxHits * 10);
	}
}