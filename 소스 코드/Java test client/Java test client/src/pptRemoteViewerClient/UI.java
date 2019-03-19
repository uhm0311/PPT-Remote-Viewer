package pptRemoteViewerClient;

import java.awt.BorderLayout;
import java.awt.EventQueue;

import javax.swing.JFrame;
import javax.swing.JPanel;
import javax.swing.border.EmptyBorder;

import pptRemoteViewerClient.utils.Bitmap;
import pptRemoteViewerClient.utils.connections.ConnectionManager;
import pptRemoteViewerClient.utils.observers.ScreenRenewalObserver;
import pptRemoteViewerClient.utils.observers.subjects.ScreenRenewalNotifier;

public class UI extends JFrame implements ScreenRenewalObserver {

	/**
	 * 
	 */
	private static final long serialVersionUID = 906108169203408911L;
	private JPanel contentPane;

	private ConnectionManager connectionManager = null;
	private ScreenRenewalNotifier notifier = new ScreenRenewalNotifier();
	
	/**
	 * Launch the application.
	 */
	public static void main(String[] args) {
		EventQueue.invokeLater(new Runnable() {
			public void run() {
				try {
					UI frame = new UI();
					frame.setVisible(true);
				} catch (Exception e) {
					e.printStackTrace();
				}
			}
		});
	}

	/**
	 * Create the frame.
	 */
	public UI() {
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		setBounds(100, 100, 450, 300);
		contentPane = new JPanel();
		contentPane.setBorder(new EmptyBorder(5, 5, 5, 5));
		contentPane.setLayout(new BorderLayout(0, 0));
		setContentPane(contentPane);
	}

	@Override
	public void onScreenChanged(Bitmap screen) {
		// TODO Auto-generated method stub
		
	}

}
