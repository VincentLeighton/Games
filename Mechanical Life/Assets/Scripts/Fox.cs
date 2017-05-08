using UnityEngine;

[RequireComponent (typeof(Attacker))]
public class Fox : MonoBehaviour
{
    public Attacker attackerScript;
    public Animator animator;

    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject obj = collider.gameObject;

        if (!obj.GetComponent<Defender>())
        {
            return;
        }
        else
        {
            if (obj.GetComponent<Blocker>())
            {
                animator.SetTrigger("isJumping");
            }
            else{
                attackerScript.SetTarget(obj);
                animator.SetBool("isAttacking", true);
            }
        }
    }
}
