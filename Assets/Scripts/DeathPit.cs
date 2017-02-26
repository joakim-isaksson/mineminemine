using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ketsu
{
    public class DeathPit : MonoBehaviour
    {
        void Awake()
        {

        }

        void Start()
        {

        }

        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Dwarf dwarf = collision.gameObject.GetComponent<Dwarf>();
            if (dwarf != null) dwarf.TakeDamage(100);
        }
    }
}