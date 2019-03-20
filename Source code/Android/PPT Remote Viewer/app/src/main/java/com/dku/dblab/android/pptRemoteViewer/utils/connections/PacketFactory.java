package com.dku.dblab.android.pptRemoteViewer.utils.connections;

public class PacketFactory {
	public static final byte[] createSetPreviousPagePacket() {
		byte packetType = (byte)PacketType.Key.ordinal();		
		return new byte[] { packetType, 0 };
	}
	
	public static final byte[] createSetNextPagePacket() {
		byte packetType = (byte)PacketType.Key.ordinal();		
		return new byte[] { packetType, 1 };
	}

	public static final byte[] createNonePacket() {
		byte packetType = (byte)PacketType.Key.ordinal();
		return new byte[] { packetType, 2 };
	}
}
