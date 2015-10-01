using DuckHunt.Controllers;
using DuckHunt.Factories;
using DuckHunt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckHunt.Behaviors.OldMove
{
    public class GravityMoveBehavior : BaseMoveBehavior
    {
        /// <summary>
        /// De zwaartekracht constante (pixels/seconde)
        /// </summary>
        protected double Gravity { get; set; }
        /// <summary>
        /// DVY is bepaald door de zwaartekracht en staat vast.
        /// </summary>
        public override double DVY
        {
            get { return Gravity; }
            set { }
        }
        /// <summary>
        /// Snelheid waarmee de unit springt.
        /// </summary>
        protected double JumpPower { get; set; }

        /// <summary>
        /// Hoe erg de unit op de grond stuitert. 
        /// 0 = niet stuiteren.
        /// 1 = evenhard terug stuiteren.
        /// </summary>
        protected double Bouncyness { get; set; }

        #region constructors
        /// <summary>
        /// MoveBehavior met een zwaartekracht gebaseerde Y-beweging.
        /// </summary>
        public GravityMoveBehavior() : base()
        {
            MaxVY = 5000; // Maximale valsnelheid
            Gravity = 2000; // Valversnelling
            Bouncyness = 0.5; // Met de helft van de snelheid terug stuiteren
            JumpPower = 1250; // Springkracht, pixels/seconde

            VX = 500; 
            DVX = 0;
        }

        /// <summary>
        /// MoveBehavior met een zwaartekracht gebaseerde Y-beweging.
        /// </summary>
        /// <param name="maxVY">Maximale valsnelheid (pixels/seconde)</param>
        /// <param name="gravity">Valversnelling (pixels/seconde/seconde)</param>
        /// <param name="bouncyness">Hoe ver de Unit terug stuitert als deze op de grond valt</param>
        /// <param name="jumpPower">De snelheid waarmee de unit omhoog springt (pixels/seconde). 0 voor niet springen.</param>
        /// <param name="vX">De x-snelheid waarmee de unit loopt. </param>
        public GravityMoveBehavior(double maxVY, double gravity, double bouncyness, double jumpPower, double vX) :base()
        {
            MaxVY = maxVY;
            Gravity = gravity;
            Bouncyness = bouncyness;
            JumpPower = jumpPower;

            VX = vX;
        }
        #endregion

        public static void RegisterSelf()
        {
            //OldMoveBehaviorFactory.register("gravity", typeof(GravityMoveBehavior));
        }
        
        public override void Move(IGame game)
        {
            // Beweeg volgens standaard regels
            baseMove(game);

            if (EnsureInScreenY(false))
            {
                if (VY > getTimeBased(DVY, game))
                {   // Unit was aan het vallen
                    VY = -VY * Bouncyness;
                }
                else
                {   // Unit stond al op de grond of raakt het plafond
                    VY = 0;
                }
            }

            if (!removeIfExpired(game))
            {
                // Zorg dat de unit het scherm niet uit loopt
                EnsureInScreenX(true);
            }
        }

        public override void FixedTimePassed(IGame game)
        {
            if (PosYBottom == CONSTANTS.CANVAS_HEIGHT && 
                VY == 0 &&
                game.Random.Next(0,50) == 0)
            {
                Jump();
            }
        }

        /// <summary>
        /// Laat de unit springen
        /// </summary>
        protected void Jump()
        {
            VY = -JumpPower;
        }

    }
}
