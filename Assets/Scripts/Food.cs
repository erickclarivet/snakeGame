using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FoodType;

public class Food : MonoBehaviour
{
    public FoodType type;
    [SerializeField] private float appearanceTime; // Time before the food disappears if not eaten
    private float timer = 0f;

    public event Action OnFoodDisapear;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (appearanceTime != -1f && timer >= appearanceTime)
        {
            timer = 0f;
            OnFoodDisapear?.Invoke();
        }
    }
}
