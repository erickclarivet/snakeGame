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
            case FoodType.Speed:
                snake.SpeedUpEffect();
                break;
            case FoodType.MoreParts:
                // Apply more parts effect
                snake.GrowBodyParts(3);
                additionalScore = 3f;
                break;
            case FoodType.Slow:
                snake.SlowEffect();
                additionalScore = 5f;
                break;
        }

        return additionalScore;
    }
}
