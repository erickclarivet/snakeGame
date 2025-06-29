using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class SnakeBehaviour : MonoBehaviour
{
    private Vector2 direction = Vector2.right;
    private GameObject snakeComponent;
    public GameObject foodComponent;
    public GameObject bodyPartPrefab;
    List<Transform> bodyParts = new List<Transform>();
    List<Vector3> previousPositions = new List<Vector3>();

    public float moveRate; // speed move
    public float timer;
    public TextMeshProUGUI scoreText;
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        moveRate = 0.2f;
        score = -1;
        timer = 0;
        UpdateScore();
        snakeComponent = this.gameObject;
        snakeComponent.transform.position = new Vector3(-9f, 0f, 0f);
        SpawnFood();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        timer += Time.deltaTime;

        if (timer > moveRate)
        {
            timer = 0;
            Move();
        } 
    }

// Custom methods
    void Move()
    {
        previousPositions.Insert(0, snakeComponent.transform.position);
        snakeComponent.transform.position += new Vector3(direction.x, direction.y, 0);
        // move parts :
        for (int i = 0; i < bodyParts.Count; i++)
        {
            bodyParts[i].position = previousPositions[i];
        }
        // delete las position
        if (previousPositions.Count > bodyParts.Count + 1)
        {
            previousPositions.RemoveAt(previousPositions.Count - 1);
        }
    }

    void SpawnFood()
    {
        // Initiation random position
        Vector2 position = new Vector2(Random.Range(-18, 18),Random.Range(-9, 9));
        Instantiate(foodComponent, position, Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            Destroy(other.gameObject);
            UpdateScore();
            SpawnFood();
            Grow();
        }
        else if (other.CompareTag("Body") || other.CompareTag("Wall"))
        {
            Debug.Log("GAME OVER!!");
            Time.timeScale = 0f;
            // not good
        }
    }

    void Grow()
    {
        Vector3 newPosition = snakeComponent.transform.position;
        previousPositions.Insert(0, snakeComponent.transform.position);
        snakeComponent.transform.position += new Vector3(direction.x, direction.y, 0);
        GameObject newPart = Instantiate(bodyPartPrefab, newPosition, Quaternion.identity);
        bodyParts.Add(newPart.transform);
    }

    void UpdateScore()
    {
        score++;
        scoreText.text = $"Score : {score}";
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down)
        {
            direction = Vector2.up;
            Rotate(0);

        }
        else if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up)
        {
            direction = Vector2.down;
            Rotate(180);
        }
        else if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left)
        {
            direction = Vector2.right;
            Rotate(-90);
        }
        else if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right)
        {
            direction = Vector2.left;
            Rotate(90);
        }
    }

    void Rotate(float angle)
    {
        snakeComponent.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
