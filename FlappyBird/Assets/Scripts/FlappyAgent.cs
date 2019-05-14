using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using UnityEngine.SceneManagement;


public class FlappyAgent : Agent 
{
    public float velocity = 1.4F;
    private new Rigidbody2D rigidbody;
    public GameManager gameManager;
    public PipeSpawner pipeSpawner;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private GameObject nextPipe;
    private Vector3 pipe_pos;
    private bool dead;
    public int counter;
    private int last_act;

    // Start is called before the first frame update
    void Start()
    {
        dead = false;
        counter = 0;
        last_act = 0;
        this.originalPosition = this.transform.position;

        rigidbody = GetComponent<Rigidbody2D>();
        // get first pipe
        nextPipe = pipeSpawner.GetNextPipe();

    }

    // Update is called once per frame
    void Update()
    {
        if(nextPipe == null)
        {
            nextPipe = pipeSpawner.GetNextPipe();
        }

    }

    void Flap()
    {
        rigidbody.velocity = Vector2.up * velocity;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        //set reward for getting passed checkpoint
        counter++;
        AddReward(1f);
        nextPipe = pipeSpawner.GetNextPipe();

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        ////Debug.Log("Done");
        dead = true;
        //Done();
        //SetReward(-1.0f);
    }

    public override void AgentReset()
    {
        pipeSpawner.ResetPipes();
        nextPipe = null;

        //reset the agent
        this.transform.position = this.originalPosition;
        dead = false;
        counter = 0;
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
       
        //check if dead
        if (dead)
        {
            SetReward(-1f);
            Done();
        }
        else
        {
            AddReward(0.1f);
            //Action size = 1: Flap/Do NothingD
            int action = Mathf.FloorToInt(vectorAction[0]);
            if (action == 1 && last_act == 0)
            {
                Flap();
                last_act = 1;
            }
            else
            {
                last_act = 0;
            }
        }
    }

    public override void CollectObservations()
    {

        //height of the bird 1.14, -0.64
        float s1 = Normalize(1.2f, -0.64f, this.transform.position.y);
        //float s1 = Normalize(1.2f, -0.64f, this.transform.localPosition.y);
        //Debug.Log("s1: " + s1);
        AddVectorObs(s1);

        //velocity
        float y_velocity = Normalize(0.9f, -4.6f, rigidbody.velocity.y);
        //Debug.Log(y_velocity);
        AddVectorObs(y_velocity);

        //last action
        AddVectorObs(last_act);

        if (nextPipe != null)
        {

            //height of next pipe 0.5, -0.5
            pipe_pos = nextPipe.GetComponentInChildren<Score>().GetPosition();
            float pipeHeight = pipe_pos.y;



            //height of the top pipe, height + 0.35
            float top_height = Normalize(0.75f, 0.15f, pipeHeight + 0.35f);
            //Debug.Log("tp: " + top_height);
            AddVectorObs(top_height);

            //height of the bottom pipe, height - 0.4
            float bot_height = Normalize(0.05f, -0.55f, pipeHeight - 0.35f);
            //Debug.Log("bp: " + bot_height);
            AddVectorObs(bot_height);

            float s3 = Normalize(1f, 0f, GetDistanceToNextPipe());
            //Debug.Log("s3: " + s3);
            AddVectorObs(s3);
        }
        else
        {
            //top pipe
            AddVectorObs(0);
            //bot pipe
            AddVectorObs(0);
            //distance
            AddVectorObs(0);
        }

    }

    public float GetDistanceToNextPipe()
    {
        return nextPipe.transform.position.x - this.transform.position.x;
        //return pipe_pos.x - this.transform.localPosition.x;
    }

    public float Normalize(float max, float min, float value)
    {
        //current - min
        return (value - min) / (max - min); 
    }


}
