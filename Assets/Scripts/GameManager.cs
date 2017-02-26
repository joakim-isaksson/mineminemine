using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Ketsu
{
    public class GameManager : MonoBehaviour
    {
        public float FadeSpeed;
        public Text GameOverText;
        public Text LivesText;
        public Text Scoretext;
        public Text StartText;


        ScreenFaider Faider;

        [HideInInspector]
        public bool GameOver;
        bool GameOverInAction;

        [HideInInspector]
        public int Score;

        Dwarf dwarf;

        void Awake()
        {
            dwarf = FindObjectOfType<Dwarf>();
        }

        void Start()
        {
            GameOverText.text = "";

            StartText.text = "Wake up my little dwarf...";
            DelayedAction(3.0f, delegate {
                StartText.text = "... it is time to get those diamonds ...";
                DelayedAction(3.0f, delegate {
                    StartText.text = "! Mine, Mine, Mine !";
                    DelayedAction(2f, delegate { StartText.text = ""; });
                });
            });

            Faider = ScreenFaider.instance;
            Faider.SetTo(Color.black);
            Faider.FadeOut(Color.black, FadeSpeed, null);
        }

        void Update()
        {
            if (GameOverInAction) return;

            LivesText.text = "Lives: " + dwarf.Lives;
            Scoretext.text = "Score: " + Score;
            
            if (Input.GetButtonDown("Cancel"))
            {
                Faider.FadeIn(Color.black, FadeSpeed, delegate
                {
                    SceneManager.LoadScene("Menu", LoadSceneMode.Single);
                });
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                Faider.FadeIn(Color.black, FadeSpeed, delegate
                {
                    SceneManager.LoadScene("Main", LoadSceneMode.Single);
                });
            }

            if (GameOver)
            {
                GameOverInAction = true;
                GameOverText.text = "Game Over";
                DelayedAction(3.0f, delegate {
                    Faider.FadeIn(Color.black, FadeSpeed, delegate
                    {
                        SceneManager.LoadScene("End", LoadSceneMode.Single);
                    });
                });
            }
        }

        public void DelayedAction(float time, Action action)
        {
            StartCoroutine(RunDelayedAction(time, action));
        }

        IEnumerator RunDelayedAction(float time, Action action)
        {
            yield return new WaitForSeconds(time);
            if (action != null) action();
        }
    }
}