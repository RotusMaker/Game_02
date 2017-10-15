using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene000Common : MonoBehaviour {

    private void Start()
    {
        MainFrame.instance.NextScene(10, 2f);
    }
}
