﻿using SRML.Console;

namespace SRMLExtras
{
	public class DebugCommand : ConsoleCommand
	{
		public static bool DebugMode = false;

		public override string ID { get; } = "vdebug";
		public override string Usage { get; } = "vdebug <mode>";
		public override string Description { get; } = "Sets the debug mode to <mode>";

		public override bool Execute(string[] args)
		{
			if (args == null || ArgsOutOfBounds(args.Length, 1, 1))
				return false;

			DebugMode = bool.Parse(args[0]);
			Console.LogSuccess($"Changed the debug mode to <color=white>{DebugMode}</color>");

			return true;
		}
	}
}
