package fragments;

import java.util.ArrayList;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;
import model.Constants;
import model.Order;
import ictd.activities.R;
import adapters.CompletedOrderAdapter;
import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.Bundle;
import android.support.v4.app.ListFragment;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.ListView;

/**
 * Implements the CompletedOrderFragment, loads up the list fragment's layout,
 * plugs the list of completed orders into the adapter and set's the layout for
 * that as well. Implements the clicking of each order to display their
 * information.
 */

public class CompletedOrderFragment extends ListFragment
{
	private ResultReceiver receiver;
	private ProgressDialog pd;

	/**
	 * Sets the layouts of the fragment and rows, places the list of completed
	 * orders into the respective adapter.
	 */

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState)
	{
		View rootView = inflater.inflate(R.layout.list_completed, container,
				false);

		setListAdapter(new CompletedOrderAdapter(getActivity(),
				R.layout.order_row, new ArrayList<Order>()));
		return rootView;
	}

	/**
	 * Implements the clicking of each order. Displays their information in a
	 * dialog.
	 */

	@Override
	public void onListItemClick(ListView l, View v, int position, long id)
	{
		Order order = (Order) getListAdapter().getItem(position);
		new AlertDialog.Builder(getActivity())
				.setIcon(
						((ImageView) v
								.findViewById(R.id.orderrow_orderstatus_iv))
								.getDrawable())
				.setTitle(order.getOrderId())
				.setMessage(
						String.format("%s: %s\n%s: %s\n%s: %s\n%s: %s\n\n%s: %d\n%s: %d\n%s: %d\n%s: %d\n%s: %d",
								Constants.NAME, order.getOrderOwner(),
								Constants.STATUS, order.getOrderStatus(),
								Constants.START_TIME, order.getStartTime(),
								Constants.FINISH_TIME, order.getFinishTime(),
								Constants.BLACK, order.getColourNumber(Constants.BLACK),
								Constants.BLUE, order.getColourNumber(Constants.BLUE),
								Constants.GREEN, order.getColourNumber(Constants.GREEN),
								Constants.RED, order.getColourNumber(Constants.RED),
								Constants.WHITE, order.getColourNumber(Constants.WHITE)))
				.setPositiveButton("Ok", new DialogInterface.OnClickListener() {
					public void onClick(DialogInterface dialog, int id)
					{
						dialog.cancel();
					}
				}).show();
	}

	// private class
	private class ResultReceiver extends BroadcastReceiver
	{
		@Override
		public void onReceive(Context context, Intent intent)
		{
			pd.dismiss();
			String response = intent.getStringExtra("result");
			handleCompletedOrders(response);
		}
	}

	private void handleCompletedOrders(String response)
	{
		Log.v("MAD", response);

		CompletedOrderAdapter adapter = (CompletedOrderAdapter) getListView()
				.getAdapter();
		adapter.clear();

		ArrayList<Order> orders = new ArrayList<Order>();
		JSONArray jArray;
		try
		{
			jArray = new JSONArray(response);

			for (int i = 0; i < jArray.length(); i++)
			{
				JSONObject jObj = jArray.getJSONObject(i);

				orders.add(new Order(jObj.getInt("mOrderId"), jObj
						.getString("mOrderOwner"), jObj
						.getString("mOrderStatus"), jObj.getInt("black"), jObj
						.getInt("blue"), jObj.getInt("green"), jObj
						.getInt("red"), jObj.getInt("white")));
			}
		}
		catch (JSONException e)
		{
			Log.v("MAD", e.toString());
		}

		adapter.addAll(orders);
		adapter.notifyDataSetChanged();

	}

	public void onStart()
	{
		receiver = new ResultReceiver();
		getActivity().registerReceiver(receiver,
				new IntentFilter(Constants.UPDATE_COMPLETED_ORDERS_COMMAND));

		// update completed orders here
		makeService(Constants.UPDATE_COMPLETED_ORDERS_COMMAND);
		super.onStart();
	}

	@Override
	public void onStop()
	{
		getActivity().unregisterReceiver(receiver);
		super.onStop();
	}

	private void makeService(String command)
	{
		Intent service = new Intent(getActivity(), services.SynchService.class);
		Bundle parcel = new Bundle();
		parcel.putString("command", command);
		service.putExtra("parcel", parcel);

		// stop any already running services associated with this activity
		// getActivity().stopService(service);
		pd = ProgressDialog.show(getActivity(), null, "Contacting server");
		getActivity().startService(service);
	}

}
