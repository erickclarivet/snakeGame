using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallResizer : MonoBehaviour
{
    public GameObject wallLeft;
    public GameObject wallRight;
    public GameObject wallTop;
    public GameObject wallBottom;

    // Start is called before the first frame update
    void Start()
    {
        ResizeWalls();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void ResizeWalls()
    {
        float height = Camera.main.orthographicSize * 2;
        float width = height * Camera.main.aspect;

        if (wallLeft != null)
        {
            SpriteRenderer wallRenderer = wallLeft.GetComponent<SpriteRenderer>();
            BoxCollider2D wallBoxCollider = wallLeft.GetComponent<BoxCollider2D>();
            wallLeft.transform.position = new Vector3((-width / 2) + (wallRenderer.size.x/2), 0, 0);
            wallRenderer.size = new Vector2(wallRenderer.size.x, height - 2f);
            wallBoxCollider.size = new Vector3(0.7f, height - 4f, 1);
        }
        if (wallRight != null)
        {
            SpriteRenderer wallRenderer = wallRight.GetComponent<SpriteRenderer>();
            BoxCollider2D wallBoxCollider = wallRight.GetComponent<BoxCollider2D>();
            wallRight.transform.position = new Vector3((width / 2) - (wallRenderer.size.x/2), 0, 0);
            wallRenderer.size = new Vector2(wallRenderer.size.x, height - 2f);
            wallBoxCollider.size = new Vector3(0.7f, height - 4f, 1);
        }
        if (wallTop != null)
        {
            SpriteRenderer wallRenderer = wallTop.GetComponent<SpriteRenderer>();
            BoxCollider2D wallBoxCollider = wallTop.GetComponent<BoxCollider2D>();
            wallTop.transform.position = new Vector3(0, (height / 2) - (wallRenderer.size.x/2), 0);
            wallRenderer.size = new Vector2(wallRenderer.size.x, width);
            wallBoxCollider.size = new Vector3(0.7f, width - 4f, 1f);
        }
        if (wallBottom != null)
        {
            SpriteRenderer wallRenderer = wallBottom.GetComponent<SpriteRenderer>();
            BoxCollider2D wallBoxCollider = wallBottom.GetComponent<BoxCollider2D>();
            wallBottom.transform.position = new Vector3(0, (-height / 2) + (wallRenderer.size.x/2), 0);
            wallRenderer.size = new Vector2(wallRenderer.size.x, width);
            wallBoxCollider.size = new Vector3(0.7f, width - 4f, 1);
        }
    }
}