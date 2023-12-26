using UnityEngine;

public class EnemyController : MonoBehaviour
{
    GameObject scoreUIText;
    float speed;
    public GameObject Explosion;

    void Start()
    {
        speed = 2f;
        scoreUIText = GameObject.FindGameObjectWithTag("ScoreTextTag");
    }

    void Update()
    {
        Vector2 position = transform.position;
        position = new Vector2(position.x, position.y - speed * Time.deltaTime);
        transform.position = position;
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        if (!(transform.position.y < min.y)) return;
        Destroy(gameObject);
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
