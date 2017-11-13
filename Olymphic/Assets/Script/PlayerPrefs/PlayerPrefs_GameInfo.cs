using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임시작 시 게임타입 정보.
public class GameStartInfo
{
	public eGameType m_eGameType;	// 종목
	public List<eNation> m_listJoinNation;	// 참여국
}

// 가공가능한 데이터는 'Info' 접미사 사용
// 저장, 불러오기가 가능해야함
public class PlayerPrefs_GameInfo : Singleton<PlayerPrefs_GameInfo>
{
	public GameStartInfo m_gameStartInfo = new GameStartInfo ();	// 게임시작정보.
	
	private int m_nGameLevel = 0;
	private Dictionary<int/*국가코드*/, int/*순위*/> m_dicMissionRanking = new Dictionary<int, int>();

	protected PlayerPrefs_GameInfo () {}	// Singletone use.

	// 게임 미션 난이도
	public int GameLevel
	{
		get
		{
			m_nGameLevel = PlayerPrefs.GetInt ("GameLevel", 0);
			return m_nGameLevel;
		}
		set
		{
			m_nGameLevel = value;
			PlayerPrefs.SetInt ("GameLevel", m_nGameLevel);
		}
	}
	/*
	// 게임 미션 생성
	public void CreateMission(int likeNation, int hateNation)
	{
		// 로드해야할 데이터.
		JSON_NationInfo.Instance.LoadJSON ();
		
		m_dicMissionRanking.Clear ();
		m_dicMissionRanking.Add (likeNation, 1);
		m_dicMissionRanking.Add (hateNation, JSON_NationInfo.Instance.GetNationCount());

		// 미션에 뽑힐 국가 선정
		List<int> m_listNationCode = new List<int>();
		for (Dictionary<int,NationData>.Enumerator it = JSON_NationInfo.Instance.m_dicNationData.GetEnumerator (); it.MoveNext ();) {
			m_listNationCode.Add (it.Current.Key);
		}
		m_listNationCode.Remove (likeNation);
		m_listNationCode.Remove (hateNation);

		List<int> m_listRank = new List<int> ();
		for (int i = 0; i < JSON_NationInfo.Instance.GetNationCount (); i++) {
			m_listRank.Add (i + 1);
		}
		m_listRank.Remove (1);
		m_listRank.Remove (JSON_NationInfo.Instance.GetNationCount ());

		// GameLevel 0부터니깐 1더해줌
		for (int i = 0; i < GameLevel+1; i++) {
			int nRandomIdx = Random.Range (0, m_listNationCode.Count);
			int nRandomNationCode = m_listNationCode [nRandomIdx];
			int nRandomRankIdx = Random.Range (0, m_listRank.Count);
			int nRandomRank = m_listRank [nRandomRankIdx];
			m_dicMissionRanking.Add (nRandomNationCode, nRandomRank);

			m_listNationCode.Remove (nRandomNationCode);
			m_listRank.Remove (nRandomRank);
		}

		// 미션 국가 데이터 완성!
		string strDebug = "# Mission ";
		for (Dictionary<int,int>.Enumerator it = m_dicMissionRanking.GetEnumerator (); it.MoveNext ();) {
			strDebug += string.Format (",Nation:{0}Rank:{1}",it.Current.Key,it.Current.Value);
		}
		Debug.Log (strDebug);
	}

	public int GetRankFromNationcode(int code)
	{
		if (m_dicMissionRanking.ContainsKey (code)) {
			return m_dicMissionRanking [code];
		}
		return -1;
	}

	public int GetMissionNationCount()
	{
		return m_dicMissionRanking.Count;
	}

	public bool IsMissionNation(int code)
	{
		return m_dicMissionRanking.ContainsKey(code);
	}
	*/
}
