/* COPYRIGHT (C) 2014 Carlo Chumroonridhi. All Rights Reserved. */

package com.ictdesign.tmc;

import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;
import android.app.Activity;
import android.content.Intent;

/**
 * Implements the CreateOrderActivity. Basically fires up a form to create a new
 * order and does some error-checking to ensure user inputs valid data.
 */

public class CreateOrderActivity extends Activity
{

	/**
	 * Sets the layout.
	 */

	@Override
	protected void onCreate(Bundle savedInstanceState)
	{
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_create_order);
	}

	/**
	 * Error-checks the form and submits the data when the "Create" button is
	 * clicked.
	 * 
	 * @param view
	 */

	public void onCreateOrderClicked(View view)
	{
		String name = ((EditText) findViewById(R.id.createorder_ordername_et))
				.getText().toString();
		String number = ((EditText) findViewById(R.id.createorder_ordernumber_et))
				.getText().toString();
		if (name.equals(""))
		{
			Toast.makeText(getBaseContext(), Constants.ENTER_NAME,
					Toast.LENGTH_SHORT).show();
			return;
		}
		if (number.equals(""))
		{
			Toast.makeText(getBaseContext(), Constants.ENTER_NUMBER,
					Toast.LENGTH_SHORT).show();
			return;
		}
		Intent intent = getIntent();
		intent.putExtra(Constants.NAME, name);
		intent.putExtra(Constants.NUMBER, number);
		setResult(Constants.CREATE_ORDER, intent);
		finish();
	}

	/**
	 * Quits the activity without data submission when the "Cancel" button is
	 * clicked.
	 * 
	 * @param view
	 */

	public void onCancelOrderClicked(View view)
	{
		Intent intent = getIntent();
		setResult(Constants.RESULT_CANCEL, intent);
		finish();
	}

}