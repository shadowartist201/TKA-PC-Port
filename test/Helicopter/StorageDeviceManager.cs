using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Storage;

namespace Helicopter;

public class StorageDeviceManager : GameComponent
{
	private const string ReselectStorageDeviceText = "No storage device was selected. Would you like to re-select the storage device?";

	private const string DisconnectReselectDeviceText = "An active storage device has been disconnected. Would you like to select a new storage device?";

	private const string ForceReselectDeviceText = "No storage device was selected. A storage device is required to continue. Select Ok to choose a storage device.";

	private const string ForceDisconnectReselectText = "An active storage device has been disconnected. A storage device is required to continue. Select Ok to choose a storage device.";

	//private bool wasDeviceConnected;

	private bool showDeviceSelector;

	//private bool promptToReSelectDevice;

	//private bool promptToForceReselect;

	//private bool promptForDisconnect;

	//private bool promptForDisconnectForced;

	private readonly StorageDeviceEventArgs eventArgs = new StorageDeviceEventArgs();

	private readonly StorageDevicePromptEventArgs promptEventArgs = new StorageDevicePromptEventArgs();

	public StorageDevice Device { get; private set; }

	public PlayerIndex? Player { get; private set; }

	public PlayerIndex PlayerToPrompt { get; set; }

	public int RequiredBytes { get; private set; }

	public event EventHandler DeviceSelected;

	public event EventHandler<StorageDeviceEventArgs> DeviceSelectorCanceled;

	public event EventHandler<StorageDevicePromptEventArgs> DevicePromptClosed;

	public event EventHandler<StorageDeviceEventArgs> DeviceDisconnected;

	public StorageDeviceManager(Game game)
		: this(game, (PlayerIndex?)null, 0)
	{
	}

	public StorageDeviceManager(Game game, PlayerIndex player)
		: this(game, player, 0)
	{
	}

	public StorageDeviceManager(Game game, int requiredBytes)
		: this(game, (PlayerIndex?)null, requiredBytes)
	{
	}

	public StorageDeviceManager(Game game, PlayerIndex player, int requiredBytes)
		: this(game, (PlayerIndex?)player, requiredBytes)
	{
	}

	private StorageDeviceManager(Game game, PlayerIndex? player, int requiredBytes)
		: base(game)
	{
		Player = player;
		RequiredBytes = requiredBytes;
		PlayerToPrompt = (PlayerIndex)0;
	}

	public void PromptForDevice()
	{
		showDeviceSelector = true;
	}

