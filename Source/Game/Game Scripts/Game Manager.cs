using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{
    public class GameManager : Script
    {

        public GameplayGlobals movesGlobals;

        public List<UICanvas> invokersCanvases;

        public List<Actor> stations;

        public Actor startingPos;

        public Actor currentCake;

        public Prefab startingObj;

        public static int moves;

        public static Action movesFinished;

        public static string difficulty;

        /// <inheritdoc/>
        public override void OnStart()
        {
            difficulty = (string)movesGlobals.GetValue("Difficulty");

            if (difficulty == "Beginner")
            {
                moves = (int)movesGlobals.GetValue("Beginner");
            }
            if (difficulty == "Intermediate")
            {
                moves = (int)movesGlobals.GetValue("Intermediate");
            }

            if (difficulty == "Expert")
            {
                moves = (int)movesGlobals.GetValue("Expert");
            }

            PrefabManager.SpawnPrefab(startingObj, startingPos);
        }

        public override void OnEnable()
        {
            // Register for event
            ColliderInvoker.showCanvas += ShowCanvas;
            ColliderInvoker.hideCanvas += HideCanvas;
        }

        public override void OnDisable()
        {
            ColliderInvoker.showCanvas -= ShowCanvas;
            ColliderInvoker.hideCanvas -= HideCanvas;
        }

        public void ShowCanvas(int collider)
        {
            var canvas = invokersCanvases[collider];
            canvas.As<Actor>().IsActive = true;
        }

        public void HideCanvas(int collider)
        {
            var canvas = invokersCanvases[collider];
            canvas.As<Actor>().IsActive = false;
        }


        /// <inheritdoc/>
        public override void OnUpdate()
        {
            if (moves <= 0)
            {
                movesFinished?.Invoke();
            }
        }
    }
}
