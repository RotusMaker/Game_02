using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPopup : MonoBehaviour 
{
	public enum eButtonType
	{
		OK,
		Yes,
		No,
	}

	public enum eTextType
	{
		Title,
		Content_1,
	}

	public enum eCustomObject
	{
		NationMission,
	}
	
	[System.Serializable]
	public class ButtonInfo
	{
		public eButtonType type;
		public Button button;
	}

	[System.Serializable]
	public class TextInfo
	{
		public eTextType type;
		public Text text;
	}

	[System.Serializable]
	public class CustomObject
	{
		public eCustomObject type;
		public GameObject obj;
	}
	
	public Image m_imgBg;
	public List<ButtonInfo> m_listBtn;
	public List<TextInfo> m_listText;
	public List<CustomObject> m_listCustom;
	public Dictionary<eButtonType,Button> m_dicBtn = null;
	public Dictionary<eTextType,Text> m_dicTxt = null;
	public Dictionary<eCustomObject,GameObject> m_dicObj = null;

	public void CreateDictionary()
	{
		if (m_dicBtn == null) {
			m_dicBtn = new Dictionary<eButtonType, Button> ();

			for (int i = 0; i < m_listBtn.Count; i++) {
				m_dicBtn.Add (m_listBtn [i].type, m_listBtn [i].button);
			}
		}
		if (m_dicTxt == null) {
			m_dicTxt = new Dictionary<eTextType, Text> ();

			for (int i = 0; i < m_listText.Count; i++) {
				m_dicTxt.Add (m_listText [i].type, m_listText [i].text);
			}
		}
		if (m_dicObj == null) {
			m_dicObj = new Dictionary<eCustomObject, GameObject> ();

			for (int i = 0; i < m_listCustom.Count; i++) {
				m_dicObj.Add (m_listCustom [i].type, m_listCustom [i].obj);
			}
		}
	}

	public Button GetButton(eButtonType type)
	{
		if (m_dicBtn != null && m_dicBtn.ContainsKey(type)) {
			return m_dicBtn [type];
		}
		return null;
	}

	public Text GetText(eTextType type)
	{
		if (m_dicTxt != null && m_dicTxt.ContainsKey(type)) {
			return m_dicTxt [type];
		}
		return null;
	}

	public GameObject GetObj(eCustomObject type)
	{
		if (m_dicObj != null && m_dicObj.ContainsKey(type)) {
			return m_dicObj [type];
		}
		return null;
	}
}
