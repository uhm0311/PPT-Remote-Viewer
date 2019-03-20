package com.dku.dblab.android.pptRemoteViewer.utils.connections;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;

import java.io.IOException;
import java.io.InputStream;
import java.nio.ByteBuffer;

public class PacketReader {
	public static final Packet read(InputStream stream) throws IOException {
		byte[] buffer = new byte[1];
		stream.read(buffer);
		PacketType type = PacketType.values()[buffer[0]];

		buffer = new byte[4];
		stream.read(buffer);

		ByteBuffer byteBuffer = ByteBuffer.allocate(4);
		int screenSize = byteBuffer.wrap(buffer).getInt();

		buffer = new byte[screenSize];
		int actualSize = 0;

		while (actualSize != screenSize) {
			actualSize += stream.read(buffer, actualSize, screenSize - actualSize);
		}

		Bitmap screen = BitmapFactory.decodeByteArray(buffer, 0, buffer.length);
		return new Packet(type, screenSize, screen);
	}
}
