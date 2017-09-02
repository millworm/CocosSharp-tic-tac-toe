using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;

namespace CocosSharpGame4.Shared
{
    public class DuoGameLayer : CCLayerColor
    {
        float PIECE_FAD_IN_TIME = 0.4f;
        bool x_stroke = false;
        CCSprite gridSprite;
        CCSprite background, cellSprite, playerSprite;
        CCRect[,] gridSpaces;
        CCSprite[,] gridPieces;
        CCLabel label;
        List<Point> positions;
        int[,] gridArray;
        STATE gameState;
        int emptyCells = 9;
        UI ui;

        public DuoGameLayer() : base()
        {
            positions = new List<Point>();
            positions.Add(new Point(134, 373)); positions.Add(new Point(247, 373)); positions.Add(new Point(360, 373));
            positions.Add(new Point(134, 258)); positions.Add(new Point(247, 258)); positions.Add(new Point(360, 258));
            positions.Add(new Point(134, 145)); positions.Add(new Point(247, 145)); positions.Add(new Point(360, 145));

            ui = new UI(this);
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();

            var bounds = VisibleBoundsWorldspace;

            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesEnded = OnTouchesEnded;
            AddEventListener(touchListener, this);
            
            background = new CCSprite("Main Menu Background.png");
            background.PositionX = 0;
            background.PositionY = 0;
            background.Scale = 2f;
            background.IsAntialiased = false;
            AddChild(background);
                        
            gridSprite = new CCSprite("Grid.png");
            gridSprite.PositionX = VisibleBoundsWorldspace.MaxX / 2;
            gridSprite.PositionY = VisibleBoundsWorldspace.MaxY / 2;
            gridSprite.AnchorPoint = CCPoint.AnchorMiddle;
            //grid.Scale = 2f;
            AddChild(gridSprite);
            
            label = new CCLabel("", "arial", 36);
            label.PositionX = VisibleBoundsWorldspace.MaxX - 100;
            label.PositionY = VisibleBoundsWorldspace.MaxY - 100;
            AddChild(label);

            cellSprite = new CCSprite("Cell.png");
            cellSprite.PositionX = gridSprite.BoundingBox.Center.X;
            cellSprite.PositionY = gridSprite.BoundingBox.MaxY + 50;
            cellSprite.AnchorPoint = CCPoint.AnchorMiddleBottom;
            AddChild(cellSprite);

            playerSprite = new CCSprite("O");
            playerSprite.Position = cellSprite.BoundingBox.Center;
            playerSprite.AnchorPoint = CCPoint.AnchorMiddle;
            AddChild(playerSprite);

            gridArray = new int[3, 3];
            InitGridRects();
            InitGridPieces();
        }

        private void InitGridRects()
        {
            float cellWidth = gridSprite.BoundingBox.Size.Width / 3,
                  cellHeight = gridSprite.BoundingBox.Size.Height / 3;
            gridSpaces = new CCRect[3, 3];

            gridSpaces[0, 0] = new CCRect(gridSprite.BoundingBox.MinX, gridSprite.BoundingBox.MinY, cellWidth, cellHeight);
            gridSpaces[1, 0] = new CCRect(gridSprite.BoundingBox.MinX + cellWidth, gridSprite.BoundingBox.MinY, cellWidth, cellHeight);
            gridSpaces[2, 0] = new CCRect(gridSprite.BoundingBox.MinX + cellWidth * 2, gridSprite.BoundingBox.MinY, cellWidth, cellHeight);

            gridSpaces[0, 1] = new CCRect(gridSprite.BoundingBox.MinX, gridSprite.BoundingBox.MinY + cellHeight, cellWidth, cellHeight);
            gridSpaces[1, 1] = new CCRect(gridSprite.BoundingBox.MinX + cellWidth, gridSprite.BoundingBox.MinY + cellHeight, cellWidth, cellHeight);
            gridSpaces[2, 1] = new CCRect(gridSprite.BoundingBox.MinX + cellWidth * 2, gridSprite.BoundingBox.MinY + cellHeight, cellWidth, cellHeight);

            gridSpaces[0, 2] = new CCRect(gridSprite.BoundingBox.MinX, gridSprite.BoundingBox.MinY + cellHeight * 2, cellWidth, cellHeight);
            gridSpaces[1, 2] = new CCRect(gridSprite.BoundingBox.MinX + cellWidth, gridSprite.BoundingBox.MinY + cellHeight * 2, cellWidth, cellHeight);
            gridSpaces[2, 2] = new CCRect(gridSprite.BoundingBox.MinX + cellWidth * 2, gridSprite.BoundingBox.MinY + cellHeight * 2, cellWidth, cellHeight);
        }

