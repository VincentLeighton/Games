using UnityEngine;
using UnityEngine.UI;

public class PlayerShip : MonoBehaviour
{
    public Laser laserPrefab;
    public Sprite LaserImage;
    public float Speed = 10f;
    public AudioClip Clip;
    public int Health = 50;
    private float xMin;
    private float xMax;
    private float rateOfFire = .75f;
    public Slider HealthSlider;
    [SerializeField] private int laserSpeed = 200;
    static public bool IsPlayerShip(GameObject gameObj)
    {
        return gameObj.tag.Equals("PlayerShip");
    }
    static public bool IsShip(GameObject gameObj)
    {
        return gameObj.tag.Contains("Ship");
    }
    private void Start()
    {
        // NOTE: could just pass this in, no need to calc
        float distance = gameObject.transform.localPosition.z - Camera.main.transform.localPosition.z;
        RectTransform sizePlaceholder = gameObject.transform as RectTransform;
        float middleOffset = sizePlaceholder.rect.width / 2f;

        // NOTE: good for test screen setup
        // NOTE: in the viewport, -0.5f is the left edge and 0.5f is the right edge
        // float leftEdge = -0.5f;
        // float rightEdge = leftEdge * -1;
        // Vector3 leftBound = Camera.main.ViewportToScreenPoint(new Vector3(leftEdge, 0, distance));
        // Vector3 rightBound = Camera.main.ViewportToScreenPoint(new Vector3(rightEdge, 0, distance));

        // NOTE: good for game setup
        float leftEdge = 0;
        float rightEdge = 1;
        Vector3 leftBound = Camera.main.ViewportToWorldPoint(new Vector3(leftEdge, 0, distance));
        Vector3 rightBound = Camera.main.ViewportToWorldPoint(new Vector3(rightEdge, 0, distance));

        xMin = leftBound.x + middleOffset;
        xMax = rightBound.x - middleOffset;
        // Debug.LogWarningFormat("Set xMin = {0} xMax = {1} (Object width: {2}, middleOffset: {3}, z-distanceToCamera: {4})\n\nleftBound: {5} rightBound: {6}",
        //     xMin,
        //     xMax,
        //     sizePlaceholder.rect.width,
        // 	middleOffset,
        //     distance,
        //     leftBound,
        //     rightBound);

        laserPrefab.speed = laserSpeed;

        HealthSlider.maxValue = Health;
        HealthSlider.value = Health;
    }

    private void Update()
    {
        UpdatePosition();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.000001f, rateOfFire);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }
    }

    private void Fire()
    {
        laserPrefab.tag = "PlayerLaser";
        Transform laserImage = laserPrefab.transform.FindChild("LaserImage");
        SpriteRenderer renderer = laserImage.GetComponent<SpriteRenderer>();
        renderer.sprite = LaserImage;
        Laser temp = Instantiate(laserPrefab, gameObject.transform.position, Quaternion.identity);
        temp.speed = laserSpeed;
    }

    private void UpdatePosition()
    {
        float movementLeft = Vector3.left.x * Speed * Time.deltaTime;
        float movementRight = Vector3.right.x * Speed * Time.deltaTime;
        // NOTE: our new x defaults to our current x
        float newX = gameObject.transform.localPosition.x;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // NOTE: Vector3.left = new vector3 (-1,0,0)
            // Vector3.left * 3 = (-3,0,0)
            newX = gameObject.transform.localPosition.x + movementLeft;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            // NOTE: Vector3.left = new vector3 (1,0,0)
            newX = gameObject.transform.localPosition.x + movementRight;
        }

        // NOTE: limit what x can be in a new Vector and
        // update the game object position to the new position
        gameObject.transform.localPosition = new Vector3(
            Mathf.Clamp(newX, xMin, xMax),
            gameObject.transform.localPosition.y,
            gameObject.transform.localPosition.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Fired laser is contacting something below
        if (Laser.IsLaser(collision.gameObject))
        {
            Laser laser = collision.gameObject.GetComponent<Laser>();
            //EmenyLaser collision logic
            if (Laser.IsEnemyLaser(collision.gameObject))
            {
                HandleEnemyLaserCollision(laser.Damage);
            }
            //PlayerLaser collision logic
            else
            {
                HandlePlayerLaserCollision();
            }
        }
    }

    private void HandlePlayerLaserCollision()
    {
        //donothing
    }

    private void HandleEnemyLaserCollision(int dmg)
    {
        Health = Health - dmg;
        HealthSlider.value = Health;
        EnemyShip.UpdateHealthBar(HealthSlider);
        if (Health <= 0)
        {
            MusicPlayer._instance.PlayDeathSound();
            Destroy(gameObject);
            ScreenManager.Instance.LoadScreen("DefeatScreen");
        }
    }
}