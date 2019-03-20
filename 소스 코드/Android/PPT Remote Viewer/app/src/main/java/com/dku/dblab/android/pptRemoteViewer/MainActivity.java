package com.dku.dblab.android.pptRemoteViewer;

import android.app.Activity;
import android.app.AlertDialog;
import android.graphics.Bitmap;
import android.os.Bundle;
import android.text.Html;
import android.view.KeyEvent;
import android.view.View;
import android.view.WindowManager;
import android.widget.EditText;
import android.widget.ImageView;

import com.dku.dblab.android.pptRemoteViewer.utils.connections.ConnectionManager;
import com.dku.dblab.android.pptRemoteViewer.utils.observers.ScreenRenewalObserver;
import com.dku.dblab.android.pptRemoteViewer.utils.observers.subjects.ScreenRenewalNotifier;

public class MainActivity extends Activity implements ScreenRenewalObserver {
    private ImageView screenViewer = null;
    private EditText ipAddress = null;

    private NetworkThread networkThread = new NetworkThread();
    private ScreenRenewalNotifier notifier = new ScreenRenewalNotifier();

    private final int port = 1282;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        getWindow().addFlags(WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);

        View connect = findViewById(R.id.connect);
        View disconnect = findViewById(R.id.disconnect);
        View previousPage = findViewById(R.id.previousPage);
        View nextPage = findViewById(R.id.nextPage);

        ipAddress = (EditText) findViewById(R.id.ipAddress);
        screenViewer = (ImageView) findViewById(R.id.screenViewer);

        previousPage.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View arg0) {
                networkThread.setPreviousPage();
            }
        });

        nextPage.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View arg0) {
                networkThread.setNextPage();
            }
        });

        //connect
        connect.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                networkThread.startClient();
            }
        });

        //disconnect
        disconnect.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                networkThread.stopClient();
            }
        });

        notifier.addObserver(this);
    }

    //volume up & down key event
    @Override
    public boolean dispatchKeyEvent(KeyEvent event) {
        if (event.getKeyCode() == KeyEvent.KEYCODE_VOLUME_UP) {
            if (event.getAction() == KeyEvent.ACTION_DOWN) {
                networkThread.setPreviousPage();
            }
            return true;
        } else if (event.getKeyCode() == KeyEvent.KEYCODE_VOLUME_DOWN) {
            if (event.getAction() == KeyEvent.ACTION_DOWN) {
                networkThread.setNextPage();
            }
            return true;
        } else {
            return super.dispatchKeyEvent(event);
        }
    }

    @Override
    public void onDestroy() {
        networkThread.stopClient();

        super.onDestroy();
    }

    public void alert(final String message, final String title) {
        this.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                try {
                    AlertDialog.Builder alert = new AlertDialog.Builder(MainActivity.this);
                    alert.setMessage(Html.fromHtml(message));
                    alert.setTitle(title);

                    alert.setPositiveButton(android.R.string.ok, null);
                    alert.show();
                } catch (Exception e) {

                }
            }
        });
    }

    public void onScreenChanged(Bitmap screen) {
        if (screen != null) {
            screenViewer.setImageBitmap(screen);
        }
    }

    private class NetworkThread {
        private ConnectionManager connectionManager = null;
        private Thread thread = null;

        private Runnable startClient, stopClient, setPreviousPage, setNextPage;

        public NetworkThread() {
            startClient = new Runnable() {
                @Override
                public void run() {
                    if (connectionManager == null || !connectionManager.isConnected()) {
                        String ip = ipAddress.getText().toString().trim();

                        try {
                            connectionManager = new ConnectionManager(notifier, ip, port);
                            connectionManager.startClient();
                        } catch (Exception e) {
                            connectionManager = null;
                            alert("<b><font color=#ff0000>CHECK</font></b> your computer <b><font color=#ff0000>BEFORE CONNECT</font></b>.", "Unavailable connection!");
                        }
                    } else {
                        alert("Your device <b><font color=#ff0000>CONNECTION</font></b> to server has been <b><font color=#ff0000>ALREADY CONNECTED</font></b>.", "Already connected!");
                    }
                }
            };

            stopClient = new Runnable() {
                @Override
                public void run() {
                    if (connectionManager != null || connectionManager.isConnected()) {
                        try {
                            connectionManager.stopClient();
                        } catch (Exception e) {
                        } finally {
                            connectionManager = null;
                        }

                        alert("Your device <b><font color=#ff0000>CONNECTION</font></b> to server has been <b><font color=#ff0000>LOST</font></b>.", "Disconnected!");
                    } else {
                        alert("Your device <b><font color=#ff0000>CONNECTION</font></b> to server has been <b><font color=#ff0000>ALREADY LOST</font></b>.", "Already not connected!");
                    }
                }
            };

            setPreviousPage = new Runnable() {
                @Override
                public void run() {
                    if (connectionManager == null || !connectionManager.isConnected()) {
                        alert("You need to <b><font color=#ff0000>CONNECT</font></b> your device to computer server <b><font color=#ff0000>BEFORE</font></b> control PPT.", "Not connected!");
                    } else {
                        try {
                            connectionManager.setPreviousPage();
                        } catch (Exception e) {
                        }
                    }
                }
            };

            setNextPage = new Runnable() {
                @Override
                public void run() {
                    if (connectionManager == null || !connectionManager.isConnected()) {
                        alert("You need to <b><font color=#ff0000>CONNECT</font></b> your device to computer server <b><font color=#ff0000>BEFORE</font></b> control PPT.", "Not connected!");
                    } else {
                        try {
                            connectionManager.setNextPage();
                        } catch (Exception e) {
                        }
                    }
                }
            };
        }

        public void startClient() {
            thread = new Thread(startClient);
            thread.start();
        }

        public void stopClient() {
            thread = new Thread(stopClient);
            thread.start();
        }

        public void setPreviousPage() {
            thread = new Thread(setPreviousPage);
            thread.start();
        }

        public void setNextPage() {
            thread = new Thread(setNextPage);
            thread.start();
        }
    }
}