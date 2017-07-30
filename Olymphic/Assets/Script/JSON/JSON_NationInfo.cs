using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eNationCode
{
	// 나라정보 쭉 입력
	None,
	Korea,
}

public enum eSprtsEvent
{
	// 스포츠 종목 입력
	None,
	Ski,
}

// 접미사 'Data' 는 외부에서 온 데이터로 가공하지 말것.
public class NationData
{
	public eNationCode nationCode;		// 국가코드
	public string spriteName;			// 국기 이미지 이름
	public string nationName;			// 국가명
	public eSprtsEvent bestSportsEvent;	// 주종목
}

// 싱글톤 선언하기
public class JSON_NationInfo : MonoBehaviour 
{
	public Dictionary<eNationCode,NationData> m_dicNationData = new Dictionary<eNationCode, NationData>();
}
