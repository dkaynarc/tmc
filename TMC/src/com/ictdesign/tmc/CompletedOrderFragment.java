package com.ictdesign.tmc;

import java.util.ArrayList;

import android.app.AlertDialog;
import android.content.DialogInterface;
import android.os.Bundle;
import android.support.v4.app.ListFragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.ListView;

public class CompletedOrderFragment extends ListFragment
{
	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState)
	{
		View rootView = inflater.inflate(R.layout.list_completed, container,
				false);
		ArrayList<Order> orders = new ArrayList<Order>();
		for (Order order : Constants.ORDERS)
			if (order.getOrderStatus().equals(Constants.COMPLETE))
				orders.add(order);
		setListAdapter(new CompletedOrderAdapter(getActivity(),
				R.layout.order_row, orders));
		return rootView;
	}

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
