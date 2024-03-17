using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using monsters;
using ClassesGame;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.Threading;
namespace UI
{ 
    public class Button : GameObject
    {
		string button_string;
		SpriteFont font;
        public bool isClicked, click;
        public Button(SpriteFont font,string button_string,Texture2D texture, Rectangle area, Color color) : base(texture,area,color)
        {
			this.button_string = button_string;
			this.font = font;
			isClicked = false;
			click = false;
        }
        bool down;
        
        public override void Update(GameTime gametime,KeyboardState currentKeyboardState, MouseState mouse, MonsterOfCreation target)
        {
			// Cria um retângulo para a posição atual do mouse
			Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

			// Verifica se o mouse está sobre o botão
			if (mouseRectangle.Intersects(Area))
			{
				down = true;
				color = Color.Gray;
				if (mouse.LeftButton == ButtonState.Pressed && isClicked == false)
				{
					isClicked = true;
                }
				else if (mouse.LeftButton == ButtonState.Released)
				{
					isClicked = false;
                }
			}
			else
			{
				down = false;
				color = Color.White;
			}

		}
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			spriteBatch.DrawString(font, button_string, new Vector2(Area.X, Area.Y), Color.Black);
		}
	}
    public class Power_Button : Button
	{
		public ElementalPowers power;
		public bool actived;
		public Power_Button(SpriteFont font, string button_string, Texture2D texture, Rectangle area, Color color,ElementalPowers power) : base(font,button_string,texture,area,color)
		{
			this.power = power;
			actived = false;
		}
	}
}