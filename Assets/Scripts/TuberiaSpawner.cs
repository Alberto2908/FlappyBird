using UnityEngine;

public class TuberiaSpawner : MonoBehaviour
{
    public GameManager manager;
    public GameObject prefab;
    public GameObjectPool tuberiasPool;
    public float spawnRate = 1f;
    public float minHeight = -1f;
    public float maxHeigth = 1f;

    private void OnEnable()
    {
        StartSpawning();
    }

    private void OnDisable()
    {
        StopSpawning();
    }

    private void StartSpawning()
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    private void StopSpawning()
    {
        CancelInvoke(nameof(Spawn));
    }

    public void RestartSpawner()
    {
        StopSpawning();
        StartSpawning();
    }

    private void Spawn()
    {
        /*if (manager.NightActive())
        {
            GameObject tuberia = tuberiasPool.GetAvailableObject();
            if (tuberia != null)
            {
                tuberia.transform.position = transform.position + Vector3.up * Random.Range(minHeight, maxHeigth);
                tuberia.SetActive(true);
            }
        } else
        {
            GameObject tuberiaNight = tuberiasPool.GetAvailableObject();
            if (tuberiaNight != null)
            {
                tuberiaNight.transform.position = transform.position + Vector3.up * Random.Range(minHeight, maxHeigth);
                tuberiaNight.SetActive(true);
            }
        }*/
        GameObject tuberia = tuberiasPool.GetAvailableObject();
        if (tuberia != null)
        {
            tuberia.transform.position = transform.position + Vector3.up * Random.Range(minHeight, maxHeigth);
            tuberia.SetActive(true);
        }
    }
}

