using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float hp = 100;
    public void DealDamage(float damage)
    {
        if ((hp - damage) > 0)
        {
			hp -= damage;
        }
        else
        {
			//trigger dealth animation here
			DestroyObject();
		}
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
