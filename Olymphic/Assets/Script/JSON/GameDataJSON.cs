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
	public List<int> very_good_thing;
	public List<int> good_thing;
	public List<int> wrong_thing;
}
// 게임종목 데이터.
public class GameTypeData
{
	public int id;
	public string name;
	public int player_cnt;
	public eWinType win_type;
}
// 해설 데이터.
public class CommentaryData
{
	public int id;
	public string commentary;
}
// 이벤트 데이터.
public class EventData
{
	public int id;
	public int game_type;
	public List<int> change_rank;	// 이건 기획적으로 더 정하고.
	public string content;
}

// 싱글톤 선언하기
public class GameDataJSON : Singleton<GameDataJSON> 
{
	public Dictionary<int, NationData> m_dicNationData = null;
	public Dictionary<int, GameTypeData> m_dicGameTypeData = null;
	public Dictionary<int, CommentaryData> m_dicCommentaryData = null;
	public Dictionary<int, EventData> m_dicEventData = null;

	public float fLoadPercent = 0;

	protected GameDataJSON () {}	// Singletone use.

	public bool IsLoaded()
	{
		return (m_dicNationData != null && m_dicEventData != null);
	}

	// 테스트용.
	public void LoadJSON()
	{
		MainFrame.instance.StartCoroutine (OnLoadingJSON ());
	}

	public IEnumerator OnLoadingJSON()
	{
		fLoadPercent = 0;

		if (IsLoaded () == true) {
			fLoadPercent = 100f;
			yield break;
		}

		if (IsLoaded() == false)
		{
			string aJSON = FileLoader.Instance.LoadTextAssetToResources ("main_data");
			JSONNode root = JSON.Parse (aJSON);

			// 나라데이터 파싱.
			m_dicNationData = new Dictionary<int, NationData> ();
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
				if (i % 100 == 0) {
					yield return null;
					fLoadPercent = 10f;
				}
			}
			fLoadPercent = 15f;

			// 종목데이터 파싱.
			m_dicGameTypeData = new Dictionary<int, GameTypeData> ();
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
				if (i % 100 == 0) {
					yield return null;
					fLoadPercent = 25f;
				}
			}
			fLoadPercent = 30f;

			// 유물데이터 파싱.

			// 해설데이터 파싱.
			m_dicCommentaryData = new Dictionary<int, CommentaryData>();
			JSONNode commNode = root ["commentary"];
			Debug.Log (commNode.Count.ToString ());
			for (int i = 0; i < commNode.Count; i++) 
			{
				JSONNode childNode = commNode [i];
				CommentaryData commData = new CommentaryData ();
				commData.id = childNode ["id"].AsInt;
				commData.commentary = childNode ["content"];

				Debug.Log (string.Format("@{0},{1}",commData.id,commData.commentary));
				m_dicCommentaryData.Add (commData.id,commData);
				if (i % 100 == 0) {
					yield return null;
					fLoadPercent = 40f;
				}
			}
			fLoadPercent = 50f;
		}

		if (IsLoaded () == false) 
		{
			string aJSON = FileLoader.Instance.LoadTextAssetToResources ("event_data");
			JSONNode root = JSON.Parse (aJSON);

			// 이벤트 데이터.
			m_dicEventData = new Dictionary<int, EventData> ();
			JSONNode eventNode = root ["event"];
			Debug.Log (eventNode.Count.ToString ());
			for (int i = 0; i < eventNode.Count; i++) {
				JSONNode childNode = eventNode [i];
				EventData eventData = new EventData ();
				eventData.id = childNode ["id"].AsInt;
				eventData.game_type = childNode ["game_type"].AsInt;
				eventData.change_rank = GetListGameType (childNode ["change_rank"]);
				eventData.content = childNode ["content"];

				Debug.Log (string.Format ("@{0},{1},{2}", eventData.id, eventData.game_type, eventData.content));
				m_dicEventData.Add (eventData.id, eventData);
				if (i % 100 == 0) {
					yield return null;
					fLoadPercent = 80f;
				}
			}
			fLoadPercent = 100f;
		}

		fLoadPercent = 100f;
	}



	// 해설데이터 가져오기.
	public CommentaryData GetComment(int id)
	{
		if (m_dicCommentaryData.ContainsKey (id)) {
			return m_dicCommentaryData [id];
		}
		return null;
	}

	public List<int> GetListGameType(string data)
	{
		List<int> listResult = new List<int> ();
		string[] split = data.Split (',');
		if (split != null)
		{
			for (int i = 0; i < split.Length; i++) {
				//listResult.Add((eGameType)System.Convert.ToInt32(split[i]));
				listResult.Add(System.Convert.ToInt32(split[i]));
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
