using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Snake : MonoBehaviour
{
    [SerializeField] private GameObject bodyPartPrefab;
    [SerializeField] private InputActionReference moveAction;

    private Vector2 direction = Vector2.right;
    private GameObject snakeGO;
    private List<Transform> bodyParts = new List<Transform>();

    private float normalSpeed = 0.15f;
    private float slowSpeed = 0.3f;
    private float fastSpeed = 0.05f;
    public float currentSpeed;
    private float timer = 0f;
    private Color defaultColor;

    public event Action OnFruitEaten;
    public event Action OnDestroySnake;
    
    // Start is called before the first frame update
    void Start()
    {
        defaultColor = GetComponent<SpriteRenderer>().color;
        currentSpeed = normalSpeed;
        snakeGO = gameObject;
        InitiateSnake(4);
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
        if (timer > currentSpeed)
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
        snakeGO.transform.position += new Vector3(direction.x, direction.y, 0);
        bodyParts[0].position = snakeGO.transform.position;        
   }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.EndsWith("Food"))
        {
            OnFruitEaten?.Invoke();
            GrowBodyParts();
        }
        else if (other.CompareTag("Body") || other.CompareTag("Wall"))
        {
            Debug.Log("GAME OVER!!");
            DestroyBodyParts();
            OnDestroySnake?.Invoke();
            //SceneManager.LoadScene("MainMenu");
        }
    }

    public void GrowBodyParts(int number = 1)
    {
        for (int i = 0; i < number; i++)
        {
            Transform newPart = Instantiate(bodyPartPrefab).transform;
            newPart.position = bodyParts[bodyParts.Count - 1].position;
            bodyParts.Add(newPart);
        }
        Move();
    }

    void InitiateSnake(int length)
    {
        DestroyBodyParts();
        bodyParts.Add(snakeGO.transform);
        // Initialize the snake with a given length
        for (int i = 1; i < length; i++)
        {
            Transform newPart = Instantiate(bodyPartPrefab).transform;
            newPart.position = snakeGO.transform.position - new Vector3(i, 0, 0);
            bodyParts.Add(newPart);
        }
    }


    void DestroyBodyParts()
    {
        foreach (Transform part in bodyParts)
        {
            if (part != snakeGO.transform)
            {
                Destroy(part.gameObject);
            }
        }
        bodyParts.Clear();
    }

    void SetDirection(Vector2 newDirection, float angle = 0)
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
        snakeGO.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void ResetEffect()
    {
        currentSpeed = normalSpeed;
        GetComponent<SpriteRenderer>().color = defaultColor; // Reset color
    }

    public void SpeedUpEffect()
    {
        currentSpeed = fastSpeed;
        GetComponent<SpriteRenderer>().color = Color.red; // Change color to indicate speed effect
    }

    public void SlowEffect()
    {
        GetComponent<SpriteRenderer>().color = Color.cyan; // Change color to indicate slow effect
        currentSpeed = slowSpeed;
    }
}
