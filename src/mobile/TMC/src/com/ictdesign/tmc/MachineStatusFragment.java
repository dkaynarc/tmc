/* COPYRIGHT (C) 2014 Carlo Chumroonridhi. All Rights Reserved. */

package com.ictdesign.tmc;

import android.app.AlertDialog;
import android.content.DialogInterface;
import android.graphics.Color;
import android.graphics.ColorFilter;
import android.graphics.LightingColorFilter;
import android.graphics.drawable.Drawable;
import android.media.MediaPlayer;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.support.v4.app.ListFragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.ListView;

/**
 * Sets the layout for the activity.
 * 
 * Creates a new adapter for a list of orders using predefined values.
 * 
 * Implements each order's "Delete" button.
 */

public class MachineStatusFragment extends ListFragment
{
	MediaPlayer mMediaPlayer = new MediaPlayer();

	private OnClickListener onEmergencyStopClickListener = new OnClickListener() {
		public void onClick(View view)
		{
			Drawable myIcon = getResources().getDrawable(
					android.R.drawable.ic_dialog_alert);
			ColorFilter filter = new LightingColorFilter(Color.RED, Color.RED);
			myIcon.setColorFilter(filter);
			new AlertDialog.Builder(getActivity())
					.setIcon(myIcon)
					.setTitle(Constants.STOP_TITLE)
					.setMessage(Constants.STOP_CONFIRM)
					.setPositiveButton(Constants.OK,
							new DialogInterface.OnClickListener() {
								public void onClick(DialogInterface dialog,
										int id)
								{
									emergencyStop();
									dialog.cancel();
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

	private OnClickListener onStartupClickListener = new OnClickListener() {
		public void onClick(final View view)
		{
			startup();
		}
	};

	private OnClickListener onShutdownClickListener = new OnClickListener() {
		public void onClick(final View view)
		{
			shutdown();
		}
	};

	private Handler mHandler = new Handler() {
		@Override
		public void handleMessage(Message msg)
		{
			switch (msg.what)
			{
			case Constants.SHUTDOWN:
				turnOff();
				break;
			case Constants.STARTUP:
				turnOn();
				break;
			}
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
		View rootView = inflater.inflate(R.layout.list_machines, container,
				false);
		((Button) rootView.findViewById(R.id.machinelist_startup_b))
				.setOnClickListener(onStartupClickListener);
		((Button) rootView.findViewById(R.id.machinelist_shutdown_b))
				.setOnClickListener(onShutdownClickListener);
		((Button) rootView.findViewById(R.id.machinelist_emergencystop_b))
				.setOnClickListener(onEmergencyStopClickListener);
		setListAdapter(new MachineStatusAdapter(getActivity(),
				R.layout.machine_row, Constants.MACHINES));
		return rootView;
	}

	/**
	 * Implements each order's "Delete" button:
	 * 
	 * Gets the adapter from the ListView.
	 * 
	 * Gets the order from the view's tag.
	 * 
	 * Removes order from adapter.
	 * 
	 * Notifies adapter to update data changes.
	 */

	@Override
	public void onListItemClick(ListView l, View v, int position, long id)
	{
		Machine machine = (Machine) getListAdapter().getItem(position);
		new AlertDialog.Builder(getActivity())
				.setIcon(
						((ImageView) v
								.findViewById(R.id.machinerow_machinetype_iv))
								.getDrawable())
				.setTitle(machine.getMachineName())
				.setMessage(
						String.format("%s: %s", Constants.STATUS,
								machine.getMachineStatus()))
				.setPositiveButton(Constants.OK,
						new DialogInterface.OnClickListener() {
							public void onClick(DialogInterface dialog, int id)
							{
								dialog.cancel();
							}
						}).show();
	}

	public void startup()
	{
		new Thread() {
			@Override
			public void run()
			{
				if (mMediaPlayer.isPlaying())
					mMediaPlayer.stop();
				mMediaPlayer = MediaPlayer.create(getActivity(), R.raw.startup);
				mMediaPlayer.setLooping(false);
				mMediaPlayer.start();
				while (mMediaPlayer.isPlaying())
				{
				}
				mHandler.sendEmptyMessage(Constants.STARTUP);
			}
		}.start();
	}

	public void shutdown()
	{
		new Thread() {
			@Override
			public void run()
			{
				if (mMediaPlayer.isPlaying())
					mMediaPlayer.stop();
				mMediaPlayer = MediaPlayer
						.create(getActivity(), R.raw.shutdown);
				mMediaPlayer.setLooping(false);
				mMediaPlayer.start();
				while (mMediaPlayer.isPlaying())
				{
				}
				mHandler.sendEmptyMessage(Constants.SHUTDOWN);
			}
		}.start();
	}

	public void turnOn()
	{
		MachineStatusAdapter adapter = (MachineStatusAdapter) getListView()
				.getAdapter();
		Machine machine;
		for (int i = 0; i < adapter.getCount(); i++)
		{
			machine = adapter.getItem(i);
			machine.setMachineStatus(Constants.ON);
		}
		adapter.notifyDataSetChanged();
	}

	public void turnOff()
	{
		MachineStatusAdapter adapter = (MachineStatusAdapter) getListView()
				.getAdapter();
		Machine machine;
		for (int i = 0; i < adapter.getCount(); i++)
		{
			machine = adapter.getItem(i);
			machine.setMachineStatus(Constants.OFF);
			adapter.notifyDataSetChanged();
		}
	}

	public void emergencyStop()
	{
		turnOff();
		if (mMediaPlayer.isPlaying())
			mMediaPlayer.stop();
		mMediaPlayer = MediaPlayer.create(getActivity(), R.raw.stop);
		mMediaPlayer.setLooping(false);
		mMediaPlayer.start();
	}
}
