
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using PPoory;

public class SkipButton : MonoBehaviour
{
    public int nextSceneNumber = 0;

    public void OnClickSkip()
    {
        uFSM fsm = GetComponent<uFSM>();

        if (null == fsm.currState)
        {
            Debug.LogWarning("Current State is NULL !!");
            return;
        }

        fsm.Event("skip");
    }
}

