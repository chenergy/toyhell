using System;
using System.Collections.Generic;
using UnityEngine;
using FSM;

namespace FSM
{
    public class State
    {
        private FSMAction entryAction;
        private FSMAction updateAction;
        private FSMAction exitAction;
        private Dictionary<string, Transition> transitionList;
        private string name;

        public string Name
        {
            get { return name; }
        }
        public State(string name, FSMAction entryAction, FSMAction updateAction, FSMAction exitAction)
        {
            this.name = name;
            this.entryAction = entryAction;
            this.updateAction = updateAction;
            this.exitAction = exitAction;
            this.transitionList = new Dictionary<string,Transition>();
        }
		
        public void addTransition(Transition transition,string eventName)
        {
            if (!transitionList.ContainsKey(eventName))
                transitionList.Add(eventName, transition);
			else
				Debug.Log("Transition "+eventName+" not Added. Already in "+name+"'s transitions.");
        }
        public void removeTransition(string eventName)
        {
            if (transitionList.ContainsKey(eventName))
                transitionList.Remove(eventName);
			else
				Debug.Log("Transition "+eventName+" not Removed. "+name+"'s transitions does not contain "+eventName);
        }
		
		public void update(FSMContext fsmc, object o){
			updateAction.execute(fsmc, o);
		}
		
        //dispatch - triggers a state transition
        public void dispatch(FSMContext fsmc, string eventName, object o)
        {
            if (transitionList.ContainsKey(eventName))
            {
				fsmc.CurrentState.exitAction.execute(fsmc, o);
                transitionList[eventName].execute(fsmc, o);
				fsmc.CurrentState.entryAction.execute(fsmc, o);
            }
			else
				Debug.Log("Cannot Transition to "+eventName+". "+name+"'s transitions does not contain "+eventName);
        }

    }
}
