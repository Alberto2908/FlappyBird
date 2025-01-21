using UnityEngine;

public class Tuberias : MonoBehaviour
{
    public float velocidad = 5f;
    private float margenIzq;

    private void Start()
    {
        margenIzq = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
    }

    private void Update()
    {
        transform.position += Vector3.left * velocidad * Time.deltaTime;

        if (transform.position.x < margenIzq)
        {
            gameObject.SetActive(false);
        }
    }
}
