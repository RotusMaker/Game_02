using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PPoory;

public class Scene101DualGame : MonoBehaviour {

    public GameObject m_objSelectButtonRoot;    // 선택버튼 부모.
    [HideInInspector]
    public int m_nChoiceNumber = -1; // 선택한 번호.
    [HideInInspector]
    public int m_nAIChoiceNumber = -1;
    [HideInInspector]
    public int m_nScore = 0;
    [HideInInspector]
    public int m_nAIScore = 0;

    public Text m_textScoreBoard;   // 현재 상황판.

    private uFSM m_uFsm = null;

	// Use this for initialization
	void Awake () {
        if (m_uFsm == null)
            m_uFsm = this.GetComponent<uFSM>();
    }

    private void Start()
    {
        m_uFsm.Event("select");
    }

    public void RefreshScoreBoard()
    {
        m_textScoreBoard.text = string.Format("<현재 점수>\n나 {0}: 상대 {1}\n<게임 결과>\n나 {2}: 상대 {3}",
            m_nScore,m_nAIScore, GetRockScissorsPaperName(m_nChoiceNumber), GetRockScissorsPaperName(m_nAIChoiceNumber));
    }

    // 가위바위보 텍스트 리턴.
    string GetRockScissorsPaperName(int number)
    {
        string nickName = string.Empty;
        switch(number)
        {
            case 0: nickName = "바위"; break;
            case 1: nickName = "가위"; break;
            case 2: nickName = "보"; break;
        }

        return nickName;
    }
}
