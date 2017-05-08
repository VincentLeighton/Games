using UnityEngine;
using System.Collections;

public class LoseCollider : MonoBehaviour
{

	void OnTriggerEnter2D (Collider2D trigger)
	{
		ScreenManager.Instance.LoadScreen ("DefeatScreen");
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		
	}
}
