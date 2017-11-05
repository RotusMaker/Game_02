using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PPoory;

public class Scene010RefereeIntroduce : MonoBehaviour {

	private uFSM m_uFsm = null;

	void Awake () {
		if (m_uFsm == null)
			m_uFsm = this.GetComponent<uFSM>();

		// Data load
		RefereeJSON.Instance.LoadJSON ();
	}
}
