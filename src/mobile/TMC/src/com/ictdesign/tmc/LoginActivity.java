/* COPYRIGHT (C) 2014 Carlo Chumroonridhi. All Rights Reserved. */

package com.ictdesign.tmc;

import android.app.Activity;
import android.content.Intent;
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
		// Replace condition with function that takes in the username and
		// password,performs the necessary confidentiality enforcements
		// and returns a boolean whether or not it is the correct details.
		if (((EditText) findViewById(R.id.loginactivity_username_et)).getText()
				.toString().equals(Constants.USERNAME)
				&& ((EditText) findViewById(R.id.loginactivity_password_et))
						.getText().toString().equals(Constants.PASSWORD))
		{
			Intent intent = new Intent(LoginActivity.this, ModuleActivity.class);
			startActivity(intent);
		}
		else
			Toast.makeText(this, Constants.WRONGINFO, Toast.LENGTH_SHORT)
					.show();
	}
}
