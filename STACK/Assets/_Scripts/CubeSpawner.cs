using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField]
    private MovingCube _cubePrefab;

    [SerializeField]
    private MoveDirection moveDirection;


    public void SpawnCube()
    {
        var _cube = Instantiate(_cubePrefab);
        if (MovingCube.lastCube != null && MovingCube.lastCube.gameObject != GameObject.Find("StartCube"))
        {
            float x = moveDirection == MoveDirection.X ? transform.position.x : MovingCube.lastCube.transform.position.x;
            float z = moveDirection == MoveDirection.Z ? transform.position.z : MovingCube.lastCube.transform.position.z;

            _cube.transform.position = new Vector3(x,
            MovingCube.lastCube.transform.position.y + _cubePrefab.transform.localScale.y,
          z);

        }
        else
        {
            _cube.transform.position = transform.position;
        }

        _cube.MoveDirection = moveDirection;


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, _cubePrefab.transform.localScale);
    }



}

