using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene002NickName : MonoBehaviour {

	public InputField mInput;
	public CameraShake mShake;
	public Animator mFailed;

	public void OnClickedStart()
	{
		if (string.IsNullOrEmpty (mInput.text)) {
			mFailed.SetTrigger ("Failed");
			mShake.ShakeCamera (20f, 0.2f);
		} 
		else {
			PlayerPrefs.SetString ("USER_NAME", mInput.text);
			MainFrame.instance.NextScene(10, 2f);
		}
	}
}
