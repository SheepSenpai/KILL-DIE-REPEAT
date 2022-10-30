using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rainbowlight : MonoBehaviour
{
    public Light lightObject; //Set your light Object
    public bool useRainbowColors;
    public Color colorStart = Color.red;
    public Color colorEnd = Color.green;
    public float duration = 0.2f;

    void Update()
    {
        if (useRainbowColors && lightObject)
        {
            float lerp = Mathf.PingPong(Time.time, duration) / duration;
            lightObject.color = Color.Lerp(colorStart, colorEnd, lerp);
        }
    }
}
