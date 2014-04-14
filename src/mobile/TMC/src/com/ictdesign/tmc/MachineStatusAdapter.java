/* COPYRIGHT (C) 2014 Carlo Chumroonridhi. All Rights Reserved. */

package com.ictdesign.tmc;

import java.util.ArrayList;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.TextView;

/**
 * Implements the OrderAdapter
 * 
 * Overrides only absolute necessary methods including constructor and
 * getView().
 */

public class MachineStatusAdapter extends ArrayAdapter<Machine>
{
	private static ArrayList<Machine> mObjects = null;
	private static Context mContext;
	private static int mResource;

	/**
	 * Initializes the Order adapter's order items.
	 */

	public MachineStatusAdapter(Context context, int resource,
			ArrayList<Machine> objects)
	{
		super(context, resource, objects);
		mObjects = objects;
	}

	public MachineStatusAdapter()
	{
		super(mContext, mResource, mObjects);
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
			v = vi.inflate(R.layout.machine_row, null);
		}
		Machine machine = mObjects.get(position);
		if (machine != null)
		{
			TextView name = (TextView) v
					.findViewById(R.id.machinerow_machinename_tv);
			TextView statusText = (TextView) v
					.findViewById(R.id.machinerow_machinestatus_tv);
			ImageView type = (ImageView) v
					.findViewById(R.id.machinerow_machinetype_iv);
			ImageView statusImage = (ImageView) v
					.findViewById(R.id.machinerow_machinestatus_iv);
			if (name != null)
				name.setText(machine.getMachineName());
			if (statusText != null)
				statusText.setText(machine.getMachineStatus());
			if (type != null)
			{
				if (machine.getMachineType().equals(Constants.ROBOT))
					type.setImageResource(R.drawable.robot);
				else if (machine.getMachineType().equals(Constants.CONVEYOR))
					type.setImageResource(R.drawable.conveyor);
			}
			if (statusImage != null)
			{
				if (machine.getMachineStatus().equals(Constants.ON))
					statusImage
							.setImageResource(android.R.drawable.button_onoff_indicator_on);
				else if (machine.getMachineStatus().equals(Constants.OFF))
					statusImage
							.setImageResource(android.R.drawable.button_onoff_indicator_off);
			}
		}
		else
			v = convertView;
		return v;
	}
}
