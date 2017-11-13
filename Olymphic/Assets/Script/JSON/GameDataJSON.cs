using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public enum eNation
{
	eNull = 0, 
	eKorea = 1, 
	eJapan = 2, 
	eChina = 3,
	eAmerica = 4, 
	eNorway = 5,
	eRusia = 6,
	eCanada = 7,
	eNetherlands = 8,
	eGermany = 9,
	eSwiss = 10,
	eBelarus = 11,
	eAustria = 12,
	eFrance = 13,
	ePoland = 14,
	eSweden = 15,
	eMax = 16
}

public enum eGameType
{
	eNull = 0,
	eAlpine_Skiing = 1,
	eBiathlon = 2,
	eBobsled = 3,
	eCrosscountry_Skiing = 4,
	eCurling = 5,
	eFigure_Skating = 6,
	eFreestyle_Skiing = 7,
	eIce_Hockey = 8,
	eLuge = 9,
	eNordic_Complex = 10,
	eShort_Track = 11,
	eSpeed_Skating = 12,
	eSkeleton = 13,
	eSki_Jump = 14,
	eSnowboard = 15,
	eMax = 16
}

public enum eWinType
{
	NULL,
	RANK,	// 경쟁승리
	POINT	// 점수승리
}

// 접미사 'Data' 는 외부에서 온 데이터로 가공하지 말것.
// 나라 데이터.
public class NationData
{
	public int id;
	public string name;
	public List<eGameType> very_good_thing;
	public List<eGameType> good_thing;
	public List<eGameType> wrong_thing;
}
// 게임종목 데이터.
public class GameTypeData
{
	public int id;
	public string name;
	public int player_cnt;
	public eWinType win_type;
}

// 싱글톤 선언하기
public class GameDataJSON : Singleton<GameDataJSON> 
{
	public Dictionary<int, NationData> m_dicNationData = null;
	public Dictionary<int, GameTypeData> m_dicGameTypeData = null;

	protected GameDataJSON () {}	// Singletone use.

	public void LoadJSON()
	{
		if (m_dicNationData == null) 
		{
			string aJSON = FileLoader.Instance.LoadTextAssetToResources ("main_data");
			JSONNode root = JSON.Parse (aJSON);

			m_dicNationData = new Dictionary<int, NationData> ();

			// 나라데이터 파싱.
			JSONNode nationNode = root ["nation"];
			Debug.Log (nationNode.Count.ToString ());
			for (int i = 0; i < nationNode.Count; i++) {
				JSONNode childNode = nationNode [i];
				NationData nationData = new NationData ();
				nationData.id = childNode ["id"].AsInt;
				nationData.name = childNode ["name"];
				nationData.very_good_thing = GetListGameType (childNode ["very_good_thing"]);
				nationData.good_thing = GetListGameType (childNode ["good_thing"]);
				nationData.wrong_thing = GetListGameType (childNode ["wrong_thing"]);

				Debug.Log (string.Format ("@{0},{1},{2}", nationData.id, nationData.name, nationData.very_good_thing.Count));
				m_dicNationData.Add (nationData.id, nationData);
			}

			m_dicGameTypeData = new Dictionary<int, GameTypeData> ();

			// 종목데이터 파싱.
			JSONNode gameTypeNode = root ["gametype"];
			Debug.Log (gameTypeNode.Count.ToString ());
			for (int i = 0; i < gameTypeNode.Count; i++) 
			{
				JSONNode childNode = gameTypeNode [i];
				GameTypeData gameTypeData = new GameTypeData ();
				gameTypeData.id = childNode ["id"].AsInt;
				gameTypeData.name = childNode ["name"];
				gameTypeData.player_cnt = childNode ["player_cnt"].AsInt;
				gameTypeData.win_type = GetWinType(childNode ["win_type"]);

				Debug.Log (string.Format("@{0},{1},{2}",gameTypeData.id,gameTypeData.name,gameTypeData.win_type.ToString()));
				m_dicGameTypeData.Add (gameTypeData.id,gameTypeData);
			}
		}
	}

	public List<eGameType> GetListGameType(string data)
	{
		List<eGameType> listResult = new List<eGameType> ();
		string[] split = data.Split (',');
		if (split != null)
		{
			for (int i = 0; i < split.Length; i++) {
				listResult.Add((eGameType)System.Convert.ToInt32(split[i]));
			}
		}
		return listResult;
	}

	public eWinType GetWinType(string data)
	{
		eWinType toEnum = (eWinType)System.Enum.Parse (typeof(eWinType), data);
		if (System.Enum.IsDefined (typeof(eWinType), toEnum)) {
			return toEnum;
		}
		return eWinType.NULL;
	}

	public NationData GetNationData(int id)
	{
		if (m_dicNationData != null && m_dicNationData.ContainsKey(id)) {
			return m_dicNationData [id];
		}
		return null;
	}
}
