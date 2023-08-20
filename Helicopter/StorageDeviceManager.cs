using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.GamerServices;
//using Microsoft.Xna.Framework.Storage;

namespace Helicopter
{
	public class StorageDeviceManager : GameComponent
	{
		private const string ReselectStorageDeviceText = "No storage device was selected. Would you like to re-select the storage device?";

		private const string DisconnectReselectDeviceText = "An active storage device has been disconnected. Would you like to select a new storage device?";

		private const string ForceReselectDeviceText = "No storage device was selected. A storage device is required to continue. Select Ok to choose a storage device.";

		private const string ForceDisconnectReselectText = "An active storage device has been disconnected. A storage device is required to continue. Select Ok to choose a storage device.";

		private bool wasDeviceConnected;

		private bool showDeviceSelector;

		private bool promptToReSelectDevice;

		private bool promptToForceReselect;

		private bool promptForDisconnect;

		private bool promptForDisconnectForced;

		private readonly StorageDeviceEventArgs eventArgs = new StorageDeviceEventArgs();

		private readonly StorageDevicePromptEventArgs promptEventArgs = new StorageDevicePromptEventArgs();

		//public StorageDevice Device { get; private set; }

		public PlayerIndex? Player { get; private set; }

		public PlayerIndex PlayerToPrompt { get; set; }

		public int RequiredBytes { get; private set; }

		public event EventHandler DeviceSelected;

		public event EventHandler<StorageDeviceEventArgs> DeviceSelectorCanceled;

		public event EventHandler<StorageDevicePromptEventArgs> DevicePromptClosed;

		public event EventHandler<StorageDeviceEventArgs> DeviceDisconnected;

		public StorageDeviceManager(Game game)
			: this(game, null, 0)
		{
		}

		public StorageDeviceManager(Game game, PlayerIndex player)
			: this(game, player, 0)
		{
		}

		public StorageDeviceManager(Game game, int requiredBytes)
			: this(game, null, requiredBytes)
		{
		}

		public StorageDeviceManager(Game game, PlayerIndex player, int requiredBytes)
			: this(game, (PlayerIndex?)player, requiredBytes)
		{
		}

		private StorageDeviceManager(Game game, PlayerIndex? player, int requiredBytes)
			: base(game)
		{
			this.Player = player;
			this.RequiredBytes = requiredBytes;
			this.PlayerToPrompt = PlayerIndex.One;
		}

		public void PromptForDevice()
		{
			this.showDeviceSelector = true;
		}

