using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Movement_AI Player;
    public GameObject prefab;
    public GameObject prefab2;
    public float timer;
    public float maxtimer;
    public float timerS;
    public float MTimerS;
    public bool timerON = false;

    private void Start()
    {
        timerON = true;
    }
    void Update()
    {
        if (timerON == true)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                UpdateTimer(timer);
            }
            if (timerS > 0)
            {
                timerS -= Time.deltaTime;
                UpdateTimer(timerS);
            }

            if (timer <= 0)
            {
                Vector3 randomizePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + Random.Range(-2, 2));
                GameObject obj = Instantiate(prefab);
                //obj.transform.position = randomizePosition;
                FallingOuch spike = obj.GetComponent<FallingOuch>();
                spike.transform.position = randomizePosition;
                spike.player = Player;
                Player.spikes.Add(spike);
                timer = maxtimer;
            }
            if (timerS <= 0)
            {
                Vector3  ShiniePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + Random.Range(-2, -2));
                GameObject SObj = Instantiate(prefab2);
                FallingShiny shiny = SObj.GetComponent<FallingShiny>();
                shiny.transform.position = ShiniePosition;
                shiny.player = Player;
                Player.collectibles.Add(shiny);
                timerS = MTimerS;
            }

        }
    }

    void UpdateTimer(float currenttime)
    {
        currenttime += 1;
        float minutes = Mathf.FloorToInt(currenttime / 60);
        float seconds = Mathf.FloorToInt(currenttime % 60);
    }

}
