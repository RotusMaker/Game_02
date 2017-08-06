using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevel01Scene : MonoSingleton<SelectLevel01Scene>
{
	protected SelectLevel01Scene () {}	// Singletone use.

	public enum eState
	{
		eNone,				// 초기화
		eLikeNationChoice,	// 응원하는 국가 선택
		eHateNationChoice,	// 저주하는 국가 선택
		eMissionOpen,		// 최종 미션 오픈
	}

	[Header("UI")]
	public Text m_txtOrder;	// 진행 문구
	public Canvas m_canvas;	// UICanvas

	private eState m_eState = eState.eNone;
	private int m_nLikeNation = 0;
	private int m_nHateNation = 0;
	private GameObject m_objSelectPopup = null;
	private GameObject m_objMissionInfoPopup = null;

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
				PlayerPrefs_GameInfo.Instance.CreateMission (m_nLikeNation, m_nHateNation);
				//m_txtOrder.text = "당신의 편파판정으로 게임에 개입하여 다음 순위를 만드시오.";
				if (m_objMissionInfoPopup == null) {
					m_objMissionInfoPopup = FileLoader.Instance.LoadPrefab ("UI/mission_ntf_popup");
					m_objMissionInfoPopup.transform.parent = m_canvas.transform;
					m_objMissionInfoPopup.transform.localScale = Vector3.one;
					m_objMissionInfoPopup.GetComponent<RectTransform> ().offsetMax = Vector2.zero;
					m_objMissionInfoPopup.GetComponent<RectTransform> ().offsetMin = Vector2.zero;
					m_objMissionInfoPopup.GetComponent<RectTransform> ().sizeDelta = Vector2.zero;
				}
				m_objMissionInfoPopup.SetActive (true);

				ButtonPopup popup = m_objMissionInfoPopup.GetComponent<ButtonPopup> ();
				popup.CreateDictionary ();
				Nation_ScrollView_Popup scroll = popup.GetObj (ButtonPopup.eCustomObject.NationMission).GetComponent<Nation_ScrollView_Popup> ();
				scroll.InitData ();
				popup.GetText (ButtonPopup.eTextType.Title).text = string.Format("편파판정으로 다음 [{0}]개 국가 순위를 만드시오.",PlayerPrefs_GameInfo.Instance.GetMissionNationCount());
				popup.GetButton (ButtonPopup.eButtonType.OK).onClick.RemoveAllListeners ();
				popup.GetButton (ButtonPopup.eButtonType.OK).onClick.AddListener (()=>{
					m_objMissionInfoPopup.SetActive(false);
					// 씬 이동 넣기~
				});
				break;
			}
		}
	}

	public void OnClickedNation(NationData data)
	{
		// 어떤 국가를 선택한 경우
		ButtonPopup popup;
		switch(m_eState)
		{
		case eState.eLikeNationChoice:
			int likeNation = data.nationCode;	// 넘어오는 오브젝트 이름은 국가코드로 약속.
			if (m_objSelectPopup == null) {
				m_objSelectPopup = FileLoader.Instance.LoadPrefab ("UI/two_btn_popup");
				m_objSelectPopup.transform.parent = m_canvas.transform;
				m_objSelectPopup.transform.localScale = Vector3.one;
				m_objSelectPopup.GetComponent<RectTransform> ().offsetMax = Vector2.zero;
				m_objSelectPopup.GetComponent<RectTransform> ().offsetMin = Vector2.zero;
				m_objSelectPopup.GetComponent<RectTransform> ().sizeDelta = Vector2.zero;
			}
			m_objSelectPopup.SetActive (true);

			popup = m_objSelectPopup.GetComponent<ButtonPopup> ();
			popup.CreateDictionary ();
			popup.GetText (ButtonPopup.eTextType.Content_1).text = string.Format ("[{0}]를 선택하시겠습니까?", data.nationName);
			popup.GetButton (ButtonPopup.eButtonType.Yes).onClick.RemoveAllListeners ();
			popup.GetButton (ButtonPopup.eButtonType.Yes).onClick.AddListener (() => {
				m_objSelectPopup.SetActive(false);
				m_nLikeNation = likeNation;
				SetState (eState.eHateNationChoice);	
			});
			popup.GetButton (ButtonPopup.eButtonType.No).onClick.RemoveAllListeners ();
			popup.GetButton (ButtonPopup.eButtonType.No).onClick.AddListener (()=>{
				m_objSelectPopup.SetActive(false);
			});
			break;
		case eState.eHateNationChoice:
			int hateNation = data.nationCode;
			if (m_objSelectPopup == null) {
				m_objSelectPopup = FileLoader.Instance.LoadPrefab ("UI/two_btn_popup");
				m_objSelectPopup.transform.parent = m_canvas.transform;
				m_objSelectPopup.transform.localScale = Vector3.one;
				m_objSelectPopup.GetComponent<RectTransform> ().offsetMax = Vector2.zero;
				m_objSelectPopup.GetComponent<RectTransform> ().offsetMin = Vector2.zero;
				m_objSelectPopup.GetComponent<RectTransform> ().sizeDelta = Vector2.zero;
			}
			m_objSelectPopup.SetActive (true);

			popup = m_objSelectPopup.GetComponent<ButtonPopup> ();
			popup.CreateDictionary ();
			popup.GetText (ButtonPopup.eTextType.Content_1).text = string.Format("[{0}]를 선택하시겠습니까?",data.nationName);
			popup.GetButton (ButtonPopup.eButtonType.Yes).onClick.RemoveAllListeners ();
			popup.GetButton (ButtonPopup.eButtonType.Yes).onClick.AddListener (()=>{
				m_objSelectPopup.SetActive(false);
				m_nHateNation = hateNation;
				SetState (eState.eMissionOpen);
			});
			popup.GetButton (ButtonPopup.eButtonType.No).onClick.RemoveAllListeners ();
			popup.GetButton (ButtonPopup.eButtonType.No).onClick.AddListener (()=>{
				m_objSelectPopup.SetActive(false);
			});
			break;
		}
	}
	#endregion
}
