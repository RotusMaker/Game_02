﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace PPoory
{
	[System.Serializable]
	[DisallowMultipleComponent]
	public class Scene010RefereeIntroduceStart : BaseState
	{
		public Animator startAction;
		public Text myName;
		public Text otherName;
		public Image myNation;
		public Image otherNation;
		
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

			// 데이터 셋팅. 내꺼랑 상대방꺼.
			RefereeData data1 = RefereeJSON.Instance.GetReferee(101);
			RefereeData data2 = RefereeJSON.Instance.GetReferee(102);
			myName.text = data1.name;
			otherName.text = data2.name;

			// 액션 시작.
			StartCoroutine(StartAction());

			base.OnEnter();
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

		public IEnumerator StartAction()
		{
			startAction.SetTrigger("start");
			while (startAction.GetCurrentAnimatorStateInfo (0).IsName ("Empty")) {
				yield return null;
			}
			FinishedStartAction ();
		}

		public void FinishedStartAction()
		{
			fsm.Event ("normal");
		}

	}

}

