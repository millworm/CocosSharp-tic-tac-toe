using System;
using System.Collections.Generic;

using CocosSharp;
using CocosDenshion;

namespace CocosSharpGame4.Shared
{
    public static class GameDelegate
    {
        public static CCGameView gameView;
        public static void LoadGame(object sender, EventArgs e)
        {
            gameView = sender as CCGameView;
            if (gameView != null)
            {
                var contentSearchPaths = new List<string>() { "Fonts", "Sounds", "Images" };
                CCSizeI viewSize = gameView.ViewSize;
                int width = 384*2;
                int height = 568*2;
               // CCSprite.DefaultTexelToContentSizeRatio = 1.0f;
                // Set world dimensions
                gameView.DesignResolution = new CCSizeI(width, height);
                gameView.Stats.Enabled = true;
                gameView.Stats.Scale = 2;
                // Determine whether to use the high or low def versions of our images
                // Make sure the default texel to content size ratio is set correctly
                // Of course you're free to have a finer set of image resolutions e.g (ld, hd, super-hd)
                //if (width < viewSize.Width)
                //{
                //    contentSearchPaths.Add("Images/Hd");
                //    CCSprite.DefaultTexelToContentSizeRatio = 2.0f;
                //}
                //else
                //{
                //    contentSearchPaths.Add("Images/Ld");
                //    CCSprite.DefaultTexelToContentSizeRatio = 1.0f;
                //}

                gameView.ContentManager.SearchPaths = contentSearchPaths;

                CCScene gameScene = new CCScene(gameView);
                gameScene.AddLayer(new GameStartLayer());
                gameView.RunWithScene(gameScene);
            }
        }

        public static void GoToDuoScene()
        {
            CCScene gameScene = new CCScene(gameView);
            gameScene.AddLayer(new DuoGameLayer());

            gameView.Director.PushScene(new CCTransitionFade(1f, gameScene));
        }

        internal static void GoToSoloScene()
        {
            throw new NotImplementedException();
        }
    }
}