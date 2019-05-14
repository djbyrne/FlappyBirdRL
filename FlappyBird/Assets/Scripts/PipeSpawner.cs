using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public float maxTime = 100;
    private float timer = 0;
    public GameObject pipe;
    public float height = 0.5f;
    public float nextPipeHeight;
    private GameObject nextPipe;
    private GameObject[] pipes;
    private Queue<GameObject> nextPipes;

    // Start is called before the first frame update
    void Awake()
    {
        nextPipes = new Queue<GameObject>();
        SpawnPipe();
    }

    // Update is called once per frame
    void Update()
    {
        if( timer > maxTime)
        {
            SpawnPipe();
            timer = 0;
        }

        timer += Time.deltaTime;

    }

    public void SpawnPipe()
    {
        nextPipeHeight = Random.Range(-0.2f, 0.4f);
        //Debug.Log(nextPipeHeight);
        nextPipe = Instantiate(pipe);
        nextPipe.transform.position = transform.position + new Vector3(
            0, nextPipeHeight, 0);

        // add to queue
        nextPipes.Enqueue(nextPipe);
        Destroy(nextPipe, 10);
    }

    public float GetNextPipeHeight()
    {
        return nextPipeHeight;
    }

    public GameObject GetNextPipe()
    { 
        if(nextPipes.Count > 0)
        {
            return nextPipes.Dequeue();
        }
        else
        {
            return null;
        }

    }

    //public Vector3 GetPipePosition()
    //{
    //    return pipe.transform.position;
    //}

    public void ResetPipes()
    {
        //foreach (GameObject np in nextPipes)
        //{
        //    Destroy(np);
        //}
        pipes = GameObject.FindGameObjectsWithTag("pipe");
        foreach(GameObject p in pipes)
        {
            Destroy(p);
        }
        nextPipes.Clear();
        timer = 0;

    }
}
