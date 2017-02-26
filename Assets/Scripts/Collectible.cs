using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ketsu
{
    public class Collectible : MonoBehaviour
    {
        public bool Boost;

        public Sprite Broken;

        SpriteRenderer spriteRenderer;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Start()
        {

        }

        void Update()
        {

        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                Dwarf drawf = collision.GetComponent<Dwarf>();
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