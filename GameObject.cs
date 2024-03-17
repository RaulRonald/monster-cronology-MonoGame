using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using monsters;
using System;
namespace ClassesGame
{

	public class GameObject
	{
		public Texture2D Texture;
		public Vector2 Position;
		public Rectangle Area;
		public Color color;
		public Vector2 Size;
		public GameObject(Texture2D Texture, Rectangle Area, Color color)
		{
			this.Texture = Texture;
			this.Area = Area;
			this.color = color;
		}
		public virtual void Update(GameTime gametime,KeyboardState currentKeyboardState,MouseState mouse, MonsterOfCreation target)
        {

        }
		public virtual void Draw(SpriteBatch spriteBatch)
        {
			if (Texture == null)
			{
				throw new Exception("Texture is null");
			}
			else if (Area == null)
			{
				throw new Exception("Area is null");
			}
			else if (color == null)
			{
				throw new Exception("color is null");
			}
			else
			{
				spriteBatch.Draw(Texture, Area, color);
			}
		}
	}
}
