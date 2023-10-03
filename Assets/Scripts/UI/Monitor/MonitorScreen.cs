using ComputerMaintenanceTraining.UI;

public class MonitorScreen : UIBase
{
	protected MonitorUIController _monitorUIController;

	public void Init(MonitorUIController monitorUIController)
	{
		_monitorUIController = monitorUIController;
	}
}
