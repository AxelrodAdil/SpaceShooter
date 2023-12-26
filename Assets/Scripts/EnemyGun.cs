using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public GameObject EnemyBullet;

    void Start()
    {
        Invoke("FireEnemyBullet", 1f);
    }

    void FireEnemyBullet()
    {
        GameObject playerShip = GameObject.Find("Player");
        if (playerShip == null) return;
        GameObject bullet = (GameObject)Instantiate(EnemyBullet);
        bullet.transform.position = transform.position;
        Vector2 direction = playerShip.transform.position - bullet.transform.position;
        bullet.GetComponent<EnemyBullet>().SetDirection(direction);
    }
}
