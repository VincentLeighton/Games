using UnityEngine;

[RequireComponent(typeof(Defender))]
public class Bot : MonoBehaviour
{
    public Defender defenderScript;
    public Animator animator;
    public GameObject shot;
    public GameObject projectiles;

    void Awake(){
        projectiles = GameObject.Find("Projectiles");
        if(projectiles == null){
            projectiles = new GameObject();
        }
    }
    void Update()
    {
		if(defenderScript.EnemyInLane){
			animator.SetBool("isAttacking", true);
		}
		else{
			animator.SetBool("isAttacking", false);
		}
    }

    public void FireProjecile()
    {
        GameObject obj = Instantiate(shot, gameObject.transform.position, Quaternion.identity);
        Projectile projectile = obj.GetComponent<Projectile>();
        projectile.transform.parent = projectiles.transform;
    }
}
