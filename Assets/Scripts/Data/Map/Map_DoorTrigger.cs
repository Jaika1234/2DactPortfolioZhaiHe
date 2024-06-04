using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_DoorTrigger : MonoBehaviour
{
    [SerializeField] private GameObject Door1;
    [SerializeField] private GameObject Door2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (Door1 != null)
                Door1.SetActive(true);

            if (Door2 != null)
                Door2.SetActive(true);
        }
    }

}
