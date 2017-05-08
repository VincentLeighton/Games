using UnityEngine;

public class LaserCollecter : MonoBehaviour
{

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
        if (Laser.IsLaser(collider.gameObject))
        {
            Destroy(collider.gameObject);
        }
    }
}
