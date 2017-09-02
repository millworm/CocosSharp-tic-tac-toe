using System;
using System.Collections.Generic;
using CocosSharp;

namespace CocosSharpGame4
{
    public class GameStartLayer : CCLayerColor
    {
        CCSprite background;
        CCSprite gameName;
        CCSprite playButtonSolo, playButtonDuo;
        CCSprite soundOn, soundOff;
        CCSprite playButtonSoloText, playButtonDuoText;


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
            AddChild(background);
        }

        private void CreateImages()
        {
            gameName = new CCSprite("Game Title");
            gameName.AnchorPoint = CCPoint.AnchorMiddleTop;
            gameName.PositionX = VisibleBoundsWorldspace.MaxX / 2;
            gameName.PositionY = VisibleBoundsWorldspace.MaxY - 50;
            gameName.IsAntialiased = true;
            AddChild(gameName);

            playButtonSolo = new CCSprite("Play Button");
            playButtonSolo.PositionX = VisibleBoundsWorldspace.MaxX / 4;
            playButtonSolo.PositionY = gameName.BoundingBox.MinY - 100;
            playButtonSolo.IsAntialiased = true;
            playButtonSolo.AnchorPoint = CCPoint.AnchorMiddleRight;
            AddChild(playButtonSolo);

            playButtonSoloText = new CCSprite("Solo Player");
            playButtonSoloText.PositionX = playButtonSolo.BoundingBox.MaxX;
            playButtonSoloText.PositionY = playButtonSolo.BoundingBox.Center.Y;
            playButtonSoloText.IsAntialiased = true;
            playButtonSoloText.AnchorPoint = CCPoint.AnchorMiddleLeft;
            AddChild(playButtonSoloText);

            playButtonDuo = new CCSprite("Play Button");
            playButtonDuo.PositionX = VisibleBoundsWorldspace.MaxX / 4 * 3;
            playButtonDuo.PositionY = playButtonSolo.BoundingBox.MinY - 200;
            playButtonDuo.IsAntialiased = true;
            playButtonDuo.FlipX = true;
            playButtonDuo.AnchorPoint = CCPoint.AnchorMiddleLeft;
            AddChild(playButtonDuo);

            playButtonDuoText = new CCSprite("Duo Players");
            playButtonDuoText.PositionX = playButtonDuo.BoundingBox.MinX-20;
            playButtonDuoText.PositionY = playButtonDuo.BoundingBox.Center.Y;
            playButtonDuoText.IsAntialiased = true;
            playButtonDuoText.AnchorPoint = CCPoint.AnchorMiddleRight;
            AddChild(playButtonDuoText);
            /*
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
             */
        }

        void OnTouchesEnded(List<CCTouch> touches, CCEvent touchEvent)
        {
            if (playButtonSolo.BoundingBoxTransformedToWorld.ContainsPoint(touches[0].Location) || playButtonSoloText.BoundingBoxTransformedToWorld.ContainsPoint(touches[0].Location))
            {
                Shared.GameDelegate.GoToSoloScene();
                return;
            }

            if (playButtonDuo.BoundingBoxTransformedToWorld.ContainsPoint(touches[0].Location) || playButtonDuoText.BoundingBoxTransformedToWorld.ContainsPoint(touches[0].Location))
            {
                Shared.GameDelegate.GoToDuoScene();
                return;
            }


        }

    }
}