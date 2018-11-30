using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamNarrationController : MonoBehaviour
{
	void Start ()
    {
        StartCoroutine(NarrationHandler.instance.PlayLineDelayed(0, 3));
        StartCoroutine(NarrationHandler.instance.PlayLineDelayed(1, 26));
    }
}