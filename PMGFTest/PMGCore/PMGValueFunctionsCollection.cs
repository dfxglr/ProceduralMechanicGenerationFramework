

namespace PMGF
{
    namespace PMGCore
    {
        public sealed partial class PMGValueFunctionsCollection
        {
            // Singleton with all the custom made value functions
            //
            // This file is just handling singleton functionality
            //  See PMGValueFunctions_BY_USER.cs
            //

            // For singletonning
            private PMGValueFunctionsCollection(){}
            public static PMGValueFunctionsCollection Instance { get { return Nested.instance; } }

            // nested class for lazy singletonning
            private class Nested
            {
                    static Nested(){}

                    internal static readonly PMGValueFunctionsCollection instance = new PMGValueFunctionsCollection();
            }



        }
    }
}
