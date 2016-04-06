namespace PMGF
{
    namespace PMGCore
    {
        public class PMGCore
        {
            // Collection of value functions
            public PMGValueFunctionsCollection ValueFunctions = new PMGValueFunctionsCollection();


            public PMGCore()
            {
                // Initialize collections of functions
                ValueFunctions.Initialize();
            }
        }
    }
}
