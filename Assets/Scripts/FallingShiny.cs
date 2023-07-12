using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingShiny : MonoBehaviour
{
    public Movement_AI player;
    CapsuleCollider area;

    private void Start()
    {
        area = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        OnTriggerEnter(area);
    }

    private void OnTriggerEnter(Collider area)
    {
        if (area.gameObject.CompareTag("Platform"))
        {
            player.collectibles.Remove(this);
            Destroy(this.gameObject);
        }
        else
        {
            if (area.gameObject.CompareTag("Player"))
            {
                player.collectibles.Remove(this);
                Destroy(this.gameObject);
            }
        }

    }
}
