using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private GameObject cam;

    [SerializeField] private float parallaxEffectX;
    [SerializeField] private float parallaxEffectY;

    private float xPosition;
    private float yPosition;
    private float length;

    void Start()
    {
        cam = GameObject.Find("Main Camera");

        length = GetComponent<SpriteRenderer>().bounds.size.x;
        xPosition = transform.position.x;
        yPosition = transform.position.y;
    }

    void Update()
    {
        float distanceMoved = cam.transform.position.x * (1 - parallaxEffectX);
        float distanceToMove = cam.transform.position.x * parallaxEffectX;

    //    float distanceMovedY = cam.transform.position.y * (1 - parallaxEffectY);
        float distanceToMoveY = cam.transform.position.y * parallaxEffectY;

        transform.position = new Vector3(xPosition + distanceToMove, yPosition+ distanceToMoveY);


        if (distanceMoved > xPosition + length)
            xPosition = xPosition + length;
        else if (distanceMoved < xPosition - length)
            xPosition = xPosition - length;

    }
}
