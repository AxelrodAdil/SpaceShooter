using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    float speed;
    GameObject scoreUIText;
    
    int previousScoreLevel = 0;

    void Start()
    {
        speed = 6f;
        scoreUIText = GameObject.FindGameObjectWithTag("ScoreTextTag");
    }

    void Update()
    {
        SetPlayerBulletSpeed();
        Vector2 position = transform.position;
        position = new Vector2(position.x, position.y + speed * Time.deltaTime);
        transform.position = position;
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        if (!(transform.position.y > max.y)) return;
        Destroy(gameObject);
    }
    
    void SetPlayerBulletSpeed()
    {
        int currentScoreLevel = scoreUIText.GetComponent<GameScore>().Score / 1000;
        if (currentScoreLevel > previousScoreLevel)
        {
            speed = speed + 1f;
            previousScoreLevel = currentScoreLevel;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("EnemyShipTag")) return;
        Destroy(gameObject);
    }
}
