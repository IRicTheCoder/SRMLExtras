using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using SRML;
//using SRML.Console;

namespace SRMLExtras
{
	/// <summary>
	/// The main class and entry point for the mod
	/// </summary>
	public class Main : ModEntryPoint
	{
		// THE EXECUTING ASSEMBLY
		public static Assembly execAssembly;

		/*[ConfigValue("TEST", "YOUR OTHER ASS")]
		public static string test;

		[ConfigValue("COOL VALUE", 250)]
		[ConfigDescription("TESTING STUFF")]
		[ConfigCategory("OTHER STUFF")]
		public static int coolValue;

		[ConfigValue("OTHER COOL VALUE", 25000)]
		[ConfigDescription("TENHO AQUI UM COMENTÁRIO\nE OUTRO FODASSE LINDO")]
		[ConfigCategory("OTHER STUFF")]
		public static int otherCoolValue;

		[ConfigValue("Test Coisa", "OtherFolder/otherfile", 0.5f)]
		[ConfigDescription("Nice description to have")]
		public static float otherFileValue;*/

		// PRE LOAD MOD
		public override void PreLoad()
		{
			FileLogger.Init();
			Console.Init();

			Console.RegisterDumpAction("ambientColor", (writer) =>
			{
				foreach (AmbianceDirector.ZoneSetting zone in SceneContext.Instance.AmbianceDirector.zones)
				{
					writer.WriteLine($"{zone.zone.ToString()} - Day Ambiance [R: {zone.dayAmbientColor.r}  G: {zone.dayAmbientColor.g}  B: {zone.dayAmbientColor.b}  HEX: {UnityEngine.ColorUtility.ToHtmlStringRGB(zone.dayAmbientColor)}]");
					writer.WriteLine($"{zone.zone.ToString()} - Night Ambiance [R: {zone.nightAmbientColor.r}  G: {zone.nightAmbientColor.g}  B: {zone.nightAmbientColor.b}  HEX: {UnityEngine.ColorUtility.ToHtmlStringRGB(zone.nightAmbientColor)}]");
				}
			});

			/*// Generates the config file
			ConfigHandler.Init(Assembly.GetExecutingAssembly());

			Console.Log($"test: {test} ({test?.GetType().ToString()})");
			Console.Log($"coolValue: {coolValue} ({coolValue.GetType().ToString()})");
			Console.Log($"otherCoolValue: {otherCoolValue} ({otherCoolValue.GetType().ToString()})");
			Console.Log($"otherFileValue: {otherFileValue} ({otherFileValue.GetType().ToString()})");

			Console.Reload += Reload;*/

			// Gets the Assembly being executed
			execAssembly = Assembly.GetExecutingAssembly();
			HarmonyInstance.PatchAll(execAssembly);
		}

		/*public void Reload()
		{
			Console.Log($"test: {test} ({test?.GetType().ToString()})");
			Console.Log($"coolValue: {coolValue} ({coolValue.GetType().ToString()})");
			Console.Log($"otherCoolValue: {otherCoolValue} ({otherCoolValue.GetType().ToString()})");
			Console.Log($"otherFileValue: {otherFileValue} ({otherFileValue.GetType().ToString()})");
		}*/

		// POST LOAD MOD
		public override void PostLoad()
		{
		}
	}
}
