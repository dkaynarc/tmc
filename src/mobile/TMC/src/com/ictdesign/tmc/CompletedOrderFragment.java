package com.ictdesign.tmc;

import java.util.ArrayList;

import Model.Order;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.os.Bundle;
import android.support.v4.app.ListFragment;
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
		// In setListAdapter, replace the list "orders" with a function that
		// returns
		// a list of the completed orders. You can then remove the 4 lines of
		// code
		// below that set up the dummy "orders" list.
		ArrayList<Order> orders = new ArrayList<Order>();
		for (Order order : Constants.ORDERS)
			if (order.getOrderStatus().equals(Constants.COMPLETE))
				orders.add(order);
		setListAdapter(new CompletedOrderAdapter(getActivity(),
				R.layout.order_row, orders));
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
				.setTitle(order.getOrderName())
				.setMessage(
						String.format("%s: %s\n%s: %s\n%s: %s", Constants.NAME,
								order.getOrderName(), Constants.NUMBER,
								order.getOrderNumber(), Constants.STATUS,
								order.getOrderStatus()))
				.setPositiveButton("Ok", new DialogInterface.OnClickListener() {
					public void onClick(DialogInterface dialog, int id)
					{
						dialog.cancel();
					}
				}).show();
	}
}
