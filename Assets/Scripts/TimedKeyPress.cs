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

    private static float time = 1f;
    private static float timeBuffer = 0.125f;

    public KeyCode keyCode;
    public float triggerPosition;
    public ObstacleType obstacleType;

    public IEnumerator Show(GameObject text, GameObject slider, Action<bool, ObstacleType> callback)
    {
        //Animate the bar and text and wait the designated time
        slider.gameObject.SetActive(true);
        text.GetComponent<Text>().text = keyCode.ToString();
        slider.GetComponent<Animator>().SetTrigger("Play");
        text.GetComponent<Animator>().SetTrigger("Play");

        yield return new WaitForSeconds(time - timeBuffer);

        //Set the text to glow and wait for input
        float counter = 0;
        bool successful = false;

        //Changed so that the callback is always at the end
        while (counter <= timeBuffer)
        {
            if (Input.GetKey(keyCode) && !successful)
            {
                successful = true;
            }

            counter += Time.deltaTime;
            yield return null;
        }

        callback(successful, obstacleType);
    }
}