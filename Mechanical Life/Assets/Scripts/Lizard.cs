using UnityEngine;

[RequireComponent (typeof(Attacker))]
public class Lizard : MonoBehaviour {

	public Attacker attackerScript;
    public Animator animator;
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject obj = collider.gameObject;

        if (!obj.GetComponent<Defender>())
        {
            return;
        }
        else
        {
                attackerScript.SetTarget(obj);
                animator.SetBool("isAttacking", true);
        }
    }
}
