using System;
using System.Collections.Generic;

namespace Vy.ML
{
    /// <summary>
    /// 
    /// </summary>
    public class Neuron
    {
        #region Private Members

        private Random mRandom = new Random();

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the bias of the neuron
        /// </summary>
        public double Bias { get; set; }

        /// <summary>
        /// Gets or sets the 
        /// </summary>
        public Func<double, double> Activation { get; set; } = Math.Tanh;

        /// <summary>
        /// Gets or sets the inputs for the neuron
        /// </summary>
        public List<Input> Inputs { get; set; }

        /// <summary>
        /// Gets the output of the neuron
        /// </summary>
        public Output Output => new Output(Activation(InputSum(Inputs, Bias)));

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Neuron()
        {
            Init();
        }

        /// <summary>
        /// Default parameterized constructor
        /// </summary>
        /// <param name="inputs"></param>
        public Neuron(List<Input> inputs)
        {
            Inputs = inputs;
            Init();
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Initializes the neuron with default values
        /// </summary>
        private void Init()
        {
            // Set up standard values
            Bias = mRandom.NextDouble();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputs"></param>
        /// <param name="bias"></param>
        /// <returns></returns>
        private double InputSum(List<Input> inputs, double bias = 0)
        {
            double output = 0;

            foreach (var i in inputs)
                output += i.Value * i.Weight;

            output += bias;

            return output;
        }

        #endregion
    }
}
