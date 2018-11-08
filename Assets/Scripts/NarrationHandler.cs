using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class NarrationHandler : MonoBehaviour {

    public static NarrationHandler instance;

    AudioSource player;

    public AudioClip[] narrationLines;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        player = GetComponent<AudioSource>();
    }

    public void PlayLine(int index) //index is the line to play
    {
        if(index >= narrationLines.Length)
        {
            Debug.LogError("Index " + index + " is out of bounds for Narration Handler");
        }
        else
        {
            player.PlayOneShot(narrationLines[index]);
        }

    }

    public IEnumerator PlayLineDelayed(int index, int delayTime) //bit of l
    {
        if (index >= narrationLines.Length)
        {
            Debug.LogError("Index " + index + " is out of bounds for Narration Handler");
        }
        else
        {
            yield return new WaitForSeconds(delayTime);
            player.PlayOneShot(narrationLines[index]);
        }
    }
}
