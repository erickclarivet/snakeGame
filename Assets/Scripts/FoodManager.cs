using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> foodPrefabs;
    private float effectDuration = -1f;
    private float effectTimer = 0f;

    public event Action OnEndEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        effectTimer += Time.deltaTime;
        if (effectDuration != -1f && effectTimer > effectDuration)
        {
            effectTimer = 0f;
            OnEndEffect?.Invoke();
        }
    }

    public GameObject GetRandomFoodPrefab()
    {
        if (foodPrefabs.Count == 0)
        {
            Debug.LogError("No food prefabs assigned!");
            return null;
        }
        
        // Randomly select a food prefab based on defined probabilities
        int number = UnityEngine.Random.Range(0, 100);
        if (number < 70)
            return foodPrefabs[0]; // Classic
        else if (number < 85)
            return foodPrefabs[1]; // Speed
        else if (number < 95)
            return foodPrefabs[2]; // MoreParts
        return foodPrefabs[3]; // Slow
    }

    // TODO : Apply the effect of the food on the snake few sec 
    // TODO : Add effect that add more body parts to the snake
    public float ApplyEffect(Snake snake, Food food)
    {
        float additionalScore = 1f;
        snake.ResetEffect();
        effectDuration = food.effectDuration;
        effectTimer = 0f;
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
