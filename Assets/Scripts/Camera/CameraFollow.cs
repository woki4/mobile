using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    private float offset;
    private float startPosX;
    private float startPosY;
    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        offset = transform.position.z - player.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(startPosX, startPosY,player.position.z + offset);
    }
}
