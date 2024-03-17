using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using ClassesGame;
using System.Linq;
using System.Runtime.InteropServices;

namespace monsters
{
    //ENUMERADORES E ESTRUTURAS
    public enum Monster_Animations { Idle, Atack, Defense, Dying, Final }
    public enum functionofmonster { space, time, life, reality , energy }
    public enum ElementalPowers { space, reality, time, life, energy, control, limbo, caos, harmony }

    /* DANO:space,caos,energy
     * VIDA:life,harmony,time
     * BUFF:reality,control,limbo
     */
    public class MonsterOfCreation : GameObject
    {
        //VARIAVEIS E VETORES
        public Monster_Animations anime;
        public functionofmonster monstertype;
        ElementalPowers[] Myelements = new ElementalPowers[5];
        public ElementalPowers[] habilidade = new ElementalPowers[1];
        public float Life, MaxLife;
        public float damage;
        public int cura, pos;
        public bool corrupted;
        public string name;
        public Vector2[] positions;
        public Vector2 vel;
        //CONSTRUTOR
        public MonsterOfCreation(string name,float Life, float damage, int cura, int[] Myelements, int monstertype, Texture2D texture, Rectangle area, Color color,Vector2[] positions)
            : base(texture, area, color)
        {
            this.name = name;
            corrupted = false;
            this.MaxLife = Life;
            this.Life = Life;
            this.damage = damage;
            this.cura = cura;
            anime = Monster_Animations.Idle;
            this.monstertype = (functionofmonster)monstertype;
            for (int i = 0; i < 5; i++)
            {
                this.Myelements[i] = (ElementalPowers)Myelements[i];
            }

            // Gerar todas as combinações únicas de três elementos
            pos = 1;
            this.positions = positions;
            
        } 
        //UPDATE
        public override void Update(GameTime gametime, KeyboardState currentKeyboardState, MouseState mouse, MonsterOfCreation target)
        {
            if (Area.X == positions[positions.Length - 1].X && Area.Y == positions[positions.Length - 1].Y)
                pos = 0;
            else
                if (Area.X == positions[pos].X && Area.Y == positions[pos].Y)
                pos++;
            Vector2 diferenca_de_distancias = (new Vector2(positions[pos].X - Area.X, positions[pos].Y - Area.Y));
            Console.WriteLine(vel);
            vel = new Vector2(diferenca_de_distancias.X / Math.Abs(diferenca_de_distancias.X), diferenca_de_distancias.Y / Math.Abs(diferenca_de_distancias.Y));
            if(float.IsNaN(vel.X))
                vel.X = 0;
            if (float.IsNaN(vel.Y))
                vel.Y = 0;
            animation(gametime);
        }
        public void animation(GameTime gametime)
        {
            switch (anime)
            {
                case Monster_Animations.Idle:
                    Area.X += (int)vel.X;
                    Area.Y += (int)vel.Y;
                    break;
                case Monster_Animations.Dying:
                    break;
                case Monster_Animations.Final:
                    break;
            }

        }
        //SELECIONAR HABILIDADE
        //ATAQUES COM BASE NOS 3 ELEMENTOS
        /* DANO:space,caos,energy
     * BUFFCURA:life,harmony
     * BUFFDANO:reality
     * DEBUFFDANO:limbo
     * DEBUFFCURA:time,control
     */
        public void select_power(ElementalPowers[] elementos, MonsterOfCreation target)
        {
            int damageeffect = 0;
            int debuffeffect = 0;
            int buffeffect = 0;
            for (int i = 0; i < elementos.Length; i++)
            {
                switch (elementos[i])
                {
                    case ElementalPowers.space:
                        target.Life -= this.damage;
                        damageeffect++;
                        break;
                    case ElementalPowers.reality:
                        this.damage += 0.5f;
                        buffeffect++;
                        break;
                    case ElementalPowers.time:
                        if (target.cura > 0) target.cura -= 1;
                        debuffeffect++;
                        break;
                    case ElementalPowers.life:
                        this.cura++;
                        buffeffect++;
                        break;
                    case ElementalPowers.energy:
                        if (target.damage > 0) target.damage -= 0.5f;
                        debuffeffect++;
                        break;
                    case ElementalPowers.control:
                        if (target.damage > 0) target.damage -= 0.5f;
                        debuffeffect++;
                        break;
                    case ElementalPowers.limbo:
                        this.damage += 0.5f;
                        buffeffect++;
                        break;
                    case ElementalPowers.caos:
                        if (target.damage > this.damage)
                        {
                            float aux = target.damage;
                            target.damage = this.damage;
                            this.damage = aux;
                        }
                        target.Life -= this.damage;
                        damageeffect++;
                        break;
                    case ElementalPowers.harmony:
                        this.cura++;
                        buffeffect++;
                        break;
                }
            }
            if (damageeffect > 1)
            {
                target.corrupted = true;
            }
            if (buffeffect > 1)
            {
                this.corrupted = false;
            }
            if (debuffeffect > 1)
            {
                Life += target.damage;
            }
            this.Life += this.cura;
            if (corrupted == true) Life -= this.damage;
            if (Life > MaxLife) Life = MaxLife;
        }
        //ATAQUE A SER EXECUTADO
        public void monster_attack(MonsterOfCreation target)
        {
            anime = Monster_Animations.Atack;
            List<ElementalPowers> copy = new List<ElementalPowers>(Myelements);
            habilidade = new ElementalPowers[3];
            for (int i = 0; i < 3; i++)
            {
                Random random = new Random();
                int indice = random.Next(copy.Count);
                habilidade[i] = copy[indice];
                copy.RemoveAt(indice);
            }
            select_power(habilidade, target);
        }
        
    }
}
