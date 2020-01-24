using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private CubeSpawner[] spawners;
    private int spawner_index;
    private CubeSpawner currentSpawner;
    public Score _score;

    private void Awake()
    {
        spawners = FindObjectsOfType<CubeSpawner>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(MovingCube.currentCube!=null)
            {
                MovingCube.currentCube.StopCube();
                
            }

            spawner_index = spawner_index == 0 ? 1 : 0;
            currentSpawner = spawners[spawner_index];
            currentSpawner.SpawnCube();
            _score.IncreaseScore();


        }
    }



}//game manager
