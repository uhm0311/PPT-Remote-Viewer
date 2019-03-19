package pptRemoteViewerClient.utils.connections;

public class PacketFactory {
	public static final byte[] createSetPreviousPagePacket() {
		byte packetType = (byte)PacketType.Key.ordinal();		
		return new byte[] { packetType, 0 };
	}
	
	public static final byte[] createSetNextPagePacket() {
		byte packetType = (byte)PacketType.Key.ordinal();		
		return new byte[] { packetType, 1 };
	}
}
