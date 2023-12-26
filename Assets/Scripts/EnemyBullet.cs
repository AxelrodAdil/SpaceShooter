using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    float speed;
    Vector2 _direction;
    bool isReady;
    GameObject scoreUIText;
    
    int previousScoreLevel = 0;

    void Awake()
    {
        speed = 5f;
        isReady = false;
    }
    
    public void Start()
    {
        scoreUIText = GameObject.FindGameObjectWithTag("ScoreTextTag");
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction.normalized;
        isReady = true;
    }
    
    void Update()
    {
        SetEnemyBulletSpeed();
        if (!isReady) return;
        Vector2 position = transform.position;
        position += _direction * speed * Time.deltaTime;
        transform.position = position;
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        if (!(transform.position.x < min.x) && !(transform.position.x > max.x) &&
            !(transform.position.y < min.y) && !(transform.position.y > max.y)) return;
        Destroy(gameObject);
    }
    
    void SetEnemyBulletSpeed()
    {
        int currentScoreLevel = scoreUIText.GetComponent<GameScore>().Score / 1000;
        if (currentScoreLevel > previousScoreLevel)
        {
            speed = speed + 0.5f;
            previousScoreLevel = currentScoreLevel;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("PlayerShipTag")) return;
        Destroy(gameObject);
    }
}
