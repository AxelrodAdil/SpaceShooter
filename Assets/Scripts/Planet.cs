using UnityEngine;
using Random = UnityEngine.Random;

public class Planet : MonoBehaviour
{
    public float speed;
    public bool isMoving;
    GameObject scoreUIText;

    Vector2 min;
    Vector2 max;

    int previousScoreLevel = 3;
    
    void Awake()
    {
        isMoving = false;
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        max.y = max.y + GetComponent<SpriteRenderer>().sprite.bounds.extents.y;
        min.y = min.y - GetComponent<SpriteRenderer>().sprite.bounds.extents.y;
    }

    private void Start()
    {
        scoreUIText = GameObject.FindGameObjectWithTag("ScoreTextTag");
    }

    void Update()
    {
        if (isMoving)
        {
            SetPlanetSpeed();
            Vector2 position = transform.position;
            position = new Vector2(position.x, position.y + speed * Time.deltaTime);
            transform.position = position;
            if (!(transform.position.y < min.y)) return;
            isMoving = false;
        }
    }
    
    void SetPlanetSpeed()
    {
        int currentScoreLevel = scoreUIText.GetComponent<GameScore>().Score / 2000;
        if (currentScoreLevel > previousScoreLevel)
        {
            speed = speed + 1f;
            previousScoreLevel = currentScoreLevel;
        }
    }

    public void ResetPosition()
    {
        transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
    }
}
