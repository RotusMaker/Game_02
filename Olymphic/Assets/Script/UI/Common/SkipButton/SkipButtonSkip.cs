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
    public class SkipButtonSkip : BaseState
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
            // ToDo ...

            fsm.ownerObj.GetComponent<Button>().interactable = false;

            MainFrame.instance.NextScene(fsm.ownerObj.GetComponent<SkipButton>().nextSceneNumber);
        }

        // State Exit
        public override void OnExit()
        {

            if (fsm.infoMessage)
            {
                Debug.Log(string.Format("{0}.{1}.{2}", name, GetType().Name, MethodBase.GetCurrentMethod().Name));
            }

            base.OnExit();
            // ToDo ...

            fsm.ownerObj.GetComponent<Button>().interactable = true;
        }

        // MonoBehaviour Function is called when this state is activated.
        void Start()
        {
        }
    }

}

