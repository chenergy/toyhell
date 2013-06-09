using System;
using System.Collections.Generic;
using UnityEngine;
using FSM;

namespace FSM
{
    public class FSMContext
    {
        private FSMAction initAction;
        private State currentState;
        
        public FSMContext(State startState, FSMAction initAction)
        {
            this.currentState = startState;
            this.initAction = initAction;
			this.initAction.execute(this, null);
        }
		
		public State CurrentState
        {
            get { return currentState; }
            set { currentState = value; }
        }
		
		
        public void dispatch(string eventName, object o)
        {
            currentState.dispatch(this, eventName, o);
        }
    }
}
