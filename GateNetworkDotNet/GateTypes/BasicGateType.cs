﻿using System;
using System.Collections.Generic;
using System.Text;

using GateNetworkDotNet.Exceptions;
using GateNetworkDotNet.Gates;

namespace GateNetworkDotNet.GateTypes
{
    /// <summary>
    /// A basic gate type.
    /// </summary>
    public class BasicGateType
        : GateType
    {
        #region Private instance fields

        /// <summary>
        /// The transition function.
        /// </summary>
        private TransitionFunction transitionFunction;

        #endregion // Private instance fields

        #region Public instance properties

        /// <summary>
        /// Gets the length of a transition.
        /// </summary>
        /// 
        /// <value>
        /// The length of a transiiton.
        /// </value>
        public int TransitionLength
        {
            get
            {
                return InputPlugCount + OutputPlugCount;
            }
        }

        #endregion // Public instance properties

        #region Public instance constructors

        /// <summary>
        /// Creates a new basic gate type.
        /// </summary>
        /// 
        /// <param name="name">The name of the gate type.</param>
        /// <param name="inputPlugNames">The list of the inputs' names.</param>
        /// <param name="outputs">The list of the outputs' names.</param>
        /// <param name="transitions">The transitions.</param>
        /// 
        /// <exception cref="Network.Exceptions.IllegalNameException">
        /// Condition 1: <c>name</c> is an illegal name.
        /// Condition 2: <c>inputPlugNames</c> contains an illegal name.
        /// Condition 3: <c>outputPlugNames</c> contains an illegal name.
        /// </exception>
        /// <exception cref="Network.Exceptions.IllegalTransitionException">
        /// Consition: the number of inputs and outputs is not equal to the length of the transition.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// Condition 1: <c>inputPlugNames</c> is <c>null</c>.
        /// Condition 2: <c>outputPlugNames</c> is <c>null</c>.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// Condition: <c>outputPlugNames</c> is empty.
        /// </exception>
        public BasicGateType( string name, List< string > inputPlugNames, List< string > outputPlugNames, List< string > transitions )
            : base( name, inputPlugNames, outputPlugNames )
        {
            transitionFunction = new TransitionFunction( InputPlugCount, OutputPlugCount, transitions );
        }

        #endregion // Public instance constructors

        #region Public instance methods

        /// <summary>
        /// Instantiates the basic gate object.
        /// </summary>
        /// 
        /// <param name="name">The name of the basic gate object.</param>
        /// 
        /// <returns>
        /// The basic gate object.
        /// </returns>
        public override Gate Instantiate( string name )
        {
            return new BasicGate( name, this );
        }

        /// <summary>
        /// Evaluates the transition function of the basic gate type.
        /// </summary>
        /// 
        /// <param name="inputPlugValues">The values of the input plugs.</param>
        /// 
        /// <returns>
        /// The values of the output plugs.
        /// </returns>
        public override string[] Evaluate( string[] inputPlugValues )
        {
            return transitionFunction.Evaluate( inputPlugValues );  
        }
        #endregion // Public instance methods
    }
}