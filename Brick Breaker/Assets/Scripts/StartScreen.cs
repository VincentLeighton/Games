﻿using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void onStart ()
	{
		ScreenManager.Instance.LoadNextScreen ();
	}
}
