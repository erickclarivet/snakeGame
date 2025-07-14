using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Fix head position snake
// Fix snake collision

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject snakePrefab;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private FoodManager foodManager;

    private GameObject snakeGO;
    private Snake snake;
    private GameObject currentFoodGO;
    private Food currentFood;

    // Start is called before the first frame update
    void Start()
    {
        scoreManager.LoadScore();
        SpawnSnake();
        SpawnFood();
    }

    // Update is called once per frame
    void Update()
    {        
    }

    private void SpawnSnake()
    {
        Vector2 spawnPosition = new Vector2(0f, 0f);
        snakeGO = Instantiate(snakePrefab, spawnPosition, Quaternion.Euler(0, 0, -90));
        snake = snakeGO.GetComponent<Snake>();
        snake.OnFruitEaten += HandleFruitEaten;
        snake.OnDestroySnake += HandleSnakeDestroyed;
    }

    private void SpawnFood()
    {
        GameObject foodPrefab = foodManager.GetRandomFoodPrefab();
        Vector2 spawnPosition = new Vector2(Random.Range(-24, 24),Random.Range(-12, 12));
        currentFoodGO = Instantiate(foodPrefab, spawnPosition, Quaternion.identity);
        currentFood = currentFoodGO.GetComponent<Food>();
        currentFood.OnFoodDisapear += HandleFoodDisapear;
    }

    private void HandleFruitEaten()
    {
        float additionalScore = foodManager.ApplyEffect(snake, currentFoodGO.GetComponent<Food>());
        scoreManager.UpdateScore(additionalScore);
        HandleFoodDisapear();
    }

    private void HandleFoodDisapear()
    {
        currentFood.OnFoodDisapear -= HandleFoodDisapear;
        Destroy(currentFoodGO);
        SpawnFood();
    }

    private void HandleSnakeDestroyed()
    {
        scoreManager.SaveScore();
        scoreManager.LoadScore();
        snake.OnFruitEaten -= HandleFruitEaten;
        snake.OnDestroySnake -= HandleSnakeDestroyed;

        Destroy(snakeGO);
        snakeGO = null;
        snake = null;
        SpawnSnake();
    }
}
