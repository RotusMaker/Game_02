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
    public class Scene101DualGameSelect : BaseState
    {
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

            fsm.ownerObj.GetComponent<Scene101DualGame>().m_nChoiceNumber = -1;
            fsm.ownerObj.GetComponent<Scene101DualGame>().m_nAIChoiceNumber = -1;

            fsm.ownerObj.GetComponent<Scene101DualGame>().m_objSelectButtonRoot.SetActive(true);
        }

        // State Exit
        public override void OnExit()
        {
            if (fsm.infoMessage)
            {
                Debug.Log(string.Format("{0}.{1}.{2}", name, GetType().Name, MethodBase.GetCurrentMethod().Name));
            }

            base.OnExit();

            fsm.ownerObj.GetComponent<Scene101DualGame>().m_objSelectButtonRoot.SetActive(false);
        }

        // MonoBehaviour Function is called when this state is activated.
        void Start()
        {
        }

        public void OnClickedScissors()
        {
            if (this.enabled)
            {
                fsm.ownerObj.GetComponent<Scene101DualGame>().m_nChoiceNumber = 1;
                fsm.Event("result");
            }
        }

        public void OnClickedRock()
        {
            if (this.enabled)
            {
                fsm.ownerObj.GetComponent<Scene101DualGame>().m_nChoiceNumber = 0;
                fsm.Event("result");
            }
        }

        public void OnClickedPaper()
        {
            if (this.enabled)
            {
                fsm.ownerObj.GetComponent<Scene101DualGame>().m_nChoiceNumber = 2;
                fsm.Event("result");
            }
        }
    }

}

