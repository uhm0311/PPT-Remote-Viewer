package pptRemoteViewerClient.utils.connections;

import java.io.IOException;
import java.net.Socket;
import java.net.UnknownHostException;

import pptRemoteViewerClient.utils.observers.subjects.ScreenRenewalSubject;

public class ConnectionManager {
	private ScreenRenewalSubject subject = null;
	private Socket client = null;
	
	private String ipAddress = null;
	private int port = 0;
	
	private Thread connectionThread = new Thread();
	private boolean isRunning = false;
	
	public ConnectionManager(ScreenRenewalSubject subject, String ipAddress, int port) {
		this.ipAddress = ipAddress;
		this.port = port;
		
		this.subject = subject;
		this.connectionThread = new Thread(new Runnable() {
			public void run() {
				runConnection();
			}
		});
	}
	
	public void startClient() throws UnknownHostException, IOException {
		if (!isRunning) {
			isRunning = true;
			
			client = new Socket(ipAddress, port);
			connectionThread.start();
		}
	}
	
	private void runConnection() {
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
			
			connectionThread.interrupt();
			client.close();
		}
	}
	
	public void setPreviousPage() throws IOException {	
		client.getOutputStream().write(PacketFactory.createSetPreviousPagePacket());
	}
	
	public void setNextPage() throws IOException {		
		client.getOutputStream().write(PacketFactory.createSetNextPagePacket());
	}
}
