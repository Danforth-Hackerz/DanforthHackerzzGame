using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class TimedKeyPress
{
    private static float time = 1f;
    private static float timeBuffer = 0.125f;

    [SerializeField] public KeyCode keyCode { get; private set; }
    [SerializeField] public Vector3 triggerPosition { get; private set; }

    public IEnumerator Show(Text text, Slider slider, Action<bool> callback)
    {
        //Animate the bar and text and wait the designated time

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