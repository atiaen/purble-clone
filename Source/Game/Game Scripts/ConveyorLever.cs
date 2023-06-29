using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{
    /// <summary>
    /// ConveyorLevel Script.
    /// </summary>
    public class ConveyorLever : Script
    {
        public string moveDirection;
        public static event Action<string> triggerMove;
        public Collider triggerCollider;

        /// <inheritdoc/>
        public override void OnStart()
        {
            // Here you can add code that needs to be called when script is created, just before the first game update
        }
        
        /// <inheritdoc/>
        public override void OnEnable()
        {
            // Register for event
            triggerCollider.As<Collider>().TriggerEnter += OnTriggerEnter;
        }

        public override void OnDisable()
        {
            // Unregister for event
            triggerCollider.As<Collider>().TriggerEnter -= OnTriggerEnter;

        }

        /// <inheritdoc/>
        public override void OnUpdate()
        {
            // Here you can add code that needs to be called every frame
        }

         void OnTriggerEnter(PhysicsColliderActor collider)
        {
           if(Input.GetAction("First")){
                triggerMove.Invoke(moveDirection);
           }

        }
    }
}
