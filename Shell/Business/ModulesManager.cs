using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extractor.Shell.Business
{
	public static class ModulesManager
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
				//PageNavigator.Business.ModuleControllerSchedule.CreateModule(
				//	new PageNavigator.Model.ModuleData("HomeModule", "Home"));
				initialized = true;
			}
		}

		private static PageNavigator.Business.ModuleControllerBase moduleControllerMapping(PageNavigator.Model.ModuleData moduleData)
		{
			return null;
			switch (moduleData.Name)
			{
				default:
					throw new InvalidOperationException(
						string.Format("not support for Module \"{0}\"", moduleData.Name));
			}
		}
	}
}