		//public override void Update(GameTime gameTime)
		//{
			//if (this.Device != null && !this.Device.IsConnected && this.wasDeviceConnected)
			//{
			//	this.FireDeviceDisconnectedEvent();
			//}
			//try
			//{
				/*if (!Guide.IsVisible)
				{
					if (this.showDeviceSelector)
					{
						this.showDeviceSelector = false;
						if (this.Player.HasValue)
						{
							StorageDevice.BeginShowSelector(this.Player.Value, this.RequiredBytes, 0, (AsyncCallback)deviceSelectorCallback, (object)null);
						}
						else
						{
							StorageDevice.BeginShowSelector(this.RequiredBytes, 0, (AsyncCallback)deviceSelectorCallback, (object)null);
						}
					}
					else if (this.promptToReSelectDevice)
					{
						if (this.Player.HasValue)
						{
							Guide.BeginShowMessageBox(this.Player.Value, "Reselect Storage Device?", "No storage device was selected. Would you like to re-select the storage device?", (IEnumerable<string>)new string[2] { "Yes. Select new device.", "No. Continue without device." }, 0, MessageBoxIcon.None, (AsyncCallback)reselectPromptCallback, (object)null);
						}
						else
						{
							Guide.BeginShowMessageBox("Reselect Storage Device?", "No storage device was selected. Would you like to re-select the storage device?", (IEnumerable<string>)new string[2] { "Yes. Select new device.", "No. Continue without device." }, 0, MessageBoxIcon.None, (AsyncCallback)reselectPromptCallback, (object)null);
						}
					}
					else if (this.promptForDisconnect)
					{
						if (this.Player.HasValue)
						{
							Guide.BeginShowMessageBox(this.Player.Value, "Storage Device Disconnected", "An active storage device has been disconnected. Would you like to select a new storage device?", (IEnumerable<string>)new string[2] { "Yes. Select new device.", "No. Continue without device." }, 0, MessageBoxIcon.None, (AsyncCallback)reselectPromptCallback, (object)null);
						}
						else
						{
							Guide.BeginShowMessageBox("Storage Device Disconnected", "An active storage device has been disconnected. Would you like to select a new storage device?", (IEnumerable<string>)new string[2] { "Yes. Select new device.", "No. Continue without device." }, 0, MessageBoxIcon.None, (AsyncCallback)reselectPromptCallback, (object)null);
						}
					}
					else if (this.promptToForceReselect)
					{
						if (this.Player.HasValue)
						{
							Guide.BeginShowMessageBox(this.Player.Value, "Reselect Storage Device", "No storage device was selected. A storage device is required to continue. Select Ok to choose a storage device.", (IEnumerable<string>)new string[1] { "Ok" }, 0, MessageBoxIcon.None, (AsyncCallback)forcePromptCallback, (object)null);
						}
						else
						{
							Guide.BeginShowMessageBox("Reselect Storage Device", "No storage device was selected. A storage device is required to continue. Select Ok to choose a storage device.", (IEnumerable<string>)new string[1] { "Ok" }, 0, MessageBoxIcon.None, (AsyncCallback)forcePromptCallback, (object)null);
						}
					}
					else if (this.promptForDisconnectForced)
					{
						if (this.Player.HasValue)
						{
							Guide.BeginShowMessageBox(this.Player.Value, "Storage Device Disconnected", "An active storage device has been disconnected. A storage device is required to continue. Select Ok to choose a storage device.", (IEnumerable<string>)new string[1] { "Ok" }, 0, MessageBoxIcon.None, (AsyncCallback)forcePromptCallback, (object)null);
						}
						else
						{
							Guide.BeginShowMessageBox("Storage Device Disconnected", "An active storage device has been disconnected. A storage device is required to continue. Select Ok to choose a storage device.", (IEnumerable<string>)new string[1] { "Ok" }, 0, MessageBoxIcon.None, (AsyncCallback)forcePromptCallback, (object)null);
						}
					}
				}*/
			//}
			//catch (GamerServicesNotAvailableException)
			//{
			//}
			//catch (GuideAlreadyVisibleException)
			//{
			//}
			//this.wasDeviceConnected = this.Device != null && this.Device.IsConnected;
		//}

		private void forcePromptCallback(IAsyncResult ar)
		{
			this.promptToForceReselect = false;
			this.promptForDisconnectForced = false;
			//Guide.EndShowMessageBox(ar);
			this.showDeviceSelector = true;
		}

		private void reselectPromptCallback(IAsyncResult ar)
		{
			this.promptForDisconnect = false;
			this.promptToReSelectDevice = false;
			//int? num = Guide.EndShowMessageBox(ar);
			//this.showDeviceSelector = num.HasValue && num.Value == 0;
			this.promptEventArgs.PromptForDevice = this.showDeviceSelector;
			if (this.DevicePromptClosed != null)
			{
				this.DevicePromptClosed(this, this.promptEventArgs);
			}
		}

		private void deviceSelectorCallback(IAsyncResult ar)
		{
			//this.Device = StorageDevice.EndShowSelector(ar);
			/*if (this.Device != null)
			{
				if (this.DeviceSelected != null)
				{
					this.DeviceSelected(this, EventArgs.Empty);
				}
				return;
			}*/
			this.eventArgs.EventResponse = StorageDeviceSelectorEventResponse.Prompt;
			if (this.DeviceSelectorCanceled != null)
			{
				this.DeviceSelectorCanceled(this, this.eventArgs);
			}
			this.HandleEventArgResults();
		}

		private void FireDeviceDisconnectedEvent()
		{
			this.eventArgs.EventResponse = StorageDeviceSelectorEventResponse.Prompt;
			if (this.DeviceDisconnected != null)
			{
				this.DeviceDisconnected(this, this.eventArgs);
			}
			this.HandleEventArgResults();
		}

		private void HandleEventArgResults()
		{
			//this.Device = null;
			switch (this.eventArgs.EventResponse)
			{
			case StorageDeviceSelectorEventResponse.Prompt:
				if (this.wasDeviceConnected)
				{
					this.promptForDisconnect = true;
				}
				else
				{
					this.promptToReSelectDevice = true;
				}
				break;
			case StorageDeviceSelectorEventResponse.Force:
				if (this.wasDeviceConnected)
				{
					this.promptForDisconnectForced = true;
				}
				else
				{
					this.promptToForceReselect = true;
				}
				break;
			default:
				this.promptForDisconnect = false;
				this.promptForDisconnectForced = false;
				this.promptToForceReselect = false;
				this.showDeviceSelector = false;
				break;
			}
		}
	}
}
