using UnityEngine;

public class AnimacionLayout : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public float velocidadAnimacion = 1f;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        meshRenderer.material.mainTextureOffset += new Vector2(velocidadAnimacion * Time.deltaTime, 0);
    }
}
