using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : MonoBehaviour
{
    public SnakeBehaviour snakeScript;

    public void SetDirectionUp()
    {
        snakeScript.SetDirection(Vector2.up, 0);
    }
    public void SetDirectionDown()
    {
        snakeScript.SetDirection(Vector2.down, 180);
    }
    public void SetDirectionLeft()
    {
        snakeScript.SetDirection(Vector2.left, 90);
    }
    public void SetDirectionRight()
    {
        snakeScript.SetDirection(Vector2.right, -90);
    }

    // Start is called before the first frame update
    void Start()
    {
        #if UNITY_EDITOR
            this.gameObject.SetActive(true); // Pour tester dans l'éditeur
        #elif UNITY_ANDROID
            this.gameObject.SetActive(true);
        #else
            this.gameObject.SetActive(false);
        #endif      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
