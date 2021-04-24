using System.Collections;
using System.Collections.Generic;
using UdemyProject3.Abstracts.States;
using UnityEngine;

namespace UdemyProject3.States
{
    public class StateTransformer
    {
        public IState To { get; }
        public IState From { get; }
        public System.Func<bool> Condition { get; }

        public StateTransformer(IState to, IState from, System.Func<bool> condition)
        {
            To = to;
            From = from;
            Condition = condition;
        }
    }
}