using System;
using System.Collections.Generic;
using FSM;

namespace FSM
{
    public class Transition
    {
        private State target;
        private FSMAction action;
        public Transition(State targetState, FSMAction action)
        {
            this.target = targetState;
            this.action = action;
        }
        public void execute(FSMContext c, object o)
        {
            action.execute(c,o);
            c.CurrentState = target;
        }

    }
}
