using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalconyManager : Room
{
    [SerializeField] private GameObject obstacleContainer;
    [SerializeField] private TimedKeyPress[] events;
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject slider;

    private Vector3 containerStartPos;
    private const float speed = 10;
    private int currentEvent = 0;
    private bool isRunning;
    private float distance;

    public override void Show()
    {
        Debug.Log("Starting");
        base.Show();
        Restart();
    }

    public void Start()
    {
        containerStartPos = obstacleContainer.transform.position;
    }

    public void Restart()
    {
        obstacleContainer.transform.position = containerStartPos;
        isRunning = true;
        currentEvent = 0;
        distance = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            Debug.Log("going");
            obstacleContainer.transform.position += speed * Time.deltaTime * Vector3.right; //Change to Vector3.left if player is moving to the right
            distance += speed * Time.deltaTime;

            if (distance >= events[currentEvent].triggerPosition) //Position check
            {
                StartCoroutine(events[currentEvent].Show(text, slider, OnEventFinish));
            }
        }
    }

    private void OnEventFinish(bool successful, TimedKeyPress.ObstacleType type)
    {
        if (successful)
        {
            //Do Stuff

            currentEvent++;
        }
        else
        {
            //Do Stuff

            Restart();
        }
    }
}
