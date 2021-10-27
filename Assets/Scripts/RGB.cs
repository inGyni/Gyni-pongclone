using UnityEngine;

public class RGB : MonoBehaviour
{
    public float rainbowSpeed;
    void Start()
    {

    }
    float c = 0.01f;
    bool reverse = false;
    void Update()
    {
        if (c >= 1.00f)
            reverse = true;
        if (c <= 0.00f)
            reverse = false;
        if (reverse == false)
            c += (rainbowSpeed / 1000) * Time.deltaTime;
        else
            c -= (rainbowSpeed / 1000) * Time.deltaTime;
        GetComponent<SpriteRenderer>().color = Color.HSVToRGB(c, 0.5f, 0.5f);
    }
}
