using System;

namespace Vy.ML
{
    /// <summary>
    /// Defines an output for a <see cref="Neuron"/>
    /// </summary>
    public class Output
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
        public Output()
        {
            Weight = mRandom.NextDouble();
        }

        /// <summary>
        /// Default parameterized constructor
        /// </summary>
        /// <param name="Value"></param>
        public Output(double Value)
        {
            this.Value = Value;
            Weight = mRandom.NextDouble();
        }

        #endregion
    }
}
