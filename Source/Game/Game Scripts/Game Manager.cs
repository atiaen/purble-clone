using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography;
using FlaxEngine;
using FlaxEngine.GUI;

namespace Game
{
    public class GameManager : Script
    {
        public Actor displayPosition;

        public int layerSize = 0;

        public Difficulty difficulty;

        public UIControl scoreDisplay;
        public UIControl movesDisplay;
        public UIControl cakesMadeDisplay;
        public UIControl gameOverPanel;

        public Dictionary<string, Model> batterOptions;
        public Dictionary<string, Model> frostingOptions;
        public Dictionary<string, Model> toppingOptions;
        public Dictionary<string, string> colorOptions;
        public string[] shapes;
        public string[] colorsWithWords;

        public int score = 0;
        public int moves;
        public int foodToMake = 0;
        public int foodMade = 0;
        public int incorrectFoodMade = 0;
        public int maxIncorrectFoodAllowed = 0;

        public int maxCakeLayers;

        private string _displayFormat;

        Random random;

        public static event Action onGameOverEvent;

        /// <inheritdoc/>
        public override void OnStart()
        {

            random = new Random();


            scoreDisplay = Scene.FindActor<UIControl>("Score");
            movesDisplay = Scene.FindActor<UIControl>("MovesNumber");
            cakesMadeDisplay = Scene.FindActor<UIControl>("CakeAmount");
            gameOverPanel = Scene.FindActor<UIControl>("GameOverPanel");

            GenerateFood();

        }

        public override void OnEnable()
        {
            Submitter.onCakeSubmitted += scoreSubmittedFood;
            Trasher.onCakeDestroy += trashedFood;
            _displayFormat = cakesMadeDisplay.Get<Label>().Text;
        }

