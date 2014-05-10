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


/**
 * Implements the activity which is basically a form that modifies the activity.
 * 
 * Gets all the existing values for the order being modified including its ID.
 * Returns the ID of the order as well as the new values input.
 */

public class ModifyOrderActivity extends Activity
{
	TextView mOrderName;
	TextView mOrderNumber;
	
	private Spinner spBlack;
	private Spinner spBlue;
	private Spinner spGreen;
	private Spinner spRed;
	private Spinner spWhite;
	private ProgressDialog pd;
	private ResultReceiver receiver;
	

	/**
	 * Sets the layout and fills the form with the pre-existing values for the
	 * order, which are passed in through the intent.
	 */

	@Override
	protected void onCreate(Bundle savedInstanceState)
	{
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_modify_order);
		
		Integer[] items = new Integer[]{ 0,1,2,3,4 };
		ArrayAdapter<Integer> adapter = new ArrayAdapter<Integer>(this,android.R.layout.simple_spinner_item, items);
		
		(spBlack = (Spinner) findViewById(R.id.createorder_black_s)).setAdapter(adapter);
		(spBlue =  (Spinner) findViewById(R.id.createorder_blue_s)).setAdapter(adapter);
		(spGreen = (Spinner) findViewById(R.id.createorder_green_s)).setAdapter(adapter);
		(spRed =   (Spinner) findViewById(R.id.createorder_red_s)).setAdapter(adapter);
		(spWhite = (Spinner) findViewById(R.id.createorder_white_s)).setAdapter(adapter);
		mOrderName = (TextView) findViewById(R.id.modifyorder_ordername_et);
		mOrderNumber = (TextView) findViewById(R.id.modifyorder_ordernumber_et);

		spBlack.setSelection(getIntent().getIntExtra("black", 0));
		spBlue.setSelection(getIntent().getIntExtra("blue", 0));  
		spGreen.setSelection(getIntent().getIntExtra("green", 0));
		spRed.setSelection(getIntent().getIntExtra("red", 0));
		spWhite.setSelection(getIntent().getIntExtra("white", 0));
			
	    mOrderName.setText(getIntent().getStringExtra(Constants.NAME));
		mOrderNumber.setText(getIntent().getStringExtra(Constants.ID));
		
	}

	/**
	 * Implements the "Modify" button.
	 * 
	 * Does some error-checking to ensure all values have been filled in
	 * correctly and then returns the order's ID and new values to the previous
	 * activity.
	 * 
	 * @param view
	 */

	public void onModifyOrderClicked(View view)
	{
		Order newOrder = new Order(
				Integer.decode(mOrderNumber.getText().toString()),
				        mOrderName.getText().toString(),
				        "Pending",
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
		makeModifyService(newOrder);
		///////////////////////////		
		
		

	}

	//////////////////////////////////////////////////////////

    private void makeModifyService(Order order) 
    {	
      Intent service = new Intent(this, services.SynchService.class);
      Bundle parcel = new Bundle();
      parcel.putString("orderName", order.getOrderName());
      parcel.putString("orderId", Integer.toString(order.getOrderId()));
      parcel.putString("status", order.getOrderStatus());
      parcel.putInt("command", Constants.MODIFY_ORDER_COMMAND);
      parcel.putString("black", Integer.toString(order.getColourNumber("black")));
      parcel.putString("blue", Integer.toString(order.getColourNumber("blue")));
      parcel.putString("green", Integer.toString(order.getColourNumber("green")));
      parcel.putString("red", Integer.toString(order.getColourNumber("red")));
      parcel.putString("white", Integer.toString(order.getColourNumber("white")));

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

          handleModifyOrderResult(response, context);
       }
    }






       private void handleModifyOrderResult(String response, Context context) 
       {

         if((response).equalsIgnoreCase("\"success\""))
         {		        	 
        	Intent intent = getIntent();
     		setResult(Constants.MODIFY_ORDER, intent);
     		finish();
         }
         else
            Toast.makeText(context, Constants.MODIFY_ORDER_FAIL, Toast.LENGTH_SHORT).show();
         }   




      @Override
      public void onStart()
      {
         IntentFilter filter = new IntentFilter(Integer.toString(Constants.MODIFY_ORDER_COMMAND));
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
	 * Implements the "Cancel" button.
	 * 
	 * Finishes the activity without submitting results.
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


