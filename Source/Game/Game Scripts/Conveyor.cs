using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{
    /// <summary>
    /// Conveyor Script.
    /// </summary>
    public class Conveyor : Script
    {
        public RigidBody rigidBody;
        public List<Actor> actors;

        public Actor point;
        public float speed = 2.0f;
        /// <inheritdoc/>
        public override void OnStart()
        {
            // Here you can add code that needs to be called when script is created, just before the first game update
        }

        /// <inheritdoc/>
        public override void OnEnable()
        {
            ConveyorLever.triggerMove += MoveItems;
            Actor.GetChild<Collider>().CollisionEnter += OnCollisionEnter;
            // Here you can add code that needs to be called when script is enabled (eg. register for events)
        }

        /// <inheritdoc/>
        public override void OnDisable()
        {
            ConveyorLever.triggerMove -= MoveItems;
            Actor.GetChild<Collider>().CollisionEnter -= OnCollisionEnter;
            // Here you can add code that needs to be called when script is disabled (eg. unregister from events)
        }

        public override void OnFixedUpdate()
        {
           
        }

        private void OnCollisionEnter(Collision collision)
        {
            // Debug.Log("We got the collision sir! With: " + collision.OtherActor);
            if(!collision.OtherActor.HasTag("Floor")){
                Debug.Log("We got the collision sir! With: " + collision.OtherCollider);
                var item = collision.OtherActor;
                actors.Add(item);
            }
        }

        public void MoveItems(string direction)
        {
            if(direction == "Left"){

            }

        }

        /// <inheritdoc/>
        public override void OnUpdate()
        {
            // Here you can add code that needs to be called every frame
        }
    }
}
