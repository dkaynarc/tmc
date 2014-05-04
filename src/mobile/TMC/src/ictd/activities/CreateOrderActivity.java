/* COPYRIGHT (C) 2014 Carlo Chumroonridhi. All Rights Reserved. */

package ictd.activities;





import model.Constants;
import model.Order;
import android.os.Bundle;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;
import android.app.Activity;
import android.app.ProgressDialog;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.SharedPreferences;

/**
 * Implements the CreateOrderActivity. Basically fires up a form to create a new
 * order and does some error-checking to ensure user inputs valid data.
 */

public class CreateOrderActivity extends Activity
{
	private ProgressDialog pd;
	private ResultReceiver receiver;
	private Spinner spBlack;
	private Spinner spBlue;
	private Spinner spGreen;
	private Spinner spRed;
	private Spinner spWhite;
	

	/**
	 * Sets the layout.
	 */

	@Override
	protected void onCreate(Bundle savedInstanceState)
	{
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_create_order);
	   
		Integer[] items = new Integer[]{0,1,2,3,4};
		ArrayAdapter<Integer> adapter = new ArrayAdapter<Integer>(this,android.R.layout.simple_spinner_item, items);
		/*((Spinner) findViewById(R.id.createorder_black_s)).setAdapter(adapter);
		((Spinner) findViewById(R.id.createorder_blue_s)).setAdapter(adapter);
		((Spinner) findViewById(R.id.createorder_green_s)).setAdapter(adapter);
		((Spinner) findViewById(R.id.createorder_red_s)).setAdapter(adapter);
		((Spinner) findViewById(R.id.createorder_white_s)).setAdapter(adapter);*/
	
		////////////////////////////
		
		(spBlack = (Spinner) findViewById(R.id.createorder_black_s)).setAdapter(adapter);
		(spBlue = (Spinner) findViewById(R.id.createorder_blue_s)).setAdapter(adapter);
		(spGreen = (Spinner) findViewById(R.id.createorder_green_s)).setAdapter(adapter);
		(spRed = (Spinner) findViewById(R.id.createorder_red_s)).setAdapter(adapter);
		(spWhite = (Spinner) findViewById(R.id.createorder_white_s)).setAdapter(adapter);
		
		
		
		((TextView) findViewById(R.id.createorder_ownername_tv)).setText(readCurrentUserName());           

        /////////////////
	}

	/**
	 * Error-checks the form and submits the data when the "Create" button is
	 * clicked.
	 * 
	 * @param view
	 */

	public void onCreateOrderClicked(View view)
	{
		/*String name = ((EditText) findViewById(R.id.createorder_ordername_et))
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
			return;}*/
		
		
		/////////////////////		
	   
		
		Order newOrder = new Order(readCurrentUserName(),
				Integer.decode(spBlack.getSelectedItem().toString()),
				Integer.decode(spBlue.getSelectedItem().toString()),
				Integer.decode(spGreen.getSelectedItem().toString()),
				Integer.decode(spRed.getSelectedItem().toString()),
				Integer.decode(spWhite.getSelectedItem().toString())); // the user name is to be changed to a real user name
		
		
		if(newOrder.getQuantity() < 1)
		{
		    Toast.makeText(getBaseContext(), Constants.ENTER_ITEMS_QUANTITY, Toast.LENGTH_SHORT).show();	
	    	return;
	    }
		
	    if (newOrder.getQuantity() > 8)
		{
	      Toast.makeText(getBaseContext(), Constants.TOTAL_NUMBER_ERROR, Toast.LENGTH_SHORT).show();				
		  return;
		}
	    
		// get here only if validation is ok
		makeNewOrderService(newOrder);
		///////////////////////////		
		
		
		/*Intent intent = getIntent();
		intent.putExtra(Constants.NAME, name);
		intent.putExtra(Constants.NUMBER, number);
		setResult(Constants.CREATE_ORDER, intent);
		finish();*/
	}

     
	
	///////////////////////////////////////////////////////	
     private void makeNewOrderService(Order newOrder) 
     {	
		Intent service = new Intent(this, services.SynchService.class);
		Bundle parcel = new Bundle();
		parcel.putString("orderName", newOrder.getOrderName());
		parcel.putString("command", Constants.NEW_ORDER_COMMAND);
		parcel.putString("black", Integer.toString(newOrder.getColourNumber("black")));
		parcel.putString("blue", Integer.toString(newOrder.getColourNumber("blue")));
		parcel.putString("green", Integer.toString(newOrder.getColourNumber("green")));
		parcel.putString("red", Integer.toString(newOrder.getColourNumber("red")));
		parcel.putString("white", Integer.toString(newOrder.getColourNumber("white")));
		
		service.putExtra("parcel", parcel);
		
		// stop any already running services associated with this activity
		stopService(service);
		pd = ProgressDialog.show(this, null, "Contacting server");
		startService(service);
		
	}

     
     
     
     
     
//private class
private class ResultReceiver extends BroadcastReceiver
{
	@Override
	public void onReceive(Context context, Intent intent) 
	{
	   pd.dismiss();	  
	   String response = intent.getStringExtra("result"); 
	   

		  handleNewOrderResult(response, context);
		
		
	}
}






private void handleNewOrderResult(String response, Context context) 
{
		
		if((response).equalsIgnoreCase("\"success\""))
		{		
			Intent intent = getIntent();
			/*intent.putExtra(Constants.NAME, readCurrentUserName());
		    intent.putExtra(Constants.NUMBER, "6767");// update order number to real one*/
			setResult(Constants.CREATE_ORDER, intent);
			finish();
		}
		else
			Toast.makeText(context, Constants.NEW_ORDER_FAIL, Toast.LENGTH_SHORT)
					.show();
	}   


	
	private String readCurrentUserName() 
	{
		SharedPreferences preferences = getSharedPreferences(Constants.APP_PERSISTANCE, 0);
	    String userName = preferences.getString(Constants.USERNAME_KEY, null);
	    return userName;
    }
	
	
	
	@Override
	public void onStart()
	{
        IntentFilter filter = new IntentFilter(Constants.NEW_ORDER_RESULT);
        receiver = new ResultReceiver();
        this.registerReceiver(receiver, filter);
        super.onStart();
	}
	
	
	
	@Override
	protected void onStop() 
	{
	   unregisterReceiver(receiver);
	   super.onStop();
	}
	
	
/////////////////////////////////////////////////////////////////
	
	
	
	
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
