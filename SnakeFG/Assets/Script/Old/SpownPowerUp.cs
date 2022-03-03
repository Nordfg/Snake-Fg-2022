using UnityEngine;
using System.Collections;

public class SpownPowerUp : MonoBehaviour
{
    // Food Prefab
    public GameObject PowerUpPrefab;

    // Borders
    public Transform borderTop;
    public Transform borderBottom;
    public Transform borderLeft;
    public Transform borderRight;

    // Use this for initialization
    void Start()
    {
        // Spawn food every 10 seconds, starting in 20
        InvokeRepeating("Spawn", 10, 20);
    }

    // Spawn one piece of food
    void Spawn()
    {
        // x position between left & right border
        int x = (int)Random.Range(borderLeft.position.x,
                                  borderRight.position.x);

        // y position between top & bottom border
        int y = (int)Random.Range(borderBottom.position.y,
                                  borderTop.position.y);

        // Instantiate the food at (x, y)
        Instantiate(PowerUpPrefab,
                    new Vector2(x, y),
                    Quaternion.identity); // default rotation
    }
}