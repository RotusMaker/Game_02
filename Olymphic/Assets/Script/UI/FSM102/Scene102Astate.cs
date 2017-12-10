using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace PPoory
{

	[System.Serializable]
	[DisallowMultipleComponent]
	public class Scene102Astate : BaseState {

		private Scene102InGame gameScene;

		// Essential
		public override string GetStateName()
		{
			return GetType().Name;
		}

		// State Enter
		public override void OnEnter()
		{

			if (fsm.infoMessage)
			{
				Debug.Log(string.Format("{0}.{1}.{2}", name, GetType().Name, MethodBase.GetCurrentMethod().Name));
			}

			base.OnEnter();

			gameScene = this.GetComponent<Scene102InGame> ();

			StartCoroutine (OnAstate ());
		}

		// State Exit
		public override void OnExit()
		{

			if (fsm.infoMessage)
			{
				Debug.Log(string.Format("{0}.{1}.{2}", name, GetType().Name, MethodBase.GetCurrentMethod().Name));
			}

			base.OnExit();
		}

		IEnumerator OnAstate()
		{
			// 멘트를 순서대로 진행.
			for (int j = 0; j < 10; j++) {
				string[] split = gameScene.GetSplitCommant (string.Format (GameDataJSON.Instance.GetComment (1).commentary, gameScene.m_nGameType, gameScene.m_nGameType));
				for (int i = 0; i < split.Length; i++) {
					gameScene.AddCommantaryLine (split [i]);
					yield return new WaitForSeconds (1f);	
				}
			}
			fsm.Event ("B_state");
		}
	}
}
