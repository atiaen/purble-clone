using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{
    /// <summary>
    /// ColliderInvoker Script.
    /// </summary>
    public class ColliderInvoker : Script
    {
        public static event Action<int> showCanvas;

        public static event Action<int> hideCanvas;


        public static event Action<string> itemToSpawn;


        private bool insideTrigger;

        public string firstToSpawn;
        public string secToSpawn;
        public string thirdToSpawn;

        public int id;

      

        /// <inheritdoc/>
        public override void OnStart()
        {
            
           
            // Here you can add code that needs to be called when script is created, just before the first game update
        }

        public override void OnEnable()
        {
            // Register for event
            Actor.As<Collider>().TriggerEnter += OnTriggerEnter;
            Actor.As<Collider>().TriggerExit += OnTriggerExit;
        }

        public override void OnDisable()
        {
            // Unregister for event
            Actor.As<Collider>().TriggerEnter -= OnTriggerEnter;
            Actor.As<Collider>().TriggerExit -= OnTriggerExit;

        }

        void OnTriggerEnter(PhysicsColliderActor collider)
        {
            showCanvas?.Invoke(id);
            insideTrigger = true;

        }

        void OnTriggerExit(PhysicsColliderActor collider)
        {
            hideCanvas?.Invoke(id);
            insideTrigger = false;
        }

        /// <inheritdoc/>
        public override void OnUpdate()
        {
            if (insideTrigger && GameManager.moves > 0)
            {
                if (Input.GetAction("First"))
                {
                    GameManager.moves = GameManager.moves - 1;
                    itemToSpawn?.Invoke(firstToSpawn);
                }

                if (Input.GetAction("Second"))
                {
                    GameManager.moves = GameManager.moves - 1;
                    itemToSpawn?.Invoke(secToSpawn);
                }

                if (Input.GetAction("Third"))
                {
                    GameManager.moves = GameManager.moves - 1;
                    itemToSpawn?.Invoke(thirdToSpawn);
                }
            }



            // Here you can add code that needs to be called every frame
        }
    }
}
