using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class Movement_AI : Agent
{

    Vector3 original_Position;
    public float movespeed = 2f;
    public Transform player { get; private set; }
    public int current;

    public BufferSensorComponent Senses;
    public List<FallingOuch> spikes;
    public List<FallingShiny> collectibles;

    public void Start()
    {
        original_Position = transform.localPosition;
        spikes = new List<FallingOuch>();
        collectibles = new List<FallingShiny>();
    }

    public override void OnEpisodeBegin()
    {
        transform.localPosition = original_Position;
        //End of Episode receives this 
        SetReward(1f);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);

        foreach(FallingOuch spike in spikes)
        {
            float[] info = {spike.transform.position.x };
            Senses.AppendObservation(info);
        }

        foreach(FallingShiny shiny in collectibles)
        {
            float[] info2 = { shiny.transform.position.x };
            Senses.AppendObservation(info2);
        }
    }


    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveZ = actions.ContinuousActions[0];
        transform.localPosition += new Vector3(0, 0, moveZ) * Time.deltaTime * movespeed;
    }

    //Being Penalised for hitting Wall or Spikes(Cargo)
    public void OnTriggerEnter(Collider damage)
    {
        if (damage.CompareTag("Wall")) 
        {
            AddReward(-0.2f);
            transform.localPosition = original_Position;
            EndEpisode();
        }
        if (damage.CompareTag("Spike"))
        {
            AddReward(-0.3f);        
            transform.localPosition = original_Position;
            EndEpisode();
        }
        if (damage.CompareTag("Shiny"))
        {
            AddReward(+0.2f);
        }
    }

    //Manual Input
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxis("Horizontal") ;
    }


}
