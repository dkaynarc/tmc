package com.ictdesign.tmc;

import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;
import android.app.Activity;
import android.content.Intent;

public class ModifyOrderActivity extends Activity
{
	EditText mOrderName;
	EditText mOrderNumber;
	int mId;

	@Override
	protected void onCreate(Bundle savedInstanceState)
	{
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_modify_order);
		mOrderName = (EditText) findViewById(R.id.modifyorder_ordername_et);
		mOrderNumber = (EditText) findViewById(R.id.modifyorder_ordernumber_et);
		Intent intent = getIntent();
		if (intent.hasExtra(Constants.NAME))
			mOrderName.setText(intent.getStringExtra(Constants.NAME));
		if (intent.hasExtra(Constants.NUMBER))
			mOrderNumber.setText(intent.getStringExtra(Constants.NUMBER));
		if (intent.hasExtra(Constants.ID))
			mId = intent.getIntExtra(Constants.ID, 0);
	}
	
	public void onModifyOrderClicked(View view)
	{
		String name = mOrderName.getText().toString();
		String number = mOrderNumber.getText().toString();
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
		intent.putExtra(Constants.ID, mId);
		setResult(Constants.MODIFY_ORDER, intent);
		finish();
	}

	public void onCancelOrderClicked(View view)
	{
		Intent intent = getIntent();
		setResult(Constants.RESULT_CANCEL, intent);
		finish();
	}

}
