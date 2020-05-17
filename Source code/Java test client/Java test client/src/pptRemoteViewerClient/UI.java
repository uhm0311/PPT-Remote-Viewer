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
import javax.swing.JTextField;
import javax.swing.JLabel;
import javax.swing.JButton;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.io.IOException;
import java.net.UnknownHostException;

public class UI extends JFrame implements ScreenRenewalObserver {

	/**
	 * 
	 */
	private static final long serialVersionUID = 906108169203408911L;
	private JPanel contentPane;

	private ConnectionManager connectionManager = null;
	private ScreenRenewalNotifier notifier = new ScreenRenewalNotifier();
	private JTextField txtTestForPage;
	private JLabel screen;
	
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
	 * @throws IOException 
	 * @throws UnknownHostException 
	 */
	public UI() throws UnknownHostException, IOException {
		connectionManager = new ConnectionManager(notifier, "127.0.0.1", 1282);
		notifier.addObserver(this);
		
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		setBounds(100, 100, 450, 300);
		contentPane = new JPanel();
		contentPane.setBorder(new EmptyBorder(5, 5, 5, 5));
		contentPane.setLayout(new BorderLayout(0, 0));
		setContentPane(contentPane);
		
		txtTestForPage = new JTextField();
		txtTestForPage.setText("Test for page movement");
		contentPane.add(txtTestForPage, BorderLayout.NORTH);
		txtTestForPage.setColumns(10);
		
		screen = new JLabel("Screen will show up here");
		contentPane.add(screen, BorderLayout.CENTER);
		
		JButton previousPage = new JButton("Previous Page");
		previousPage.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent arg0) {
				try {
					connectionManager.setPreviousPage();
				} catch (IOException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
			}
		});
		contentPane.add(previousPage, BorderLayout.WEST);
		
		JButton nextPage = new JButton("Next Page");
		nextPage.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
				try {
					txtTestForPage.requestFocus();
					connectionManager.setNextPage();
				} catch (IOException e1) {
					// TODO Auto-generated catch block
					e1.printStackTrace();
				}
			}
		});
		contentPane.add(nextPage, BorderLayout.EAST);
		
		JButton stopClient = new JButton("Stop Client");
		stopClient.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
				try {
					txtTestForPage.requestFocus();
					connectionManager.stopClient();
				} catch (IOException e1) {
					// TODO Auto-generated catch block
					e1.printStackTrace();
				}
			}
		});
		contentPane.add(stopClient, BorderLayout.SOUTH);
		
		connectionManager.startClient();
	}

	@Override
	public void onScreenChanged(Bitmap screen) {
		// TODO Auto-generated method stub		
		if (screen != null) {
			this.screen.setIcon(screen);
		}
	}

}