        void InitGridPieces()
        {
            

            gridPieces = new CCSprite[3, 3];
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    gridPieces[x, y] = new CCSprite("X");
                    gridPieces[x, y].PositionX = gridSprite.PositionX + (gridPieces[x, y].ContentSize.Width * (x - 1));
                    gridPieces[x, y].PositionY = gridSprite.PositionY + (gridPieces[x, y].ContentSize.Height * (y - 1));
                    gridPieces[x, y].Visible = false;
                    gridPieces[x, y].Opacity = 0;
                    AddChild(gridPieces[x, y]);
                }
            }
        }
        void OnTouchesEnded(List<CCTouch> touches, CCEvent touchEvent)
        {
            if (touches.Count > 0)
            {
                if (gameState == STATE.STATE_PLAYING)
                    CheckAndPlacePiece(touches[0]);
                else
                {

                }
                //label.Text = $"{touches[0].Location.X.ToString("0")}:{touches[0].Location.Y.ToString("0")}";

            }


        }

        private void CheckWin()
        {
            Check3PiecesForMatch(0, 0, 1, 0, 2, 0, PIECE.O);
            Check3PiecesForMatch(0, 1, 1, 1, 2, 1, PIECE.O);
            Check3PiecesForMatch(0, 2, 1, 2, 2, 2, PIECE.O);
            Check3PiecesForMatch(0, 0, 0, 1, 0, 2, PIECE.O);
            Check3PiecesForMatch(1, 0, 1, 1, 1, 2, PIECE.O);
            Check3PiecesForMatch(2, 0, 2, 1, 2, 2, PIECE.O);
            Check3PiecesForMatch(0, 0, 1, 1, 2, 2, PIECE.O);
            Check3PiecesForMatch(0, 2, 1, 1, 2, 0, PIECE.O);

            if (gameState != STATE.STATE_WIN)
            {
                Check3PiecesForMatch(0, 0, 1, 0, 2, 0, PIECE.X);
                Check3PiecesForMatch(0, 1, 1, 1, 2, 1, PIECE.X);
                Check3PiecesForMatch(0, 2, 1, 2, 2, 2, PIECE.X);
                Check3PiecesForMatch(0, 0, 0, 1, 0, 2, PIECE.X);
                Check3PiecesForMatch(1, 0, 1, 1, 1, 2, PIECE.X);
                Check3PiecesForMatch(2, 0, 2, 1, 2, 2, PIECE.X);
                Check3PiecesForMatch(0, 0, 1, 1, 2, 2, PIECE.X);
                Check3PiecesForMatch(0, 2, 1, 1, 2, 0, PIECE.X);
            }

            if(emptyCells == 0)
            {
                gameState = STATE.STATE_DRAW;
            }

            if (gameState == STATE.STATE_DRAW || gameState == STATE.STATE_LOSE || gameState == STATE.STATE_WIN)
            {
                ui.ShowGameOver();
            }

        }

        private void CheckAndPlacePiece(CCTouch touch)
        {
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (gridSpaces[x, y].ContainsPoint(touch.Location) && gridArray[x, y] == 0 && gameState == STATE.STATE_PLAYING)
                    {
                        
                        if (x_stroke)
                        {
                            gridPieces[x, y].Texture = new CCTexture2D("X");
                            gridArray[x, y] = 1;

                            playerSprite.Texture = new CCTexture2D("O");
                        }
                        else
                        {
                            gridPieces[x, y].Texture = new CCTexture2D("O");
                            gridArray[x, y] = 2;

                            playerSprite.Texture = new CCTexture2D("X");
                        }

                        gridPieces[x, y].Visible = true;
                        gridPieces[x,y].RunAction(new CCSequence(new CCFadeIn(PIECE_FAD_IN_TIME),new CCCallFunc(CheckWin)));
                        x_stroke = !x_stroke;


                        emptyCells--;
                        return;
                    }
                }
            }
        }

        void Check3PiecesForMatch(int x1, int y1, int x2, int y2, int x3, int y3, PIECE pieceToMatch)
        {
            if (gridArray[x1, y1] == (int)pieceToMatch && gridArray[x2, y2] == (int)pieceToMatch && gridArray[x3, y3] == (int)pieceToMatch)
            {
                string winningPieceName;
                CCSprite[] winningPiece = new CCSprite[3];

                if (pieceToMatch == PIECE.X)
                {
                    winningPieceName = "X Win";
                    gameState = STATE.STATE_WIN;                    
                }
                else
                {
                    winningPieceName = "O Win";
                    gameState = STATE.STATE_LOSE;                                      
                }

                CCDelayTime tt = new CCDelayTime(1.5f);

                winningPiece[0] = new CCSprite(winningPieceName);
                winningPiece[0].Position = gridPieces[x1, y1].Position;
                winningPiece[0].Opacity = 0;
                AddChild(winningPiece[0]);

                winningPiece[1] = new CCSprite(winningPieceName);
                winningPiece[1].Position = gridPieces[x2, y2].Position;
                winningPiece[1].Opacity = 0;
                AddChild(winningPiece[1]);

                winningPiece[2] = new CCSprite(winningPieceName);
                winningPiece[2].Position = gridPieces[x3, y3].Position;
                winningPiece[2].Opacity = 0;
                AddChild(winningPiece[2]);

                playerSprite.Texture = new CCTexture2D(winningPieceName);
                playerSprite.Opacity = 0;

                playerSprite.RunAction(new CCSequence(new CCDelayTime(0.3f), new CCFadeIn(PIECE_FAD_IN_TIME)));
                winningPiece[0].RunAction(new CCFadeIn(PIECE_FAD_IN_TIME));
                winningPiece[1].RunAction(new CCSequence(new CCDelayTime(0.3f), new CCFadeIn(PIECE_FAD_IN_TIME)));
                winningPiece[2].RunAction(new CCSequence(new CCDelayTime(0.6f), new CCFadeIn(PIECE_FAD_IN_TIME)));

            }
        }
        
        enum STATE
        {
            STATE_PLAYING, STATE_WIN, STATE_LOSE, STATE_DRAW
        }

        enum PIECE
        {
            EMPTY = 0, X = 1, O = 2
        }
    }

}