	public override void Update(GameTime gameTime)
	{
		/*														if (Device != null && !Device.IsConnected && wasDeviceConnected)
		{
			FireDeviceDisconnectedEvent();
		}
		try
		{
			if (!Guide.IsVisible)
			{
				if (showDeviceSelector)
				{
					showDeviceSelector = false;
					if (Player.HasValue)
					{
						StorageDevice.BeginShowSelector(Player.Value, RequiredBytes, 0, (AsyncCallback)deviceSelectorCallback, (object)null);
					}
					else
					{
						StorageDevice.BeginShowSelector(RequiredBytes, 0, (AsyncCallback)deviceSelectorCallback, (object)null);
					}
				}
				else if (promptToReSelectDevice)
				{
					if (Player.HasValue)
					{
						//Guide.BeginShowMessageBox(Player.Value, "Reselect Storage Device?", "No storage device was selected. Would you like to re-select the storage device?", (IEnumerable<string>)new string[2] { "Yes. Select new device.", "No. Continue without device." }, 0, (MessageBoxIcon)0, (AsyncCallback)reselectPromptCallback, (object)null);
					}
					else
					{
						//Guide.BeginShowMessageBox("Reselect Storage Device?", "No storage device was selected. Would you like to re-select the storage device?", (IEnumerable<string>)new string[2] { "Yes. Select new device.", "No. Continue without device." }, 0, (MessageBoxIcon)0, (AsyncCallback)reselectPromptCallback, (object)null);
					}
				}
				else if (promptForDisconnect)
				{
					if (Player.HasValue)
					{
						//Guide.BeginShowMessageBox(Player.Value, "Storage Device Disconnected", "An active storage device has been disconnected. Would you like to select a new storage device?", (IEnumerable<string>)new string[2] { "Yes. Select new device.", "No. Continue without device." }, 0, (MessageBoxIcon)0, (AsyncCallback)reselectPromptCallback, (object)null);
					}
					else
					{
						//Guide.BeginShowMessageBox("Storage Device Disconnected", "An active storage device has been disconnected. Would you like to select a new storage device?", (IEnumerable<string>)new string[2] { "Yes. Select new device.", "No. Continue without device." }, 0, (MessageBoxIcon)0, (AsyncCallback)reselectPromptCallback, (object)null);
					}
				}
				else if (promptToForceReselect)
				{
					if (Player.HasValue)
					{
						//Guide.BeginShowMessageBox(Player.Value, "Reselect Storage Device", "No storage device was selected. A storage device is required to continue. Select Ok to choose a storage device.", (IEnumerable<string>)new string[1] { "Ok" }, 0, (MessageBoxIcon)0, (AsyncCallback)forcePromptCallback, (object)null);
					}
					else
					{
						//Guide.BeginShowMessageBox("Reselect Storage Device", "No storage device was selected. A storage device is required to continue. Select Ok to choose a storage device.", (IEnumerable<string>)new string[1] { "Ok" }, 0, (MessageBoxIcon)0, (AsyncCallback)forcePromptCallback, (object)null);
					}
				}
				else if (promptForDisconnectForced)
				{
					if (Player.HasValue)
					{
						//Guide.BeginShowMessageBox(Player.Value, "Storage Device Disconnected", "An active storage device has been disconnected. A storage device is required to continue. Select Ok to choose a storage device.", (IEnumerable<string>)new string[1] { "Ok" }, 0, (MessageBoxIcon)0, (AsyncCallback)forcePromptCallback, (object)null);
					}
					else
					{
						//Guide.BeginShowMessageBox("Storage Device Disconnected", "An active storage device has been disconnected. A storage device is required to continue. Select Ok to choose a storage device.", (IEnumerable<string>)new string[1] { "Ok" }, 0, (MessageBoxIcon)0, (AsyncCallback)forcePromptCallback, (object)null);
					}
				}
			}
		}
		catch (GamerServicesNotAvailableException val)
		{
			GamerServicesNotAvailableException val2 = val;
		}
		catch (GuideAlreadyVisibleException val3)
		{
			GuideAlreadyVisibleException val4 = val3;
		}
		wasDeviceConnected = Device != null && Device.IsConnected;*/
	}

	private void forcePromptCallback(IAsyncResult ar)
	{
		//promptToForceReselect = false;
		//promptForDisconnectForced = false;
		//Guide.EndShowMessageBox(ar);
		showDeviceSelector = true;
	}

	private void reselectPromptCallback(IAsyncResult ar)
	{
		//promptForDisconnect = false;
		//promptToReSelectDevice = false;
		//int? num = Guide.EndShowMessageBox(ar);
		//showDeviceSelector = num.HasValue && num.Value == 0;
		promptEventArgs.PromptForDevice = showDeviceSelector;
		if (this.DevicePromptClosed != null)
		{
			this.DevicePromptClosed(this, promptEventArgs);
		}
	}

	private void deviceSelectorCallback(IAsyncResult ar)
	{
		Device = StorageDevice.EndShowSelector(ar);
		if (Device != null)
		{
			if (this.DeviceSelected != null)
			{
				this.DeviceSelected(this, EventArgs.Empty);
			}
			return;
		}
		eventArgs.EventResponse = StorageDeviceSelectorEventResponse.Prompt;
		if (this.DeviceSelectorCanceled != null)
		{
			this.DeviceSelectorCanceled(this, eventArgs);
		}
		HandleEventArgResults();
	}

	private void FireDeviceDisconnectedEvent()
	{
		eventArgs.EventResponse = StorageDeviceSelectorEventResponse.Prompt;
		if (this.DeviceDisconnected != null)
		{
			this.DeviceDisconnected(this, eventArgs);
		}
		HandleEventArgResults();
	}

	private void HandleEventArgResults()
	{
		Device = null;
		/*switch (eventArgs.EventResponse)
		{
		case StorageDeviceSelectorEventResponse.Prompt:
			if (wasDeviceConnected)
			{
				//promptForDisconnect = true;
			}
			else
			{
				//promptToReSelectDevice = true;
			}
			break;
		case StorageDeviceSelectorEventResponse.Force:
			if (wasDeviceConnected)
			{
				//promptForDisconnectForced = true;
			}
			else
			{
				//promptToForceReselect = true;
			}
			break;
		default:
			//promptForDisconnect = false;
			//promptForDisconnectForced = false;
			//promptToForceReselect = false;
			showDeviceSelector = false;
			break;
		}*/
	}
}
