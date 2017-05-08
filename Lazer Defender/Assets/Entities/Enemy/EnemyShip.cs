using UnityEngine;
using UnityEngine.UI;

public class EnemyShip : MonoBehaviour
{
    public AudioClip Clip;
    public Laser laserPrefab;
    public Sprite LaserImage;
    public int Health = 10;
    [SerializeField] private int laserSpeed = -200;
    private EnemySpawner spawner;
    public float FireMultiplier = 100f;
    public Slider HealthSlider;
    public ParticleSystem Explosion;
    static public bool IsEnemyShip(GameObject gameObj)
    {
        return gameObj.tag.Equals("EnemyShip");
    }
    // Use this for initialization
    void Start()
    {
        spawner = GameObject.FindObjectOfType<EnemySpawner>();
        laserPrefab.speed = laserSpeed;
        HealthSlider.maxValue = Health;
        HealthSlider.value = Health;
    }

    // Update is called once per frame
    void Update()
    {
        int maxRange = 201;
        int fireChance = UnityEngine.Random.Range(0, maxRange);
        if (fireChance > maxRange * (1f - (FireMultiplier / 1000)))
        {
            Fire();
        }
    }

    private void Fire()
    {
        if(!spawner.SetupFinished){
            return;
        }
        laserPrefab.tag = "EnemyLaser";
        Transform laserImage = laserPrefab.transform.FindChild("LaserImage");
        SpriteRenderer renderer = laserImage.GetComponent<SpriteRenderer>();
        renderer.sprite = LaserImage;
        Laser temp = Instantiate(laserPrefab, gameObject.transform.position, Quaternion.identity);
        temp.speed = laserSpeed;
        //Inverses only the Y os the scale on a laser created by an enemy ship
        temp.transform.localScale = new Vector3(1, -1, 1);
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
                HandleEnemyLaserCollision();
            }
            //PlayerLaser collision logic
            else
            {
                HandlePlayerLaserCollision(laser.Damage);
            }
        }
    }

    private void HandlePlayerLaserCollision(int dmg)
    {
        if(!spawner.SetupFinished){
            return;
        }
        Health = Health - dmg;
        HealthSlider.value = Health;
        EnemyShip.UpdateHealthBar(HealthSlider);
        if (Health <= 0)
        {
            spawner.ShipDestryed(this);
            AudioSource.PlayClipAtPoint(Clip, Vector3.zero);
            ParticleSystem temp = Object.Instantiate<ParticleSystem>(Explosion, gameObject.transform.position, Quaternion.identity);
            temp.Play();
            Destroy(gameObject);
        }
    }

    public static void UpdateHealthBar(Slider slider)
    {
        if (slider.value >= slider.maxValue * .75f)
        {
            slider.transform.Find("Fill").GetComponent<Image>().color = Color.green;
        }
        else if (slider.value >= slider.maxValue * .5f)
        {
            slider.transform.Find("Fill").GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            slider.transform.Find("Fill").GetComponent<Image>().color = Color.red;
        }
    }

    private void HandleEnemyLaserCollision()
    {
        //Donothing
    }
}
