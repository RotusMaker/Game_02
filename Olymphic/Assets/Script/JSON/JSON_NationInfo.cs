using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

// 접미사 'Data' 는 외부에서 온 데이터로 가공하지 말것.
public class NationData
{
	public int nationCode;		// 국가코드
	public string spriteName;	// 국기 이미지 이름
	public string nationName;	// 국가명
	public int bestSportsEvent;	// 주종목
}

// 싱글톤 선언하기
public class JSON_NationInfo : Singleton<JSON_NationInfo> 
{
	public Dictionary<int, NationData> m_dicNationData = null;

	protected JSON_NationInfo () {}	// Singletone use.

	public void LoadJSON()
	{
		if (m_dicNationData == null) 
		{
			m_dicNationData = new Dictionary<int, NationData> ();

			string aJSON = FileLoader.Instance.LoadTextAssetToResources ("NationInfo");
			JSONNode root = JSON.Parse (aJSON);
			JSONNode node = root [0];
			Debug.Log (node.Count.ToString ());
			for (int i = 0; i < node.Count; i++) 
			{
				JSONNode childNode = node [i];
				NationData data = new NationData ();
				data.nationCode = childNode ["code"].AsInt;
				data.nationName = childNode ["name"];
				data.spriteName = childNode ["sprite"];
				data.bestSportsEvent = childNode ["best_sports_event"].AsInt;

				Debug.Log (string.Format("@{0},{1},{2},{3}",data.nationCode,data.nationName,data.spriteName,data.bestSportsEvent));
				m_dicNationData.Add (data.nationCode,data);
			}
		}
	}

	// 현재 데이터상 나라 숫자
	public int GetNationCount()
	{
		if (m_dicNationData != null) 
		{
			return m_dicNationData.Count;
		}
		return -1;
	}
}
