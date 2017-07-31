using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevel01Scene : MonoBehaviour 
{
	public enum eState
	{
		eNone,				// 초기화
		eLikeNationChoice,	// 응원하는 국가 선택
		eHateNationChoice,	// 저주하는 국가 선택
		eMissionOpen,		// 최종 미션 오픈
	}

	[Header("UI")]
	public Text m_txtOrder;	// 진행 문구  

	private eState m_eState = eState.eNone;
	private int m_nLikeNation = 0;
	private int m_nHateNation = 0;

	#region UnityFunc
	void Start()
	{
		SetState (eState.eLikeNationChoice);
	}
	#endregion


	#region CustomFunc
	private void SetState(eState state)
	{
		if (m_eState != state) 
		{
			m_eState = state;
			switch (m_eState) {
			case eState.eLikeNationChoice:
				m_txtOrder.text = "응원하는 국가를 고르시오.";	// 엑셀데이터로 빼기
				break;
			case eState.eHateNationChoice:
				m_txtOrder.text = "얄미운 국가를 고르시오.";
				break;
			case eState.eMissionOpen:
				m_txtOrder.text = "당신의 편파판정으로 게임에 개입하여 다음 순위를 만드시오.";
				break;
			}
		}
	}

	public void OnClickedNation(GameObject sender)
	{
		// 어떤 국가를 선택한 경우
		switch(m_eState)
		{
		case eState.eLikeNationChoice:
			m_nLikeNation = System.Convert.ToInt32 (sender.name);	// 넘어오는 오브젝트 이름은 국가코드로 약속.
			SetState(eState.eHateNationChoice);
			break;
		case eState.eHateNationChoice:
			m_nHateNation = System.Convert.ToInt32 (sender.name);
			SetState (eState.eMissionOpen);
			break;
		}
	}
	#endregion
}
