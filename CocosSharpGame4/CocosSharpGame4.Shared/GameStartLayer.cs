using System;
using System.Collections.Generic;
using CocosSharp;

namespace CocosSharpGame4
{
    public class GameStartLayer : CCLayerColor
    {
        CCSprite background;
        CCSprite gameName;
        CCSprite playButton, playButtonOuter;
        CCSprite soundOn, soundOff;


        protected override void AddedToScene()
        {
            base.AddedToScene();
            var bounds = VisibleBoundsWorldspace;
            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesEnded = OnTouchesEnded;
            AddEventListener(touchListener, this);

            CreateBackground();
            CreateImages();
        }



        private void CreateBackground()
        {
            background = new CCSprite("Main Menu Background");
            background.PositionX = 0;
            background.PositionY = 0;
            background.Scale = 2f;
            //background.IsAntialiased = true;
            AddChild(background);
        }

        private void CreateImages()
        {
            gameName = new CCSprite("Game Title");
            gameName.AnchorPoint = CCPoint.AnchorMiddleTop;
            gameName.PositionX = VisibleBoundsWorldspace.MaxX / 2;
            gameName.PositionY = VisibleBoundsWorldspace.MaxY - 100;
            gameName.IsAntialiased = true;
            //gameName.Scale = 0.9f;
            AddChild(gameName);

            playButton = new CCSprite("Play Button");
            playButton.PositionX = VisibleBoundsWorldspace.MaxX / 2;
            playButton.PositionY = gameName.PositionY - gameName.Texture.PixelsHigh - 100;
            playButton.IsAntialiased = true;
            playButton.AnchorPoint = CCPoint.AnchorMiddleTop;
            AddChild(playButton);

            playButtonOuter = new CCSprite("Play Button Outer");
            playButtonOuter.PositionX = VisibleBoundsWorldspace.MaxX / 2;
            playButtonOuter.PositionY = playButton.PositionY - playButton.Texture.PixelsHigh / 2 + 10;
            playButtonOuter.IsAntialiased = true;
            playButtonOuter.AnchorPoint = CCPoint.AnchorMiddleTop;
            AddChild(playButtonOuter);

            soundOn = new CCSprite("sound On");
            soundOn.PositionX = VisibleBoundsWorldspace.MinX + 20;
            soundOn.PositionY = VisibleBoundsWorldspace.MinY + 20;
            soundOn.IsAntialiased = true;
            soundOn.AnchorPoint = CCPoint.AnchorLowerLeft;
            AddChild(soundOn);

            soundOff = new CCSprite("sound Off");
            soundOff.PositionX = VisibleBoundsWorldspace.MinX + 20;
            soundOff.PositionY = VisibleBoundsWorldspace.MinY + 20;
            soundOff.IsAntialiased = true;
            soundOff.AnchorPoint = CCPoint.AnchorLowerLeft;
            soundOff.Visible = false;
            AddChild(soundOff);
        }

        void OnTouchesEnded(List<CCTouch> touches, CCEvent touchEvent)
        {
            if (playButton.BoundingBoxTransformedToWorld.ContainsPoint(touches[0].Location) || playButtonOuter.BoundingBoxTransformedToWorld.ContainsPoint(touches[0].Location))
            {
                // gameName.Visible = !gameName.Visible;
                CocosSharpGame4.Shared.GameDelegate.GoToHowToScene();
            }
            else
              if (soundOn.BoundingBoxTransformedToWorld.ContainsPoint(touches[0].Location))
            {
                soundOn.Visible = !soundOn.Visible;
                soundOff.Visible = !soundOff.Visible;
            }

        }

    }
}