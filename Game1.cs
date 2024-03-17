using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using ClassesGame;
using monsters;
using UI;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using System.Threading;

namespace creations_monsters
{
    enum room { start,warnings,select_powers_room,Monster_Room,win,lose}
    public class Game1 : Game
    {
        SpriteFont font;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Rectangle Background;
        Texture2D Background_Texture;
        Rectangle monster_enemy_bar_range,player_bar_range;
        Button[] Options_Buttons = new Button[4];
        Power_Button[] Hability_Buttons = new Power_Button[5];
        MonsterOfCreation Monster_Model,player;
        ElementalPowers[] elementos_player = new ElementalPowers[0];
        string elementos_escritos,elementos_inimigo;
        room Loaded_room;
        private int level;
        float deltaTime;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.PreferredBackBufferWidth = 1280;  // Largura desejada
            _graphics.PreferredBackBufferHeight = 720; // Altura desejada
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            Loaded_room = room.start;
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }
        int posscenarebary;
        protected override void LoadContent()
        {
            switch (Loaded_room)
            {
                case room.start:
                    level = 1;
                    font = Content.Load<SpriteFont>("fonte");
                    Background = new Rectangle(0, 0, 1280, 720);
                    Background_Texture = Content.Load<Texture2D>("start_background");
                    Options_Buttons[0] = new Button(font, "", Content.Load<Texture2D>("start_button1"), new Rectangle(50, 100, 170, 170), Color.White);
                    Options_Buttons[1] = new Button(font, "", Content.Load<Texture2D>("start_button2"), new Rectangle(50, 300, 170, 170), Color.White);
                    Options_Buttons[2] = new Button(font, "", Content.Load<Texture2D>("start_button3"), new Rectangle(50, 500, 170, 170), Color.White);
                    Options_Buttons[3] = new Button(font, "", Content.Load<Texture2D>("start_button4"), new Rectangle(930, 450, 250, 250), Color.White);
                    break;
                case room.warnings:
                    break;
                case room.Monster_Room:
                    elementos_escritos = "";
                    elementos_inimigo = "";
                    Hability_Buttons[0] = new Power_Button(font, "", Content.Load<Texture2D>("space_rune"), new Rectangle(200, 566, 64, 64), Color.White, ElementalPowers.space);
                    Hability_Buttons[1] = new Power_Button(font, "", Content.Load<Texture2D>("time_rune"), new Rectangle(264, 566, 64, 64), Color.White, ElementalPowers.time);
                    Hability_Buttons[2] = new Power_Button(font, "", Content.Load<Texture2D>("reality_rune"), new Rectangle(328, 566, 64, 64), Color.White, ElementalPowers.reality);
                    Hability_Buttons[3] = new Power_Button(font, "", Content.Load<Texture2D>("life_rune"), new Rectangle(392, 566, 64, 64), Color.White, ElementalPowers.life);
                    Hability_Buttons[4] = new Power_Button(font, "", Content.Load<Texture2D>("energy_rune"), new Rectangle(456, 566, 64, 64), Color.White, ElementalPowers.energy);
                    player = new MonsterOfCreation("Player",100, 1, 1, new int[] { 0, 1, 2, 3, 4 }, 0, Content.Load<Texture2D>("player_background"), new Rectangle(100, 650, 512, 64), Color.White,null);
                    monster_enemy_bar_range = new Rectangle(400, 5, 512, 16);
                    player_bar_range = new Rectangle(player.Area.X + 75, player.Area.Y + 15, 350, 32);
                    Vector2[] positions;
                    switch (level) {
                        case 1: //prigon
                            positions = new Vector2[12];
                            positions[0] = new Vector2(200, 25);
                            positions[1] = new Vector2(225, 50);
                            positions[2] = new Vector2(225, 75);
                            positions[3] = new Vector2(200, 100);
                            positions[4] = new Vector2(250, 100);
                            positions[5] = new Vector2(200, 100);
                            positions[6] = new Vector2(200, 150);
                            positions[7] = new Vector2(200, 100);
                            positions[8] = new Vector2(150, 100);
                            positions[9] = new Vector2(200, 100);
                            positions[10] = new Vector2(175, 75);
                            positions[11] = new Vector2(175, 50);
                            Monster_Model = new MonsterOfCreation("PRIGON",150, 5, 2, new int[] { 0, 5, 6, 7, 8 }, 0, Content.Load<Texture2D>("spacemonster"), new Rectangle(200, 25, 900, 506), Color.White,positions);
                            break;
                        case 2: //time monster
                            Monster_Model = new MonsterOfCreation("NULL",200, 3, 4, new int[] { 0, 5, 6, 7, 8 }, 1, Content.Load<Texture2D>("timemonster"), new Rectangle(200, 25, 900, 506), Color.White,null);
                            break;
                        case 3: //life monster
                            Monster_Model = new MonsterOfCreation("NULL",300, 2, 5, new int[] { 0, 5, 6, 7, 8 }, 2, Content.Load<Texture2D>("lifemonster"), new Rectangle(200, 25, 900, 506), Color.White, null);
                            break;
                    }
                    //graph divece
                    posscenarebary = 715;
                    break;
                case room.win:
                    break;
                case room.lose:
                    break;
            }
            // TODO: use this.Content to load your game content here
        }
        //graph divices
        float color_transition = 0.8f;
        float colorfeel = 0.002f;
        protected override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            switch (Loaded_room)
            {
                case room.start:
                    for (int i = 0; i < 4; i++)
                    {
                        Options_Buttons[i].Update(gameTime, state, Mouse.GetState(), null);
                    }
                    if (Options_Buttons[3].isClicked)
                    {
                        Options_Buttons[3].Area = new Rectangle(Options_Buttons[3].Area.X, Options_Buttons[3].Area.Y - 250, Options_Buttons[3].Area.Width, Options_Buttons[3].Area.Height);
                        Loaded_room = room.warnings;
                        Options_Buttons[3].isClicked = false;
                    }
                    break;
                case room.warnings:
                    Options_Buttons[3].Update(gameTime, state, Mouse.GetState(), null);
                    if (Options_Buttons[3].isClicked)
                    {
                        Loaded_room = room.Monster_Room;
                        LoadContent();
                    }
                    break;
                case room.Monster_Room:
                    for (int i = 0; i < Hability_Buttons.Length; i++) Hability_Buttons[i].Update(gameTime, state, Mouse.GetState(), null);
                    if (monster_enemy_bar_range.Width > 512 * (int)Monster_Model.Life / (int)Monster_Model.MaxLife) {monster_enemy_bar_range.Width--; Console.WriteLine("teste"); }
                    else
                    if (player_bar_range.Width > 350 * (int)player.Life / (int)player.MaxLife) {player_bar_range.Width--; Console.WriteLine("teste"); }
                    else
                    if (monster_enemy_bar_range.Width < 512 * (int)Monster_Model.Life / (int)Monster_Model.MaxLife) {monster_enemy_bar_range.Width++; Console.WriteLine("teste");}
                    else
                    if (player_bar_range.Width < 350 * (int)player.Life / (int)player.MaxLife) {player_bar_range.Width++;Console.WriteLine("teste"); }
                    else
                    if (elementos_player.Length == 3)
                    {
                        Console.WriteLine("encheu");
                        player.select_power(elementos_player, Monster_Model);
                        Monster_Model.monster_attack(player);
                        elementos_player = new ElementalPowers[0];
                        for (int i = 0; i < Hability_Buttons.Length; i++) Hability_Buttons[i].actived = false;
                    }
                    else
                        for (int i = 0; i < Hability_Buttons.Length; i++)
                        {
                            if (Hability_Buttons[i].isClicked && Hability_Buttons[i].actived == false && elementos_player.Length < 3)
                            {
                                Console.WriteLine("escolheu");
                                Hability_Buttons[i].actived = true;
                                ElementalPowers[] aux = elementos_player;
                                elementos_player = new ElementalPowers[elementos_player.Length + 1];
                                for (int j = 0; j < aux.Length; j++)
                                {
                                    elementos_player[j] = aux[j];
                                }
                                elementos_player[elementos_player.Length - 1] = Hability_Buttons[i].power;
                            }
                        }
                    if (elementos_player.Length == 1) elementos_escritos = "YOUR ELEMENTS : \n" + elementos_player[0];
                    else if (elementos_player.Length == 2) elementos_escritos = "YOUR ELEMENTS : \n" + elementos_player[0] + "\n" + elementos_player[1];
                    else if (elementos_player.Length == 3) elementos_escritos = "YOUR ELEMENTS : \n" + elementos_player[0] + "\n" + elementos_player[1] + "\n" + elementos_player[2];
                    if (Monster_Model.habilidade.Length == 3) elementos_inimigo = "ENEMY ELEMENTS : \n" + Monster_Model.habilidade[0] + "\n" + Monster_Model.habilidade[1] + "\n" + Monster_Model.habilidade[2];
                    if (Monster_Model.Life <= 0) { 
                        level++;
                        if(level > 3)Loaded_room = room.win;
                        LoadContent(); 
                    }
                    if (player.Life <= 0) {Loaded_room = room.lose; LoadContent(); }
                    Monster_Model.Update(gameTime, state, Mouse.GetState(), null);
                    //graph divices
                    if (color_transition > 1)
                        colorfeel *= -1;
                    if (color_transition < 0.7)
                        colorfeel *= -1;
                    color_transition += colorfeel;
                    if (posscenarebary <= 0)
                        posscenarebary = 715;
                    posscenarebary-=5;
                    break;
                case room.win:
                    break;
                case room.lose:
                    break;
            }
            // TODO: Add your update logic here
            base.Update(gameTime);
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            // TODO: Add your drawing code here
            switch (Loaded_room) { 
                case room.start:
            _spriteBatch.Draw(Background_Texture, Background, Color.White);
                    for(int i = 0; i < 4; i++)
                    {
                        Options_Buttons[i].Draw(_spriteBatch);
                    }
                    break;
                case room.warnings:
                    GraphicsDevice.Clear(Color.Black);
                    _spriteBatch.DrawString(font, "AGRADECIMENTOS \n OBRIGADO POR TESTAR A ALPHA DO GAME. O PROJETO AINDA ESTA MUITO NO INICIO, \n ENTAO ESPERAMOS QUE GOSTEM DO PROJETO E UM FEEDBACK PARA MELHORARMOS O GAME. \n HABILIDADES \n VOCE SE UTILIZARA DA JUNCAO DE 3 ELEMENTOS PARA FORMAR UMA HABILIDADE. \n A HABILIDADE IRA CONDIZER COM BASE NOS ELEMENTOS. EXEMPLO: \n SPACE -> DANO, \n LIFE -> AUMENTO DE CURA, \n REALITY -> AUMENTO DO DANO, \n ENERGY -> REDUZIR DANO DO INIMIGO, \n TIME -> REDUZIR CURA DO INIMIGO. \n STATS \n DAMAGE: DANO QUE SERA CAUSADO AO INIMIGO, \n HEALTH: CURA QUE SERA REALIZADA POR VOCE. \n EFEITOS \n CORRUPTED: NA RODADA, VOCE DARA DANO A SI MESMO.", new Vector2(100, 16), Color.White);
                    //_spriteBatch.DrawString(font, "THANK YOU FOR TESTING THIS GAME'S ALPHA, \n THE PROJECT IS STILL IN ITS BEGINNING, \n SO WE HOPE YOU LIKE THE PROJECT, \n AND GIVE US FEEDBACK TO IMPROVE THE GAME", new Vector2(100, 500), Color.White);
                    Options_Buttons[3].Draw(_spriteBatch);
                    break;
                case room.Monster_Room:
                    //definições de background
                    Color back = Color.Lerp(Color.Purple, Color.Black, color_transition);
                    GraphicsDevice.Clear(back);
                    _spriteBatch.Draw(Content.Load<Texture2D>("life_bar"), new Rectangle(0,posscenarebary,250,20), Color.White);
                    _spriteBatch.Draw(Content.Load<Texture2D>("life_bar"), new Rectangle(1030, posscenarebary, 500, 20), Color.White);
                    switch (Monster_Model.monstertype)
                    {
                        case functionofmonster.space:
                            _spriteBatch.Draw(Content.Load<Texture2D>("circle"), new Rectangle(375, 50, 520, 520), Color.White);
                            break;
                        case functionofmonster.time:
                            _spriteBatch.Draw(Content.Load<Texture2D>("triangle"), new Rectangle(375, 50, 520, 520), Color.White);
                            break;
                        case functionofmonster.life:
                            _spriteBatch.Draw(Content.Load<Texture2D>("square"), new Rectangle(375, 50, 520, 520), Color.White);
                            break;
                    }
                    //player e monster
                    Monster_Model.Draw(_spriteBatch);
                    player.Draw(_spriteBatch);
                    //barras de vida
                    _spriteBatch.Draw(Content.Load<Texture2D>("life_bar"), monster_enemy_bar_range, Color.White);
                    _spriteBatch.Draw(Content.Load<Texture2D>("life_bar"), player_bar_range, Color.White);
                    _spriteBatch.DrawString(font,Monster_Model.name + (int)Monster_Model.Life + " / " + Monster_Model.MaxLife, new Vector2(monster_enemy_bar_range.X, monster_enemy_bar_range.Y), Color.Black);
                    _spriteBatch.DrawString(font, "YOU " + (int)player.Life + " / 100", new Vector2(player_bar_range.X, player_bar_range.Y), Color.Black);
                    //caixas de texto
                    _spriteBatch.Draw(Content.Load<Texture2D>("box_text"), new Rectangle(0, 0, 200, 192), Color.White);
                    _spriteBatch.Draw(Content.Load<Texture2D>("box_text"), new Rectangle(800,516,200,192), Color.White);
                    _spriteBatch.DrawString(font, elementos_inimigo, new Vector2(8, 16), Color.White);
                    _spriteBatch.DrawString(font,elementos_escritos, new Vector2(808, 532), Color.White);
                    //dano e cura
                    _spriteBatch.DrawString(font, "DAMAGE : " + (int)Monster_Model.damage + " HEALTH : " + (int)Monster_Model.cura, new Vector2(8, 96), Color.White);
                    _spriteBatch.DrawString(font, "DAMAGE : " + (int)player.damage + " HEALTH : " + (int)player.cura, new Vector2(808, 612), Color.White);
                    //effects
                    if(Monster_Model.corrupted) _spriteBatch.DrawString(font,"CORRUPTED", new Vector2(8, 112), Color.Red);
                    if(player.corrupted) _spriteBatch.DrawString(font, "CORRUPTED", new Vector2(808, 628), Color.Red);
                    for (int i = 0; i < 5; i++) Hability_Buttons[i].Draw(_spriteBatch);
                    break;
                case room.win:
                    _spriteBatch.DrawString(font, "easy", new Vector2(500, 225), Color.Red);
                    _spriteBatch.Draw(Content.Load<Texture2D>("easy"), new Rectangle(500,250, 200, 200), Color.White);
                    break;
                case room.lose:
                    _spriteBatch.DrawString(font, "HAHAHAHAHA", new Vector2(500, 225), Color.Red);
                    _spriteBatch.Draw(Content.Load<Texture2D>("misery"), new Rectangle(500,250, 200, 200), Color.White);
                    break;
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}