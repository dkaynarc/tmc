/* COPYRIGHT (C) 2014 Carlo Chumroonridhi. All Rights Reserved. */

package com.ictdesign.tmc;

import java.util.ArrayList;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.TextView;

/**
 * Implements the OrderAdapter
 * 
 * Overrides only absolute necessary methods including constructor and
 * getView().
 */

public class CompletedOrderAdapter extends ArrayAdapter<Order>
{
	ArrayList<Order> mObjects = null;

	/**
	 * Initializes the Order adapter's order items.
	 */

	public CompletedOrderAdapter(Context context, int resource,
			ArrayList<Order> objects)
	{
		super(context, resource, objects);
		mObjects = objects;
	}

	/**
	 * Checks if view has been inflated yet, if not then it inflates the view
	 * using the respective layout.
	 * 
	 * Obtains the order at the respective position and checks whether the
	 * object contains empty data before plugging the data into their respective
	 * fields in the layout.
	 * 
	 * Main inconsistency is checking the order type to determine which picture
	 * goes into the OrderType ImageView as well as setting the onClickListener
	 * to the Delete ImageButton while storing the order in its tag for future
	 * reference.
	 * 
	 * Maybe I should've passed its position rather than the object itself for
	 * memory purposes but it should be okay for now.
	 */

	@Override
	public View getView(int position, View convertView, ViewGroup parent)
	{
		View v = convertView;
		if (v == null)
		{
			LayoutInflater vi = (LayoutInflater) getContext().getSystemService(
					Context.LAYOUT_INFLATER_SERVICE);
			v = vi.inflate(R.layout.order_row, null);
		}
		Order order = mObjects.get(position);
		if (order != null)
		{
			TextView name = (TextView) v
					.findViewById(R.id.orderrow_ordername_tv);
			TextView number = (TextView) v
					.findViewById(R.id.orderrow_ordernumber_tv);
			TextView type = (TextView) v
					.findViewById(R.id.orderrow_orderstatus_tv);
			ImageView picture = (ImageView) v
					.findViewById(R.id.orderrow_orderstatus_iv);
			ImageButton delete = (ImageButton) v
					.findViewById(R.id.orderrow_deleteorder_ib);
			delete.setVisibility(View.GONE);
			if (name != null)
				name.setText(order.getOrderName());
			if (number != null)
				number.setText(order.getOrderNumber());
			if (type != null)
				type.setText(order.getOrderStatus());
			if (picture != null)
			{
				if (order.getOrderStatus().equals(Constants.PENDING))
					picture.setImageResource(R.drawable.pending);
				else if (order.getOrderStatus().equals(Constants.ACTIVE))
					picture.setImageResource(R.drawable.active);
				else if (order.getOrderStatus().equals(Constants.COMPLETE))
					picture.setImageResource(R.drawable.complete);
			}
		}
		else
			v = convertView;
		return v;
	}
}
