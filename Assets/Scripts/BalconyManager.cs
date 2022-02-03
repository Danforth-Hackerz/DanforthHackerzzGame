using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalconyManager : MonoBehaviour
{
    [SerializeField] private readonly GameObject obstacleContainer;
    [SerializeField] private readonly GameObject player;
    [SerializeField] private readonly TimedKeyPress[] events;
    [SerializeField] private readonly GameObject text;
    [SerializeField] private readonly GameObject slider;

    Vector3 containerStartPos;
    private const float speed = 10;
    private int currentEvent = 0;
    private bool isRunning;
    private float distance;

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
            obstacleContainer.transform.position += speed * Time.deltaTime * Vector3.right; //Change to left if player if moving to the right
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
