using System.Collections;
using UnityEngine;

//Static class to store methods for multiple animations
public static class Animations
{
    /*
    //Method to transition the bloom of a collectable item
    public static IEnumerator TransitionBloom(CollectableItem item, float startIntensity, float intensity, float time)
    {
        float intensityDiffPerUpdate = (intensity - startIntensity) / (time / Time.fixedDeltaTime); //Stores the intensity difference per update
        float timeCount = 0;

        //Loops while there is still time left in the animation
        while(timeCount <  time)
        {
            //Updates the intensity
            startIntensity += intensityDiffPerUpdate;
            item.SetIntensity(startIntensity);

            //Waits the fixed time update
            timeCount += Time.fixedDeltaTime;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }

        item.SetIntensity(intensity); //Sets the intensity if it was slightly off in the loop
    }
    */
}