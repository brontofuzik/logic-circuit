﻿using System;
using System.Collections.Generic;
using System.Text;

using GateNetworkDotNet.Exceptions;
using GateNetworkDotNet.GateTypes;

namespace GateNetworkDotNet.Gates
{
    /// <summary>
    /// An abstract gate.
    /// </summary>
    public abstract class Gate
    {
        #region Private instance fields

        /// <summary>
        /// The name of the (abstract) gate.
        /// </summary>
        private string name;

        /// <summary>
        /// The type of the (abstract) gate.
        /// </summary>
        private GateType type;

        /// <summary>
        /// The input plugs.
        /// </summary>
        private Plug[] inputPlugs;

        /// <summary>
        /// The output plugs.
        /// </summary>
        private Plug[] outputPlugs;

        #endregion // Private instance fields

        #region Public instance properties

        /// <summary>
        /// Gets the name of the gate.
        /// </summary>
        /// 
        /// <value>
        /// The name of the gate type.
        /// </value>
        public string Name
        {
            get
            {
                return name;
            }
        }

        /// <summary>
        /// Gets the input plugs.
        /// </summary>
        /// 
        /// <value>
        /// The input plugs.
        /// </value>
        public Plug[] InputPlugs
        {
            get
            {
                return inputPlugs;
            }
        }

        /// <summary>
        /// Gets the number of the input plugs.
        /// </summary>
        /// 
        /// <value>
        /// The number of the input plugs.
        /// </value>
        public int InputPlugCount
        {
            get
            {
                return type.InputPlugCount;
            }
        }

        /// <summary>
        /// Gets the output plugs.
        /// </summary>
        /// 
        /// <value>
        /// The output plugs.
        /// </value>
        public Plug[] OutputPlugs
        {
            get
            {
                return outputPlugs;
            }
        }

        /// <summary>
        /// Gets the number of the output plugs.
        /// </summary>
        /// 
        /// <value>
        /// The number of the output plugs.
        /// </value>
        public int OutputPlugCount
        {
            get
            {
                return type.OutputPlugCount;
            }
        }

        #endregion // Public instance properties

        #region Protected instance constructors

        /// <summary>
        /// Creates a new gate.
        /// </summary>
        /// 
        /// <param name="name">The name of the gate.</param>
        /// <param name="type">The type of the gate.</param>
        /// 
        /// <exception cref="System.ArgumentNullException">
        /// Condition 1: <c>name</c> is <c>null</c>.
        /// Condition 2: <c>type</c> is <c>null</c>.
        /// </exception>
        /// <exception cref="GateNetworkDotNet.Exceptions.MyException">
        /// Condition: <c>name</c> is not a legal identifier.
        /// </exception>
        protected Gate( string name, GateType type )
        {
            // Validate the name.
            if (!Program.IsLegalIdentifier( name ))
            {
                throw new ArgumentException( name );
            }
            this.name = name;

            // Validate the (abstract) gate type.
            if (type == null)
            {
                throw new ArgumentNullException();
            }
            this.type = type;

            // Create the input plugs.
            inputPlugs = new Plug[ InputPlugCount ];
            for (int i = 0; i < InputPlugCount; i++)
            {
                inputPlugs[ i ] = new Plug( this );
            }

            // Create the output plugs.
            outputPlugs = new Plug[ OutputPlugCount ];
            for (int i = 0; i < OutputPlugCount; i++)
            {
                outputPlugs[ i ] = new Plug( this );
            }
        }

        #endregion // Protected instance constructors

        #region Public instance methods

        /// <summary>
        /// Gets an input plug specified by its name.
        /// </summary>
        /// 
        /// <param name="inputPlugName">The name of the input plug.</param>
        /// 
        /// <returns>
        /// The input plug (or <c>null</c> if such input plug does not exist).
        /// </returns>
        public Plug GetInputPlugByName( string inputPlugName )
        {
            int inputPlugIndex = type.GetInputPlugIndex( inputPlugName );
            return (inputPlugIndex != -1) ? InputPlugs[ inputPlugIndex ] : null;
        }

