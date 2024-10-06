using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public List<GameObject> objectsToSpawn;
    [SerializeField] public float minDelay = 3f;
    [SerializeField] public float maxDelay = 5f;
    private bool hasSpawned = false;

    private void Update()
    {
        if (transform.childCount == 0)
        {
            if (!hasSpawned)
            {
                StartCoroutine(SpawnObjectAfterDelay());
                hasSpawned = true;
            }
        }
        else
        {
            hasSpawned = false;
        }
    }

    private IEnumerator SpawnObjectAfterDelay()
    {
        float delay = Random.Range(minDelay, maxDelay);
        yield return new WaitForSeconds(delay);

        if (objectsToSpawn.Count > 0)
        {
            GameObject selectedObject = objectsToSpawn[Random.Range(0, objectsToSpawn.Count)];
            GameObject spawnedObject = Instantiate(selectedObject, transform.position, transform.rotation);
            spawnedObject.transform.parent = transform;
            spawnedObject.transform.localPosition = Vector3.zero;
        }
    }
}
