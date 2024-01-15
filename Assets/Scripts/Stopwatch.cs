using UnityEngine;
using UnityEngine.UI;

public class Stopwatch : MonoBehaviour
{
    private float startTime;
    private float elapsedTime;
    private bool hasStartedLap = false;
    public GameObject fastest, player;

    void Update()
    {
        if (hasStartedLap) {
            elapsedTime = Time.time - startTime;
            player.GetComponent<Text>().text = elapsedTime.ToString("F2");
        }
    }

    private void OnTriggerEnter(Collider other) {
        hasStartedLap = true;
        startTime = Time.time;
    }
}