        /// <summary>
        /// Gets an output plug specified by its name.
        /// </summary>
        /// 
        /// <param name="outputPlugName">The name of the output plug.</param>
        /// 
        /// <returns>
        /// The output plug (or <c>null</c> if such output plug does not exist).
        /// </returns>
        public Plug GetOutputPlugByName( string outputPlugName )
        {
            int outputPlugIndex = type.GetOutputPlugIndex( outputPlugName );
            return (outputPlugIndex != -1) ? OutputPlugs[ outputPlugIndex ] : null;
        }

        /// <summary>
        /// Gets the values of the input plugs.
        /// </summary>
        /// 
        /// <returns>
        /// The values of the input plugs.
        /// </returns>
        public string GetInputPlugValues()
        {
            // Build the string representation of the values of the input plugs.
            StringBuilder inputPlugValuesSB = new StringBuilder();
            for (int i = 0; i < InputPlugCount; i++)
            {
                inputPlugValuesSB.Append(InputPlugs[i].Value + " ");
            }
            // Remove the trailing space character if necessary.
            if (inputPlugValuesSB.Length != 0)
            {
                inputPlugValuesSB.Remove(inputPlugValuesSB.Length - 1, 1);
            }
            return inputPlugValuesSB.ToString();
        }

        /// <summary>
        /// Sets the values of the input plugs.
        /// </summary>
        /// 
        /// <param name="inputPlugValuesString">The values of the input plugs.</param>
        public abstract void SetInputPlugValues(string inputPlugValuesString);

        /// <summary>
        /// Gets the values of the output plugs.
        /// </summary>
        /// 
        /// <returns>
        /// The values of the output plugs.
        /// </returns>
        public string GetOutputPlugValues()
        {
            // Build the string representation of the values of the output plugs.
            StringBuilder outputPlugValuesSB = new StringBuilder();
            for (int i = 0; i < OutputPlugCount; i++)
            {
                outputPlugValuesSB.Append( OutputPlugs[ i ].Value + " " );
            }
            // Remove the trailing space character if necessary.
            if (outputPlugValuesSB.Length != 0)
            {
                outputPlugValuesSB.Remove( outputPlugValuesSB.Length - 1, 1 );
            }
            return outputPlugValuesSB.ToString();
        }

        /// <summary>
        /// Sets the values of the output plugs.
        /// </summary>
        /// 
        /// <param name="outputPlugValues">The values of the output plugs.</param>
        public void SetOutputPlugValues(string outputPlugValuesString)
        {
            string[] outputPlugValues = outputPlugValuesString.Split(' ');
            for (int i = 0; i < OutputPlugCount; i++)
            {
                OutputPlugs[i].Value = outputPlugValues[i];
            }
        }

        /// <summary>
        /// Initializes the (abstract) gate.
        /// </summary>
        public abstract void Initialize();

        /// <summary>
        /// Updates the values of the input plugs of the (abstract) gate.
        /// </summary>
        public abstract bool UpdateInputPlugValues();

        /// <summary>
        /// Evaluates the (abstract) gate.
        /// </summary>
        public abstract void UpdateOutputPlugValues();

        /// <summary>
        /// Evaluates the (abstract) gate.
        /// </summary>
        /// 
        /// <param name="inputPlugValues">The values of the input plugs.</param>
        /// 
        /// <returns>
        /// The computation time (in cycles) and the values of the output plugs.
        /// </returns>
        public string Evaluate(string inputPlugValues)
        {
            SetInputPlugValues(inputPlugValues);
            bool updatePerformed = true;

            int cycles = 0;
            while (cycles < 1000000)
            {
                // Update the values of the input plugs.
                updatePerformed = UpdateInputPlugValues();

                if (!updatePerformed)
                {
                    break;
                }

                // Update the values of the output plugs.
                UpdateOutputPlugValues();

                cycles++;
            }

            return cycles + " " + GetOutputPlugValues();
        }

        #endregion // Public instance methods
    }
}
