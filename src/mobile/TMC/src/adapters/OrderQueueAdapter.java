/* COPYRIGHT (C) 2014 Carlo Chumroonridhi. All Rights Reserved. */

package adapters;

import ictd.activities.R;

import java.util.ArrayList;

import model.Constants;
import model.Order;
import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.TextView;

/**
 * Implements the OrderQueueAdapter
 * 
 * Overrides only absolute necessary methods including constructor and
 * getView().
 */

public class OrderQueueAdapter extends ArrayAdapter<Order>
{
	ArrayList<Order> mObjects = null;
	OnClickListener mOnClickListener;

	/**
	 * Initializes the Order adapter's order items.
	 * 
	 * @param mOnClickListener
	 */

	public OrderQueueAdapter(Context context, int resource,
			ArrayList<Order> objects, OnClickListener onClickListener)
	{
		super(context, resource, objects);
		mObjects = objects;
		mOnClickListener = onClickListener;
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
					.findViewById(R.id.orderrow_orderowner_tv);
			TextView number = (TextView) v
					.findViewById(R.id.orderrow_orderid_tv);//orderrow_orderid_tv
			TextView type = (TextView) v
					.findViewById(R.id.orderrow_orderstatus_tv);
			ImageView picture = (ImageView) v
					.findViewById(R.id.orderrow_orderstatus_iv);
			ImageButton delete = (ImageButton) v
					.findViewById(R.id.orderrow_deleteorder_ib);
			if (name != null)
				name.setText(order.getOrderName());
			if (number != null)//////////////////exception here
				number.setText(Integer.toString(order.getOrderId()));
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
			if (delete != null)
			{
				delete.setOnClickListener(mOnClickListener);
				delete.setTag(order);
			}
		}
		else
			v = convertView;
		return v;
	}
}
