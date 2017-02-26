using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ketsu
{
    public class Mineable : MonoBehaviour
    {
        public Sprite Mined;
        public int Score;
        public int Lives;
        public int Damage;
        public AudioClip HitSound;
        public AudioClip BreakSound;
        public GameObject Explosion;

        SpriteRenderer spirteRender;
        public SpriteRenderer ChildSprite;

        Dwarf dwarf;
        Collider2D collider2d;
        AudioSource ac;

        GameManager gameManager;

        void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
            spirteRender = GetComponent<SpriteRenderer>();
            collider2d = GetComponent<Collider2D>();
            ac = GetComponent<AudioSource>();
            dwarf = FindObjectOfType<Dwarf>();
        }

        void Start()
        {

        }

        void Update()
        {

        }

        public void Mine()
        {
            Lives--;
            ac.PlayOneShot(HitSound);

            if (--Lives <= 0)
            {
                dwarf.TakeDamage(Damage);
                gameManager.Score += Score;
                collider2d.enabled = false;
                ac.PlayOneShot(BreakSound);
                spirteRender.sprite = Mined;
                if (ChildSprite != null) ChildSprite.sprite = null;
                if (Explosion != null) Instantiate(Explosion, transform.position, transform.rotation);
            }
        }
    }
}