using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageFloat : MonoBehaviour
{
    public GameObject[] colors;
    private Rigidbody2D rb;
    private int scaler = 1000;
    private int index = 0;

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();

        rb.AddForce(GenerateRandomNormalizedVector2() * scaler, ForceMode2D.Impulse);
    }

    public static Vector2 GenerateRandomNormalizedVector2()
    {
        Vector2 randomVector = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        return randomVector.normalized;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        colors[index].SetActive(false);
        index = (index + 1) % 4;
        colors[index].SetActive(true);
    }
}
