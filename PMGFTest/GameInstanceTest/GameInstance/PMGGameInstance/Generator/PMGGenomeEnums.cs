namespace PMGF
{
    namespace PMGGenerator
    {


        public enum GenomeKeys { ActorKey = -1, EventKey = -2, ValueFunc = -3,
                                 CondFunc = -4, UtilFunc = -5, ChangeFunc = -6,
                                 MethodKey = -7, TimeStep = -8 };


        // specific ones, for easier generation of genomes (values will be
        // the same in the genomes)


        public enum FunctionKeys { ValueFunc = -3, CondFunc = -4, UtilFunc = -5,
                                    ChangeFunc = -6 };

        public enum MethodKeys { MethodKey = -7, TimeStep = -8 };


        public enum MethodGenomeEnums { ValueFunc = -3, UtilFunc = -5,
                                        ChangeFunc = -6, MethodKey = -7,
                                        TimeStep = -8 };

        public enum EventGenomeEnum { EventKey = -2, ValueFunc = -3,
                                        CondFunc = -4, UtilFunc = -5 };

        // more? ....


    }
}
