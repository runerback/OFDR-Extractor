using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extractor.Business
{
	internal static class ModulesManager
	{
		private static bool initialized;
		public static bool Initialized
		{
			get { return initialized; }
		}

		public static void Initialize()
		{
			if (!initialized)
			{
				PageNavigator.Business.ModulesManager.Load();
				PageNavigator.Business.ModuleControllerSchedule.SetModuleControllerMapping(moduleControllerMapping);
				PageNavigator.Business.ModuleControllerSchedule.CreateModule(
					new PageNavigator.Model.ModuleData("home", "Hierarchy"));
				initialized = true;
			}
		}

		private static PageNavigator.Business.ModuleControllerBase moduleControllerMapping(PageNavigator.Model.ModuleData moduleData)
		{
			switch (moduleData.Name)
			{
				case "options":
					return new Controllers.Tool.OptionsController(moduleData);
				case "about":
					return null;
				case "home":
					return new Controllers.HomePageController(moduleData);
				default:
					throw new InvalidOperationException(
						string.Format("not support for Module \"{0}\"", moduleData.Name));
			}
		}
	}
}
