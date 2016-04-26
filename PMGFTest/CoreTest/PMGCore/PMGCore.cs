namespace PMGF
{
    namespace PMGCore
    {
        public class PMGCore
        {
            // Collection of value functions
            public PMGValueFunctionsCollection ValueFunctions = new PMGValueFunctionsCollection();

            // Collection of utility functions
            public PMGUtilityFunctionsCollection UtilityFunctions = new PMGUtilityFunctionsCollection();

            // Collection of condition functions
            public PMGConditionFunctionsCollection ConditionFunctions = new PMGConditionFunctionsCollection();

            // Collection of change functions
            public PMGChangeFunctionsCollection ChangeFunctions = new PMGChangeFunctionsCollection();


            // World variables. TODO move at some point?
            public PMGValueStack WorldStack = new PMGValueStack();
            public int WorldTimeSteps;

            public PMGCore()
            {
                // Initialize collections of functions
                ValueFunctions.Initialize();
                UtilityFunctions.Initialize();
                ConditionFunctions.Initialize();
                ChangeFunctions.Initialize();
            }
        }
    }
}
