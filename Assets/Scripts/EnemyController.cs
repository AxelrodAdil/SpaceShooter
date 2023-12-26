using UnityEngine;

public class EnemyController : MonoBehaviour
{
    GameObject scoreUIText;
    float speed;
    public GameObject Explosion;
    
    int previousScoreLevel = 0;

    void Start()
    {
        speed = 2f;
        scoreUIText = GameObject.FindGameObjectWithTag("ScoreTextTag");
    }

    void Update()
    {
        Vector2 position = transform.position;
        SetEnemySpeed();
        position = new Vector2(position.x, position.y - speed * Time.deltaTime);
        transform.position = position;
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        if (!(transform.position.y < min.y)) return;
        Destroy(gameObject);
    }

    void SetEnemySpeed()
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
        if (!col.CompareTag("PlayerShipTag") && !col.CompareTag("PlayerBulletTag")) return;
        PlayExplosion();
        scoreUIText.GetComponent<GameScore>().Score += 100;
        Destroy(gameObject);
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(Explosion);
        explosion.transform.position = transform.position;
    }
}
