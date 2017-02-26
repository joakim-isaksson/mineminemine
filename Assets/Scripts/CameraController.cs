using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ketsu
{
    public class CameraController : MonoBehaviour
    {
        public Transform Follow;
        public float Speed;
        public float Power;

        void Update()
        {
            float distance = Vector3.Distance(transform.position, Follow.position);
            transform.position = Vector3.MoveTowards(transform.position, Follow.position, Mathf.Pow(Speed * distance, Power));
        }
    }
}