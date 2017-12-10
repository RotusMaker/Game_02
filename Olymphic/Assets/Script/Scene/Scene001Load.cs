using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene001Load : MonoBehaviour {

	public Slider mSlider;

	void Start () 
	{
		StartCoroutine (OnStart ());
	}

	public IEnumerator OnStart()
	{
		StartCoroutine(GameDataJSON.Instance.OnLoadingJSON ());

		while (GameDataJSON.Instance.IsLoaded () == false) {
			yield return null;
			mSlider.value = GameDataJSON.Instance.fLoadPercent / 100f;
		}
		mSlider.value = 1f;

		// 유저이름있으면 진행. 아니면 캐릭터 만들기로.
		string userName = PlayerPrefs.GetString ("USER_NAME", string.Empty);
		if (string.IsNullOrEmpty (userName)) {
			MainFrame.instance.NextScene (2, 2f);
		} 
		else {
			MainFrame.instance.NextScene (10, 2f);
		}
	}
}
