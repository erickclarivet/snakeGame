using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> foodPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetRandomFoodPrefab()
    {
        if (foodPrefabs.Count == 0)
        {
            Debug.LogError("No food prefabs assigned!");
            return null;
        }
        
        return foodPrefabs[Random.Range(0, foodPrefabs.Count)];
    }

    // TODO : Apply the effect of the food on the snake few sec 
    // TODO : Add effect that add more body parts to the snake
    public float ApplyEffect(Snake snake, Food food)
    {
        float additionalScore = 1f;
        snake.ResetEffect();
        switch (food.type)
        {
            case FoodType.Watermelon:
                snake.SpeedUpEffect();
                break;
            case FoodType.Cherry:
                // Apply cherry effect
                additionalScore = 3f; // Example effect: 1.5x score
                break;
            case FoodType.Grape:
                // Apply grape effect
                additionalScore = 4f; // Example effect: 2x score
                break;
            case FoodType.Yellow:
                snake.SlowEffect();
                break;
        }

        return additionalScore;
    }
}
