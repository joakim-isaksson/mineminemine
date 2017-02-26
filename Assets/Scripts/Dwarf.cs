using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ketsu
{
    public class Dwarf : MonoBehaviour
    {
        public int Lives;

        public float Speed;

        public float MiningTime;

        Rigidbody2D rb;
        AudioSource asrc;
        Animator anim;

        bool dead;

        GameManager gameManager;

        HashSet<Mineable> targets;

        float passedTime;

        bool startDelay;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            asrc = GetComponent<AudioSource>();
            anim = GetComponent<Animator>();
            gameManager = FindObjectOfType<GameManager>();
            targets = new HashSet<Mineable>();

            startDelay = true;
            DelayedAction(2.0f, delegate { startDelay = false; anim.SetTrigger("Start"); });
        }

        void Start()
        {

        }

        void Update()
        {
            if (dead) return;
            
            if (Input.GetAxis("Horizontal") < -0.001)
            {
                transform.localScale = new Vector3(-Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            } else if (Input.GetAxis("Horizontal") > 0.001)
            {
                transform.localScale = new Vector3(Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

            rb.AddForce(new Vector2(Input.GetAxis("Horizontal") * Speed, 0.0f));
        }

        private void LateUpdate()
        {
            if (dead || startDelay) return;

            passedTime += Time.deltaTime;
            if (passedTime > MiningTime)
            {
                passedTime = 0;
                foreach (Mineable target in targets)
                {
                    target.Mine();
                }
            }
        }

        private void FixedUpdate()
        {
            targets.Clear();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Mineable mineable = collision.gameObject.GetComponent<Mineable>();
            if (mineable != null) targets.Add(mineable);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            Mineable mineable = collision.gameObject.GetComponent<Mineable>();
            if (mineable != null) targets.Add(mineable);
        }

        public void TakeDamage(int amount)
        {
            if (dead) return;

            Lives -= amount;
            if (Lives <= 0)
            {
                Lives = 0;
                dead = true;
                anim.SetTrigger("Dead");
                DelayedAction(1.5f, delegate { gameManager.GameOver = true; });
            }
        }

        void DelayedAction(float time, Action action)
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