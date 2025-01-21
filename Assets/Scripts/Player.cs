using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex;
    private Vector3 direccion;
    public float gravedad = -9.8f;
    public float fuerza = 5f;
    public float tilt = 5f;
    public AudioSource volarAudio;
    public AudioSource gameOverAudio;
    public AudioSource puntoAudio;
    

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimacionSprite), 0.15f, 0.15f);
    }

    private void OnEnable()
    {
        Vector3 posicion = transform.position;
        posicion.y = 0f;
        transform.position = posicion;
        direccion = Vector3.zero;
    }

    private void Update()
    {
        // Si el juego está pausado, no hacemos nada
        if (Time.timeScale == 0f) return;

#if UNITY_STANDALONE || UNITY_EDITOR
        // Ignorar clics en la UI
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            direccion = Vector3.up * fuerza;
            volarAudio.Play();
        }

#elif UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Ignorar toques en la UI
            if (touch.phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                direccion = Vector3.up * fuerza;
                volarAudio.Play();
            }
        }
#endif

        direccion.y += gravedad * Time.deltaTime;
        transform.position += direccion * Time.deltaTime;

        // Rotación del pájaro
        Vector3 rotacion = transform.eulerAngles;
        rotacion.z = direccion.y * tilt;
        transform.eulerAngles = rotacion;
    }

private void AnimacionSprite()
    {
        spriteIndex++;

        if (spriteIndex >= sprites.Length)
        {
            spriteIndex = 0;
        }

        spriteRenderer.sprite = sprites[spriteIndex];
    }

    private void OnTriggerEnter2D(Collider2D objeto)
    {
        if (objeto.gameObject.tag == "Obstaculo")
        {
            FindObjectOfType<GameManager>().GameOver();
            gameOverAudio.Play();
        }
        else if (objeto.gameObject.tag == "Punto")
        {
            FindObjectOfType<GameManager>().GanarPunto();
            puntoAudio.Play();
        }
    }
}