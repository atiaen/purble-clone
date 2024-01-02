using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{
    /// <summary>
    /// Conveyor Script.
    /// </summary>
    public class Cake : Script
    {
        public int maxLayers;

        public int currentLayer = 0;

        public Layers[] layers;

        public Prefab sheetPrefab;

        public Tag cakeTag;

        /// <inheritdoc/>
        public override void OnStart()
        {
            // Here you can add code that needs to be called when script is created, just before the first game update
            //First create an empty actor to represent our sheet

            var sheet  = PrefabManager.SpawnPrefab(sheetPrefab, Actor);
            //sheet.AddScript<Layers>();

         
            //add a box collider
            var bc = Actor.AddChild<BoxCollider>();
            bc.Size = new Vector3(100,50,100);
            bc.AddTag(Actor.Tags[0]);
            //bc.Center = new Vector3(0, 2.3, 0);
        }

        /// <inheritdoc/>
        public override void OnEnable()
        {
            // Here you can add code that needs to be called when script is enabled (eg. register for events)
        }

        /// <inheritdoc/>
        public override void OnDisable()
        {
            // Here you can add code that needs to be called when script is disabled (eg. unregister from events)
        }

        public override void OnFixedUpdate()
        {
          
        }

        /// <inheritdoc/>
        public override void OnUpdate()
        {
            // Here you can add code that needs to be called every frame
            //if(Actor.Orientation.Angle != 0)
            //{
            //    Actor.Orientation = new Quaternion(0, 0, 0, 0);
            //}
        }
    }
}
