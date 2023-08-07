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

    /// <summary>
    /// This method generates a random Vector2.
    /// </summary>
    /// <returns>Random normalized Vector2.</returns>
    public static Vector2 GenerateRandomNormalizedVector2()
    {
        Vector2 o_RandomVector = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        return o_RandomVector.normalized;
    }

    /// <summary>
    /// This method changes the sprite every collision.
    /// </summary>
    public void OnCollisionEnter2D(Collision2D collision)
    {
        colors[index].SetActive(false);
        index = (index + 1) % 4;
        colors[index].SetActive(true);
    }
}