        public override void OnDisable()
        {
            Submitter.onCakeSubmitted -= scoreSubmittedFood;
            Trasher.onCakeDestroy -= trashedFood;
            cakesMadeDisplay.Get<Label>().Text = _displayFormat;
        }
        /// <inheritdoc/>
        public override void OnUpdate()
        {
            scoreDisplay.Get<Label>().Text = score.ToString();
            movesDisplay.Get<Label>().Text = moves.ToString();
            cakesMadeDisplay.Get<Label>().Text = string.Format(_displayFormat,foodMade.ToString(), foodToMake.ToString());

            //var prevDiff = difficulty;

            //Debug.Log(difficulty);

            if (Input.GetAction("Reload"))
            {
                displayPosition.DestroyChildren();
                GenerateFood();
            }

            if(foodMade == foodToMake || incorrectFoodMade == maxIncorrectFoodAllowed)
            {
                onGameOverEvent?.Invoke();
                GameOver();
            }


        }
        public void setParameters()
        {
            switch (difficulty)
            {
                case Difficulty.Beginner:
                    moves = 100;
                    foodToMake = 3;
                    maxCakeLayers = 1;
                    maxIncorrectFoodAllowed = 5;
                    break;
                case Difficulty.Intermediate:
                    moves = 50;
                    foodToMake = 5;
                    maxCakeLayers = 3;
                    maxIncorrectFoodAllowed = 3;
                    break;
                case Difficulty.Expert:
                    moves = 150;
                    foodToMake = 10;
                    maxCakeLayers = 5;
                    maxIncorrectFoodAllowed = 1;
                    break;
            }

        }
        public void GenerateFood()
        {
            setParameters();

            layerSize = getLayerSizeFromDiff();


            for (int i = 0; i < layerSize; i++)
            {
                int shapeIndex = random.Next(shapes.Length);
                int shapeColor = random.Next(colorsWithWords.Length);
                int frostingFlavourColor = random.Next(colorsWithWords.Length);

                string chosenShape = shapes[shapeIndex];
                string chosenShapeColor = colorsWithWords[shapeColor];
                string frostingColor = colorsWithWords[frostingFlavourColor];

                if (displayPosition.ChildrenCount != 0)
                {
                    Vector3 lastItemPos = displayPosition.GetChild(displayPosition.ChildrenCount - 1).Position;

                    var model = displayPosition.AddChild<StaticModel>();
                    var sc = model.AddScript<Layers>();

                    sc.flavourType = colorOptions[chosenShapeColor];
                    sc.shapeType = chosenShape;
                    sc.frostingFlavour = colorOptions[frostingColor];

                    model.Position = new Vector3(lastItemPos.X, lastItemPos.Y + 20, lastItemPos.Z);
                    model.Model = batterOptions[chosenShape];

                    var modelMaterial = displayPosition.GetChild(displayPosition.ChildrenCount - 1).As<StaticModel>().MaterialSlots[0].Material.CreateVirtualInstance();
                    modelMaterial.SetParameterValue("Color", Color.ParseHex(colorOptions[chosenShapeColor]));

                    displayPosition.GetChild(displayPosition.ChildrenCount - 1).As<StaticModel>().SetMaterial(0, modelMaterial);

                    var frost = model.AddChild<StaticModel>();
                    frost.Model = frostingOptions[chosenShape];

                    var frostMaterial = displayPosition.GetChild(displayPosition.ChildrenCount - 1).GetChild(0).As<StaticModel>().MaterialSlots[0].Material.CreateVirtualInstance();
                    frostMaterial.SetParameterValue("Color", Color.ParseHex(colorOptions[frostingColor]));

                    displayPosition.GetChild(displayPosition.ChildrenCount - 1).GetChild(0).As<StaticModel>().SetMaterial(0, frostMaterial);

                }
                else
                {
                    var model = displayPosition.AddChild<StaticModel>();
                    model.Model = batterOptions[chosenShape];
                    var sc = model.AddScript<Layers>();

                    sc.flavourType = colorOptions[chosenShapeColor];
                    sc.shapeType = chosenShape;
                    sc.frostingFlavour = colorOptions[frostingColor];

                    var modelMaterial = displayPosition.GetChild(0).As<StaticModel>().MaterialSlots[0].Material.CreateVirtualInstance();

                    modelMaterial.SetParameterValue("Color", Color.ParseHex(colorOptions[chosenShapeColor]));

                    displayPosition.GetChild(0).As<StaticModel>().SetMaterial(0, modelMaterial);


                    var frost = model.AddChild<StaticModel>();
                    frost.Model = frostingOptions[chosenShape];

                    var frostMaterial = displayPosition.GetChild(0).GetChild(0).As<StaticModel>().MaterialSlots[0].Material.CreateVirtualInstance();
                    frostMaterial.SetParameterValue("Color", Color.ParseHex(colorOptions[frostingColor]));

                    displayPosition.GetChild(0).GetChild(0).As<StaticModel>().SetMaterial(0, frostMaterial);

                }
            }
        }

        public int getLayerSizeFromDiff()
        {

            if (difficulty == Difficulty.Intermediate)
            {
                int rand = random.Next(1, maxCakeLayers + 1);
                return rand;
            }
            else if (difficulty == Difficulty.Expert)
            {
                int rand = random.Next(2, maxCakeLayers + 1);
                return rand;
            }
            else
            {
                return maxCakeLayers;
            }
        }

        public void scoreSubmittedFood(PhysicsColliderActor food)
        {
            var parent = food.Parent;
            var sheet = parent.GetChild(0);

            if (sheet.ChildrenCount != layerSize)
            {
                score -= -40;
                food.Parent.IsActive = false;
            }
            else
            {
                for (int i = 0; i < layerSize; i++)
                {
                    var layer = sheet.GetChild(i);
                    var sc = layer.GetScript<Layers>();

                    var matchingLayer = displayPosition.GetChild(i);
                    var matchingScript = matchingLayer.GetScript<Layers>();

                    if(sc.shapeType == matchingScript.shapeType)
                    {
                        score += 100;
                    }
                    

                    if (sc.flavourType == matchingScript.flavourType)
                    {
                        score += 100;
                    }

                    if (sc.frostingFlavour == matchingScript.frostingFlavour)
                    {
                        score += 100;
                    }
                    //if (layer.Ge
                }

                food.Parent.IsActive = false;
                foodMade++;
            }
        }

        public void GameOver()
        {
            if(foodMade == foodToMake)
            {

            }

            if(incorrectFoodMade == maxIncorrectFoodAllowed)
            {
                score = 0;
            }
        }

        public void trashedFood()
        {
            score -= 300;
        }
    }
}
