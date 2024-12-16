using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Snooooooooooooooooooooooooooooooo;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    Texture2D pixel;

    


    // skärmen är x = 800, y = 465

    Random r = new Random();

    List<Rectangle> boll = new List<Rectangle>();

    List<Point> bollhastighet = new List<Point>();

    List<Rectangle> golv = new List<Rectangle>();
    
    int bollxvo = 5;
    int bollyvo = 5;

    int kordinatx = 1;
    int kordinaty = 1;
    int xbollgräns = 800;
    int ybollgräns = 465;

    int countdown = 1;
    int countdown1 = 1;
    
    
    int stop = 2;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        
        base.Initialize();

    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        pixel = new Texture2D(GraphicsDevice, 1,1);
        pixel.SetData(new Color[]{Color.White});
        
        golv.Add(new Rectangle(1,ybollgräns-20,xbollgräns,40));
        golv.Add(new Rectangle(1,ybollgräns-40,xbollgräns-xbollgräns/3,40));
        
        

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)){
            Exit();
        }if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.O)){
            ResetElapsedTime();
        }

        KeyboardState kstate  = Keyboard.GetState();
    
    

        if(kstate.IsKeyDown(Keys.F)){
            stop = 2;
        }
        if(kstate.IsKeyDown(Keys.R)){
            stop = 1;
        }



        if(countdown != 10 && stop == 1){
            countdown++;
        }
        if(countdown == 10 && stop == 1 || kstate.IsKeyDown(Keys.R) /*|| countdown == 10*/){
            
            int Hurmångaloopar = 5;

            for(int i = 1; i < Hurmångaloopar; i++){
                kordinatx = r.Next(1 - xbollgräns,xbollgräns+1);
                boll.Add(new Rectangle(kordinatx,1,20,20));
                bollhastighet.Add(new Point(bollxvo += r.Next(1,6),bollyvo += r.Next(1,6))); 
                bollhastighet.Add(new Point(bollxvo += r.Next(1,6),bollyvo += r.Next(1,6)));
            }
            


            bollxvo = 3;
            bollyvo = 3;
            countdown = 1;
        }

        //gör ytan störe
        if (Keyboard.GetState().IsKeyDown(Keys.Up))
        {
            //skärmen
            _graphics.PreferredBackBufferWidth+=2;
            _graphics.PreferredBackBufferHeight+=2;
            //där bollen studsar
            ybollgräns+=2;
            xbollgräns+=2;
            //där de studsar
            kordinatx+=1;
            kordinaty+=1;

            golv[0] = new Rectangle(1,ybollgräns-20,xbollgräns,40);
            golv[1] = new Rectangle(1,ybollgräns-40,xbollgräns-xbollgräns/3,40);

            _graphics.ApplyChanges();
        }

        //gör den mindre
        if (Keyboard.GetState().IsKeyDown(Keys.Down))
        {
            //skärmen
            _graphics.PreferredBackBufferWidth-=2;
            _graphics.PreferredBackBufferHeight-=2;
            //där bollen studsar
            ybollgräns-=2;
            xbollgräns-=2;
            //där de studsar
            kordinatx-=1;
            kordinaty-=1;

            golv[0] = new Rectangle(1,ybollgräns-20,xbollgräns,40);
            golv[1] = new Rectangle(1,ybollgräns-40,xbollgräns-xbollgräns/3,40);

            _graphics.ApplyChanges();
        }

        
        
        
        
        

        for(int i = 0; i < boll.Count; i++){
            boll[i] = new Rectangle(boll[i].X + bollhastighet[i].X, boll[i].Y + bollhastighet[i].Y, boll[i].Width,boll[i].Height);
        

            if(boll[i].Y <= 0 || boll[i].Y >= ybollgräns){
                //bollhastighet[i] = new Point(bollhastighet[i].X, -bollhastighet[i].Y);
                boll[i] = new Rectangle(r.Next(-xbollgräns,xbollgräns+1),1,20,20);
            }

            if(boll[i].X <= -xbollgräns || boll[i].X >= xbollgräns){
                boll[i] = new Rectangle(r.Next(-xbollgräns,xbollgräns+1),1,20,20);
                //bollhastighet[i] = new Point(bollxvo *= -1,bollyvo *= -1);
                //bollhastighet[i] = new Point(-bollhastighet[i].X ,bollhastighet[i].Y);
                

            }
        
        }

        // TODO: Add your update logic here

        base.Update(gameTime);
    }


    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        
        foreach(Rectangle bollar in boll){
            _spriteBatch.Draw(pixel,bollar,Color.White);
        }
        foreach(Rectangle golv in golv){
            _spriteBatch.Draw(pixel,golv,Color.White);
        }
        _spriteBatch.End();


        base.Draw(gameTime);
    }
}