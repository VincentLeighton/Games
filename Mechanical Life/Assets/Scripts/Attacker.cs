using UnityEngine;

public class Attacker : MonoBehaviour
{
    [Range(-100f, 200f)]
    public float CurrentSpeed;
    public float Damage;
    public Defender target;
    private float WalkingSpeed = 27f;
    private Animator animator;
    void Awake()
    {
        ScreenManager._instance.AddAttacker(this);
    }
    // Use this for initialization
    void Start()
    {
        Rigidbody2D myRigidBody = gameObject.AddComponent<Rigidbody2D>();
        myRigidBody.isKinematic = true;
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * CurrentSpeed * Time.deltaTime);
        if(target == null){
           animator.SetBool("isAttacking", false);
        }
    }

    public bool Attack()
    {
        bool targetDestroyed = false;
        if (target != null)
        {
            Health health = target.GetComponent<Health>();
            if (health != null)
            {
                health.DealDamage(Damage);
                if (health.hp == 0)
                {
                    target = null;
                }
            }
        }
        return targetDestroyed;
    }
    public void TakeDamage(float damage)
    {
        //Use health class to calculate health
        Health characterHealth = gameObject.GetComponent<Health>();
        characterHealth.DealDamage(damage);
    }

    public void ChangeSpeed(float speed)
    {
        CurrentSpeed = speed;
    }

    public void SetTarget(GameObject obj)
    {
        target = obj.GetComponent<Defender>();
    }
}
