using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Character : MonoBehaviour
    {
        protected Movement movement;
        protected LegendaryTaxiDriver legendaryTaxiDriver;
        protected BoxCollider2D bodyCollider;
        protected Rigidbody2D body;

        protected virtual void Awake()
        {
            movement = GetComponent<Movement>();
            bodyCollider = GetComponent<BoxCollider2D>();
            body = GetComponent<Rigidbody2D>();
            legendaryTaxiDriver = new LegendaryTaxiDriver();
        }
        void Start()
        {

        }

        void Update()
        {

        }
    }
}

