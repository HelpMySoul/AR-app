using Assets.Scripts.Scene;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static Assets.Scripts.ProjectEnums;

namespace Assets.Scripts
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject scoreObject;
        private TMP_Text scoreText;


        private void Start()
        {
            scoreText = scoreObject.GetComponent<TMP_Text>();
        }

        public void SetScore()
        {
            scoreText.text = $"Goals: {SessionInfo.GetScore()}";
        }

        public void SetStrategy(int strategy)
        {
            ProgrammManager.Strategy = (Strategies)strategy;
        }
    }
}