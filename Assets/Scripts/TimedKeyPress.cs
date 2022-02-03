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
    public Vector3 triggerPosition;
    public ObstacleType obstacleType;

    public IEnumerator Show(Text text, GameObject slider, Animator animator, Action<bool> callback)
    {
        //Animate the bar and text and wait the designated time
        slider.gameObject.SetActive(true);

        text.text = keyCode.ToString();
        yield return new WaitForSeconds(time - timeBuffer);

        //Set the text to glow and wait for input
        float counter = 0;

        while (counter <= timeBuffer)
        {
            if (Input.GetKey(keyCode))
            {
                callback(true);
                yield break;
            }

            counter += Time.deltaTime;
            yield return null;
        }

        callback(false);
    }
}