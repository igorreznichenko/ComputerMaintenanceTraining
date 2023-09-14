using ComputerMaintenanceTraining.Enums;

namespace ComputerMaintenanceTraining.AssemblyObjects
{
	public interface IAssemblyObject
	{
		public AssemblyObjectType AssemblyObjectType { get; }
	}
}