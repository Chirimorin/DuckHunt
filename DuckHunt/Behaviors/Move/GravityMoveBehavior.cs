using DuckHunt.Controllers;
using DuckHunt.Factories;
using DuckHunt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckHunt.Behaviors.Move
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

        public static void RegisterSelf()
        {
            MoveBehaviorFactory.register("gravity", typeof(GravityMoveBehavior));
        }
        
        protected override void Move()
        {
            // Beweeg volgens standaard regels
            baseMove();

            if (EnsureInScreenY(false))
            {
                if (VY > getTimeBased(DVY))
                {   // Unit was aan het vallen
                    VY = -VY * Bouncyness;
                }
                else
                {   // Unit stond al op de grond of raakt het plafond
                    VY = 0;
                }
            }

            // Zorg dat de unit het scherm niet uit loopt
            EnsureInScreenX(true);
        }

        public override void FixedTimePassed()
        {
            if (PosYBottom == WindowHeight && 
                VY == 0 &&
                Random.Next(0,50) == 0)
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
