using UnityEngine;

public class Laser : MonoBehaviour
{
    private float yMax;

    public float speed = 200f;
    public AudioClip LaserSound;
    public int Damage = 10;


    static public bool IsEnemyLaser(GameObject gameObj)
    {
        return gameObj.tag.Equals("EnemyLaser");
    }
    static public bool IsPlayerLaser(GameObject gameObj)
    {
        return gameObj.tag.Equals("PlayerLaser");
    }
    static public bool IsLaser(GameObject gameObj)
    {
        return gameObj.tag.Contains("Laser");
    }
    void Awake()
    {

    }
    // Use this for initialization
    void Start()
    {
        float distance = gameObject.transform.localPosition.z - Camera.main.transform.localPosition.z;
        RectTransform sizePlaceholder = gameObject.transform as RectTransform;
        float middleOffset = sizePlaceholder.rect.height / 2f;

        float topEdge = 1;
        Vector3 topBound = Camera.main.ViewportToWorldPoint(new Vector3(0, topEdge, distance));

        yMax = topBound.y + middleOffset;
        AudioSource.PlayClipAtPoint(LaserSound, Vector3.zero, .5f);
    }

    // Update is called once per frame
    void Update()
    {
        //Teacher uses a ridgedbody2d with velocity on the Y vector instead of reusing the movement code
        float movementUp = Vector3.up.y * speed * Time.deltaTime;
        float newY = gameObject.transform.localPosition.y + movementUp;

        if (newY >= yMax)
        {
            Destroy(gameObject);
            return;
        }
        else
        {

            gameObject.transform.localPosition = new Vector3(
                gameObject.transform.localPosition.x,
                newY,
                gameObject.transform.localPosition.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Fired laser is contacting something below
        if (Laser.IsLaser(collision.gameObject))
        {
            //EmenyLaser collision logic
            if (Laser.IsEnemyLaser(collision.gameObject))
            {
                HandleEnemyLaserCollision();
            }
            //PlayerLaser collision logic
            else
            {
                HandlePlayerLaserCollision();
            }
        }
        else
        {
            //Emenyship collision logic
            if (EnemyShip.IsEnemyShip(collision.gameObject))
            {
                HandleEnemyShipCollision();
            }
            //Playership collision logic
            else
            {
                HandlePlayerShipCollision();
            }
        }
    }

    private void HandlePlayerShipCollision()
    {
        if(Laser.IsEnemyLaser(gameObject)){
            //Enemy laser hitting player ship, destroy
            Destroy(gameObject);
        }
        else{
            //Player laser hitting Player ship, do nothing
        }
    }

    private void HandleEnemyShipCollision()
    {
        if(Laser.IsEnemyLaser(gameObject)){
            //Enemy laser hitting enemy ship, do nothing
        }
        else{
            //Player laser hitting player ship, destroy
            Destroy(gameObject);
        }
    }

    private void HandlePlayerLaserCollision()
    {
        Destroy(gameObject);
    }

    private void HandleEnemyLaserCollision()
    {
        Destroy(gameObject);
    }
}
