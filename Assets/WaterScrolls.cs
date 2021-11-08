using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScrolls : MonoBehaviour
{
    [Tooltip("Game Units Per Second")]

    public float Scrollspeed =1f;

    void Update()
    {
        float yMove = Scrollspeed * Time.deltaTime;
        transform.Translate(new Vector2(0f, yMove));
    }
}
