/* COPYRIGHT (C) 2014 Carlo Chumroonridhi. All Rights Reserved. */

package fragments;

import java.util.ArrayList;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import com.google.gson.Gson;

import model.Constants;
import model.Order;
import model.OrderParcel;
import ictd.activities.CreateOrderActivity;
import ictd.activities.ModifyOrderActivity;
import ictd.activities.R;
import adapters.OrderQueueAdapter;
import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v4.app.ListFragment;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.Toast;

/**
 * Sets the layout for the activity.
 * 
 * Creates a new adapter for a list of orders using predefined values.
 * 
 * Implements each order's "Delete" button and "Modify" option.
 */

public class OrderQueueFragment extends ListFragment
{
	private ResultReceiver receiver;
	private ProgressDialog pd;
	private Timer timer;

	/**
	 * Implements the order's delete button.
	 */

	private OnClickListener onDeleteOrderClickListener = new OnClickListener() {
		public void onClick(final View view)
		{
			new AlertDialog.Builder(getActivity())
					.setIcon(android.R.drawable.ic_delete)
					.setTitle(Constants.DELETE_TITLE)
					.setMessage(Constants.DELETE_CONFIRM)
					.setPositiveButton(Constants.OK,

					new DialogInterface.OnClickListener() {
						public void onClick(DialogInterface dialog, int id)
						{

							if ((readUserRole()
									.equalsIgnoreCase(Constants.OPERATOR_ROLE))
									|| (readCurrentUserName()
											.equalsIgnoreCase(((Order) view
													.getTag()).getOrderOwner())))
							{
								makeService(Constants.DELETE_ORDER_COMMAND,
										((Order) view.getTag()).getOrderId());
								pd = ProgressDialog.show(getActivity(), null,
										"Deleting the order");
							}
							else
								Toast.makeText(
										getActivity(),
										"You are not authorized to delete this order",
										Toast.LENGTH_SHORT).show();

						}
					})
					.setNegativeButton(Constants.CANCEL,
							new DialogInterface.OnClickListener() {
								public void onClick(DialogInterface dialog,
										int id)
								{
									dialog.cancel();
								}
							}).show();
		}
	};

	/**
	 * Implements the create order onClick Listener which starts up the new
	 * activity.
	 */

	private OnClickListener onCreateOrderClickListener = new OnClickListener() {
		public void onClick(View view)
		{
			Intent intent = new Intent(getActivity(), CreateOrderActivity.class);
			startActivityForResult(intent, Constants.REQUEST_CODE);
		}
	};

