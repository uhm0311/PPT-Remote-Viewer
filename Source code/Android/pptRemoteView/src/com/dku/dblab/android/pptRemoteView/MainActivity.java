package com.dku.dblab.android.pptRemoteView;

import java.io.InputStream;
import java.io.OutputStream;
import java.net.Socket;

import android.app.Activity;
import android.app.AlertDialog;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.Bundle;
import android.text.Html;
import android.view.KeyEvent;
import android.view.Menu;
import android.view.View;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;

public class MainActivity extends Activity {
	private Button B_con, B_discon, B_left, B_right;
	private ImageView Img_View = null;
	private EditText ip_input=null;
	
	private String ipaddr=null;	//아이피
	final private int port=1828;      //포트번호
	final private int port_Img = 9999; //이미지 로드하는 포트번호.
	
	private Thread img_Load = null;
	private Thread ppt_Con = null;
	private Boolean Thread_On = false;
	
	final private byte Right = 0x1; //right
	final private byte Left = 0x2; //left
	
	private Boolean isRight = false;
	private Boolean isLeft = false;
	
    private Thread ConnectionThread = null;
	
	public void Show_alert(String msg, String title) 
	{ 
		AlertDialog.Builder alert = new AlertDialog.Builder(MainActivity.this);
		alert.setMessage(Html.fromHtml(msg));
		alert.setPositiveButton(android.R.string.ok, null);
		alert.setTitle(title);
		alert.show();
	}
	
	private void Clear_Threads()
	{
	    runOnUiThread(new Runnable() { //UI가 존재하는 쓰레드에서 실행하겠다는 의미
			@Override
			public void run() {
				if(Thread_On == true)
				{
					Thread_On = false; 
					try { Thread.sleep(100); } catch(Exception ex) { }
					
					try { img_Load.stop(); } catch (Exception ex) { }
					img_Load=null; 
					try { ppt_Con.stop(); } catch (Exception ex) { }
					ppt_Con = null;
				}
			}
		});
	}
	
	private void new_Connection()
	{
		ConnectionThread = new Thread(new Runnable() {
			@Override
			public void run() {
				try
				{
					Clear_Threads();
					
					// TODO Auto-generated method stub
					Socket TestSoc = null;
					TestSoc = new Socket(ipaddr, port);
					TestSoc.close();
					
					runOnUiThread(new Runnable() { //UI가 존재하는 쓰레드에서 실행하겠다는 의미
    					@Override
    					public void run() {
    						Thread_On = true;
							new_ImgLoad(); img_Load.start(); 
							new_pptCon(); ppt_Con.start(); //실제 연결 쓰레드 작동
							
							Show_alert("Your device <b><font color=#ff0000>CONNECTION</font></b> to server has been <b><font color=#ff0000>CONNECTED</font></b>.", "Connected!");
    					}
    				});
				} 
			catch (Exception ex) {
				
				runOnUiThread(new Runnable() { //UI가 존재하는 쓰레드에서 실행하겠다는 의미
					@Override
					public void run() {
						Show_alert("Check your <b><font color=#ff0000>IP IS CORRECT</font></b>."
								+ "<br></br>" +
								"Check whether <b><font color=#ff0000>PORT</font></b> 9999 or 1828 is <b><font color=#ff0000>ALREADY OPENED</font></b>." 
								+ "<br></br>" +
								"Check whether your computer <b><font color=#ff0000>SERVER IS RUNNING</font></b>." 
								+ "<br></br>" +
								"If your computer IP is in private IP Address Range, your device need to be in same IP Address Range."
								, "Unavailable connection!");
    					}
    				});
				}
			}
		});
	}
	
	private void new_ImgLoad()
	{
		img_Load = new Thread(new Runnable() { //새로운 쓰레드에서 계속 이미지를 받아옴
        	@Override
        	public void run()
        	{ 
        		Socket soc = null;
        		InputStream IS = null;
        		
        		while(Thread_On)
        		{
        	    	try {
        	    		try { soc = new Socket(ipaddr, port_Img); } //연결시도
                		catch (Exception ex) {
                			Clear_Threads();
        					runOnUiThread(new Runnable() { //UI가 존재하는 쓰레드에서 실행하겠다는 의미
            					@Override
            					public void run() {
            						Show_alert("Your device <b><font color=#ff0000>CONNECTION</font></b> to server has been <b><font color=#ff0000>LOST</font></b>.", "Disconnected!");
            					}
            				});
        					break;
                		}
        	    		
        				IS = soc.getInputStream();
        				final Bitmap bmp = BitmapFactory.decodeStream(IS);
        			    IS.close();
        			    
        			    runOnUiThread(new Runnable() { //UI가 존재하는 쓰레드에서 실행하겠다는 의미
        					@Override
        					public void run() {
        						Img_View.setImageBitmap(bmp); //해당 UI가 존재하는 쓰레드에서만 이미지 설정 가능
        					}
        				});
        				soc.close();
        				
        				Thread.sleep(100); //0.1초 슬립
        				
        			} catch (Exception e) { 
    					runOnUiThread(new Runnable() { //UI가 존재하는 쓰레드에서 실행하겠다는 의미
        					@Override
        					public void run() {
        						Show_alert("UNKNOWN ERROR", "ERROR");
        					}
        				});
    					break; 
					}
        		}
        	}
        });
	}
	
