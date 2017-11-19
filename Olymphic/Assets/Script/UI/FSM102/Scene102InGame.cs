using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PPoory;

// 게임 룰
// 1.해설 진행.
// 2.상황발생.
// 3.3가지 찬스카드중 하나를 선택.
// 4.적용된 결과 나옴.
// 5.1-4 3번 반복후 최종 결과.

// 데이터
// 경기 종목, 참여국

public class NationRankInfo
{
	public int m_nNationID;	// 나라 아이디.
	public int m_nWinGauge;	// (경쟁형)승리게이지(100이 먼저되면 이긴다.)
	public int m_nScore;	// (포인트형)현재 점수.
}

public class Scene102InGame : MonoBehaviour 
{
	private uFSM m_uFsm = null;

	[Header("UI")]
	public RectTransform m_rtCommantary;
	public Text m_textCommantary;

	[HideInInspector]
	public int m_nSetCount = 0;	// 세트진행 횟수.
	[HideInInspector]
	public int m_nGameType = 0;	// 경기 종목.
	[HideInInspector]
	public List<NationRankInfo> m_listRanking = new List<NationRankInfo>();	// 현재 게임 점수.

	void Awake () 
	{
		if (m_uFsm == null)
			m_uFsm = this.GetComponent<uFSM>();

		m_textCommantary.text = string.Empty;
	}

	void Start()
	{
		StartCoroutine (OnStart ());
	}

	IEnumerator OnStart()
	{
		// UI 셋팅.
		// 시작 연출.
		
		yield return null;

		AddCommantary ("commantarycommantary");
		AddCommantary ("commantary");
		AddCommantary ("commantarycommantary");
		AddCommantary ("commantary");
		AddCommantary ("commantary");

		// 해설 발생.
		//m_uFsm.Event ("Astate");
	}

	public void AddCommantary(string commantary)
	{
		string text = m_textCommantary.text;
		if (string.IsNullOrEmpty (text)) {
			text = commantary;
		} 
		else {
			text = string.Format ("{0}\n{1}", text, commantary);
		}

		//Debug.Log(LayoutUtility.GetPreferredHeight(m_textCommantary.rectTransform) + " vs " + m_textCommantary.rectTransform.rect.height);
		m_textCommantary.text = text;
		//Debug.Log(LayoutUtility.GetPreferredHeight(m_textCommantary.rectTransform) + " vs " + m_textCommantary.rectTransform.rect.height);

		m_rtCommantary.sizeDelta = new Vector2 (m_rtCommantary.sizeDelta.x, LayoutUtility.GetPreferredHeight(m_textCommantary.rectTransform));
	}
}
