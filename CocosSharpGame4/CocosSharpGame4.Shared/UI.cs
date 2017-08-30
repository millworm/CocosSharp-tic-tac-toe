using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;

namespace CocosSharpGame4
{
   public  class UI
    {
        public static void  ShowGameOver(CCLayerColor layer)
        {
            CCSprite background = new CCSprite("Pause Background");
            background.Position = layer.Position;
            background.Opacity = 0;
            background.Scale = 2f;
            layer.AddChild(background);
            background.RunAction(new CCSequence(new CCDelayTime(0.5f), new CCFadeIn(0.5f)));

            CCMenuItemImage overlayWindowsItem = new CCMenuItemImage("Pause Window", "Pause Window", "Pause Window",null);
            CCMenuItemImage retryItem = new CCMenuItemImage("Retry Button", "Retry Button","Retry Button",null);
            retryItem.Position = new CCPoint(overlayWindowsItem.ContentSize.Width / 4, retryItem.PositionY);
            CCMenuItemImage mainMenuItem = new CCMenuItemImage("Home Button", "Home Button", "Home Button", null);
            mainMenuItem.Position = new CCPoint(-overlayWindowsItem.ContentSize.Width / 4, mainMenuItem.PositionY);

            CCMenu menu = new CCMenu(overlayWindowsItem, retryItem, mainMenuItem);
            menu.Position = layer.ContentSize.Center;
            //menu.PositionY = layer.ContentSize.Center.Y + 
            layer.AddChild(menu);
               
                }
    }
}
