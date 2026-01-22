using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Project.Develop.Runtime.Utilities.Conditions
{
    public interface ICompositeCondition : ICondition
    {
        ICompositeCondition Add(ICondition condition);

        ICompositeCondition Remove(ICondition condition);
    }

    public class LogicOperations
    {
        public static bool And(bool a, bool b) => a && b;
        public static bool Or(bool a, bool b) => a || b;
    }

    public class CompositeCondition : ICompositeCondition
    {
        private List<ICondition> _conditions = new();

        private Func<bool, bool, bool> _standartLogicOperation;

        public CompositeCondition(Func<bool, bool, bool> standartLogicOperation)
        {
            _standartLogicOperation = standartLogicOperation;
        }

        public CompositeCondition() : this(LogicOperations.And)
        {

        }

        public bool Evaluate()
        {
            if (_conditions.Count == 0)
                return false;

            bool result = _conditions[0].Evaluate();

            for (int i = 1; i < _conditions.Count; i++)
            {
                ICondition condition = _conditions[i];

                result = _standartLogicOperation.Invoke(result, condition.Evaluate());
            }

            return result;
        }

        public ICompositeCondition Add(ICondition condition)
        {
            _conditions.Add(condition);
            return this;
        }

        public ICompositeCondition Remove(ICondition condition)
        {
            _conditions.Remove(condition);
            return this;
        }
    }
}
