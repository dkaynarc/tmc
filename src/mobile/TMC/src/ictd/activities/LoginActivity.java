/* COPYRIGHT (C) 2014 Carlo Chumroonridhi. All Rights Reserved. */

package ictd.activities;

import com.google.gson.Gson;

import model.AuthMessage;
import model.Constants;
import android.app.Activity;
import android.app.ProgressDialog;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.view.View;
import android.widget.CheckBox;
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
		/*
		 * Intent intent = new Intent(LoginActivity.this, ModuleActivity.class);
		 * if (turnedOn == false)
		 */
		// Replace this with something you might want to do only once during
		// startup.
		/* startActivity(intent); */

		// turnedOn = true;
	}

	/**
	 * Checks to see if the username and password match up. Outputs a toast if
	 * the details are incorrect, otherwise the next activity is started up.
	 * 
	 * @param v
	 */

	public void onLoginClicked(View v)
	{
		// //////////////////// SERGEI: PLEASE IMPLEMENT THIS, WILL REMOVE CHECKBOX ONCE YOU HAVE
		if (((CheckBox) findViewById(R.id.loginactivity_operator_cb)).isChecked()) 
			Constants.USER = Constants.OPERATOR;
		else
			Constants.USER = Constants.ORDERER;
				
		String userName = ((EditText) findViewById(R.id.loginactivity_username_et))
				.getText().toString();
		String password = ((EditText) findViewById(R.id.loginactivity_password_et))
				.getText().toString();
		makeLoginService(userName, password);
		// ////////////////////
		// Replace condition with function that takes in the username and
		// password,performs the necessary confidentiality enforcements
		// and returns a boolean whether or not it is the correct details.

		/*
		 * if(false((EditText)
		 * findViewById(R.id.loginactivity_username_et)).getText()
		 * .toString().equals(Constants.USERNAME) && ((EditText)
		 * findViewById(R.id.loginactivity_password_et))
		 * .getText().toString().equals(Constants.PASSWORD)) { Intent intent =
		 * new Intent(LoginActivity.this, ModuleActivity.class);
		 * startActivity(intent); } else Toast.makeText(this,
		 * Constants.WRONGINFO, Toast.LENGTH_SHORT) .show();
		 */
	}

	private void makeLoginService(String userName, String password)
	{
		Intent service = new Intent(this, services.SynchService.class);
		Bundle parcel = new Bundle();
		parcel.putString("userName", userName);
		parcel.putString("password", password);
		parcel.putInt("command", Constants.AUTHENTICATE_COMMAND);
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

			String response = intent.getStringExtra("result");

			handleAuthenticationResult(response);

		}
	}

	@Override
	public void onStop()
	{
		unregisterReceiver(receiver);
		super.onStop();
	}

	@Override
	public void onStart()
	{
		super.onStart();
		receiver = new ResultReceiver();
		this.registerReceiver(
				receiver,
				new IntentFilter(Integer
						.toString(Constants.AUTHENTICATE_COMMAND)));
	}

	private void handleAuthenticationResult(String response)
	{
		Gson gsn = new Gson();
		AuthMessage msg = gsn.fromJson(response, AuthMessage.class);

		if (msg != null)
		{
			if ((msg.getResult()).equalsIgnoreCase("success"))
			{
				String userName = msg.getUserName();
				saveToSharedPref(Constants.USERNAME_KEY, userName);
				Intent intent = new Intent(LoginActivity.this, ModuleActivity.class);
				startActivity(intent);
			}
		}
		else
			Toast.makeText(this, Constants.WRONGINFO, Toast.LENGTH_SHORT)
					.show();

	}

	private void saveToSharedPref(String usernameKey, String userName)
	{
		SharedPreferences preferences = getSharedPreferences(
				Constants.APP_PERSISTANCE, 0);
		SharedPreferences.Editor editor = preferences.edit();
		editor.putString(usernameKey, userName);
		editor.commit();
	}

}
