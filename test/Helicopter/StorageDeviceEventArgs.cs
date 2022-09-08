using System;

namespace Helicopter
{
	public class StorageDeviceEventArgs : EventArgs
	{
		public StorageDeviceSelectorEventResponse EventResponse { get; set; }
	}
}
