using System;

namespace PMGF
{
	namespace PMGCCETest
	{
		public sealed class DebugHelpers
		{
			private static readonly DebugHelpers _instance = new DebugHelpers();

			static DebugHelpers()
			{
			}

			private DebugHelpers()
			{
			}

			public static DebugHelpers Instance
			{
				get{
					return _instance;
				}
			}



			public static bool _debugMsgs = true;


			public static void DebugMsg(string message)
			{
				if (_debugMsgs)
					Console.WriteLine (message);
			}
		}
	}
}
