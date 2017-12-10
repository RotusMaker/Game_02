using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene000Common : MonoBehaviour {

    private void Start()
    {
		// 데이터 로드는 타이틀에서 하자.
		MainFrame.instance.NextScene(1, 2f);
    }
}
