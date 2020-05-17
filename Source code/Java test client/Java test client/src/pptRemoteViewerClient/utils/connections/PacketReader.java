package pptRemoteViewerClient.utils.connections;

import java.io.IOException;
import java.io.InputStream;
import java.nio.ByteBuffer;

import pptRemoteViewerClient.utils.Bitmap;

public class PacketReader {
	public static final Packet read(InputStream stream) throws IOException {		
		byte[] buffer = new byte[1];
		stream.read(buffer);
		PacketType type = PacketType.values()[buffer[0]];
		
		buffer = new byte[4];
		stream.read(buffer);
		int screenSize = ByteBuffer.wrap(buffer).getInt();
		
		buffer = new byte[screenSize];
		stream.read(buffer);
		Bitmap screen = new Bitmap(buffer);
		
		return new Packet(type, screenSize, screen);
	}
}
