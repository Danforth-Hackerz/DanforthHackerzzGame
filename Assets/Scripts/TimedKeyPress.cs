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

    //private const float time = 2f;
    //private const float timeBuffer = 0.25f;
    private const float textFadeInTime = 0.5f;
    private const float pressKeyTime = 0.5f;
    private const string textMessagePrefix = "Press ";
    private const string textMessageSuffix = "!";

    public KeyCode keyCode;
    public float triggerPosition;
    public ObstacleType obstacleType;

    public IEnumerator Show(GameObject text, GameObject slider, Action<bool, ObstacleType> callback)
    {
        //Animate the bar and text and wait the designated time

        text.GetComponent<Text>().text = textMessagePrefix + keyCode.ToString() + textMessageSuffix;
        text.SetActive(true);
        Animator textAnimator = text.GetComponent<Animator>();
        textAnimator.speed = 1 / textFadeInTime;
        textAnimator.SetTrigger("Play");

        yield return new WaitForSeconds(textFadeInTime);

        //Shows the slider and starts the time

        Animator sliderAnimator = slider.GetComponent<Animator>();
        sliderAnimator.speed = 1 / pressKeyTime;
        slider.SetActive(true);
        sliderAnimator.SetTrigger("Play");

        Debug.Log("Buffer Start");
        
        //Set the text to glow and wait for input
        float counter = 0;
        bool successful = false;

        //Debug.Log(counter);

        //Changed so that the callback is always at the end
        while (counter <= pressKeyTime)
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

        text.SetActive(false);
        slider.SetActive(false);
        sliderAnimator.SetTrigger("Reset");
        textAnimator.SetTrigger("Reset");
        callback(successful, obstacleType);
    }
}