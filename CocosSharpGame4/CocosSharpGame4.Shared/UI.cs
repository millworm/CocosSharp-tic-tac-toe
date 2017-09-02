using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;
namespace CocosSharpGame4
{
    public class UI
    {
        CCLayerColor layer;
        public UI(CCLayerColor _layer)
        {
            layer = _layer;
        }
        public void ShowGameOver()
        {
            CCSprite retryButton = new CCSprite("Retry Button"),
                     homeButton = new CCSprite("Home Button"),
                     pauseWindow = new CCSprite("Pause Window");

            CCSprite background = new CCSprite("Pause Background");
            background.Position = layer.Position;
            background.Opacity = 0;
            background.Scale = 2f;
            layer.AddChild(background);
            background.RunAction(new CCSequence(new CCDelayTime(2f), new CCFadeIn(1f)));

            CCMenuItemImage overlayWindowItem = new CCMenuItemImage(pauseWindow, null);

            CCMenuItemImage retryItem = new CCMenuItemImage(retryButton, retryButton, Retry);
            retryItem.Position = new CCPoint(overlayWindowItem.ContentSize.Width/ 4, retryItem.PositionY);
            
            CCMenuItemImage mainMenuItem = new CCMenuItemImage(homeButton, homeButton, HomeScene);
            mainMenuItem.Position = new CCPoint(-overlayWindowItem.ContentSize.Width / 4, mainMenuItem.PositionY);
            
            CCMenu menu = new CCMenu(retryItem, mainMenuItem, overlayWindowItem, retryItem, mainMenuItem);
            
            menu.Position = layer.ContentSize.Center;
            menu.Opacity = 0;
            menu.RunAction(new CCSequence(new CCDelayTime(2f), new CCFadeIn(1f)));

           // menu.RunAction(new CCSequence(new CCDelayTime(1.5f), new CCEaseBounceInOut(new CCMoveTo(1.5f, layer.ContentSize.Center))));

            layer.AddChild(menu);

           
        }



        private void HomeScene(object obj)
        {
            CCScene scene = new CCScene(Shared.GameDelegate.gameView);
            scene.AddLayer(new CocosSharpGame4.GameStartLayer());
            Shared.GameDelegate.gameView.Director.PopScene(1f,new CCTransitionFade(1f, scene));
        }

        private void Retry(object obj)
        {
            CCScene scene = new CCScene(Shared.GameDelegate.gameView);
            scene.AddLayer(new Shared.DuoGameLayer());
            CCTransitionScene tr = new CCTransitionFade(1f, scene);

            Shared.GameDelegate.gameView.Director.ReplaceScene(tr);

        }
    }
}
