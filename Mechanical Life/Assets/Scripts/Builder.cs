using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Defender))]
public class Builder : MonoBehaviour {

    public Level0 level;
	public Defender defenderScript;
    public Animator animator;
    public GameObject currency;

    void Awake(){
        level = GameObject.Find("Canvas").GetComponent<Level0>();
        currency = GameObject.Find("Currency");
        if(currency == null){
            currency = new GameObject();
        }
    }

    public void RewardCurrency()
    {
        level.IncreaseCurrency(1);
    }
}