	private void new_pptCon()
	{
		ppt_Con = new Thread(new Runnable() {
        	@Override
        	public void run()
        	{ 
        		Socket soc = null;
        		OutputStream OS = null; 
        		try{ soc = new Socket(ipaddr, port); } //쓰레드 키기 전에 연결을 이미 확인함
        		catch (Exception ex) {
					runOnUiThread(new Runnable() { //UI가 존재하는 쓰레드에서 실행하겠다는 의미
    					@Override
    					public void run() {
    						Show_alert("UNKNOWN ERROR", "ERROR");
    					}
    				});
        		}
        		
        		while(Thread_On)
        		{
        	    	try {
        	    		OS = soc.getOutputStream();
        	    		
        				if(isRight)
        					{ OS.write(Right); isRight = false; }
        				if(isLeft) 
        					{ OS.write(Left); isLeft = false; }
        				
        				Thread.sleep(1);
        			} catch (Exception e) { 
    					runOnUiThread(new Runnable() { //UI가 존재하는 쓰레드에서 실행하겠다는 의미
        					@Override
        					public void run() {
        						Show_alert("UNKNOWN ERROR", "ERROR");
        					}
        				});
    					break; 
					}
        		}
        		try { soc.close(); }
        		catch (Exception ex) { 
					runOnUiThread(new Runnable() { //UI가 존재하는 쓰레드에서 실행하겠다는 의미
    					@Override
    					public void run() {
    						Show_alert("UNKNOWN ERROR", "ERROR");
    					}
    				});
        		}
        	}
        });
	}

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);
		
		getWindow().addFlags(WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);
		
		B_con=(Button)findViewById(R.id.con);
		B_discon=(Button)findViewById(R.id.discon);
		ip_input=(EditText)findViewById(R.id.ip2);
		Img_View=(ImageView)findViewById(R.id.imageView1);
		
		B_left = (Button)findViewById(R.id.btn1);
		B_right = (Button)findViewById(R.id.btn2);
		
		B_left.setOnClickListener(new View.OnClickListener() {
			
			@Override
			public void onClick(View arg0) {
				// TODO Auto-generated method stub
				if(Thread_On == false) //not connected
            		Show_alert("You need to <b><font color=#ff0000>CONNECT</font></b> your device to computer server <b><font color=#ff0000>BEFORE</font></b> control PPT.", "Not connected!");

				else if(isLeft == false) isLeft = true;
            	//send "left" string to server
			}
		});
		
		B_right.setOnClickListener(new View.OnClickListener() {
			
			@Override
			public void onClick(View arg0) {
				// TODO Auto-generated method stub
				if(Thread_On == false) //not connected
            		Show_alert("You need to <b><font color=#ff0000>CONNECT</font></b> your device to computer server <b><font color=#ff0000>BEFORE</font></b> control PPT.", "Not connected!");

				else if(isRight == false) isRight = true;
            	//send "right" string to server
			}
		});
		
		//connect		
		B_con.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				if(Thread_On == false)
				{
					try { ipaddr=ip_input.getText().toString().trim(); }
					catch (Exception ex) { ipaddr = null; }
					
					if(ipaddr.length() > 0)
					{
						new_Connection();
						ConnectionThread.start();
					}
					else Show_alert("<b><font color=#ff0000>INPUT</font></b> your computer <b><font color=#ff0000>IP BEFORE CONNECT</font></b>.", "Unavailable connection!");
				}
				else Show_alert("Your device <b><font color=#ff0000>CONNECTION</font></b> to server has been <b><font color=#ff0000>ALREADY CONNECTED</font></b>.", "Already connected!");
			}
		});
		
		//disconnect
		B_discon.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) { 
				if(Thread_On == true)
				{
					Clear_Threads();
					Show_alert("Your device <b><font color=#ff0000>CONNECTION</font></b> to server has been <b><font color=#ff0000>LOST</font></b>.", "Disconnected!");
				}
				
				else Show_alert("Your device <b><font color=#ff0000>CONNECTION</font></b> to server has been <b><font color=#ff0000>ALREADY LOST</font></b>.", "Already not connected!");
			}
		});	
	}
	
	//volume up & down key event
	@Override 
	  public boolean dispatchKeyEvent(KeyEvent event)
	 { 
			//if press the volume_up key
	        if(event.getKeyCode() == KeyEvent.KEYCODE_VOLUME_UP )
	        { 
	            if(event.getAction() == KeyEvent.ACTION_DOWN)
	            {
	            	if(Thread_On == false) //not connected
	            		Show_alert("You need to <b><font color=#ff0000>CONNECT</font></b> your device to computer server <b><font color=#ff0000>BEFORE</font></b> control PPT.", "Not connected!");

					else if(isLeft == false) isLeft = true;
	            	//send "left" string to server
	            } 
	            return true; 
	        }
	        //if press the volum_down key
	        if(event.getKeyCode() == KeyEvent.KEYCODE_VOLUME_DOWN )
	        {
	            if(event.getAction() == KeyEvent.ACTION_DOWN)
	            {
	            	if(Thread_On == false) //not connected
	            		Show_alert("You need to <b><font color=#ff0000>CONNECT</font></b> your device to computer server <b><font color=#ff0000>BEFORE</font></b> control PPT.", "Not connected!");

					else if(isRight == false) isRight = true;
	            	//send "right" string to server
	            } 
	            return true; 
	        } 
	        if(event.getKeyCode() == KeyEvent.KEYCODE_BACK)
	        {
	            if(event.getAction() == KeyEvent.ACTION_DOWN) {
	            	Clear_Threads();
	            	try{ ConnectionThread.stop(); } catch (Exception ex) { }
	            	ConnectionThread = null;
            	} 
	            return super.dispatchKeyEvent(event); 
	        }
	        return super.dispatchKeyEvent(event); 
	    }
	
	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.main, menu);
		return true;
	}

}