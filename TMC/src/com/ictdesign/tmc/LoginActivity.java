package com.ictdesign.tmc;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;

public class LoginActivity extends Activity
{

	static int turnedOn = 0;

	@Override
	protected void onCreate(Bundle savedInstanceState)
	{
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_login);
		Intent intent = new Intent(LoginActivity.this, ModuleActivity.class);
		if (turnedOn == 0)
			startActivity(intent);
		turnedOn = 1;
	}

	public void onLoginClicked(View v)
	{
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
