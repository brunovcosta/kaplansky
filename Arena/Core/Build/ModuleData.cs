using System;
using Arena;

namespace Arena
{
	public class ModuleData{
		public Type type;
		public string buttonPath;
		public string texturePath;
		public ModuleSelection selection;
		public int size;
		public ModuleData(Type type,string buttonPath,string texturePath,ModuleSelection selection,int size=0){
			this.type=type;
			this.buttonPath = buttonPath;
			this.texturePath = texturePath;
			this.selection = selection;
			if (size != 0)
				this.size = size;
		}
		public static ModuleSelection[] all = (ModuleSelection[])Enum.GetValues(typeof(ModuleSelection));
		public static ModuleData fromSelect(ModuleSelection selection){
			switch(selection){
				case ModuleSelection.GUN:
					return Gun.moduleData;
				case ModuleSelection.TURBINE:
					return Turbine.moduleData;
				case ModuleSelection.WHEEL_CLOCK_WISE:
					return WheelClockWise.moduleData;
				case ModuleSelection.WHEEL_ANTI_CLOCK_WISE:
					return WheelAntiClockWise.moduleData;
				default:
					return null;
			}
		}
	}
}