	/**
	 * Sets the layout for the activity.
	 * 
	 * Creates a new adapter for a list of orders using predefined values.
	 */
	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState)
	{

		View rootView = inflater.inflate(R.layout.list_queue, container, false);
		((Button) rootView.findViewById(R.id.queuelist_createorder_b))
				.setOnClickListener(onCreateOrderClickListener);

		// Replace list "orders" with the list of orders returned
		setListAdapter(new OrderQueueAdapter(getActivity(), R.layout.order_row,
				new ArrayList<Order>(), onDeleteOrderClickListener));

		return rootView;
	}

	/**
	 * Implements each order's onClick listener, displaying its current values
	 * and providing the option to modify the order.
	 */

	@Override
	public void onListItemClick(ListView l, View v, final int position, long id)
	{
		final Order order = (Order) getListAdapter().getItem(position);
		new AlertDialog.Builder(getActivity())
				.setIcon(
						((ImageView) v
								.findViewById(R.id.orderrow_orderstatus_iv))
								.getDrawable())
				.setTitle(Integer.toString(order.getOrderId()))
				.setMessage(
						String.format(
								"%s: %s\n%s: %s\n\n%s: %d\n%s: %d\n%s: %d\n%s: %d\n%s: %d",
								Constants.NAME, order.getOrderOwner(),
								Constants.STATUS, order.getOrderStatus(),
								Constants.BLACK,
								order.getColourNumber(Constants.BLACK),
								Constants.BLUE,
								order.getColourNumber(Constants.BLUE),
								Constants.GREEN,
								order.getColourNumber(Constants.GREEN),
								Constants.RED,
								order.getColourNumber(Constants.RED),
								Constants.WHITE,
								order.getColourNumber(Constants.WHITE)))
				.setPositiveButton(Constants.OK,
						new DialogInterface.OnClickListener() {
							public void onClick(DialogInterface dialog, int id)
							{
								dialog.cancel();
							}
						})

				.setNegativeButton(Constants.MODIFY,
						new DialogInterface.OnClickListener() {
							public void onClick(DialogInterface dialog, int id)
							{

								if ((readUserRole()
										.equalsIgnoreCase(Constants.OPERATOR_ROLE))
										|| (readCurrentUserName()
												.equalsIgnoreCase(order
														.getOrderOwner())))
								{
									Intent intent = new Intent(getActivity(),
											ModifyOrderActivity.class);
									// //////////////////////////////////////////////
									intent.putExtra(Constants.ID, Integer
											.toString(order.getOrderId()));
									intent.putExtra(Constants.NAME,
											order.getOrderOwner());
									intent.putExtra("black",
											order.getColourNumber("black"));
									intent.putExtra("blue",
											order.getColourNumber("blue"));
									intent.putExtra("green",
											order.getColourNumber("green"));
									intent.putExtra("red",
											order.getColourNumber("red"));
									intent.putExtra("white",
											order.getColourNumber("white"));
									// ///////////////////////////////////////////////
									startActivityForResult(intent,
											Constants.REQUEST_CODE);
									dialog.cancel();
								}
								else
								{
									Toast.makeText(getActivity(),
											Constants.NOT_AUTHORIZED,
											Toast.LENGTH_SHORT).show();
								}
							}
						}).show();
	}

	private String readCurrentUserName()
	{
		SharedPreferences preferences = getActivity().getSharedPreferences(
				Constants.APP_PERSISTANCE, 0);
		String userName = preferences.getString(Constants.USERNAME_KEY, null);
		return userName;
	}

	private String readUserRole()
	{
		SharedPreferences preferences = getActivity().getSharedPreferences(
				Constants.APP_PERSISTANCE, 0);
		String userName = preferences.getString(Constants.USERROLE_KEY, null);
		return userName;
	}

	/**
	 * Handles the return of the create and modify order activities.
	 * 
	 * Does some error-checking to ensure that necessary values have been
	 * passed.
	 */

	@Override
	public void onActivityResult(int requestCode, int resultCode, Intent data)
	{
		super.onActivityResult(requestCode, resultCode, data);
		switch (resultCode)
		{
		case Constants.CREATE_ORDER:
			Toast.makeText(this.getActivity(), "Order created.",
					Toast.LENGTH_SHORT).show();
			break;

		case Constants.MODIFY_ORDER:
			Toast.makeText(this.getActivity(), "Order modified.",
					Toast.LENGTH_SHORT).show();
			break;
		}
	}

	// ///////////////////////////////////////////////////////////////////////////
	private void makeService(int command, int orderId)
	{
		Intent service = new Intent(getActivity(), services.SynchService.class);
		Bundle parcel = new Bundle();
		parcel.putInt("command", command);
		parcel.putString("orderId", Integer.toString(orderId));
		service.putExtra("parcel", parcel);

		// stop any already running services associated with this activity
		getActivity().stopService(service);
		getActivity().startService(service);
	}

	// private class
	private class ResultReceiver extends BroadcastReceiver
	{
		@Override
		public void onReceive(Context context, Intent intent)
		{
			if (pd != null)
				pd.dismiss();

			String response = intent.getStringExtra("result");

			switch (Integer.decode(intent.getStringExtra("command")))
			{
			case 3:
				handleOrdersUpdate(response);
				break;

			case 4:
				handleOrderDelete(response);
				break;

			}
		}
	}

	private void handleOrderDelete(String response)
	{
		try
		{
			JSONObject jsObj = new JSONObject(response);

			String result = jsObj.getString("result");

			if (result.equalsIgnoreCase("success"))
			{
				int orderId = jsObj.getInt("orderId");

				OrderQueueAdapter adapter = (OrderQueueAdapter) getListView()
						.getAdapter();
				for (int i = 0; i < adapter.getCount(); i++)
				{
					Order order = adapter.getItem(i);
					if (order.getOrderId() == orderId)
						adapter.remove(order);
				}
				adapter.notifyDataSetChanged();
				Toast.makeText(this.getActivity(), "Order deleted.",
						Toast.LENGTH_SHORT).show();
				return;
			}

		}

		catch (JSONException exc)
		{
			Log.v("MAD", exc.toString());
		}

		Toast.makeText(this.getActivity(), Constants.DELETE_ORDER_FAIL,
				Toast.LENGTH_SHORT).show();
	}

	private void handleOrdersUpdate(String response)
	{
		OrderQueueAdapter adapter = (OrderQueueAdapter) getListView()
				.getAdapter();

		ArrayList<Order> orders = new ArrayList<Order>();

		JSONArray jArray;
		try
		{
			jArray = new JSONArray(response);

			for (int i = 0; i < jArray.length(); i++)
			{
				JSONObject jObj = jArray.getJSONObject(i);

				Order order = new Order(jObj.getInt("mOrderId"),
						jObj.getString("mOrderOwner"),
						jObj.getString("mOrderStatus"), jObj.getInt("black"),
						jObj.getInt("blue"), jObj.getInt("green"),
						jObj.getInt("red"), jObj.getInt("white"));

				order.setFinishTime(jObj.getString("endTime"));
				order.setStartTime(jObj.getString("startTime"));
				orders.add(order);
			}

			if (orders.size() > 0)
			{
				adapter.clear();
				adapter.addAll(orders);
				adapter.notifyDataSetChanged();
				saveOrderDataLocally(orders);
				Toast.makeText(getActivity(), "Updated orders information",
						Toast.LENGTH_SHORT).show();
			}
		}
		catch (JSONException e)
		{
			Toast.makeText(getActivity(), Constants.ORDER_UPDATE_FAIL,
					Toast.LENGTH_SHORT).show();
		}
	}

	@Override
	public void onStart()
	{
		super.onStart();
		receiver = new ResultReceiver();
		getActivity().registerReceiver(
				receiver,
				new IntentFilter(Integer
						.toString(Constants.UPDATE_ORDERS_COMMAND)));
		getActivity().registerReceiver(
				receiver,
				new IntentFilter(Integer
						.toString(Constants.DELETE_ORDER_COMMAND)));

		if (localDataExist())
		{
			OrderQueueAdapter adapter = (OrderQueueAdapter) getListView()
					.getAdapter();
			adapter.clear();
			adapter.addAll(readLocalOrderData());
			adapter.notifyDataSetChanged();
		}
		else
			pd = ProgressDialog.show(getActivity(), null,
					"Updating incomplete orders");

		makeService(Constants.UPDATE_ORDERS_COMMAND, 0);
		startTimer(Constants.UPDATE_INTERVAL);
	}

	private boolean localDataExist()
	{
		ArrayList<Order> orders = readLocalOrderData();
		if (orders.size() > 0)
			return true;
		else
			return false;
	}

	private void saveOrderDataLocally(ArrayList<Order> orders)
	{
		Gson json = new Gson();

		SharedPreferences preferences = getActivity().getSharedPreferences(
				Constants.APP_PERSISTANCE, 0);
		SharedPreferences.Editor editor = preferences.edit();

		OrderParcel parc = new OrderParcel();
		parc.setOrders(orders);
		editor.putString(Constants.ORDERS_KEY, json.toJson(parc));

		editor.commit();
	}

	private ArrayList<Order> readLocalOrderData()
	{
		Gson json = new Gson();
		SharedPreferences preferences = getActivity().getSharedPreferences(
				Constants.APP_PERSISTANCE, 0);

		OrderParcel ordrPrc = json.fromJson(
				preferences.getString(Constants.ORDERS_KEY, null),
				OrderParcel.class);
		if (ordrPrc != null)
			return ordrPrc.getOrders();

		else
			return new ArrayList<Order>();
	}

	@Override
	public void onStop()
	{
		super.onStop();
		getActivity().unregisterReceiver(receiver);
		stopTimer();
	}

	private void stopTimer()
	{
		timer.cancel(true);
	}

	private void startTimer(long i)
	{
		timer = new Timer();
		timer.execute(i);
	}

	private class Timer extends AsyncTask<Long, Object, Object>
	{

		@Override
		protected String doInBackground(Long... params)
		{
			try
			{
				Thread.sleep(params[0]);
				return "done";
			}

			catch (InterruptedException e)
			{
				e.printStackTrace();
				return "done";
			}
		}

		@Override
		protected void onPostExecute(Object result)
		{
			// this creates regular updates of the view
			makeService(Constants.UPDATE_ORDERS_COMMAND, 0);
			startTimer(Constants.UPDATE_INTERVAL);
		}

	}

}
