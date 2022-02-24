using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class TimedKeyPress
{
    public enum ObstacleType
    {
        JumpOver,
        JumpUp,
        Slide,
        Drop
    }

    private static float time = 2f;
    private static float timeBuffer = 0.25f;

    public KeyCode keyCode;
    public float triggerPosition;
    public ObstacleType obstacleType;

    public IEnumerator Show(GameObject text, GameObject slider, Action<bool, ObstacleType> callback)
    {
        //Animate the bar and text and wait the designated time
        slider.gameObject.SetActive(true);
        text.GetComponent<Text>().text = keyCode.ToString();

        Animator sliderAnimator = slider.GetComponent<Animator>();
        Animator textAnimator = text.GetComponent<Animator>();

        sliderAnimator.speed = 1 / time;



        sliderAnimator.SetTrigger("Reset");
        textAnimator.SetTrigger("Reset");
        sliderAnimator.SetTrigger("Play");
        textAnimator.SetTrigger("Play");


        yield return new WaitForSeconds(time - (timeBuffer / 2));


        Debug.Log("Buffer Start");
        
        //Set the text to glow and wait for input
        float counter = 0;
        bool successful = false;

        //Debug.Log(counter);

        //Changed so that the callback is always at the end
        while (counter <= timeBuffer)
        {
            if (Input.GetKey(keyCode) && !successful)
            {
                successful = true;
            }

            //Debug.Log(counter);

            counter += Time.deltaTime;
            yield return null;
        }

        //Debug.Log("Buffer End");

        callback(successful, obstacleType);
    }
}