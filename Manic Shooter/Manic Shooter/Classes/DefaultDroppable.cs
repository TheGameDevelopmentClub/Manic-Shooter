﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Manic_Shooter.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Manic_Shooter.Classes
{
    class DefaultDroppable:Sprite, IDroppable
    {
        public float downwardAccel = 100;
        public float maxSpeed = 250;

        public DefaultDroppable(Texture2D texture, Vector2 position)
            : base(texture, position)
        {
            this.Velocity = Vector2.Zero;
            this.hitboxRadius = 10.0F;
        }

        public override void Update(GameTime gameTime)
        {
            float deltaA = downwardAccel * ((float)gameTime.ElapsedGameTime.TotalSeconds);
            float newV = this.Velocity.Y + deltaA;
            if(newV > maxSpeed)
                newV = maxSpeed;
            this.Velocity = new Vector2(this.Velocity.X, newV);
            
            Vector2 deltaV = this.Velocity * ((float)gameTime.ElapsedGameTime.TotalSeconds);
            this.MoveBy(deltaV.X, deltaV.Y);

            //We can also use gameTime.ElapsedGameTime.TotalSeconds to achieve the same value without the division
            //this.Position += this.Velocity * ((float)gameTime.ElapsedGameTime.Milliseconds / 1000);

            //Do hit detection here, or en masse?

            //RESPONSE: It would probably be best to do it en masse since we could do some filtering for
            //efficiency ~Nick Boen

            //Detect if it is off screen and de-activate it
            if (IsOffScreen())
            {
                IsActive = false;
            }
        }

        public virtual void ApplyEffect(IPlayer player)
        {
        }
    }
}
