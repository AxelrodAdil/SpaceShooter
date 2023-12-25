using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;



public class PlayerController : MonoBehaviour
{
    public GameObject GameManager;

    public GameObject PlayerBullet;
    public GameObject BulletFirstPosition;
    public GameObject BulletSecondPosition;
    public GameObject Explosion;
    public Text LivesUIText;

    const int MaxLives = 3;
    int lives;

    public float speed;

    public void Init()
    {
        lives = MaxLives;
        LivesUIText.text = lives.ToString();
        transform.position = new Vector2(0, 0);
        gameObject.SetActive(true);
    }


    void Start()
    {

    }

    void Update()
    {

        if (Input.GetKeyDown("space"))
        {
            gameObject.GetComponent<AudioSource>().Play();


            GameObject firstbullet = (GameObject)Instantiate(PlayerBullet);
            firstbullet.transform.position = BulletFirstPosition.transform.position;

            GameObject secondbullet = (GameObject)Instantiate(PlayerBullet);
            secondbullet.transform.position = BulletSecondPosition.transform.position;

        }

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(x, y).normalized;

        Move(direction);

    }
    void Move(Vector2 direction)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        max.x = max.x - 0.225f;
        min.x = min.x + 0.225f;

        max.y = max.y - 0.285f;
        min.y = min.y + 0.285f;

        Vector2 pos = transform.position;

        pos += direction * speed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag == "EnemyShipTag") || (col.tag == "EnemyBulletTag"))
        {
            PlayExplosion();

            lives--;

            LivesUIText.text = lives.ToString();

            if (lives == 0)
            {
                GameManager.GetComponent<GameManager>().SetGameManagerState(global::GameManager.GameManagerState.GameOver);


                gameObject.SetActive(false);
            }

        }
    }
    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(Explosion);

        explosion.transform.position = transform.position;

    }

}
