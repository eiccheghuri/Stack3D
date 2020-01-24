using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingCube : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 1f;

    public static MovingCube currentCube { get; private set; }
    public static MovingCube lastCube { get; private set; }
    public MoveDirection MoveDirection { get; set; }

    private void OnEnable()
    {
        if (lastCube == null)
        {
            lastCube = GameObject.FindGameObjectWithTag("StartCube").GetComponent<MovingCube>();

        }

        currentCube = this;
        GetComponent<Renderer>().material.color = GetRandomColor();
        transform.localScale = new Vector3(lastCube.transform.localScale.x, transform.localScale.y, lastCube.transform.localScale.z);



    }

    public void Update()
    {
        if (MoveDirection == MoveDirection.Z)
        {
            transform.position = transform.position + transform.forward * Time.deltaTime * _moveSpeed;
        }
        else
        {
            transform.position = transform.position + transform.right * Time.deltaTime * _moveSpeed;
        }

    }

    public float HangOver()
    {
        if(MoveDirection==MoveDirection.Z)
        {
            return transform.position.z - lastCube.transform.position.z;
        }
        else
        {
            return transform.position.x - lastCube.transform.position.x;
        }
       
    }

    public void StopCube()
    {
        _moveSpeed = 0f;
        float _hangOver = HangOver();

        float max = MoveDirection == MoveDirection.Z ? lastCube.transform.localScale.z : lastCube.transform.localScale.x;

        if (Mathf.Abs(_hangOver) > max)
        {
            lastCube = null;
            currentCube = null;
            SceneManager.LoadScene(0);

        }


        float _direction = _hangOver > 0 ? 1f : -1f;


        if (MoveDirection == MoveDirection.X)
        {
            SplitCubeOnX(_hangOver, _direction);
        }
        else
        {
            SplitCubeOnZ(_hangOver, _direction);
        }

        lastCube = this;

    }

    public Color GetRandomColor()
    {
        Color _newColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        return _newColor;


    }

    public void SplitCubeOnX(float hangover, float direction)
    {
        if(lastCube==null)
        {
            return;
        }

        float _newXSize = lastCube.transform.localScale.x - Mathf.Abs(hangover);//size of the stopted block
        float _fallingBlockSize = transform.localScale.x - (_newXSize);//extra block size
        float _newXPosition = lastCube.transform.position.x + (hangover / 2);//new position for the stopped block
        transform.localScale = new Vector3((_newXSize), transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(_newXPosition, transform.position.y, transform.position.z);


        float _cubeEdge = transform.position.x + (_newXSize / 2f * direction);
        float _fallingBlockXPosition = _cubeEdge + (_fallingBlockSize / 2 * direction);



        SpawnDropCube(_fallingBlockXPosition, _fallingBlockSize);

    }

    public void SplitCubeOnZ(float hangover, float direction)
    {

        if (lastCube == null)
        {
            return;
        }

        float _newZSize = lastCube.transform.localScale.z - Mathf.Abs(hangover);//size of the stopted block
        float _fallingBlockSize = transform.localScale.z - (_newZSize);//extra block size
        float _newZPosition = lastCube.transform.position.z + (hangover / 2);//new position for the stopped block
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, (_newZSize));
        transform.position = new Vector3(transform.position.x, transform.position.y, _newZPosition);


        float _cubeEdge = transform.position.z + (_newZSize / 2f * direction);
        float _fallingBlockZPosition = _cubeEdge + (_fallingBlockSize / 2 * direction);



        SpawnDropCube(_fallingBlockZPosition, _fallingBlockSize);

    }

    public void SpawnDropCube(float _fallingBlockPosition, float _fallingBlockSize)
    {
        var _cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        if (MoveDirection == MoveDirection.Z)
        {
            _cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, _fallingBlockSize);
            _cube.transform.position = new Vector3(transform.position.x, transform.position.y, _fallingBlockPosition);
        }
        else
        {
            _cube.transform.localScale = new Vector3(_fallingBlockSize, transform.localScale.y, transform.localScale.z);
            _cube.transform.position = new Vector3(_fallingBlockPosition, transform.position.y, transform.position.z);
        }



        _cube.AddComponent<Rigidbody>();
        _cube.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
        Destroy(_cube.gameObject, 1f);
    }




}//moving view class
