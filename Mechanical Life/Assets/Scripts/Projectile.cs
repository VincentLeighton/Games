using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float Speed;
	public float Damage;
	// Use this for initialization
	void Start () {
		Rigidbody2D myRigidBody = gameObject.AddComponent<Rigidbody2D>();
		myRigidBody.isKinematic = true;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.right * Speed * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.gameObject.GetComponent<Attacker>()){
			Attacker a = collider.gameObject.GetComponent<Attacker>();
			a.TakeDamage(Damage);
			DestroyProjectile();
		}
	}

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
