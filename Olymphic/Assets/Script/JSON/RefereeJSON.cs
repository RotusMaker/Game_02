using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

// 접미사 'Data' 는 외부에서 온 데이터로 가공하지 말것.
public class RefereeData
{
	public int id;
	public string name;
	public int nationID;
	public string desc;
}

// 싱글톤 선언하기
public class RefereeJSON : Singleton<RefereeJSON> 
{
	public Dictionary<int, RefereeData> m_dicRefereeData = null;

	protected RefereeJSON () {}	// Singletone use.

	public void LoadJSON()
	{
		if (m_dicRefereeData == null) 
		{
			m_dicRefereeData = new Dictionary<int, RefereeData> ();

			string aJSON = FileLoader.Instance.LoadTextAssetToResources ("referee_data");
			JSONNode root = JSON.Parse (aJSON);
			JSONNode node = root [0];
			Debug.Log (node.Count.ToString ());
			for (int i = 0; i < node.Count; i++) 
			{
				JSONNode childNode = node [i];
				RefereeData data = new RefereeData ();
				data.id = childNode ["id"].AsInt;
				data.name = childNode ["name"];
				data.nationID = childNode ["nation"].AsInt;
				data.desc = childNode ["desc"];

				Debug.Log (string.Format("@{0},{1},{2},{3}",data.id,data.name,data.nationID,data.desc));
				m_dicRefereeData.Add (data.id,data);
			}
		}
	}

	public RefereeData GetReferee(int id)
	{
		if (m_dicRefereeData != null && m_dicRefereeData.ContainsKey(id)) {
			return m_dicRefereeData [id];
		}
		return null;
	}
}
