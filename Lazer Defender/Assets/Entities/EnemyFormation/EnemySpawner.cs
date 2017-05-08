using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyShip enemyPrefab;
    public float Width = 380;
    public float Height = 220;
    public float Speed = 40;
    private float xMin;
    private float xMax;
    private bool movingRight = true;
    public int NumShips = 0;

    private float SpeedMultiplyer = 1f;
    private float HealthMultiplyer = 1f;
    private float FireRateMultiplyer = 1f;
    public bool SetupFinished = false;

    // Use this for initialization
    void Start()
    {
        float distance = gameObject.transform.localPosition.z - Camera.main.transform.localPosition.z;
        RectTransform sizePlaceholder = gameObject.transform as RectTransform;

        float middleOffset = sizePlaceholder.rect.width / 2f;
        float leftEdge = 0;
        float rightEdge = 1;
        Vector3 leftBound = Camera.main.ViewportToWorldPoint(new Vector3(leftEdge, 0, distance));
        Vector3 rightBound = Camera.main.ViewportToWorldPoint(new Vector3(rightEdge, 0, distance));

        xMin = leftBound.x + middleOffset;
        xMax = rightBound.x - middleOffset;

        SpawnUntilFull();
    }

    // Update is called once per frame
    void Update()
    {
        float movementLeft = Vector3.left.x * Speed * Time.deltaTime;
        float movementRight = Vector3.right.x * Speed * Time.deltaTime;
        // NOTE: our new x defaults to our current x
        float newX = gameObject.transform.localPosition.x;

        if (!movingRight)
        {
            // NOTE: Vector3.left = new vector3 (-1,0,0)
            // Vector3.left * 3 = (-3,0,0)
            newX = gameObject.transform.localPosition.x + movementLeft;
            if (gameObject.transform.localPosition.x + movementLeft <= xMin)
            {
                movingRight = !movingRight;
            }
        }
        else
        {
            // NOTE: Vector3.left = new vector3 (1,0,0)
            newX = gameObject.transform.localPosition.x + movementRight;
            if (gameObject.transform.localPosition.x + movementRight >= xMax)
            {
                movingRight = !movingRight;
            }
        }

        // NOTE: limit what x can be in a new Vector and
        // update the game object position to the new position
        gameObject.transform.localPosition = new Vector3(
            Mathf.Clamp(newX, xMin, xMax),
            gameObject.transform.localPosition.y,
            gameObject.transform.localPosition.z);
    }

    internal void ShipDestryed(EnemyShip enemyShip)
    {
        NumShips--;
        if (NumShips <= 0)
        {
            SetupFinished = false;
            SpawnUntilFull();
            UpdateMultipliers();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height));
    }

    public Transform NextFreePosition()
    {
        foreach (Transform position in gameObject.transform)
        {
            if (position.childCount == 0)
            {
                return position;
            }
        }
        return null;
    }

    void SpawnUntilFull()
    {
        Transform freePos = NextFreePosition();

        if (freePos != null)
        {
            NumShips++;
            EnemyShip enemy = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity) as EnemyShip;
            enemy.Health = Mathf.RoundToInt(enemy.Health * HealthMultiplyer);
            Speed += SpeedMultiplyer;
            enemy.FireMultiplier += FireRateMultiplyer;
            enemy.transform.SetParent(freePos);
            if (NextFreePosition() != null)
            {
                Invoke("SpawnUntilFull", .3f);
            }
            else{
                SetupFinished = true;
            }
        }
    }

    void UpdateMultipliers()
    {
        SpeedMultiplyer += 3f;
        HealthMultiplyer += 1.1f;
        FireRateMultiplyer *= 1.1f;
    }
}
