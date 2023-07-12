using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingOuch : MonoBehaviour
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
        if(area.gameObject.CompareTag("Platform"))
        {
            player.spikes.Remove(this);
            Destroy(this.gameObject);
        }
        else
        {
            if (area.gameObject.CompareTag("Player"))
            {
                player.spikes.Remove(this);
                Destroy(this.gameObject);
            }
        }
        
    }
}
