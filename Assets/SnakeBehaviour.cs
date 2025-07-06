using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class SnakeBehaviour : MonoBehaviour
{
    private Vector2 direction = Vector2.right;
    private GameObject snakeComponent;
    public GameObject foodComponent;
    public GameObject bodyPartPrefab;
    List<Transform> bodyParts = new List<Transform>();
    List<Vector3> previousPositions = new List<Vector3>();

    public InputActionReference moveAction;

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
            SceneManager.LoadScene("Menu"); // reload scene
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
