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

import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

public class MainActivity extends Activity implements ScreenRenewalObserver {
    private ImageView screenViewer = null;
    private EditText ipAddress = null;
    private final int port = 1282;

    private ConnectionManager connectionManager = null;
    private ExecutorService threadPool = Executors.newCachedThreadPool();
    private Runnable startClient, stopClient, setPreviousPage, setNextPage;

    private ScreenRenewalNotifier notifier = new ScreenRenewalNotifier();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        getWindow().addFlags(WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);

        initViews();
        initConnections();

        notifier.addObserver(this);
    }
    
    private void initViews() {
        View connect = findViewById(R.id.connect);
        View disconnect = findViewById(R.id.disconnect);
        View previousPage = findViewById(R.id.previousPage);
        View nextPage = findViewById(R.id.nextPage);

        ipAddress = (EditText) findViewById(R.id.ipAddress);
        screenViewer = (ImageView) findViewById(R.id.screenViewer);

        previousPage.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View arg0) {
                setPreviousPage();
            }
        });

        nextPage.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View arg0) {
                setNextPage();
            }
        });

        //connect
        connect.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                startClient();
            }
        });

        //disconnect
        disconnect.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                stopClient();
            }
        });
    }
    
    private void initConnections() {
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
                        alert(getString(R.string.connection_fail), getString(R.string.connection_fail_title));
                    }
                } else {
                    alert(getString(R.string.connection_already_success), getString(R.string.connection_already_success_title));
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

                    alert(getString(R.string.connection_lost), getString(R.string.connection_lost_title));
                } else {
                    alert(getString(R.string.connection_already_lost), getString(R.string.connection_already_lost_title));
                }
            }
        };

        setPreviousPage = new Runnable() {
            @Override
            public void run() {
                if (connectionManager == null || !connectionManager.isConnected()) {
                    alert(getString(R.string.connection_need), getString(R.string.connection_need_title));
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
                    alert(getString(R.string.connection_need), getString(R.string.connection_need_title));
                } else {
                    try {
                        connectionManager.setNextPage();
                    } catch (Exception e) {
                    }
                }
            }
        };
    }

    //volume up & down key event
    @Override
    public boolean dispatchKeyEvent(KeyEvent event) {
        if (event.getKeyCode() == KeyEvent.KEYCODE_VOLUME_UP) {
            if (event.getAction() == KeyEvent.ACTION_DOWN) {
                setPreviousPage();
            }
            return true;
        } else if (event.getKeyCode() == KeyEvent.KEYCODE_VOLUME_DOWN) {
            if (event.getAction() == KeyEvent.ACTION_DOWN) {
                setNextPage();
            }
            return true;
        } else {
            return super.dispatchKeyEvent(event);
        }
    }

    @Override
    public void onDestroy() {
        stopClient();

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

    private void startClient() {
        threadPool.execute(startClient);
    }

    private void stopClient() {
        threadPool.execute(stopClient);
    }

    private void setPreviousPage() {
        threadPool.execute(setPreviousPage);
    }

    private void setNextPage() {
        threadPool.execute(setNextPage);
    }
}