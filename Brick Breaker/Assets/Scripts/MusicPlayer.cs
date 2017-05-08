using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour
{
	public static MusicPlayer _instance;

	// Use this for initialization
	public void Awake ()
	{
		if (_instance == null) {
			_instance = this;
			GameObject.DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
		
	}

	// Update is called once per frame
	void Update ()
	{
	
	}
}
