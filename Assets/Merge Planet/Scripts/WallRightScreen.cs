using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRightScreen : MonoBehaviour
{
    void Start()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        Vector3 newPosition = new Vector3(screenWidth - transform.localScale.x / 2, screenHeight / 2, 0);
        transform.position = newPosition;
    }
}
