using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;
    public Rigidbody rb;
    public float cameraX = 10f;
    public float cameraY = 10f;
    public float cameraZ = -10f;
    private Vector3 pos;
    void Update() {
        pos.x += cameraX + rb.velocity.normalized.x * 50f;
        pos.y += cameraY + rb.velocity.normalized.z * 50f;
        pos.z += cameraZ + rb.velocity.normalized.z * 50f;
        transform.position = player.transform.position + new Vector3(cameraX, cameraY, cameraZ);
    }
}