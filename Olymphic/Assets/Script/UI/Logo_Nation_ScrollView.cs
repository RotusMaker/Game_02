using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Logo_Nation_ScrollView : MonoBehaviour {

	public GameObject objElement;	// logo_nation_scrollview_element
	public RectTransform rtContent;

	void Start()
	{
		InitData ();
	}

	public void InitData()
	{
		// nation 데이터 불러오기.
		/*
		 * 국기이미지, 국가명, 국가번호, 주종목
		*/
		JSON_NationInfo.Instance.LoadJSON ();	// 필요한 데이터 체크

		float elementHeight = objElement.GetComponent<RectTransform> ().sizeDelta.y;

		int elementCnt = JSON_NationInfo.Instance.m_dicNationData.Count + 1;
		rtContent.sizeDelta = new Vector2(rtContent.sizeDelta.x, elementHeight * elementCnt);

		for (Dictionary<int,NationData>.Enumerator it = JSON_NationInfo.Instance.m_dicNationData.GetEnumerator (); it.MoveNext ();) 
		{
			GameObject element = GameObject.Instantiate (objElement);
			element.transform.parent = rtContent.transform;
			element.transform.localScale = Vector3.one;
			element.name = it.Current.Key.ToString ();
			SetElement (element, it.Current.Value);
		}
	}

	public void SetElement(GameObject obj, NationData data)
	{
		for (int i = 0; i < obj.transform.childCount; i++) 
		{
			Transform child = obj.transform.GetChild (i);
			if (child.name.CompareTo ("name") == 0) 
			{
				// 국가 이름
				child.GetComponent<Text> ().text = data.nationName;
			}
			else if (child.name.CompareTo ("button") == 0) 
			{
				// 버튼
				child.GetComponent<Button> ().onClick.AddListener (()=>{
					SelectLevel01Scene.instance.OnClickedNation (data);
				});
			}
		}
	}
}
