using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Fix head position snake
// Fix snake collision

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject snakePrefab;
    [SerializeField] private GameObject fruitPrefab;
    [SerializeField] private ScoreManager scoreManager;

    private GameObject snakeGO;
    private Snake snake;
    private GameObject currentFruit;

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
        // TODO : Rework => use camera bounds for spawning food
        // TODO : Create class to create different types of food
        Vector2 spawnPosition = new Vector2(Random.Range(-18, 18),Random.Range(-9, 9));
        currentFruit = Instantiate(fruitPrefab, spawnPosition, Quaternion.identity);
    }

    private void HandleFruitEaten()
    {
        scoreManager.UpdateScore(1);
        Destroy(currentFruit);
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
