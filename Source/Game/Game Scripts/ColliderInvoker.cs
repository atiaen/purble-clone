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

        public string[] options;
        public string stationToTrigger;

        public static event Action<string,string> stationAndChoseAction;

        public bool insideTrigger;

      

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
            //Debug.Log(collider.Tag);
            if (collider.HasTag("Player"))
            {
                insideTrigger = true;
            }
           
        }

        void OnTriggerExit(PhysicsColliderActor collider)
        {
            if (collider.HasTag("Player"))
            {
                insideTrigger = false;
            }
        }

        /// <inheritdoc/>
        public override void OnUpdate()
        {

            if (insideTrigger)
            {

                if (Input.GetAction("First"))
                {
                    stationAndChoseAction?.Invoke(stationToTrigger, options[0]);
                }
                
                if (Input.GetAction("Second"))
                {
                    stationAndChoseAction?.Invoke(stationToTrigger, options[1]);

                }

                if (Input.GetAction("Third"))
                {
                    stationAndChoseAction?.Invoke(stationToTrigger, options[2]);

                }
            }
        }
    }
}
