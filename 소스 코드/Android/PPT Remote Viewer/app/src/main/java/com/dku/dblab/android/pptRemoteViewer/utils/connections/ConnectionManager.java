package com.dku.dblab.android.pptRemoteViewer.utils.connections;

import com.dku.dblab.android.pptRemoteViewer.utils.observers.subjects.ScreenRenewalSubject;

import java.io.IOException;
import java.net.Socket;
import java.net.UnknownHostException;

public class ConnectionManager {
	private ScreenRenewalSubject subject = null;
	private Socket client = null;
	
	private String ipAddress = null;
	private int port = 0;

	private Thread heartbeatThread = null;
	private Thread receivingThread = null;
	private boolean isRunning = false;
	
	public ConnectionManager(ScreenRenewalSubject subject, String ipAddress, int port) {
		this.ipAddress = ipAddress;
		this.port = port;
		
		this.subject = subject;
		this.heartbeatThread = new Thread(new Runnable() {
			@Override
			public void run() {
				runHeartbeat();
			}
		});
		this.receivingThread = new Thread(new Runnable() {
			public void run() {
				runReceiving();
			}
		});
	}
	
	public void startClient() throws UnknownHostException, IOException {
		if (!isRunning) {
			isRunning = true;
			
			client = new Socket(ipAddress, port);
			receivingThread.start();
			heartbeatThread.start();
		}
	}

	private void runHeartbeat() {
		while (isRunning) {
			try {
				client.getOutputStream().write(PacketFactory.createNonePacket());
			} catch (Exception e) {
				try { stopClient(); }
				catch (Exception ex) { }
			}
		}
	}
	
	private void runReceiving() {
		while (isRunning) {
			try {
				Packet packet = PacketReader.read(client.getInputStream());
				subject.notify(packet.getScreen());
			} catch (Exception e) {

			}
		}
	}
	
	public void stopClient() throws IOException {
		if (isRunning) {
			isRunning = false;

			heartbeatThread.interrupt();
			receivingThread.interrupt();
			client.close();
		}
	}

	public boolean isConnected() {
		return isRunning;
	}
	
	public void setPreviousPage() throws IOException {	
		client.getOutputStream().write(PacketFactory.createSetPreviousPagePacket());
	}
	
	public void setNextPage() throws IOException {		
		client.getOutputStream().write(PacketFactory.createSetNextPagePacket());
	}
}
