﻿
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace PPoory {

    [System.Serializable]
    [DisallowMultipleComponent]
    public class StateBtnStop : BaseState {

        // Essential
        public override string GetStateName () {
            return GetType().Name;
        }

        // State Enter
        public override void OnEnter () {

            if ( fsm.infoMessage ) {
                Debug.Log( string.Format( "{0}.{1}.{2}", name, GetType().Name, MethodBase.GetCurrentMethod().Name ) );
            }

            base.OnEnter();

            // button color setting
            GuiButtonPlayStop button = fsm.ownerObj.GetComponent<GuiButtonPlayStop>();
            button.btnImage.color = button.stopColor;
            button.btnText.text = "Stop";

            SceneManager.instance.GetComponent<uFSM>().Event( "play" );
        }

        // State Exit
        public override void OnExit () {

            if ( fsm.infoMessage ) {
                Debug.Log( string.Format( "{0}.{1}.{2}", name, GetType().Name, MethodBase.GetCurrentMethod().Name ) );
            }

            base.OnExit();
            // ToDo ...
        }

        // MonoBehaviour Function is called when this state is activated.
        void Start () {
        }

        // MonoBehaviour Function is called when this state is activated.
        void FixedUpdate () {
        }

        // MonoBehaviour Function is called when this state is activated.
        void Update () {
        }

        // MonoBehaviour Function is called when this state is activated.
        void LateUpdate () {
        }

    }

}

