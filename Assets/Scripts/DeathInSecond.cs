using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ketsu
{
    public class DeathInSecond : MonoBehaviour
    {
        void Awake()
        {

        }

        void Start()
        {
            StartCoroutine(WaitAndDie());
        }

        void Update()
        {

        }

        IEnumerator WaitAndDie()
        {
            yield return new WaitForSeconds(1.5f);
            Destroy(gameObject);
        }
    }
}