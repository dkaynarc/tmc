/* COPYRIGHT (C) 2014 Carlo Chumroonridhi. All Rights Reserved. */

package com.ictdesign.tmc;

import Model.Constants;
import android.app.Activity;
import android.app.ProgressDialog;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;

/**
 * Implements the initial login activity that checks for the correct user
 * details.
 */
public class LoginActivity extends Activity
{
	static boolean turnedOn = false;
	private ResultReceiver receiver;
	private ProgressDialog pd;

	/**
	 * Sets the layout and starts the next activity once during start-up for
	 * testing purposes.
	 */

	
	
	@Override
	protected void onCreate(Bundle savedInstanceState)
	{
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_login);
		Intent intent = new Intent(LoginActivity.this, ModuleActivity.class);
		if (turnedOn == false)
			// Replace this with something you might want to do only once during
			// startup.
			startActivity(intent);
		
		//////////////////
		IntentFilter filter = new IntentFilter(Constants.FEEDBACK);
		receiver = new ResultReceiver();
		this.registerReceiver(receiver, filter);
		/////////////////
		
		turnedOn = true;
	}

	
	
	
	/**
	 * Checks to see if the username and password match up. Outputs a toast if
	 * the details are incorrect, otherwise the next activity is started up.
	 * 
	 * @param v
	 */

	public void onLoginClicked(View v)
	{
		//////////////////////
		makeLoginService(((EditText) findViewById(R.id.loginactivity_username_et)).getText().toString(),
				((EditText) findViewById(R.id.loginactivity_password_et)).getText().toString());
		//////////////////////
		// Replace condition with function that takes in the username and
		// password,performs the necessary confidentiality enforcements
		// and returns a boolean whether or not it is the correct details.
		
		/*if(false((EditText) findViewById(R.id.loginactivity_username_et)).getText()
				.toString().equals(Constants.USERNAME)
				&& ((EditText) findViewById(R.id.loginactivity_password_et))
						.getText().toString().equals(Constants.PASSWORD))
		{
			Intent intent = new Intent(LoginActivity.this, ModuleActivity.class);
			startActivity(intent);
		}
		else
			Toast.makeText(this, Constants.WRONGINFO, Toast.LENGTH_SHORT)
					.show();*/
	}

	private void makeLoginService(String userName, String password) {
		
		Intent service = new Intent(this, services.SynchService.class);
		Bundle parcel = new Bundle();
		parcel.putString("userName",userName);
		parcel.putString("password", password);
		parcel.putString("command",Constants.AUTHENTICATE_COMMAND);
		service.putExtra("parcel", parcel);
		
		// stop any already running services associated with this activity
		stopService(service);
		pd = ProgressDialog.show(this, null, "Contacting server");
		startService(service);
		
	}

		
	
	
	
// private class
private class ResultReceiver extends BroadcastReceiver
{
	@Override
	public void onReceive(Context context, Intent intent) 
	{
	   pd.dismiss();
		
	   String command = intent.getStringExtra("command");
	   String response = intent.getStringExtra("result"); 
	   
	   // convert command value into an integer and do "switch"
	   switch(Integer.decode(command))
		{
		  case 1:
           handleAuthenticationResult(response, context);
		  break;
				 
		  case 2:
		 // handleSaveDataResp(command,intent);
		  break;

		}
	}

	
	
	
	
	private void handleAuthenticationResult(String response, Context context) 
	{
		
		if((response).equalsIgnoreCase("\"success\""))
		{		
			unregisterReceiver(receiver);
			Intent intent = new Intent(LoginActivity.this, ModuleActivity.class);
			startActivity(intent);
		}
		else
			Toast.makeText(context, Constants.WRONGINFO, Toast.LENGTH_SHORT)
					.show();
	}
	
	
  }



}