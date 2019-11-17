using System;

namespace Vy.ML
{
    /// <summary>
    /// Defines an input to a <see cref="Neuron"/>
    /// </summary>
    public class Input
    {
        #region Private members

        private Random mRandom = new Random();

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the value of the input
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Gets or sets the wight of the input
        /// </summary>
        public double Weight { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public Input() 
        {
            Weight = mRandom.NextDouble();
        }

        /// <summary>
        /// Default parameterized constructor
        /// </summary>
        /// <param name="Value"></param>
        public Input(double Value)
        {
            this.Value = Value;
            Weight = mRandom.NextDouble();
        }

        #endregion
    }
}
