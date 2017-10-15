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
    public class Scene101DualGameResult : BaseState
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

            ResultCalculation();
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
        }

        // MonoBehaviour Function is called when this state is activated.
        void Start()
        {
        }

        // 가위바위보 결과 만들기.
        void ResultCalculation()
        {
            int userScore = fsm.ownerObj.GetComponent<Scene101DualGame>().m_nScore;
            int aiScore = fsm.ownerObj.GetComponent<Scene101DualGame>().m_nAIScore;

            int userChoice = fsm.ownerObj.GetComponent<Scene101DualGame>().m_nChoiceNumber;
            int aiChoice = -1;
            int random = UnityEngine.Random.Range(0, 1001);
            if (random <= 333)
            {
                // 비김
                aiChoice = userChoice;
            }
            else if (random <= 666)
            {
                // 이김
                switch(userChoice)
                {
                    case 0: aiChoice = 1; break;
                    case 1: aiChoice = 2; break;
                    case 2: aiChoice = 0; break;
                }
                userScore += 1;
            }
            else
            {
                // 짐
                switch (userChoice)
                {
                    case 0: aiChoice = 2; break;
                    case 1: aiChoice = 0; break;
                    case 2: aiChoice = 1; break;
                }
                aiScore += 1;
            }

            fsm.ownerObj.GetComponent<Scene101DualGame>().m_nAIChoiceNumber = aiChoice;

            fsm.ownerObj.GetComponent<Scene101DualGame>().m_nScore = userScore;
            fsm.ownerObj.GetComponent<Scene101DualGame>().m_nAIScore = aiScore;

            if (userScore >= 3)
            {
                // 최종 승리.
            }
            else if (aiScore >= 3)
            {
                // 최종 패배.
            }
            //else
            {
                // 계속 진행.
                fsm.ownerObj.GetComponent<Scene101DualGame>().RefreshScoreBoard();

                fsm.Event("select");
            }
        }
    }

}

