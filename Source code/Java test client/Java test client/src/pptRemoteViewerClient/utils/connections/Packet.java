package pptRemoteViewerClient.utils.connections;

import pptRemoteViewerClient.utils.Bitmap;

public class Packet {
	private PacketType type;
	private int screenSize = -1;
	private Bitmap screen = null;
	
	public Packet(PacketType type, int screenSize, Bitmap screen) {
		this.type = type;
		this.screenSize = screenSize;
		this.screen = screen;
	}
	
	public PacketType getPacketType() {
		return type;
	}
	
	public int getScreenSize() {
		return screenSize;
	}
	
	public Bitmap getScreen() {
		return screen;
	}
}
