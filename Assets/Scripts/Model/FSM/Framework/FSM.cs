using System;
using System.Collections.Generic;
using FSM;

namespace FSM
{
    public static class FSM
    {
        public static FSMContext createFSMInstance(State startState,FSMAction initAction,object o)
        {
            return new FSMContext(startState,initAction,o);
        }
    }
}
