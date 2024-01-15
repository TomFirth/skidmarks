using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject car;

    void Start()
    {
        Instantiate(car, new Vector3(0, 2, 2), transform.rotation * Quaternion.Euler(0, 0, 0));
        // pick colour
    }
}
