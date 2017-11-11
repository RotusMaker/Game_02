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
			yield return null;
			fsm.Event ("Bstate");
		}
	}
}
