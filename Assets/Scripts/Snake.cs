using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Snake : MonoBehaviour
{
    private Vector2 direction = Vector2.right;
    private GameObject snakeComponent;
    public GameObject foodComponent;
    public GameObject bodyPartPrefab;
    List<Transform> bodyParts = new List<Transform>();

    public InputActionReference moveAction;

    public float moveRate; // speed move
    public float timer;
    public TextMeshProUGUI scoreText;
    public int score;
    public TextMeshProUGUI highScoreText;

    // Start is called before the first frame update
    void Start()
    {
        moveRate = 0.2f;
        LoadScore();
        timer = 0;
        UpdateScore();
        snakeComponent = this.gameObject;
        snakeComponent.transform.position = new Vector3(0f, 0f, 0f);
        InitiateSnake(4);
        SpawnFood();
    }

    void OnEnable()
    {
        moveAction.action.Enable();
    }

    void OnDisable() 
    {
        moveAction.action.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = moveAction.action.ReadValue<Vector2>();
        // Know if input is pressed hard enough
        if (input.magnitude > 0.5f)
        {
            // Choose x or y movement based on input
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            {
                if (input.x > 0.5f)
                    SetDirection(Vector2.right, -90f);
                else if (input.x < -0.5f)
                    SetDirection(Vector2.left, 90f);
            }
            else
            {
                if (input.y > 0.5f)
                    SetDirection(Vector2.up, 0f);
                else if (input.y < -0.5)
                    SetDirection(Vector2.down, 180f);
            }
        }
        timer += Time.deltaTime;
        if (timer > moveRate)
        {
            timer = 0;
            Move();
        } 
    }

// Custom methods

    float GetAngleFromDirection(Vector2 dir)
    {
        if (dir == Vector2.up) return 0;
        if (dir == Vector2.down) return 180;
        if (dir == Vector2.right) return -90;
        if (dir == Vector2.left) return 90;
        return 0;
    }

    void Move()
    {
        for (int i = bodyParts.Count - 1; i > 0; i--)
        {
            bodyParts[i].position = bodyParts[i - 1].position;
        }
        snakeComponent.transform.position += new Vector3(direction.x, direction.y, 0);
        bodyParts[0].position = snakeComponent.transform.position;        
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
            SaveScore();
            SpawnFood();
            Grow();
        }
        else if (other.CompareTag("Body") || other.CompareTag("Wall"))
        {
            Debug.Log("GAME OVER!!");
            SceneManager.LoadScene("Menu");
        }
    }

    void Grow()
    {
        // Add a new body part at the end of the snake
        Transform newPart = Instantiate(bodyPartPrefab).transform;
        newPart.position = bodyParts[bodyParts.Count - 1].position;
        bodyParts.Add(newPart);
        Move();
    }

    void UpdateScore()
    {
        score++;
        scoreText.text = $"Score : {score}";
    }

    void SaveScore()
    {
        // Save the score to PlayerPrefs or any other method
        PlayerPrefs.SetInt("SnakeScore", score);
        PlayerPrefs.Save();
    }

    void LoadScore()
    {
        int highScore = 0;
        // Load the score from PlayerPrefs or any other method
        if (PlayerPrefs.HasKey("SnakeScore"))
        {
            highScore = PlayerPrefs.GetInt("SnakeScore");
        }
        highScoreText.text = "High score : " + highScore;
    }

    void InitiateSnake(int length)
    {
        // Clear previous body parts if any
        foreach (Transform part in bodyParts)
        {
            Destroy(part.gameObject);
        }           
        bodyParts.Clear();
        bodyParts.Add(snakeComponent.transform);
        // Initialize the snake with a given length
        for (int i = 1; i < length; i++)
        {
            Transform newPart = Instantiate(bodyPartPrefab).transform;
            newPart.position = snakeComponent.transform.position - new Vector3(i, 0, 0);
            bodyParts.Add(newPart);
        }
    }

    public void SetDirection(Vector2 newDirection, float angle = 0)
    {
        // Prevent the snake from reversing direction
        if (-direction != newDirection)
        {
            direction = newDirection;
            Rotate(angle);
        }
    }

    void Rotate(float angle)
    {
        snakeComponent.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
