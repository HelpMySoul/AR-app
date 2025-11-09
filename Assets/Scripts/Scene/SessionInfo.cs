using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Templates.AR;

namespace Assets.Scripts.Scene
{
    public static class SessionInfo
    {
        private static UInt16 _score = 0;

        public static void Goal()
        {
            _score++;
            Debug.Log("Goal!");
            ProgrammManager.uIManager.SetScore();
        }

        public static UInt16 GetScore()
        {
            return _score;
        }
    }
}